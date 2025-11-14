using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AbstractGeometry
{
    class EquilateralTriangle : Triangle, IHaveHeight
    {
        public EquilateralTriangle(double side, int startX, int startY, int lineWidth, Color color)
            : base(side, side, side, startX, startY, lineWidth, color)
        {
        }

        public double GetHeight()
        {
            // Высота равностороннего треугольника: h = (a√3)/2
            return (SideA * Math.Sqrt(3)) / 2;
        }

        public void PrintHeight()
        {
            Console.WriteLine($"Высота равностороннего треугольника: {GetHeight():F2}");
        }

        public override void Info(PaintEventArgs e)
        {
            Console.WriteLine($"Равносторонний треугольник:");
            Console.WriteLine($"  Сторона: {SideA}");
            Console.WriteLine($"  Координаты: ({StartX}, {StartY})");
            base.Info(e);
            PrintHeight();
            Console.WriteLine();
        }
    }
}
