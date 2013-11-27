using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VS_solutionBidon
{
    public partial class Form1 : Form
    {
        public Point ptInit;

        public Form1()
        {
            //this.Modal = true;
            //MessageBox.Show(this.MouseDown
            
            InitializeComponent();
            plop(250);
            
            
            //ptInit = new Point(20,20);
        }

        public void plop(int a)
        {
            this.Location = new Point(a, a);
            this.Refresh();
        }
        public void plop(int a, int b)
        {
            this.Location = new Point(a, b);
            this.Refresh();
        }

        public Form1(string str)
        {
            InitializeComponent();
            
            //ptInit = new Point(20, 20);
            string[] strTab = str.Split(';');
            plop(int.Parse(strTab[0]), int.Parse(strTab[1]));
            this.Refresh();

            
        }

        private void Form1_MouseDown(object sender, EventArgs e)
        {
            MessageBox.Show("clic");
            label1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bravo, un clic de qualité !");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Move(System.Object sender, System.EventArgs e)
        {
            //this.Location = ptInit;
        }
    }
}
