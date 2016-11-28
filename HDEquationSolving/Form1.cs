using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HDEquationSolving
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            double[] D = new double[] { 1, -1, -3, -1 };
            double[] d = new double[] { 1, -2, -1 };
            run(D, d);
        }

        private void run(double[] D, double[] d)
        {
            Polynomial pD = new Polynomial();
            pD.Rates = D;
            pD.Location = new Point();
            Controls.Add(pD);

            Polynomial pd = new Polynomial();
            pd.Rates = d;
            pd.Location = new Point(pD.Width, 0);
            Controls.Add(pd);

            double[] ans = new double[D.Length - d.Length + 1];
            double[] curD = new double[d.Length];
            for (int i = 0; i < curD.Length; i++)
                curD[i] = D[i];

            for (int j = 0; j <= D.Length - d.Length; j++)
            {
                if (curD[0] == 0)
                {
                    shift(curD, 1);
                    curD[d.Length - 1] = D[d.Length + j];
                    continue;
                }
                ans[j] = curD[0] / d[0];
                for (int i = 1; i < d.Length; i++)
                    curD[i - 1] = curD[i] - (d[i] * ans[j]);
                if (j < D.Length - d.Length)
                    curD[d.Length - 1] = D[d.Length + j];
            }


            ////////////// Visualization /////////////
            //for (int i = 0; i < D.Length; i++)
            //    Console.Write("{0}x^{1}{2}", D[i], D.Length - i - 1, i < D.Length - 1 ? " + " : "");
            //Console.WriteLine();
            //for (int i = 0; i < d.Length; i++)
            //    Console.Write("{0}x^{1}{2}", d[i], d.Length - i - 1, i < d.Length - 1 ? " + " : "");
            //Console.WriteLine("\n");
            //for (int i = 0; i < ans.Length; i++)
            //    Console.Write("{0}x^{1}{2}", ans[i], ans.Length - i - 1, i < ans.Length - 1 ? " + " : "");
            //Console.WriteLine();
            //for (int i = 0; i < curD.Length - 1; i++)
            //    Console.Write("{0}x^{1}{2}", curD[i], curD.Length - i - 2, i < curD.Length - 2 ? " + " : "");

            //Console.ReadKey();
        }
        private void shift(double[] arr, int n)
        {
            for (int i = 0; i < arr.Length - n; i++)
                arr[i] = arr[i + n];
        }
    }
}
