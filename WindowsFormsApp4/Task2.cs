using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{

    public partial class Task2 : Form
    {
        Queue<line> queue = new Queue<line>();
        line original = new line();
        bool line_created = false;
        bool line_updated = false;
        private Bitmap bmp1;
        double roughness = 0.5;
        Random rand = new Random();

        public Task2()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp1 = (Bitmap)pictureBox1.Image;
            pictureBox1.Image = bmp1;
        }

        private void button1_click(object sender, EventArgs e)
        {
            roughness = double.Parse(textBox1.Text);
            if (line_updated)
            {
                Graphics.FromImage(pictureBox1.Image).Clear(Color.White);
                pictureBox1.Invalidate();
                queue = midpoint_displacement(queue);
                Print_lines(queue);
            }
            else
            {
                Print_lines(queue);
                line_updated = true;
            }
        }

        private void Print_lines(Queue<line> q)
        {
            var pen = new Pen(Color.Black, 1);
            var g = Graphics.FromImage(pictureBox1.Image);
            foreach (line l in q)
            {
                g.DrawLine(pen, l.Start, l.End);
            }
            pictureBox1.Image = pictureBox1.Image;
        }

        private Tuple<line, line> line_dev(line l)
        {
            int constantin = (int)Math.Abs(roughness * l.lenght());
            Point p_avg = new Point((int)(Math.Abs(l.Start.X + l.End.X) / 2), 
                  ((l.Start.Y + l.End.Y) / 2) + (rand.Next(0, constantin) - (constantin / 2)));
            Tuple<line, line> lines = new Tuple<line, line>(new line(l.Start, p_avg), new line(p_avg, l.End));
            return lines;
        }

        private Queue<line> midpoint_displacement(Queue<line> q)
        {
            Queue<line> buf_queue = new Queue<line>();
            Tuple<line, line> lines = line_dev(q.Dequeue());
            buf_queue.Enqueue(lines.Item1);
            buf_queue.Enqueue(lines.Item2);

            while (q.Count != 0)
            {
                lines = line_dev(q.Dequeue());
                buf_queue.Enqueue(lines.Item1);
                buf_queue.Enqueue(lines.Item2);
            }
            return buf_queue;
        }

        private void picturebox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (line_created)
            {
                original.End = new Point(e.X, e.Y);
                queue.Enqueue(original);
            }
            else
            {
                original.Start = new Point(e.X, e.Y);
                line_created = true;
            }
            ((Bitmap)pictureBox1.Image).SetPixel(e.X, e.Y, Color.Black);
            pictureBox1.Image = pictureBox1.Image;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char el = e.KeyChar;
            if (!Char.IsDigit(el) && el != (char)Keys.Back && el != '-' && el != ',') // можно вводить только цифры, минус и стирать
                e.Handled = true;
        }

        private void button2_click(object sender, EventArgs e)
        {
            Clear_all();
        }

        private void Clear_all()
        {
            queue.Clear();
            line_created = false;
            line_updated = false;
            original = new line();
            Graphics.FromImage(pictureBox1.Image).Clear(System.Drawing.SystemColors.AppWorkspace);
            pictureBox1.Invalidate();
        }
    }

    class line
    {
        private Point start = new Point();
        private Point end = new Point();
        public line(int x1, int y1, int x2, int y2)
        {
            this.start.X = x1;
            this.start.Y = y1;
            this.end.X = x2;
            this.end.Y = y2;
        }
        public line(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }
        public line()
        {
            this.start = new Point();
            this.end = new Point();
        }
        public Point Start
        {
            get { return start; }
            set { this.start = value; }
        }
        public Point End
        {
            get { return end; }
            set { this.end = value; }
        }
        public double lenght()
        {
            return Math.Sqrt((this.start.X - this.end.X)*(this.start.X - this.end.X) + (this.start.Y - this.end.Y)*(this.start.Y - this.end.Y));
        }
    }
}
