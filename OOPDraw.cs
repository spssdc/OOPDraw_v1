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
            LineWidth.SelectedItem = "Medium";
            Colour.SelectedItem = "Green";
            ShapeOpt.SelectedItem = "Line";
        }

        Pen currentPen = new Pen(Color.Black);  // set up pen parameters
        bool dragging = false;                  // not initially in drag mode
        Point startOfDrag = Point.Empty;        // initialise line start point
        Point lastMousePosition = Point.Empty;  // initialise most position
        private List<Shape> shapes = new List<Shape>();    

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
           Graphics gr = e.Graphics;
           foreach (dynamic shape in shapes)
            {
                shape.Draw(gr);
            }

        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startOfDrag = lastMousePosition = e.Location;
            
            // Switch on type of shape selected in combo box
            switch (ShapeOpt.Text)
            {
                case "Line":
                    shapes.Add(new Line(currentPen, e.X, e.Y));  // initialise line (point)
                    break;
                case "Rectangle":
                    shapes.Add(new Rectangle(currentPen, e.X, e.Y));  // initialise line (point)
                    break;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {

                dynamic shape = shapes.Last();    // list function for last item
                shape.GrowTo(e.X, e.Y);           // invoke method to update the line
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
