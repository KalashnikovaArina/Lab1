using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WindowsFormsApp2
{
    enum Type
    {
        Pen, Fill, FillImage, Highlight
    }

    public partial class task1 : Form
    {
        private Type type;
        private Bitmap bitmap;
        private Graphics g;
        private Color boundaryColor;
        private bool isDrawing = false;
        private Point previousPoint;
        private int lineThickness = 4; // Толщина линии

        public task1()
        {
            InitializeComponent();
            type = Type.Highlight;
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bitmap;
            g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            boundaryColor = Color.Aqua;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (type == Type.Highlight)
            {
                boundaryColor = bitmap.GetPixel(e.Location.X, e.Location.Y);
                HighlightBoundary(e.Location.X, e.Location.Y, bitmap);
            }
            else if (type == Type.Pen && e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                previousPoint = e.Location;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (type == Type.Pen && isDrawing && e.Button == MouseButtons.Left)
            {
                DrawThickLine(previousPoint, e.Location, lineThickness);
                previousPoint = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (type == Type.Pen && e.Button == MouseButtons.Left)
            {
                isDrawing = false;
            }
        }

        private void buttonFindBoundary_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click on the image to select the starting point for boundary detection.");
            type = Type.Highlight;
        }

        private void buttonDrawBoundary_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click and drag on the image to draw the boundary.");
            type = Type.Pen;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pictureBox1.Invalidate();
        }

        private List<Point> GetOrderedBoundaryPoints(int x, int y, Bitmap b, Color boundaryColor)
        {
            List<Point> borderPoints = new List<Point>();
            HashSet<Point> visited = new HashSet<Point>();
            Queue<Point> queue = new Queue<Point>();
            Point startPoint = new Point(x, y);

            queue.Enqueue(startPoint);
            visited.Add(startPoint);

            List<Point> directions = new List<Point>
            {
                new Point(1, 0), new Point(1, 1), new Point(0, 1),
                new Point(-1, 1), new Point(-1, 0), new Point(-1, -1),
                new Point(0, -1), new Point(1, -1)
            };

            while (queue.Count > 0)
            {
                Point point = queue.Dequeue();
                borderPoints.Add(point);

                foreach (Point dir in directions)
                {
                    Point neighbor = new Point(point.X + dir.X, point.Y + dir.Y);

                    if (neighbor.X >= 0 && neighbor.X < b.Width && neighbor.Y >= 0 && neighbor.Y < b.Height &&
                        !visited.Contains(neighbor) && b.GetPixel(neighbor.X, neighbor.Y) == boundaryColor)
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return borderPoints;
        }

        public void HighlightBoundary(int x, int y, Bitmap b)
        {
            Color targetColor = b.GetPixel(x, y);

            if (targetColor != boundaryColor)
            {
                MessageBox.Show("The selected point is not on the boundary.");
                return;
            }

            List<Point> boundaryPoints = GetOrderedBoundaryPoints(x, y, b, boundaryColor);

            foreach (Point point in boundaryPoints)
            {
                b.SetPixel(point.X, point.Y, Color.LimeGreen);
            }
            pictureBox1.Invalidate();
        }

        private void DrawThickLine(Point p1, Point p2, int thickness)
        {
            using (Pen pen = new Pen(boundaryColor, thickness))
            {
                g.DrawLine(pen, p1, p2);
            }
        }
    }
}
