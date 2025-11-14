using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AbstractGeometry
{
    class IsoscelesTriangle : Triangle, IHaveHeight
    {
        public IsoscelesTriangle(double baseSide, double equalSide, int startX, int startY, int lineWidth, Color color)
            : base(baseSide, equalSide, equalSide, startX, startY, lineWidth, color)
        {
        }

        public double Base => SideA;
        public double Leg => SideB;

        public double GetHeight()
        {
            // Высота равнобедренного треугольника: h = √(b² - (a/2)²)
            return Math.Sqrt(Leg * Leg - (Base * Base) / 4);
        }

        public void PrintHeight()
        {
            Console.WriteLine($"Высота равнобедренного треугольника: {GetHeight():F2}");
        }

        public override void Info(PaintEventArgs e)
        {
            Console.WriteLine($"Равнобедренный треугольник:");
            Console.WriteLine($"  Основание: {Base}");
            Console.WriteLine($"  Боковая сторона: {Leg}");
            Console.WriteLine($"  Координаты: ({StartX}, {StartY})");
            base.Info(e);
            PrintHeight();
            Console.WriteLine();
        }

        protected override double GetBoundingWidth() => Base;
        protected override double GetBoundingHeight() => GetHeight();
    }
}
