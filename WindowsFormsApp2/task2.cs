using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class task2 : Form
    {
        public task2()
        {
            WindowInitializer();
        }

        //Initializer of form
        private void WindowInitializer()
        {
            // first point
            this.p_init = new Point(1, 1);
            // second point 
            this.p_end = new Point(340, 140);
        
            this.pictureBoxHeiz = new PictureBox();
            this.pictureBoxWu = new PictureBox();
            int dx = Math.Abs(p_init.X - p_end.X);
            int dy = Math.Abs(p_init.Y - p_end.Y);

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeiz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWu)).BeginInit();
            this.SuspendLayout();

            //picturebox Breizenhem
            this.pictureBoxHeiz.Name = "Breizenham line";
            this.pictureBoxHeiz.Image = PrintLineBrezenhem(p_init, p_end);
            this.pictureBoxHeiz.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxHeiz.SizeMode = PictureBoxSizeMode.AutoSize;
            this.pictureBoxHeiz.MouseWheel += PictureBoxHeiz_MouseWheel;

            //picturebox Wu
            this.pictureBoxWu.Name = "Wu line";
            this.pictureBoxWu.Image = PrintLineWu(p_init, p_end);
            this.pictureBoxWu.Location = new System.Drawing.Point(450, 0);
            this.pictureBoxWu.SizeMode = PictureBoxSizeMode.AutoSize;
            this.pictureBoxWu.MouseWheel += PictureBoxHeiz_MouseWheel;

            //form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.pictureBoxHeiz);
            this.Controls.Add(this.pictureBoxWu);
            this.Name = "Form task 2";
            this.Text = "Form task 2";

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeiz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }


        // Mouse wheel chages event
        private void PictureBoxHeiz_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                this.pictureBoxHeiz.Scale(new Size(pictureBoxHeiz.Width / 2, pictureBoxHeiz.Height / 2));
            }
            if (e.Delta < 0)
            {
                this.pictureBoxHeiz.Scale(new Size(pictureBoxHeiz.Width * 2, pictureBoxHeiz.Height * 2));
            }
        }


        //calculating gradient
        private double gradient_calc(Point p1, Point p2)
        {
            return (double)Math.Abs(p1.Y - p2.Y) / Math.Abs(p1.X - p2.X);
        }


        //procedure 
        //breizenham's lin create procedure
        private Bitmap PrintLineBrezenhem(Point p_init, Point p_end)
        {
            Point p_st;
            Point p_fin;
            //buffer points 
            if (p_init.X <= p_end.X)
            {
                p_fin = new Point(p_init.X, p_init.Y);
                p_st = new Point(p_end.X, p_end.Y);
            }
            else
            {
                p_st = new Point(p_init.X, p_init.Y);
                p_fin = new Point(p_end.X, p_end.Y);
            }
            //delta max-min
            int dxm = Math.Abs(p_st.X - p_fin.X);
            int dym = Math.Abs(p_st.Y - p_fin.Y);
            Bitmap bitmap_heiz = new Bitmap(dxm + 4, dym + 4);
            //if dx or dy == 0 create line with const x or y
            double delta = Math.Min(dxm, dym);
            if (dxm == 0 || dym == 0)
            {
                delta = Math.Max(dxm, dym);

                if (p_st.X == p_fin.X)
                    for (int i = 0; i < delta; i++)
                        bitmap_heiz.SetPixel(2, p_st.Y+i, Color.Black);
                else
                    for (int i = 0; i < delta; i++)
                        bitmap_heiz.SetPixel(p_st.X+i, 2, Color.Black);

                return bitmap_heiz;
            }
            //calculation of gradient 
            double gradient = gradient_calc(p_init, p_end);
            //some variables
            double dx = dxm / delta;
            double dy = dym / delta;
            delta = Math.Max(dxm, dym);
            double d_next;
            int x = 2;
            int y;
            int step;
            //check the way of line 
            if (p_st.Y < p_fin.Y)
            {
                step = -1;
                y = Math.Abs(dym) + 2;
            }
            else
            {
                step = 1;
                y = 2;
            }
            //print line with angle less than 45 degrees 
            if (gradient <= 1)
            { 
                double d_curr = 2 * dy - dx;
                for (int i = 0; i < delta; i++)
                {
                    bitmap_heiz.SetPixel(x, y, Color.Black);
                    if (d_curr < 0)
                        d_next = d_curr + 2 * dy;
                    else
                    {
                        y += step;
                        d_next = d_curr + 2 * (dy - dx);
                    }
                    x++;
                    d_curr = d_next;
                }
            }
            //print line with angle more than 45 degrees
            else
            {
                double d_curr = 2 * dx - dy;
                for (int i = 0; i < delta; i++)
                {
                    bitmap_heiz.SetPixel(x, y, Color.Black);
                    if (d_curr < 0)
                        d_next = d_curr + 2 * dx;
                    else
                    {
                        x++;
                        d_next = d_curr + 2 * (dx - dy);
                    }
                    y += step;
                    d_curr = d_next;
                }
            }
            return bitmap_heiz;
        }


        //procedure
        //Wu's line create procedure
        private Bitmap PrintLineWu(Point p_init, Point p_end)
        {
            Point p_st;
            Point p_fin;

            //buffer points 
            if (p_init.X <= p_end.X)
            {
                p_fin = new Point(p_init.X, p_init.Y);
                p_st = new Point(p_end.X, p_end.Y);
            }
            else
            {
                p_st = new Point(p_init.X, p_init.Y);
                p_fin = new Point(p_end.X, p_end.Y);
            }
            //delta max-min
            int dxm = Math.Abs(p_st.X - p_fin.X);
            int dym = Math.Abs(p_st.Y - p_fin.Y);
            Bitmap bitmap_wu = new Bitmap(dxm + 4, dym + 4);
            //if dx or dy == 0 create line with const x or y
            double delta = Math.Min(dxm, dym);
            if (dxm == 0 || dym == 0)
            {
                delta = Math.Max(dxm, dym);

                if (p_st.X == p_fin.X)
                    for (int i = 0; i < delta; i++)
                        bitmap_wu.SetPixel(2, p_st.Y + i, Color.Black);
                else
                    for (int i = 0; i < delta; i++)
                        bitmap_wu.SetPixel(p_st.X + i, 2, Color.Black);

                return bitmap_wu;

            }
            //calculation of gradient 
            double gradient = gradient_calc(p_init, p_end);
            //some variables
            double dx = dxm / delta;
            double dy = dym / delta;
            double y;
            int step;
            if (p_st.Y < p_fin.Y)
            {
                step = -1;
                y = Math.Abs(dym) + 2;
            }
            else
            {
                step = 1;
                y = 2;
            }
            //print line with angle less than 45 degrees 
            if (gradient <= 1)
            {
                // Последний аргумент — интенсивность в долях единицы
                bitmap_wu.SetPixel(0, (int)y, Color.FromArgb(0, 0, 0));
                for (var x = 1; x <= dxm - 1; x++)
                {
                    double pow = y - (int)y;
                    bitmap_wu.SetPixel(x, (int)y, Color.FromArgb((int)(pow * 255), (int)(pow * 255), (int)(pow * 255)));
                    bitmap_wu.SetPixel(x, (int)y + 1, Color.FromArgb((int)((1 - pow) * 255), (int)((1 - pow) * 255), (int)((1 - pow) * 255)));
                    y += gradient*step;
                }
            }
            
            return bitmap_wu;
        }

        private Point p_init;
        private Point p_end;
        private PictureBox pictureBoxHeiz;
        private PictureBox pictureBoxWu;
    }
}
