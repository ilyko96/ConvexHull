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
        Font fnt_main = new Font("Comic Sans MS", 10.0f);
        Font fnt_up = new Font("Comic Sans MS", 7.0f);

        private Bitmap bmp = null;
        private double[] rates = null;
        public double[] Rates
        {
            get { return rates; }
            set
            {
                rates = value;
                if (rates == null || rates.Length == 0)
                    return;
                draw();
            }
        }
        public int DegreeOffset { get; set; }
        private void draw()
        {
            if (bmp != null)
                bmp.Dispose();

            if (Parent != null)
                bmp = new Bitmap(Parent.Width, Parent.Height);
            else
                bmp = new Bitmap(1000, 1000);

            Graphics g = Graphics.FromImage(bmp);

            List<float> off = new List<float>();

            float cur_x = 0;
            bool first = true;
            for (int i = 0; i < rates.Length; i++)
            {
                if (rates[i] == 0 && !Zeros) continue;
                if (first)
                {
                    if (rates[i] < 0)
                    {
                        g.DrawString("-", fnt_main, Brushes.Black, new PointF(cur_x, 0));
                        cur_x += g.MeasureString("-", fnt_main).Width;
                    }
                    first = false;
                }
                else
                {
                    string sign = rates[i] > 0 ? " + " : " - ";
                    g.DrawString(sign, fnt_main, Brushes.Black, new PointF(cur_x, 0));
                    cur_x += g.MeasureString(sign, fnt_main).Width;
                }

                off.Add(cur_x);
                string sr = "";
                if (i < rates.Length - 1)
                {
                    if (Math.Abs(rates[i]) != 1)
                        sr = Math.Abs(rates[i]).ToString();
                    sr += "x";
                }
                else
                    sr = Math.Abs(rates[i]).ToString();
                g.DrawString(sr, fnt_main, Brushes.Black, new PointF(cur_x, 0));
                cur_x += g.MeasureString(sr, fnt_main).Width - 4;

                if (rates.Length - i > 2)
                {
                    g.DrawString((rates.Length - i - 1 + DegreeOffset).ToString(), fnt_up, Brushes.Black, new PointF(cur_x, 0));
                    cur_x += g.MeasureString((rates.Length - i - 1 + DegreeOffset).ToString(), fnt_up).Width - 4;
                }
            }
            Width = (int)cur_x + 4 + 1;
            Height = (int)g.MeasureString("X", fnt_up).Height + 7;

            Offsets = off.ToArray();
        }
        public float[] Offsets { get; private set; }
        public bool Zeros { get; set; }

        public Polynomial()
        {
            InitializeComponent();

            Zeros = false;
        }

        private void Polynomial_Paint(object sender, PaintEventArgs e)
        {
            if (bmp != null)
                e.Graphics.DrawImage(bmp, 0, 0);
        }
    }
}
