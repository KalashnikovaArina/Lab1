using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            task1 newForm = new task1();
            newForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            task2 newForm = new task2();
            newForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            task3 newForm = new task3();
            newForm.Show();
        }
    }
}
