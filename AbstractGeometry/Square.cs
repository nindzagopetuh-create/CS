using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AbstractGeometry
{
    class Square : Shape
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

        public override void Draw(PaintEventArgs e)
        {
            Pen pen = new Pen(Color, LineWidth);
            SolidBrush brush = new SolidBrush(Color.FromArgb(100, Color)); // Полупрозрачная заливка

            e.Graphics.DrawRectangle(pen, StartX, StartY, (float)Side, (float)Side);
            e.Graphics.FillRectangle(brush, StartX, StartY, (float)Side, (float)Side);

            // Рисуем bounding box
            DrawBoundingBox(e);
        }

        public override void Info(PaintEventArgs e)
        {
            Console.WriteLine($"Квадрат:");
            Console.WriteLine($"  Сторона: {Side}");
            Console.WriteLine($"  Координаты: ({StartX}, {StartY})");
            base.Info(e);
            Console.WriteLine();
        }

        protected override double GetBoundingWidth() => Side;
        protected override double GetBoundingHeight() => Side;
    }
}
