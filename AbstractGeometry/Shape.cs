using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AbstractGeometry
{
    abstract class Shape
    {
        public static readonly int MIN_START_X = 10;
        public static readonly int MIN_START_Y = 10;
        public static readonly int MAX_START_X = 1000;
        public static readonly int MAX_START_Y = 700;
        public static readonly int MIN_LINE_WIDTH = 1;
        public static readonly int MAX_LINE_WIDTH = 32;
        public static readonly double MIN_SIZE = 20;
        public static readonly double MAX_SIZE = 500;

        int startX;
        int startY;
        int lineWidth;

        public int StartX
        {
            get => startX;
            set => startX =
                value < MIN_START_X ? MIN_START_X :
                value > MAX_START_X ? MAX_START_X :
                value;
        }
        public int StartY
        {
            get => startY;
            set => startY =
                value < MIN_START_Y ? MIN_START_Y :
                value > MAX_START_Y ? MAX_START_Y :
                value;
        }
        public int LineWidth
        {
            get => lineWidth;
            set => lineWidth =
                value < MIN_LINE_WIDTH ? MIN_LINE_WIDTH :
                value > MAX_LINE_WIDTH ? MAX_LINE_WIDTH :
                value;
        }

        public double FilterSize(double value) =>
            value < MIN_SIZE ? MIN_SIZE :
            value > MAX_SIZE ? MAX_SIZE :
            value;

        public Color Color { get; set; }

        public Shape(int startX, int startY, int lineWidth, Color color)
        {
            StartX = startX;
            StartY = startY;
            LineWidth = lineWidth;
            Color = color;
        }

        public abstract double GetArea();
        public abstract double GetPerimeter();
        public abstract void Draw(PaintEventArgs e);

        public virtual void Info(PaintEventArgs e)
        {
            Console.WriteLine($"Площадь фигуры: {GetArea():F2}");
            Console.WriteLine($"Периметр фигуры: {GetPerimeter():F2}");
            Draw(e);
        }

        protected void DrawBoundingBox(PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Gray, 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            e.Graphics.DrawRectangle(pen, StartX - 5, StartY - 5,
                                   (float)GetBoundingWidth() + 10,
                                   (float)GetBoundingHeight() + 10);
        }

        protected virtual double GetBoundingWidth() => 0;
        protected virtual double GetBoundingHeight() => 0;
    }
}
