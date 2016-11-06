using System;
using System.Drawing;

namespace ArtyomsSolution
{
    class Line
    {
        public static Color Color;
        public Vertex V1 { get; set; }
        public Vertex V2 { get; set; }
        
        static Line() { Color = Color.Gray; }
        public Line(Vertex v1, Vertex v2) { V1 = v1; V2 = v2; }

        public void Draw(Graphics g)
        {
            g.DrawLine(new Pen(Color), V1.Location, V2.Location);
        }
    }
}
