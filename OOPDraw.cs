using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOPDraw
{
    public partial class OOPDraw : Form
    {
        public OOPDraw()
        {
            InitializeComponent();
            DoubleBuffered = true;  // Stops image flickering
        }

        Pen currentPen = new Pen(Color.Black);  // set up pen parameters
        bool dragging = false;                  // not initially in drag mode
        Point startOfDrag = Point.Empty;        // initialise line start point
        Point lastMousePosition = Point.Empty;  // initialise most position
        List<Line> lines = new List<Line>();    // create list of type Line

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
           Graphics gr = e.Graphics;
           foreach (Line line in lines)
            {
                line.Draw(gr);
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startOfDrag = lastMousePosition = e.Location;
            lines.Add(new Line(currentPen, e.X, e.Y));  // initialise line (point)
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Line currentLine = lines.Last();  // list function for last item
                currentLine.GrowTo(e.X, e.Y);     // invoke method to update the line
                lastMousePosition = e.Location;
                Refresh();                        // clear canvas & then canvas_paint called
            };
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void LineWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            float width = currentPen.Width;
            switch (LineWidth.Text)
            {
                case "Thin":
                    width = 2.0F;
                    break;
                case "Medium":
                    width = 4.0F;
                    break;
                case "Thick":
                    width = 8.0F;
                    break;
            }
            currentPen = new Pen(currentPen.Color, width);
        }

        private void Colour_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color color = currentPen.Color;
            switch (Colour.Text)  // read current text property of combo box
            {
                case "Red":
                    color = Color.Red;
                    break;
                case "Green":
                    color = Color.Green;
                    break;
                case "Blue":
                    color = Color.Blue;
                    break;
            }
            currentPen = new Pen(color, currentPen.Width);
        }
    }
}
