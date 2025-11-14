using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AbstractGeometry
{
    class Circle : Shape, IHaveDiameter
    {
        double radius;
        public double Radius
        {
            get => radius;
            set => radius = FilterSize(value) / 2;
        }

        public Circle(double radius, int startX, int startY, int lineWidth, Color color)
            : base(startX, startY, lineWidth, color)
        {
            Radius = radius;
        }

        public override double GetArea() => Math.PI * Radius * Radius;
        public override double GetPerimeter() => 2 * Math.PI * Radius;

        // Реализация IHaveDiameter
        public double GetDiameter() => Radius * 2;

        public void PrintDiameter()
        {
            Console.WriteLine($"Диаметр круга: {GetDiameter()}");
        }

        public override void Draw(PaintEventArgs e)
        {
            Pen pen = new Pen(Color, LineWidth);
            SolidBrush brush = new SolidBrush(Color.FromArgb(100, Color));

            float diameter = (float)(Radius * 2);
            e.Graphics.DrawEllipse(pen, StartX, StartY, diameter, diameter);
            e.Graphics.FillEllipse(brush, StartX, StartY, diameter, diameter);

            DrawBoundingBox(e);
        }

        public override void Info(PaintEventArgs e)
        {
            Console.WriteLine($"Круг:");
            Console.WriteLine($"  Радиус: {Radius}");
            Console.WriteLine($"  Координаты центра: ({StartX + Radius}, {StartY + Radius})");
            base.Info(e);
            PrintDiameter();
            Console.WriteLine();
        }

        protected override double GetBoundingWidth() => Radius * 2;
        protected override double GetBoundingHeight() => Radius * 2;
    }
}
