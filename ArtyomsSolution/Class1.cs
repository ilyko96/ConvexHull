using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtyomsSolution
{
    public abstract class Vershina
    {
        static protected Color col = Color.Aqua;
        static protected int r = 50;
        protected int x0;
        protected int y0;
        public int SETX { get { return x0; } set { x0 = value; } }
        public int SETY { get { return y0; } set { y0 = value; } }

        public Vershina()
        { x0 = y0 = 100; }

        public Vershina(int x, int y)
        {
            this.x0 = x;
            this.y0 = y;
        }

        static Vershina()
        {
            r = 50;
            col = Color.Aqua;
        }

        public abstract void DrawFigure(Graphics g);
        abstract public bool Check(int x, int y);
    }

    public class Krug : Vershina
    {
        public Krug(int x, int y) : base(x, y) { }
        public override void DrawFigure(Graphics g)
        {
            g.FillEllipse(new SolidBrush(col), x0 - r / 2, y0 - r / 2, r, r);
        }

        override public bool Check(int x, int y)
        {
            if (Math.Sqrt((Math.Pow(x - x0, 2)) + (Math.Pow(y - y0, 2))) <= r)
                return true;
            return false;
        }
    }

    public class Kvadrat : Vershina
    {
        public Kvadrat(int x, int y) : base(x, y) { }
        public override void DrawFigure(Graphics g)
        {
            g.FillRectangle(new SolidBrush(col), x0 - r / 2, y0 - r / 2, r, r);
        }

        override public bool Check(int x, int y)
        {
            if (Math.Abs(x - x0) <= r / 2 && (Math.Abs(y - y0) <= r / 2))
                return true;
            return false;
        }
    }

    public class Treygolnik : Vershina
    {
        public Treygolnik(int x, int y) : base(x, y) { }
        public override void DrawFigure(Graphics g)
        {
            PointF[] triangle = new PointF[] { new PointF(x0 - r/2, y0 + (float)Math .Sqrt(3)*r/6),
                            new PointF(x0 + r / 2, y0 + (float)Math.Sqrt(3) * r / 6),
                            new PointF(x0, y0 - (float)Math.Sqrt(3) * r / 3) };
            g.FillPolygon(new SolidBrush(col), triangle);
        }

        override public bool Check(int x, int y)
        {
            float l1, l2, l3, p1, p2, p3, p, S1, S2, S3, S;
            l1 = (float)Math.Sqrt((-x + (x0 - r / 2)) * (-x + (x0 - r / 2)) + (-y + (y0 + (float)Math.Sqrt(3) * r / 6) - Math.Sqrt(3) * r / 3) * (-y + ((y0 + (float)Math.Sqrt(3) * r / 6)) - Math.Sqrt(3) * r / 3));
            l2 = (float)Math.Sqrt(((x0 + r / 2) - r / 2 - x) * ((x0 + r / 2) - r / 2 - x) + ((y0 + (float)Math.Sqrt(3) * r / 6) + Math.Sqrt(3) * r / 6 - y)
                            * ((y0 + (float)Math.Sqrt(3) * r / 6) + Math.Sqrt(3) * r / 6 - y));
            l3 = (float)Math.Sqrt((x0 + r / 2 - x) * (x0 + r / 2 - x) + ((y0 - (float)Math.Sqrt(3) * r / 3) - y + Math.Sqrt(3) * r / 6) * ((y0 - (float)Math.Sqrt(3) * r / 3) - y + Math.Sqrt(3) * r / 6));
            p1 = (r + l1 + l2) / 2;
            p2 = (l2 + l3 + r) / 2;
            p3 = (l1 + l3 + r) / 2;
            p = (r + r + r) / 2;
            S1 = (float)Math.Sqrt(p1 * (p1 - l1) * (p1 - l2) * (p1 - r));
            S2 = (float)Math.Sqrt(p2 * (p2 - l3) * (p2 - l2) * (p2 - r));
            S3 = (float)Math.Sqrt(p3 * (p3 - l1) * (p3 - l3) * (p3 - r));
            S = (float)Math.Sqrt(p * (p - r) * (p - r) * (p - r));
            if (S == S1 + S2 + S3)
                return true;
            return false;
        }
    }
}