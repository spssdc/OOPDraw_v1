using System;
using System.Drawing;

namespace OOPDraw
{
    // Declare new class type called Rectangle
    public class Rectangle : Shape
    {
        // Set up class attirbutes as publicly accesible, but only privately changeable
        public Pen Pen { get; private set; }
        public int X1 { get; private set; }
        public int Y1 { get; private set; }
        public int X2 { get; private set; }
        public int Y2 { get; private set; }

        // class constructor
        public Rectangle(Pen p, int x1, int y1, int x2, int y2)
        {
            Pen = p;
            X1 = x1;
            Y1 = y1;
            X2 = X2;
            Y2 = Y2;
        }

        // second version of the constructor, called with fewer parameters,
        // for a zero length line (at the start of drag/click operation)
        public Rectangle(Pen p, int x1, int y1) : this(p, x1, y1, x1, y1)
        {

        }

        // publicly invokable method that can be applied to a line
        public override void Draw(Graphics g)
        {
            int x = Math.Min(X1, X2);
            int y = Math.Min(Y1, Y2);
            int w = Math.Max(X1, X2) - x;
            int h = Math.Max(Y1, Y2) - y;
            g.DrawRectangle(Pen, x,y,w,h);
        }

        // publicly invokable method that can be applied to a line
        public override void GrowTo(int x2, int y2)
        {
            X2 = x2;
            Y2 = y2;
        }
    }
}
