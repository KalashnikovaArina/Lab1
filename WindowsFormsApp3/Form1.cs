using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        List<Tuple<double, double>> poligon = new List<Tuple<double, double>>(); //список точек для полигона
        List<Point> list = new List<Point>();
        bool Draw = true; //допустимо ли рисовать сейчас на picturebox
        bool Matrix; //преобразование с матрицей(true) или без(false). Используется для кнопки "применить"
        double[,] transferalMatrix; //матрица преобразования

        public Form1()
        {
            InitializeComponent();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            pictureBox1.Image = bmp;
        }



        //Смещение
        //Поворот вокруг заданной точки
        //Поворот вокруг своего центра
        //Масштабирование относительно заданной точки
        //Масштабирование относительно своего центра
        //Поиск точки пересечения двух ребер
        //Проверка принадлежит ли заданная пользователем(с помощью мыши) точка выпуклому и невыпуклому полигонам
        //Классифицировать положение точки относительно ребра(справа или слева)
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = comboBox1.SelectedItem.ToString();
            Hide_labels();
            switch (str)
            {
                case "Смещение":
                    label1.Text = str;
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    Matrix = true;
                    DrawDot = false;
                    break;
                case "Поворот вокруг заданной точки":
                    label1.Text = str + "; Нарисуйте точку и введите угол поворота";
                    Draw = true; //разрешаем рисование
                    label7.Visible = true;
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    Matrix = true;
                    DrawDot = true;
                    break;
                case "Поворот вокруг своего центра":
                    label1.Text = str + "; Введите угол поворота";
                    label7.Visible = true;
                    textBox3.Visible = true;
                    Draw = false;
                    Matrix = true;
                    DrawDot = false;
                    break;
                case "Масштабирование относительно заданной точки":
                    label1.Text = str + "; Нарисуйте точку";
                    label3.Visible = true;
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    textBox4.Visible = true;
                    Draw = true; //разрешаем рисование
                    Matrix = true;
                    DrawDot = true;
                    break;
                case "Масштабирование относительно своего центра":
                    label1.Text = str;
                    label3.Visible = true;
                    textBox4.Visible = true;
                    Matrix = true;
                    Draw = false;
                    DrawDot = false;
                    break;
                case "Поиск точки пересечения двух ребер":
                    label1.Text = str + "; Нарисуйте ребро";
                    Draw = true; //разрешаем рисование
                    break;
                case "Проверка принадлежности точки к полигону":
                    label1.Text = "Проверка принадлежности точки к полигону; Нарисуйте точку";
                    Draw = true; //разрешаем рисование
                    break;
                case "Определить положение точки относительно полигона":
                    label1.Text = "Определить положение точки относительно полигона; Нарисуйте точку";
                    Draw = true; //разрешаем рисование
                    break;
                case "Рисовать":
                    Draw = true; //разрешаем рисование
                    break;
                default:
                    break;

            }
        }
        //прячет все лейблы и текстбоксы для действий
        private void Hide_labels()
        {
            label1.Text = "Выберите значение";
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox1.Text = "";
            textBox2.Text = "";
            Draw = false;
        }
        //реализация рисования
        bool DrawDot = false;
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (Draw)
            {
                button2.Visible = true;
                ((Bitmap)pictureBox1.Image).SetPixel(e.X, e.Y, Color.Black);// рисуют точку
                pictureBox1.Image = pictureBox1.Image;// 
               
                if (!DrawDot) 
                {
                    list.Add(new Point(e.X, e.Y));
                    poligon.Add(Tuple.Create(e.X * 1.0, e.Y * 1.0));
                    Point start = list.First();
                    foreach (var p in list)
                    {
                        var pen = new Pen(Color.Black, 1);
                        var g = Graphics.FromImage(pictureBox1.Image);
                        g.DrawLine(pen, start, p);
                        pen.Dispose();
                        g.Dispose();
                        pictureBox1.Image = pictureBox1.Image;

                        start = p;
                    }
                }
                else //на случай когда нужно нарисовать только точку(по заданию)
                {
                    textBox1.Text = e.X.ToString();
                    textBox2.Text = e.Y.ToString();
                }
            }
        }

        //перемножение матриц
        private double[,] matrix_multiplication(double[,] m1, double[,] m2)
        {
            double[,] res = new double[m1.GetLength(0), m2.GetLength(1)];

            for (int i = 0; i < m1.GetLength(0); ++i)
                for (int j = 0; j < m2.GetLength(1); ++j)
                    for (int k = 0; k < m2.GetLength(0); k++)
                    {
                        res[i, j] += m1[i, k] * m2[k, j];
                    }

            return res;
        }

        private void RealisationTask()
        {
            string str = comboBox1.SelectedItem.ToString();
            switch (str)
            {
                case "Смещение":
                    double tX = System.Convert.ToDouble(textBox1.Text);
                    double tY = System.Convert.ToDouble(textBox2.Text);
                    transferalMatrix = new double[,] { { 1.0, 0, 0 }, { 0, 1.0, 0 }, { tX, tY, 1.0 } };
                    break;
                case "Поворот вокруг заданной точки":
                    double c = System.Convert.ToDouble(textBox1.Text);
                    double d = System.Convert.ToDouble(textBox2.Text);
                    double p = System.Convert.ToDouble(textBox3.Text) * Math.PI / 180;
                    double cos = Math.Cos(p);
                    double sin = Math.Sin(p);
                    transferalMatrix = new double[,] { {cos, sin, 0}, {-sin, cos, 0},
                        {cos*(-c)+d*sin+c, (-c)*sin-d*cos+d, 1}};
                    break;
                case "Поворот вокруг своего центра":
                    double p1 = System.Convert.ToDouble(textBox3.Text) * Math.PI / 180;
                    double cos1 = Math.Cos(p1);
                    double sin1 = Math.Sin(p1);
                    double a = 0, b = 0;
                    find_center(ref a, ref b);
                    transferalMatrix = new double[,] { {cos1, sin1, 0}, {-sin1, cos1, 0},
                        {cos1*(-a)+b*sin1+a, (-a)*sin1-b*cos1+b, 1}};
                    break;
                case "Масштабирование относительно заданной точки": //доделать!!!!
                    double cm1 = System.Convert.ToDouble(textBox4.Text); 
                    double c1 = System.Convert.ToDouble(textBox1.Text);
                    double d1 = System.Convert.ToDouble(textBox2.Text);
                    transferalMatrix = new double[3, 3] { { cm1, 0, 0 }, { 0, cm1, 0 }, { (1 - cm1) * c1, (1 - cm1) * d1, 1 } };
                    break;
                case "Масштабирование относительно своего центра":
                    double cm = System.Convert.ToDouble(textBox4.Text);
                    double a1 = 0, b1 = 0;
                    find_center(ref a1, ref b1);
                    transferalMatrix = new double[3, 3] { { cm, 0, 0 }, { 0, cm, 0 }, { (1 - cm) * a1, (1 - cm) * b1, 1 } };
                    
                    break;
                case "Поиск точки пересечения двух ребер":
                    
                    break;
                case "Проверка принадлежности точки к полигону":
                    
                    break;
                case "Определить положение точки относительно полигона":
                    
                    break;
                default:
                    break;

            }
        }
        //поиск центра полигона
        private void find_center(ref double x, ref double y)
        {

            foreach (var c in poligon)
            {
                x += c.Item1;
                y += c.Item2;
            }

            x /= poligon.Count;
            y /= poligon.Count;
        }
        //очищает холст
        private void ClearPictureBox() 
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
        }
        //кнопка применить
        private void button2_Click(object sender, EventArgs e)
        {
            RealisationTask();
            if (Matrix)
            {
                List<Point> newprimitiv = new List<Point>();
                List<Tuple<double, double>> l1 = new List<Tuple<double, double>>();
                foreach (Tuple<double, double> p in poligon)
                {
                    double[,] point = new double[,] { { p.Item1, p.Item2, 1.0 } };
                    double[,] res = matrix_multiplication(point, transferalMatrix);
                    l1.Add(Tuple.Create(res[0, 0], res[0, 1]));
                    newprimitiv.Add(new Point(Convert.ToInt32(Math.Round(res[0, 0])), Convert.ToInt32(Math.Round(res[0, 1]))));
                }

                ClearPictureBox();

                poligon.Clear();

                Point p1 = newprimitiv.First();
                poligon.Add(Tuple.Create(p1.X * 1.0, p1.Y * 1.0));
                foreach (Point c in newprimitiv)
                {
                    if (c != p1)
                    {
                        var g = Graphics.FromImage(bmp);
                        Pen p = new Pen(Color.Black, 1);
                        g.DrawLine(p, p1, c);
                        p1 = c;
                        p.Dispose();
                        g.Dispose();
                    }
                }

                poligon = l1;
                pictureBox1.Image = bmp;
            }
        }
        //кнопка очистить
        private void button1_Click(object sender, EventArgs e) 
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
            list.Clear();
            poligon.Clear();
            label5.Text = "Выберите действие";
            comboBox1.SelectedItem = "Рисовать";
        }
    }
}
