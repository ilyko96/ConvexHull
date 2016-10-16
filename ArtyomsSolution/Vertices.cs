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
    public static class Service
    {
        public static double dist(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }
        public static double dist(Point p1, Point p2)
        {
            return dist(p1.X, p1.Y, p2.X, p2.Y);
        }

        public static double square_triangle(double a, double b, double c)
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public static bool point_on_vector(double x0, double y0, double x1, double y1, double x, double y)
        {
            return x * (y1 - y0) + y * (x0 - x1) + x1 * y0 - x0 * y1 > 0;
        }
        public static bool point_on_vector(Point v0, Point v1, Point p)
        {
            return point_on_vector(v0.X, v0.Y, v1.X, v1.Y, p.X, p.Y);
        }
    }
    public abstract class Vertex
    {
        static protected Color col = Color.Aqua;
        static protected int r = 50;
        protected int x0;
        protected int y0;
        public int SETX { get { return x0; } set { x0 = value; } }
        public int SETY { get { return y0; } set { y0 = value; } }

        public Vertex()
        { x0 = y0 = 100; }

        public Vertex(int x, int y)
        {
            this.x0 = x;
            this.y0 = y;
        }

        static Vertex()
        {
            r = 50;
            col = Color.Aqua;
        }

        public abstract void DrawFigure(Graphics g);
        abstract public bool Check(int x, int y);
    }

    public class Circle : Vertex
    {
        public Circle(int x, int y) : base(x, y) { }
        public override void DrawFigure(Graphics g)
        {
            g.FillEllipse(new SolidBrush(col), x0 - r / 2, y0 - r / 2, r, r);
        }

        override public bool Check(int x, int y)
        {
            if (Math.Sqrt((Math.Pow(x - x0, 2)) + (Math.Pow(y - y0, 2))) <= r / 2)
                return true;
            return false;
        }
    }

    public class Square : Vertex
    {
        public Square(int x, int y) : base(x, y) { }
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

    public class Triangle : Vertex
    {
        private Point P1 { get { return new Point(x0, (int)(y0 - (float)Math.Sqrt(3) * r / 3)); } }
        private Point P2 { get { return new Point(x0 - r / 2, (int)(y0 + (float)Math.Sqrt(3) * r / 6)); } }
        private Point P3 { get { return new Point(x0 + r / 2, (int)(y0 + (float)Math.Sqrt(3) * r / 6)); } }

        public Triangle(int x, int y) : base(x, y) { }
        public override void DrawFigure(Graphics g)
        {
            Point[] triangle = new Point[] { P1, P2, P3 };
            g.FillPolygon(new SolidBrush(col), triangle);
        }

        override public bool Check(int x, int y)
        {
            Point p = new Point(x, y);

            bool sign = Service.point_on_vector(P1, P2, p);
            if (Service.point_on_vector(P2, P3, p) != sign || Service.point_on_vector(P3, P1, p) != sign)
                return false;
            return true;
        }
    }
}