using System;
using System.Drawing;

namespace ArtyomsSolution
{
    public class Line
    {
        public static Color Color;
        public Vertex V1 { get; set; }
        public Vertex V2 { get; set; }
        public Point DeltaMouse { get; set; }

        public double Length
        {
            get
            {
                return Math.Sqrt((V2.X - V1.X) * (V2.X - V1.X) + (V2.Y - V1.Y) * (V2.Y - V1.Y));
            }
        }
        
        static Line() { Color = Color.Gray; }
        public Line(Vertex v1, Vertex v2) { V1 = v1; V2 = v2; }

        public void Draw(Graphics g)
        {
            g.DrawLine(new Pen(Color), V1.Location, V2.Location);
        }

        public override string ToString()
        {
            return V1 + "; " + V2;
        }
    }
}
