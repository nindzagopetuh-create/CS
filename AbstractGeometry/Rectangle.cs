using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AbstractGeometry
{
    class Rectangle : Shape, IHaveDiagonal, IHaveHeight
    {
        double width;
        double height;

        public double Width
        {
            get => width;
            set => width = FilterSize(value);
        }
        public double Height
        {
            get => height;
            set => height = FilterSize(value);
        }

        public Rectangle(double width, double height,
            int startX, int startY, int lineWidth, Color color)
            : base(startX, startY, lineWidth, color)
        {
            Width = width;
            Height = height;
        }

        public override double GetArea() => Width * Height;
        public override double GetPerimeter() => 2 * (Width + Height);

        // Реализация IHaveDiagonal
        public double GetDiagonal()
        {
            return Math.Sqrt(Width * Width + Height * Height);
        }

        public void PrintDiagonal()
        {
            Console.WriteLine($"Диагональ прямоугольника: {GetDiagonal():F2}");
        }

        // Реализация IHaveHeight
        public double GetHeight() => Height;

        public void PrintHeight()
        {
            Console.WriteLine($"Высота прямоугольника: {Height}");
        }

        public override void Draw(PaintEventArgs e)
        {
            Pen pen = new Pen(Color, LineWidth);
            SolidBrush brush = new SolidBrush(Color.FromArgb(100, Color));

            e.Graphics.DrawRectangle(pen, StartX, StartY, (float)Width, (float)Height);
            e.Graphics.FillRectangle(brush, StartX, StartY, (float)Width, (float)Height);

            DrawBoundingBox(e);
        }

        public override void Info(PaintEventArgs e)
        {
            Console.WriteLine($"Прямоугольник:");
            Console.WriteLine($"  Ширина: {Width}");
            Console.WriteLine($"  Высота: {Height}");
            Console.WriteLine($"  Координаты: ({StartX}, {StartY})");
            base.Info(e);
            PrintDiagonal();
            PrintHeight();
            Console.WriteLine();
        }

        protected override double GetBoundingWidth() => Width;
        protected override double GetBoundingHeight() => Height;
    }
}