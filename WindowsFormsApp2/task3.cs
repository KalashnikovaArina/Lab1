using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsFormsApp2
{
    public partial class task3 : Form
    {
        private Point[] points = new Point[3];
        private int currentPointIndex = -1;
        private Bitmap bitmap;
        private Graphics graphics;
        SolidBrush sb = new SolidBrush(Color.Black);
        Color[] cl = new Color[3];
        public task3()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            points[currentPointIndex+1] = new Point(e.X, e.Y);
            cl[currentPointIndex + 1] = sb.Color;
            // Рисование точки на Bitmap
            pictureBox1.Invalidate();
            graphics.FillEllipse(sb, e.X - 2, e.Y - 2, 4, 4);
            pictureBox1.Image = bitmap;
            currentPointIndex += 1;

            if (currentPointIndex == 2)
            {
                pictureBox1.Invalidate();
                currentPointIndex = 0; // Сброс индекса для следующего треугольника
                                       
                DrawGradientTriangle(graphics, points, cl);

                // Обновление PictureBox
                pictureBox1.Invalidate();

                // Сброс индекса для следующего треугольника
                currentPointIndex = -1;
            }
        }

        // Сортировка вершин по Y 
        private void SortPointsByY(Point[] points, Color[] colors)
        {
            if (points[0].Y > points[1].Y)
            {
                int tmp = points[0].Y;
                points[0].Y = points[1].Y;
                points[1].Y = tmp;
                tmp = points[0].X;
                points[0].X = points[1].X;
                points[1].X = tmp;
                (colors[0], colors[1]) = (colors[1], colors[0]);
            }
            if(points[0].Y > points[2].Y)
            {
                int tmp = points[0].Y;
                points[0].Y = points[2].Y;
                points[2].Y = tmp;
                tmp = points[0].X;
                points[0].X = points[2].X;
                points[2].X = tmp;
                (colors[0], colors[2]) = (colors[2], colors[0]);
            }
            if (points[1].Y > points[2].Y)
            {
                int tmp = points[1].Y;
                points[1].Y = points[2].Y;
                points[2].Y = tmp;
                tmp = points[1].X;
                points[1].X = points[2].X;
                points[2].X = tmp;
                (colors[1], colors[2]) = (colors[2], colors[1]);
            }
            //Array.Sort(points, (p1, p2) => p1.Y.CompareTo(p2.Y));
        }

        // Растеризация треугольника с градиентным окрашиванием
        private void DrawGradientTriangle(Graphics g, Point[] points, Color[] colors)
        {
            SortPointsByY(points, colors);

            int x0 = points[0].X;
            int y0 = points[0].Y;
            int x1 = points[1].X;
            int y1 = points[1].Y;
            int x2 = points[2].X;
            int y2 = points[2].Y;

            int cross_x1, cross_x2;
            int dx1 = x1 - x0;
            int dy1 = y1 - y0;
            int dx2 = x2 - x0;
            int dy2 = y2 - y0;

            int top_y = y0;

            // Рисуем верхнюю часть 
            while (top_y < y1)
            {
                dx1 = x1 - x0;
                dy1 = y1 - y0;
                dx2 = x2 - x0;
                dy2 = y2 - y0;

                cross_x1 = x0 + dx1 * (top_y - y0) / dy1;
                cross_x2 = x0 + dx2 * (top_y - y0) / dy2;

                if (cross_x1 > cross_x2)
                {
                    (cross_x1, cross_x2) = (cross_x2, cross_x1);
                }

                for (int x = cross_x1; x <= cross_x2; x++)
                {

                    Color pixelColor = CalculatePixelColor(x, top_y, colors[0], colors[1], colors[2], x0, y0, x1, y1, x2, y2); 
                    bitmap.SetPixel(x, top_y, pixelColor);
                }

                top_y++;
            }

            // Рисуем нижнюю часть
            dx1 = x2 - x1;
            dy1 = y2 - y1;
            while (top_y < y2)
            {
                cross_x1 = x1 + dx1 * (top_y - y1) / dy1;
                cross_x2 = x0 + dx2 * (top_y - y0) / dy2;

                if (cross_x1 > cross_x2)
                {
                    (cross_x1, cross_x2) = (cross_x2, cross_x1);
                }

                for (int x = cross_x1; x <= cross_x2; x++)
                {
                    Color pixelColor = CalculatePixelColor(x, top_y, colors[0], colors[1], colors[2], x0, y0, x1, y1, x2, y2);
                    bitmap.SetPixel(x, top_y, pixelColor);
                }

                top_y++;
            }

            pictureBox1.Image = bitmap;
        }

        private Color CalculatePixelColor(int x, int y, Color c1, Color c2, Color c3, int x0, int y0, int x1, int y1, int x2, int y2)
        {
            double a, b, c;
            CalculateBarycentricCoordinates(x, y, x0, y0, x1, y1, x2, y2, out a, out b, out c);

            int r = (int)(c1.R * a + c2.R * b + c3.R * c);
            int g = (int)(c1.G * a + c2.G * b + c3.G * c);
            int b1 = (int)(c1.B * a + c2.B * b + c3.B * c);

            // ограничение от 0 до 255
            r = MathExtensions.Clamp(r, 0, 255);
            g = MathExtensions.Clamp(g, 0, 255);
            b1 = MathExtensions.Clamp(b1, 0, 255);

            return Color.FromArgb(r, g, b1);
        }
        public static class MathExtensions
        {
            public static int Clamp(int value, int min, int max)
            {
                return Math.Max(min, Math.Min(value, max));
            }
        }

        private void CalculateBarycentricCoordinates(int x, int y, int x1, int y1, int x2, int y2, int x3, int y3, out double a, out double b, out double c)
        {
            double denominator = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);
            a = ((x2 * y3 - x3 * y2) + (y2 - y3) * x + (x3 - x2) * y) / denominator;
            b = ((x3 * y1 - x1 * y3) + (y3 - y1) * x + (x1 - x3) * y) / denominator;
            c = 1 - a - b;
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            sb = new SolidBrush( ((Button)sender).BackColor);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = bitmap;
            currentPointIndex = -1;
        }
    }
}
