using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AbstractGeometry
{
    class Square : Shape, IHaveDiagonal, IHaveHeight
    {
        double side;
        public double Side
        {
            get => side;
            set => side = FilterSize(value);
        }

        public Square(double side, int startX, int startY, int lineWidth, Color color)
            : base(startX, startY, lineWidth, color)
        {
            Side = side;
        }

        public override double GetArea() => Side * Side;
        public override double GetPerimeter() => 4 * Side;

        // Реализация IHaveDiagonal
        public double GetDiagonal()
        {
            return Side * Math.Sqrt(2);
        }

        public void PrintDiagonal()
        {
            Console.WriteLine($"Диагональ квадрата: {GetDiagonal():F2}");
        }

        // Реализация IHaveHeight
        public double GetHeight() => Side;

        public void PrintHeight()
        {
            Console.WriteLine($"Высота квадрата: {Side}");
        }

        public override void Draw(PaintEventArgs e)
        {
            Pen pen = new Pen(Color, LineWidth);
            SolidBrush brush = new SolidBrush(Color.FromArgb(100, Color));

            e.Graphics.DrawRectangle(pen, StartX, StartY, (float)Side, (float)Side);
            e.Graphics.FillRectangle(brush, StartX, StartY, (float)Side, (float)Side);

            DrawBoundingBox(e);
        }

        public override void Info(PaintEventArgs e)
        {
            Console.WriteLine($"Квадрат:");
            Console.WriteLine($"  Сторона: {Side}");
            Console.WriteLine($"  Координаты: ({StartX}, {StartY})");
            base.Info(e);
            PrintDiagonal();
            PrintHeight();
            Console.WriteLine();
        }

        protected override double GetBoundingWidth() => Side;
        protected override double GetBoundingHeight() => Side;
    }
}
