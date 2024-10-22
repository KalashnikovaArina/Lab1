using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class task1c : Form
    {
        private Bitmap image;
        private bool isDrawing = false;
        private Color borderColor = Color.Black;
        private Color fillBorderColor = Color.Red;
        private int differenceBetweenColors = 125;

        public task1c()
        {
            InitializeComponent();
            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = image;

            // Подключаем события для рисования
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
        }

        // Запуск рисования при нажатии кнопки мыши
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
        }

        // Рисование при перемещении мыши
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    g.FillRectangle(new SolidBrush(borderColor), e.X, e.Y, 1, 1);
                }
                pictureBox1.Invalidate();
            }
        }

        // Остановка рисования
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;

            // После завершения рисования ищем границу и обходим ее
            var points = GetBorderPoints();
            DrawBorder(points);
        }

        // Метод определения границы и сохранения точек границы
        private LinkedList<Tuple<int, int>> GetBorderPoints()
        {
            LinkedList<Tuple<int, int>> points = new LinkedList<Tuple<int, int>>();
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (IsBorderPoint(x, y))
                    {
                        points.AddLast(Tuple.Create(x, y));
                    }
                }
            }
            return points;
        }

        // Проверка, является ли пиксель граничной точкой
        private bool IsBorderPoint(int x, int y)
        {
            Color currentColor = image.GetPixel(x, y);
            if (!colorsEqual(borderColor, currentColor)) return false;

            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    int nx = x + dx, ny = y + dy;
                    if (nx >= 0 && ny >= 0 && nx < image.Width && ny < image.Height)
                    {
                        if (!colorsEqual(currentColor, image.GetPixel(nx, ny)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // Сравнение цветов с учетом допуска
        private bool colorsEqual(Color c1, Color c2)
        {
            return (Math.Abs(c1.R - c2.R) < differenceBetweenColors &&
                    Math.Abs(c1.G - c2.G) < differenceBetweenColors &&
                    Math.Abs(c1.B - c2.B) < differenceBetweenColors);
        }

        // Прорисовка границы поверх изображения
        private void DrawBorder(LinkedList<Tuple<int, int>> points)
        {
            foreach (var point in points)
            {
                image.SetPixel(point.Item1, point.Item2, fillBorderColor);
            }
            pictureBox1.Invalidate();
        }
    }
}
