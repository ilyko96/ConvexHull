using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ArtyomsSolution
{
    public partial class Form1 : Form
    {
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;

        bool moveon = false;
        int x, y;

        Vershina v;

        public Form1()
        {
            InitializeComponent();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                flag2 = false;
                flag1 = true;
                flag3 = false;
            }
            else
            {
                flag2 = false;
                flag1 = false;
                flag3 = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                flag2 = true;
                flag1 = false;
                flag3 = false;
            }
            else
            {
                flag2 = false;
                flag1 = false;
                flag3 = false;
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                flag2 = false;
                flag1 = false;
                flag3 = true;
            }
            else
            {
                flag2 = false;
                flag1 = false;
                flag3 = false;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if ((flag1 || flag2 || flag3) && v != null)
            {
                v.DrawFigure(e.Graphics);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            moveon = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && v != null)
            {
                if (v.Check(e.X, e.Y)) { v = null; }
            }
            else
            {
                if (checkBox2.Checked)
                {
                    v = new Treygolnik(e.X - x, e.Y - y);
                    flag2 = true;
                    flag1 = false;
                    flag3 = false;
                    checkBox1.Checked = false;
                    checkBox3.Checked = false;
                }

                if (checkBox3.Checked)
                {
                    v = new Kvadrat(e.X - x, e.Y - y);
                    flag3 = true;
                    flag1 = false;
                    flag2 = false;
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                }

                if (checkBox1.Checked)
                {
                    v = new Krug(e.X - x, e.Y - y);
                    flag1 = true;
                    flag2 = false;
                    flag3 = false;
                    checkBox3.Checked = false;
                    checkBox2.Checked = false;
                }
            }
            Invalidate();

            if (e.Button == MouseButtons.Left && (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked))
            {
                moveon = true;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveon)
            {
                v.SETX = e.X - x;
                v.SETY = e.Y - y;
                Invalidate();
            }
        }
    }
}