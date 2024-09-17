using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }



        private void InitializePictures()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

                Bitmap image2 = new Bitmap(openFileDialog1.FileName, true);
                pictureBox2.Image = image2;
                Bitmap image3 = new Bitmap(openFileDialog1.FileName, true);
                pictureBox3.Image = image3;
                Bitmap image4 = new Bitmap(openFileDialog1.FileName, true);
                pictureBox4.Image = image4;

                //Подсчет значений пикселей
                long[] lr = new long[256];
                long[] lg = new long[256];
                long[] lb = new long[256];
                long r = 0;
                long g = 0;
                long b = 0;
                for (int x = 0; x < image2.Width; x++)
                {
                    for (int y = 0; y < image2.Height; y++)
                    {
                        Color pixelColor = image2.GetPixel(x, y);

                        ++lr[pixelColor.R];
                        ++lg[pixelColor.G];
                        ++lb[pixelColor.B];
                        r += pixelColor.R;
                        g += pixelColor.G;
                        b += pixelColor.B;
                    }
                }

                //результат выделения каналов rgb
                label1.Text = "r = " + r / image2.Width / image2.Height + " | g = " + g / image2.Width / image2.Height + " | b = " + b / image2.Width / image2.Height;

                //выделение каналов
                for (int x = 0; x < image2.Width; x++)
                {
                    for (int y = 0; y < image2.Height; y++)
                    {
                        Color pixelColor = image2.GetPixel(x, y);

                        Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                        image2.SetPixel(x, y, newColor);

                        newColor = Color.FromArgb(0, pixelColor.G, 0);
                        image3.SetPixel(x, y, newColor);

                        newColor = Color.FromArgb(0, 0, pixelColor.B);
                        image4.SetPixel(x, y, newColor);
                    }
                }

                //построение гистограмм
                var chart1 = new Chart();
                var chart2 = new Chart();
                var chart3 = new Chart();

                chart1.ChartAreas.Add(new ChartArea("Red"));
                chart2.ChartAreas.Add(new ChartArea("Green"));
                chart3.ChartAreas.Add(new ChartArea("Blue"));

                chart1.Series.Add(new Series("Red"));
                chart2.Series.Add(new Series("Green"));
                chart3.Series.Add(new Series("Blue"));

                chart1.Series["Red"].ChartType = SeriesChartType.Column;
                chart2.Series["Green"].ChartType = SeriesChartType.Column;
                chart3.Series["Blue"].ChartType = SeriesChartType.Column;

                chart1.Series["Red"].Points.DataBindY(lr);
                chart2.Series["Green"].Points.DataBindY(lg);
                chart3.Series["Blue"].Points.DataBindY(lb);

                chart1.ChartAreas[0].AxisY.Title = "Количество пикселей";
                chart2.ChartAreas[0].AxisY.Title = "Количество пикселей";
                chart3.ChartAreas[0].AxisY.Title = "Количество пикселей";

                // Создание новой формы для гистограмм
                var histogramForm = new Form();
                histogramForm.Text = "Гистограммы";
                TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
                tableLayoutPanel1.Dock = DockStyle.Fill;
                histogramForm.Controls.Add(tableLayoutPanel1);

                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.RowCount = 1;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));

                tableLayoutPanel1.Controls.Add(chart1, 0, 0);
                tableLayoutPanel1.Controls.Add(chart2, 1, 0);
                tableLayoutPanel1.Controls.Add(chart3, 2, 0);

                histogramForm.ShowDialog();
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            InitializePictures();
        }
    }
    
}
