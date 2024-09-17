using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        // Метод для загрузки изображения с использованием OpenFileDialog
        private void InitializePictures()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Загружаем изображение в pictureBox1
                pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                Bitmap originalImage = new Bitmap(openFileDialog1.FileName);

                // Преобразуем изображение в оттенки серого двумя методами
                Bitmap grayImage1 = ConvertToGrayScale1(originalImage);
                Bitmap grayImage2 = ConvertToGrayScale2(originalImage);

                // Отображаем изображения в pictureBox2 и pictureBox3
                pictureBox2.Image = grayImage1;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                pictureBox3.Image = grayImage2;
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                // Вычисляем разность между двумя изображениями
                Bitmap diffImage = CalculateDifference(grayImage1, grayImage2);
                pictureBox4.Image = diffImage;
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

                // Построение гистограмм интенсивности для изображений
                long[] histogramGray1 = CalculateHistogram(grayImage1);
                long[] histogramGray2 = CalculateHistogram(grayImage2);

                // Отображаем гистограммы в новой форме
                DisplayHistograms(histogramGray1, histogramGray2);
            }
        }

        // Метод для построения гистограмм и отображения их в новой форме
        private void DisplayHistograms(long[] histogram1, long[] histogram2)
        {
            var histogramForm = new Form();
            histogramForm.Text = "Гистограммы";

            // Создаем компоненты Chart
            var chart1 = new Chart();
            var chart2 = new Chart();

            // Устанавливаем области для графиков
            chart1.ChartAreas.Add(new ChartArea("Gray1"));
            chart2.ChartAreas.Add(new ChartArea("Gray2"));

            // Добавляем серии данных
            chart1.Series.Add(new Series("Gray1"));
            chart2.Series.Add(new Series("Gray2"));

            chart1.Series["Gray1"].ChartType = SeriesChartType.Column;
            chart2.Series["Gray2"].ChartType = SeriesChartType.Column;

            // Привязываем данные
            chart1.Series["Gray1"].Points.DataBindY(histogram1);
            chart2.Series["Gray2"].Points.DataBindY(histogram2);

            // Устанавливаем подписи осей
            chart1.ChartAreas[0].AxisY.Title = "Количество пикселей";
            chart2.ChartAreas[0].AxisY.Title = "Количество пикселей";

            // Табличная компоновка для гистограмм
            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.Dock = DockStyle.Fill;
            histogramForm.Controls.Add(tableLayoutPanel1);

            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            tableLayoutPanel1.Controls.Add(chart1, 0, 0);
            tableLayoutPanel1.Controls.Add(chart2, 1, 0);

            histogramForm.ShowDialog();
        }

        // Метод для вычисления гистограммы
        private long[] CalculateHistogram(Bitmap grayImage)
        {
            long[] histogram = new long[256];

            for (int y = 0; y < grayImage.Height; y++)
            {
                for (int x = 0; x < grayImage.Width; x++)
                {
                    int intensity = grayImage.GetPixel(x, y).R; // Пиксели одинаковы по R, G и B
                    histogram[intensity]++;
                }
            }

            return histogram;
        }

        // Первый метод преобразования RGB в оттенки серого (0.299R + 0.587G + 0.114B)
        private Bitmap ConvertToGrayScale1(Bitmap source)
        {
            Bitmap grayImage = new Bitmap(source.Width, source.Height);

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixelColor = source.GetPixel(x, y);
                    int gray = (int)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B);
                    grayImage.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }
            return grayImage;
        }

        // Второй метод преобразования RGB в оттенки серого (0.2126R + 0.7152G + 0.0722B)
        private Bitmap ConvertToGrayScale2(Bitmap source)
        {
            Bitmap grayImage = new Bitmap(source.Width, source.Height);

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixelColor = source.GetPixel(x, y);
                    int gray = (int)(0.2126 * pixelColor.R + 0.7152 * pixelColor.G + 0.0722 * pixelColor.B);
                    grayImage.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }
            return grayImage;
        }

        // Метод для нахождения разности двух изображений
        private Bitmap CalculateDifference(Bitmap image1, Bitmap image2)
        {
            Bitmap diffImage = new Bitmap(image1.Width, image1.Height);

            for (int y = 0; y < image1.Height; y++)
            {
                for (int x = 0; x < image1.Width; x++)
                {
                    int diff = Math.Abs(image1.GetPixel(x, y).R - image2.GetPixel(x, y).R);
                    diffImage.SetPixel(x, y, Color.FromArgb(diff, diff, diff));
                }
            }
            return diffImage;
        }

        // Обработчик для кнопки загрузки изображения
        private void button1_Click(object sender, EventArgs e)
        {
            InitializePictures();
        }
    }
}
