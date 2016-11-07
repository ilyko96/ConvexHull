using System;
using System.Drawing;

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

        public static int point_on_vector(double x0, double y0, double x1, double y1, double x, double y)
        {
            return Math.Sign(x * (y1 - y0) + y * (x0 - x1) + x1 * y0 - x0 * y1);
        }
        public static int point_on_vector(Point v0, Point v1, Point p)
        {
            return point_on_vector(v0.X, v0.Y, v1.X, v1.Y, p.X, p.Y);
        }

        public static bool isLinesCollinear(Line L1, Line L2, double prec = 1e-5)
        {
            double cos = ((L1.V2.X - L1.V1.X) * (L2.V2.X - L2.V1.X) + (L1.V2.Y - L1.V1.Y) * (L2.V2.Y - L2.V1.Y)) / L1.Length / L2.Length;
            return Math.Abs(cos - 1) < prec;
        }
    }
}
