using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HDEquationSolving
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            run();
        }

        static void run()
        {
            Random rnd = new Random();
            int[] D = new int[7],
                    d = new int[4];
            for (int i = 0; i < D.Length; i++)
            {
                D[i] = rnd.NextDouble() > 0.4 ? rnd.Next(-5, 6) : 0;
                if (i < d.Length)
                    d[i] = rnd.Next(-5, 6);
            }
            D = new int[] { 1, -1, -3, -1 };
            d = new int[] { 1, -2, -1 };

            double[] ans = new double[D.Length - d.Length + 1];
            double[] curD = new double[d.Length];
            for (int i = 0; i < curD.Length; i++)
                curD[i] = D[i];


            //ans[0] = (double)curD[0] / d[0];
            //for (int i = 1; i < d.Length; i++)
            //{
            //    curD[i - 1] = (double)curD[i] - (d[i] * ans[0]);
            //}
            //curD[d.Length - 1] = D[d.Length - 1 + 1];


            //ans[1] = (double)curD[0] / d[0];
            //for (int i = 1; i < d.Length; i++)
            //{
            //    curD[i - 1] = (double)curD[i] - (d[i] * ans[1]);
            //}
            //curD[d.Length - 1] = D[d.Length - 1 + 2];


            //ans[2] = (double)curD[0] / d[0];
            //for (int i = 1; i < d.Length; i++)
            //{
            //    curD[i - 1] = (double)curD[i] - (d[i] * ans[2]);
            //}
            //curD[d.Length - 1] = D[d.Length - 1 + 3];

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
            for (int i = 0; i < D.Length; i++)
                Console.Write("{0}x^{1}{2}", D[i], D.Length - i - 1, i < D.Length - 1 ? " + " : "");
            Console.WriteLine();
            for (int i = 0; i < d.Length; i++)
                Console.Write("{0}x^{1}{2}", d[i], d.Length - i - 1, i < d.Length - 1 ? " + " : "");
            Console.WriteLine("\n");
            for (int i = 0; i < ans.Length; i++)
                Console.Write("{0}x^{1}{2}", ans[i], ans.Length - i - 1, i < ans.Length - 1 ? " + " : "");
            Console.WriteLine();
            for (int i = 0; i < curD.Length - 1; i++)
                Console.Write("{0}x^{1}{2}", curD[i], curD.Length - i - 2, i < curD.Length - 2 ? " + " : "");

            Console.ReadKey();
        }
        static void shift(double[] arr, int n)
        {
            for (int i = 0; i < arr.Length - n; i++)
                arr[i] = arr[i + n];
        }
    }
}
