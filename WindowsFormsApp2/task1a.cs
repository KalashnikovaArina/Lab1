using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class task1a : Form
    {
        Bitmap bmp;
        Graphics g;
        Pen needColor;
        bool isDrawing = false;
        List<Point> points = new List<Point>();

        public task1a()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            Clear();
            colorDialog1.Color = Color.Red;
            label5.BackColor = colorDialog1.Color;
            needColor = new Pen(colorDialog1.Color);
            g = Graphics.FromImage(bmp);
        }

        private void Clear()
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                points.Clear(); // Очищаем список точек для нового штриха
                points.Add(e.Location); // Добавляем первую точку
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                points.Add(e.Location);
                int penWidth = trackBar1.Value;
                g.DrawLines(new Pen(needColor.Color, penWidth), points.ToArray());
                pictureBox1.Invalidate(); // Обновляем изображение
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = false; // Останавливаем рисование
                points.Clear(); // Очищаем список точек
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.MouseDown -= pictureBox1_MouseDown;
            pictureBox1.MouseDown += pictureBox1_MouseDown2;
            button3.Enabled = true;
            button2.Enabled = false;
            label2.Visible = true;
            label3.Visible = false;
        }

        private void pictureBox1_MouseDown2(object sender, MouseEventArgs e)
        {
            Point firstPoint = new Point(e.X, e.Y);
            fill(firstPoint);
        }

        private bool equalColors(Color c1, Color c2)
        {
            return c1.R == c2.R && c1.G == c2.G && c1.B == c2.B;
        }

        private void fill(Point p)
        {
            Color formColor = bmp.GetPixel(p.X, p.Y);
            if (0 <= p.X && p.X < bmp.Width && 0 <= p.Y && p.Y < bmp.Height - 1 && !equalColors(formColor, Color.Black) &&
                !equalColors(formColor, needColor.Color))
            {
                Point leftBound = new Point(p.X, p.Y);
                Point rightBound = new Point(p.X, p.Y);
                Color currentColor = formColor;
                while (0 < leftBound.X && !equalColors(currentColor, Color.Black))
                {
                    leftBound.X -= 1;
                    currentColor = bmp.GetPixel(leftBound.X, p.Y);
                }
                currentColor = formColor;
                while (rightBound.X < pictureBox1.Width - 1 && !equalColors(currentColor, Color.Black))
                {
                    rightBound.X += 1;
                    currentColor = bmp.GetPixel(rightBound.X, p.Y);
                }
                if (leftBound.X != 0)
                    leftBound.X += 1;
                rightBound.X -= 1;
                if (rightBound.X - leftBound.X == 0)
                    bmp.SetPixel(rightBound.X, rightBound.Y, needColor.Color);
                g.DrawLine(needColor, leftBound, rightBound);
                pictureBox1.Image = bmp;
                for (int i = leftBound.X; i < rightBound.X + 1; ++i)
                    fill(new Point(i, p.Y + 1));
                for (int i = leftBound.X; i < rightBound.X + 1; ++i)
                    if (p.Y > 0)
                        fill(new Point(i, p.Y - 1));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.MouseDown -= pictureBox1_MouseDown2;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            button3.Enabled = false;
            button2.Enabled = true;
            label3.Visible = true;
            label2.Visible = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Filter = "Image Files(*.JPG)|*.JPG|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            Color c = colorDialog1.Color;
            label5.BackColor = c;
            needColor.Color = c;
        }
    }
}
