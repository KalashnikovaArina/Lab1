using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

class HSV
{
    public double hue;
    public double saturation;
    public double brightness;
    public HSV(double h, double s, double v)
    {
        this.hue = h;
        this.saturation = s;
        this.brightness = v;
    }
}

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent3();
        }

        private void InitializeComponent3()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.trackBarHue = new System.Windows.Forms.TrackBar();
            this.trackBarSaturation = new System.Windows.Forms.TrackBar();
            this.trackBarBrightness = new System.Windows.Forms.TrackBar();
            this.buttonSave = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSaturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            this.SuspendLayout();

            // pictureBox1
            originalImage = new Bitmap("C:\\Users\\User\\Desktop\\fruits.jpg");
            processedImage = new Bitmap(originalImage);
            this.pictureBox1.Image = ((System.Drawing.Image)(originalImage));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(768, 514);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;

            // trackBarHue
            this.trackBarHue.Name = "trackBarHue";
            this.trackBarHue.Location = new System.Drawing.Point(10, 530);
            this.trackBarHue.Maximum = 360;
            this.trackBarHue.Size = new System.Drawing.Size(220, 40);
            this.trackBarHue.TabIndex = 4;
            this.trackBarHue.MouseCaptureChanged += new System.EventHandler(this.trackBarHue_Scroll);
            this.trackBarHue.TickFrequency = 30;

            // trackBarSaturation
            this.trackBarSaturation.Name = "trackBarSaturation";
            this.trackBarSaturation.Location = new System.Drawing.Point(10, 580);
            this.trackBarSaturation.Maximum = 200;
            this.trackBarSaturation.Size = new System.Drawing.Size(220, 40);
            this.trackBarSaturation.TabIndex = 5;
            this.trackBarSaturation.MouseCaptureChanged += new System.EventHandler(this.trackBarSaturation_Scroll);
            this.trackBarSaturation.TickFrequency = 10;
            this.trackBarSaturation.Value = 100;

            // trackBarBrightness
            this.trackBarBrightness.Name = "trackBarBrightness";
            this.trackBarBrightness.Location = new System.Drawing.Point(10, 630);
            this.trackBarBrightness.Maximum = 200;
            this.trackBarBrightness.Size = new System.Drawing.Size(220, 40);
            this.trackBarBrightness.TabIndex = 6;
            this.trackBarBrightness.MouseCaptureChanged += new System.EventHandler(this.trackBarBrightness_Scroll);
            this.trackBarBrightness.TickFrequency = 10;
            this.trackBarBrightness.Value = 100;

            // textBox1
            this.textBox1.Location = new System.Drawing.Point(240, 530);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(80, 22);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Hue: " + trackBarHue.Value.ToString();

            // textBox2
            this.textBox2.Location = new System.Drawing.Point(240, 580);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(80, 22);
            this.textBox2.TabIndex = 0;
            this.textBox2.Text = "Satur: " + trackBarSaturation.Value.ToString();

            // textBox3
            this.textBox3.Location = new System.Drawing.Point(240, 630);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(80, 22);
            this.textBox3.TabIndex = 0;
            this.textBox3.Text = "Bright: " + trackBarBrightness.Value.ToString();

            // buttonSave
            this.buttonSave.Location = new System.Drawing.Point(400, 550);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(300, 100);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Save this brilliant!";

            // Form3
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 700);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.trackBarHue);
            this.Controls.Add(this.trackBarSaturation);
            this.Controls.Add(this.trackBarBrightness);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.buttonSave);
            this.Name = "Form3";
            this.Text = "Form3";

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSaturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void ConvertRGBtoHSV(Color color, out HSV pix)
        {
            double r = color.R;
            double g = color.G;
            double b = color.B;

            double max = Math.Max(Math.Max(r, g), b);
            double min = Math.Min(Math.Min(r, g), b);

            double hue = get_hue(r, g, b, min, max);

            double saturation;
            if (max == 0)
                saturation = 0;
            else
                saturation = 1 - (min / max);

            double brightness = max;

            pix = new HSV(hue, saturation, brightness);
        }

        double get_hue(double r, double g, double b, double min, double max)
        {

            if (min == max)
                return 0;
            else if (max == r)
            {
                if (g >= b)
                    return (g - b) * 60 / (max - min);
                else
                    return ((g - b) * 60 / (max - min)) + 360;
            }
            else
            {
                if (max == g)
                    return ((b - r) * 60 / (max - min)) + 120;
                else
                    return ((r - g) * 60 / (max - min)) + 240;
            }
        }

        Color ConvertHSVtoRGB(HSV pix)
        {
            double hue = pix.hue;
            double saturation = pix.saturation;
            double value = pix.brightness;
            int Hi = (int)Math.Floor(hue / 60) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);
            int p = (int)(value * (1 - saturation));
            int q = (int)(value * (1 - f * saturation));
            int t = (int)(value * (1 - (1 - f) * saturation));
            int v = (int)(value);


            if (Hi == 0)
                return Color.FromArgb(v, t, p);
            else if (Hi == 1)
                return Color.FromArgb(q, v, p);
            else if (Hi == 2)
                return Color.FromArgb(p, v, t);
            else if (Hi == 3)
                return Color.FromArgb(p, q, v);
            else if (Hi == 4)
                return Color.FromArgb(t, p, v);
            else
                return Color.FromArgb(v, p, q);
        }

        private HSV[,] pixel = new HSV[768, 514];
        private HSV[,] pixel_buf = new HSV[768, 514];
        private bool f = true;

        void UpdateImage()
        {
            int widht = originalImage.Width;
            int height = originalImage.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < widht; x++)
                {
                    Color pixelColor = originalImage.GetPixel(x, y);

                    // Преобразуем RGB в HSV
                    ConvertRGBtoHSV(pixelColor, out pixel[x, y]);
                    ConvertRGBtoHSV(pixelColor, out pixel_buf[x, y]);
                }
            }
            f = false;
        }

        void UpdateImageHue()
        {
            int widht = originalImage.Width;
            int height = originalImage.Height;
            double delta = trackBarHue.Value;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < widht; x++)
                {
                    // Изменяем значения HSV на основе ползунков
                    pixel_buf[x, y].hue = (pixel[x, y].hue + delta) % 360;
                    // Преобразуем обратно в RGB
                    Color newColor = ConvertHSVtoRGB(pixel_buf[x, y]);
                    processedImage.SetPixel(x, y, newColor);
                }
            }
            pictureBox1.Image = processedImage;
        }

        void UpdateImageSaturation()
        {
            int widht = originalImage.Width;
            int height = originalImage.Height;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < widht; x++)
                {
                    // Изменяем значения HSV на основе ползунков
                    pixel_buf[x, y].saturation = Math.Min(1, pixel[x, y].saturation * trackBarSaturation.Value / 100);
                    // Преобразуем обратно в RGB
                    Color newColor = ConvertHSVtoRGB(pixel_buf[x, y]);
                    processedImage.SetPixel(x, y, newColor);
                }
            }
            pictureBox1.Image = processedImage;
        }

        void UpdateImageBrightness()
        {
            int widht = originalImage.Width;
            int height = originalImage.Height;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < widht; x++)
                {
                    // Изменяем значения HSV на основе ползунков
                    pixel_buf[x, y].brightness = Math.Min(255, pixel[x, y].brightness * trackBarBrightness.Value / 100);
                    // Преобразуем обратно в RGB
                    Color newColor = ConvertHSVtoRGB(pixel_buf[x, y]);
                    processedImage.SetPixel(x, y, newColor);
                }
            }
            pictureBox1.Image = processedImage;
        }


        private void trackBarHue_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = "Hue: " + trackBarHue.Value.ToString();
            if (f)
                UpdateImage();
            UpdateImageHue();
        }

        private void trackBarSaturation_Scroll(object sender, EventArgs e)
        {
            textBox3.Text = "Satur: " + trackBarSaturation.Value.ToString();
            if (f)
                UpdateImage();
            UpdateImageSaturation();
        }

        private void trackBarBrightness_Scroll(object sender, EventArgs e)
        {
            textBox3.Text = "Bright: " + trackBarBrightness.Value.ToString();
            if (f)
                UpdateImage();
            UpdateImageBrightness();
        }

        int counter = 0;
        void buttonSave_Click(object sender, EventArgs e)
        {
            processedImage.Save($"C:\\Users\\User\\Desktop\\processed_image{counter}.jpg");
            counter++;
            MessageBox.Show("Изображение сохранено!");
        }

        private Bitmap originalImage;
        private Bitmap processedImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TrackBar trackBarHue;
        private System.Windows.Forms.TrackBar trackBarSaturation;
        private System.Windows.Forms.TrackBar trackBarBrightness;
        private System.Windows.Forms.Button buttonSave;
    }
}
