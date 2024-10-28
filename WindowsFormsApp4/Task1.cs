using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static WindowsFormsApp4.LSys;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace WindowsFormsApp4
{
    public partial class Task1 : Form
    {
        private static string l_system_dir = "../../L-sys/";
        private Graphics graphics;
        private Bitmap bitmap;
        private string _cur_fractal = l_system_dir + "КриваяКоха.txt";


        public Task1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = bitmap;
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ind = comboBox1.SelectedIndex;
            switch (ind)
            {
                case 0:
                    _cur_fractal = l_system_dir + "КриваяКоха" + ".txt";
                    label2.Visible = false;
                    textBox1.Visible = false;

                    break;
                case 1:
                    _cur_fractal = l_system_dir + "СнежинкаКоха" + ".txt";
                    label2.Visible = false;
                    textBox1.Visible = false;

                    break;
                case 2:
                    _cur_fractal = l_system_dir + "КвадратныйОстровКоха" + ".txt";
                    label2.Visible = false;
                    textBox1.Visible = false;

                    break;
                case 3:
                    _cur_fractal = l_system_dir + "ТреугольникСерпинского" + ".txt";
                    label2.Visible = false;
                    textBox1.Visible = false;

                    break;
                case 4:
                    _cur_fractal = l_system_dir + "НаконечникСерпинского" + ".txt";
                    label2.Visible = false;
                    textBox1.Visible = false;

                    break;
                case 5:
                    _cur_fractal = l_system_dir + "КриваяГильберта" + ".txt";
                    label2.Visible = false;
                    textBox1.Visible = false;

                    break;
                case 6:
                    _cur_fractal = l_system_dir + "КриваяДракона" + ".txt";
                    label2.Visible = false;
                    textBox1.Visible = false;

                    break;
                case 7:
                    _cur_fractal = l_system_dir + "ШестиугольнаяКриваяГоспера" + ".txt";
                    label2.Visible = false;
                    textBox1.Visible = false;

                    break;
                case 8:
                    _cur_fractal = l_system_dir + "Куст1" + ".txt";
                    label2.Visible = true;
                    textBox1.Visible = true;
                    break;
                case 9:
                    _cur_fractal = l_system_dir + "Куст2" + ".txt";
                    label2.Visible = true;
                    textBox1.Visible = true;
                    break;
                case 10:
                    _cur_fractal = l_system_dir + "Куст3" + ".txt";
                    label2.Visible = true;
                    textBox1.Visible = true;
                    break;
                case 11:
                    _cur_fractal = l_system_dir + "ШестиугольнаяМозаика" + ".txt";
                    label2.Visible = false;
                    textBox1.Visible = false;

                    break;
                case 12:
                    _cur_fractal = l_system_dir + "ВысокоеДерево" + ".txt";
                    label2.Visible = true;
                    textBox1.Visible = true;
                    break;
                case 13:
                    _cur_fractal = l_system_dir + "ШирокоеДерево" + ".txt";
                    label2.Visible = true;
                    textBox1.Visible = true;
                    break;
                case 14:
                    _cur_fractal = l_system_dir + "СлучайноеДерево" + ".txt";
                    label2.Visible = true;
                    textBox1.Visible = true;
                    break;
            }
        }
        private string iteration_fractal(ParamsFromFile parameters, string input) //реализация одной итерации
        {
            string result = "";

            foreach (char symbol in input)
            {
                if (parameters.ruleDict.ContainsKey(symbol))
                {
                    result += parameters.ruleDict[symbol];
                }
                else
                {
                    result += symbol;
                }
            }

            return result;
        }


        private string Apply_rules(ParamsFromFile parameters, int iterations) //выдаст то, по чему строить
        {
            if (iterations > parameters.maxDepth) 
                iterations = parameters.maxDepth;
            string current = parameters.axiom;
            for (int i = 0; i < iterations; i++)
            {
                current = iteration_fractal(parameters, current);
            }
            return current;
        }

        private void Draw_fractal(ParamsFromFile parameters)
        {
            graphics.Clear(Color.White);
            pictureBox1.Invalidate();

            int count = 0;
            float width = 7;
            Color color = Color.FromArgb(44, 0, 0);

            bool is_tree = false;
            if (parameters.name.Contains("Дерево") || parameters.name.Contains("Куст")) 
                is_tree = true;
            bool thin = false;


            string fractal = Apply_rules(parameters, int.Parse(textBox2.Text));

            Stack<(PointF, float, Color, float, bool)> stateStack = new Stack<(PointF, float, Color, float, bool)>(); //сохраненные точки
            List<Tuple<PointF, PointF>> points = new List<Tuple<PointF, PointF>>();
            Dictionary<PointF, Tuple<Color, float>> tree_dict = new Dictionary<PointF, Tuple<Color, float>>(); //для деревьев(цвет, ширина)
            Random rand = new Random();

            PointF point = new PointF(0, 0);
            float cur_angle = parameters.startDirect;

            foreach (char symbol in fractal)
            {
                switch (symbol)
                {
                    case 'F': // Движение вперед

                        float newX = point.X + (float)Math.Cos(cur_angle);
                        float newY = point.Y + (float)Math.Sin(cur_angle);
                        PointF new_point = new PointF(newX, newY);
                        points.Add(Tuple.Create(point, new_point));

                        if (is_tree)
                        {
                            if (count < 3)
                            {
                                width--;
                                count++;
                            }
                            tree_dict[point] = new Tuple<Color, float>(color, width);
                        }
                        point = new_point;

                        break;
                    case '+':
                        if (is_tree)
                        {
                            float rand_angle = rand.Next(0, int.Parse(textBox1.Text) + 1) * (float)Math.PI / 180;
                            cur_angle += rand_angle + parameters.rotationAngle;
                        }
                        else
                        {
                            cur_angle += parameters.rotationAngle;
                        }
                        break;
                    case '-': // Поворот против часовой стрелки
                        if (is_tree)
                        {
                            float rand_angle = rand.Next(0, int.Parse(textBox1.Text) + 1) * (float)Math.PI / 180;
                            cur_angle -= rand_angle - parameters.rotationAngle;
                        }
                        else
                        {
                            cur_angle -= parameters.rotationAngle;
                        }
                        break;
                    case '[': // запомнили позицию
                        stateStack.Push((point, cur_angle, color, width, thin));

                        if (is_tree && thin)
                        {
                            color = color.G + 20 > 255 ? Color.FromArgb(color.R, 255, color.B) : Color.FromArgb(color.R, color.G + 20, color.B); //осветлили
                            color = color.G - color.R == 24 && color.R + 10 < 255 && color.B + 10 < 255 ? Color.FromArgb(color.R + 10, 255, color.B + 10): Color.FromArgb(color.R, color.G, color.B); //осветлили
                            width--;
                        }

                        break;

                    case ']': // вернулись на позицию
                        (point, cur_angle, color, width, thin) = stateStack.Pop();
                        if (thin == true) thin = false;
                        break;
                    case '@': // для осветления текущего цвета рисования, уменьшения толщины и длины штрихов
                        thin = true;
                        break;
                    default:
                        break;

                }
            }

            // находим минимум и максимум полученных точек для масштабирования
            float minX = points.Min(p => Math.Min(p.Item1.X, p.Item2.X));
            float maxX = points.Max(p => Math.Max(p.Item1.X, p.Item2.X));
            float minY = points.Min(p => Math.Min(p.Item1.Y, p.Item2.Y));
            float maxY = points.Max(p => Math.Max(p.Item1.Y, p.Item2.Y));

            // центр полученного фрактала
            PointF center_fractal = new PointF(minX + (maxX - minX) / 2, minY + (maxY - minY) / 2);
            // шаг для масштабирования
            float step = Math.Min(pictureBox1.Width / (maxX - minX), (pictureBox1.Height - 1) / (maxY - minY));

            List<Tuple<PointF, PointF>> scale_points = new List<Tuple<PointF, PointF>>(points);
            PointF center = new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2);

            // масштабируем список точек
            for (int i = 0; i < points.Count(); i++)
            {
                float scaleX = center.X + (points[i].Item1.X - center_fractal.X) * step;
                float scaleY = center.Y + (points[i].Item1.Y - center_fractal.Y) * step;
                float scaleNextX = center.X + (points[i].Item2.X - center_fractal.X) * step;
                float scaleNextY = center.Y + (points[i].Item2.Y - center_fractal.Y) * step;

                scale_points[i] = new Tuple<PointF, PointF>(new PointF(scaleX, scaleY), new PointF(scaleNextX, scaleNextY));
            }


            if (is_tree)
            {
                for (int i = 0; i < points.Count(); ++i)
                    graphics.DrawLine(new Pen(tree_dict[points[i].Item1].Item1, tree_dict[points[i].Item1].Item2), scale_points[i].Item1, scale_points[i].Item2);
            }
            else
            {
                for (int i = 0; i < points.Count(); ++i)
                    graphics.DrawLine(new Pen(Color.Black), scale_points[i].Item1, scale_points[i].Item2);
            }

            // Отобразим изображение
            pictureBox1.Invalidate();

        }
        //рисование
        private void button1_Click(object sender, EventArgs e)
        {
            ParamsFromFile param = new ParamsFromFile(_cur_fractal);
            Draw_fractal(param);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //_cur_fractal = l_system_dir + "КриваяКоха.txt";
            graphics.Clear(Color.White);
            pictureBox1.Invalidate();
            textBox1.Text = "0";
            textBox2.Text = "1";
            //comboBox1.SelectedIndex = 0;
        }
        //обработчик ввода случайности

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char el = e.KeyChar;
            if (!Char.IsDigit(el) && el != (char)Keys.Back && el != '-') // можно вводить только цифры, минус и стирать
                e.Handled = true;

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char el = e.KeyChar;
            if (!Char.IsDigit(el) && el != (char)Keys.Back && el != '-') // можно вводить только цифры, минус и стирать
                e.Handled = true;

        }

    }
}
