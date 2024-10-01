using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Point[] points; //массив точек полигона
        bool Draw; //допустимо ли рисовать сейчас на picturebox

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
                    //TODO вызов функции
                    break;
                case "Поворот вокруг заданной точки":
                    label1.Text = str + "; Нарисуйте точку и введите угол поворота";
                    Draw = true; //разрешаем рисование
                    label7.Visible = true;
                    textBox3.Visible = true;
                    //TODO вызов функции
                    break;
                case "Поворот вокруг своего центра":
                    label1.Text = str + "; Введите угол поворота";
                    label7.Visible = true;
                    textBox3.Visible = true;
                    //TODO вызов функции
                    break;
                case "Масштабирование относительно заданной точки":
                    label1.Text = str + "; Нарисуйте точку";
                    label3.Visible = true;
                    textBox4.Visible = true;
                    Draw = true; //разрешаем рисование
                    //TODO вызов функции
                    break;
                case "Масштабирование относительно своего центра":
                    label1.Text = str;
                    label3.Visible = true;
                    textBox4.Visible = true;
                    //TODO вызов функции
                    break;
                case "Поиск точки пересечения двух ребер":
                    label1.Text = str + "; Нарисуйте ребро";
                    Draw = true; //разрешаем рисование
                    //TODO вызов функции
                    break;
                case "Проверка принадлежности точки к полигону":
                    label1.Text = "Проверка принадлежности точки к полигону; Нарисуйте точку";
                    Draw = true; //разрешаем рисование
                    //TODO вызов функции
                    break;
                case "Определить положение точки относительно полигона":
                    label1.Text = "Определить положение точки относительно полигона; Нарисуйте точку";
                    Draw = true; //разрешаем рисование
                    //TODO вызов функции
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
            Draw = false;
        }
    }
}
