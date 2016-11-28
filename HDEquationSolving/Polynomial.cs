using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HDEquationSolving
{
    public partial class Polynomial : UserControl
    {
        Graphics g = null;
        Font fnt_main = new Font("Comic Sans MS", 10.0f);
        Font fnt_up = new Font("Comic Sans MS", 7.0f);

        private double[] rates = null;
        public double[] Rates
        {
            get { return rates; }
            set
            {
                rates = value;
                if (g == null)
                    g = Graphics.FromImage(new Bitmap(Width, Height));

                float cur_x = 0;
                bool first = true;
                for (int i = 0; i < rates.Length; i++)
                {
                    if (rates[i] == 0) continue;
                    if (first)
                    {
                        if (rates[i] < 0)
                            cur_x += g.MeasureString("-", fnt_main).Width;
                        first = false;
                    }
                    else
                    {
                        string sign = rates[i] > 0 ? " + " : " - ";
                        cur_x += g.MeasureString(sign, fnt_main).Width;
                    }

                    string sr = "";
                    if (i < rates.Length - 1)
                    {
                        if (Math.Abs(rates[i]) != 1)
                            sr = Math.Abs(rates[i]).ToString();
                        sr += "x";
                    }
                    else
                        sr = Math.Abs(rates[i]).ToString();
                    cur_x += g.MeasureString(sr, fnt_main).Width - 4;

                    if (rates.Length - i > 2)
                        cur_x += g.MeasureString((rates.Length - i - 1).ToString(), fnt_up).Width - 4;
                    Width = (int)cur_x + 4 + 1;
                }
            }
        }

        public Polynomial()
        {
            InitializeComponent();
        }

        private void Polynomial_Paint(object sender, PaintEventArgs e)
        {
            if (rates == null || rates.Length == 0)
                return;

            g = e.Graphics;
            float cur_x = 0;

            bool first = true;
            for (int i = 0; i < rates.Length; i++)
            {
                if (rates[i] == 0) continue;
                if (first)
                {
                    if (rates[i] < 0)
                    {
                        g.DrawString("-", fnt_main, Brushes.Black, new PointF(cur_x, 0));
                        cur_x += g.MeasureString("-", fnt_main).Width;
                    }
                    first = false;
                } else
                {
                    string sign = rates[i] > 0 ? " + " : " - ";
                    g.DrawString(sign, fnt_main, Brushes.Black, new PointF(cur_x, 0));
                    cur_x += g.MeasureString(sign, fnt_main).Width;
                }

                string sr = "";
                if (i < rates.Length - 1)
                {
                    if (Math.Abs(rates[i]) != 1)
                        sr = Math.Abs(rates[i]).ToString();
                    sr += "x";
                } else
                    sr = Math.Abs(rates[i]).ToString();
                g.DrawString(sr, fnt_main, Brushes.Black, new PointF(cur_x, 0));
                cur_x += g.MeasureString(sr, fnt_main).Width - 4;

                if (rates.Length - i > 2)
                {
                    g.DrawString((rates.Length - i - 1).ToString(), fnt_up, Brushes.Black, new PointF(cur_x, 0));
                    cur_x += g.MeasureString((rates.Length - i - 1).ToString(), fnt_up).Width - 4;
                }
            }

            //g.DrawString("5x", fnt_main, Brushes.Black, new PointF(cur_x, 0));
            //cur_x += g.MeasureString("5x", fnt_main).Width - 4;
            //g.DrawString("3", fnt_up, Brushes.Black, new PointF(cur_x, 0));
            //cur_x += g.MeasureString("3", fnt_up).Width - 4;
            //g.DrawString(" + ", fnt_main, Brushes.Black, new PointF(cur_x, 0));
            //cur_x += g.MeasureString(" + ", fnt_main).Width;

            //g.DrawString("10x", fnt_main, Brushes.Black, new PointF(cur_x, 0));
            //cur_x += g.MeasureString("10x", fnt_main).Width - 4;
            //g.DrawString("2", fnt_up, Brushes.Black, new PointF(cur_x, 0));
            //cur_x += g.MeasureString("2", fnt_up).Width - 4;
            //g.DrawString(" + ", fnt_main, Brushes.Black, new PointF(cur_x, 0));
            //cur_x += g.MeasureString(" + ", fnt_main).Width;

            //g.DrawString("x", fnt_main, Brushes.Black, new PointF(cur_x, 0));
            //cur_x += g.MeasureString("x", fnt_main).Width - 4;
            //g.DrawString(" + ", fnt_main, Brushes.Black, new PointF(cur_x, 0));
            //cur_x += g.MeasureString(" + ", fnt_main).Width;

            //g.DrawString("101", fnt_main, Brushes.Black, new PointF(cur_x, 0));
            //cur_x += g.MeasureString("101", fnt_main).Width - 4;

            Size = new Size((int)cur_x + 4 + 1, (int)g.MeasureString("0123456789.", fnt_main).Height + 1);
        }
    }
}
