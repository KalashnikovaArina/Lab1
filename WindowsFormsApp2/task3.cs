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
                                       // Растеризация треугольника с градиентным окрашиванием
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
            //точки
            int x0 = points[0].X;
            int y0 = points[0].Y;
            int x1 = points[1].X;
            int y1 = points[1].Y;
            int x2 = points[2].X;
            int y2 = points[2].Y;
            //пересечения
            int cross_x1, cross_x2;
            int dx1 = x1 - x0;
            int dy1 = y1 - y0;
            int dx2 = x2 - x0;
            int dy2 = y2 - y0;

            int top_y = y0; 

            while(top_y < y1)
            {
                cross_x1 = x0 + dx1*(top_y - y0)/dy1;
                cross_x2 = x0 + dx2 * (top_y - y0) / dy2;
                if (cross_x1 > cross_x2)
                {
                    // Интерполяция цвета между color1 и color2
                    // Используем cross_x1 и cross_x2 для определения фактора
                    float colorFactor = (float)(top_y - y0) / (y1 - y0); // используем расстояние по Y от y0 до y1
                    Color interpolatedColor = InterpolateColor(colors[0], colors[1], colorFactor);
                    g.FillRectangle(new SolidBrush(interpolatedColor), cross_x2, top_y, cross_x1 - cross_x2, 1);

                    //g.DrawRectangle(pen, cross_x2, top_y, cross_x1 - cross_x2, 1);
                    pictureBox1.Image = bitmap;
                }
                else
                {
                    // Интерполяция цвета между color1 и color3
                    // Используем cross_x1 и cross_x2 для определения фактора
                    float colorFactor = (float)(top_y - y0) / (y1 - y0); // Верно: используем расстояние по Y от y0 до y2
                    Color interpolatedColor = InterpolateColor(colors[0], colors[1], colorFactor);
                    g.FillRectangle(new SolidBrush(interpolatedColor), cross_x1, top_y, cross_x2 - cross_x1, 1);

                    //g.DrawRectangle(pen, cross_x1, top_y, cross_x2 - cross_x1, 1);
                    pictureBox1.Image = bitmap;
                }
                top_y++;
            }

            dx1 = x2 - x1;
            dy1 = y2 - y1;
            while (top_y < y2)
            {
                cross_x1 = x1 + dx1 * (top_y - y1) / dy1;
                cross_x2 = x0 + dx2 * (top_y - y0) / dy2;
                if (cross_x1 > cross_x2)
                {
                    // Интерполяция цвета между color2 и color3
                    // Используем cross_x1 и cross_x2 для определения фактора
                    float colorFactor = (float)(top_y - y1) / (y2 - y1); // Верно: используем расстояние по Y от y1 до y2
                    Color interpolatedColor = InterpolateColor(colors[1], colors[2], colorFactor);
                    g.FillRectangle(new SolidBrush(interpolatedColor), cross_x2, top_y, cross_x1 - cross_x2, 1);
                    //g.DrawRectangle(pen, cross_x2, top_y, cross_x1 - cross_x2, 1);
                    pictureBox1.Image = bitmap;
                }
                else
                {
                    // Интерполяция цвета между color2 и color3
                    // Используем cross_x1 и cross_x2 для определения фактора
                    float colorFactor = (float)(top_y - y1) / (y2 - y1); // Верно: используем расстояние по Y от y1 до y2
                    Color interpolatedColor = InterpolateColor(colors[1], colors[2], colorFactor);
                    g.FillRectangle(new SolidBrush(interpolatedColor), cross_x1, top_y, cross_x2 - cross_x1, 1);
                    //g.DrawRectangle(pen, cross_x1, top_y, cross_x2 - cross_x1, 1);
                    pictureBox1.Image = bitmap;
                }
                top_y++;
            }
        }
        
    

        // Интерполяция цветов
        private Color InterpolateColor(Color color1, Color color2, float factor)
        {
            int r = (int)Math.Round(color1.R + (color2.R - color1.R) * factor);
            int g = (int)Math.Round(color1.G + (color2.G - color1.G) * factor);
            int b = (int)Math.Round(color1.B + (color2.B - color1.B) * factor);

            // Ограничение значений r, g, b в диапазоне от 0 до 255
            r = MathExtensions.Clamp(r, 0, 255);
            g = MathExtensions.Clamp(g, 0, 255);
            b = MathExtensions.Clamp(b, 0, 255);

            return Color.FromArgb(r, g, b);
        }
        public static class MathExtensions
        {
            public static int Clamp(int value, int min, int max)
            {
                return Math.Max(min, Math.Min(value, max));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sb = new SolidBrush( ((Button)sender).BackColor);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = bitmap;
        }
    }
}
