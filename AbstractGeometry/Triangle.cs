using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AbstractGeometry
{
    class Triangle : Shape
    {
        double sideA;
        double sideB;
        double sideC;

        public double SideA
        {
            get => sideA;
            set
            {
                sideA = FilterSize(value);
                if (!IsValidTriangle()) RevalidateSides();
            }
        }
        public double SideB
        {
            get => sideB;
            set
            {
                sideB = FilterSize(value);
                if (!IsValidTriangle()) RevalidateSides();
            }
        }
        public double SideC
        {
            get => sideC;
            set
            {
                sideC = FilterSize(value);
                if (!IsValidTriangle()) RevalidateSides();
            }
        }

        public Triangle(double sideA, double sideB, double sideC,
                       int startX, int startY, int lineWidth, Color color)
            : base(startX, startY, lineWidth, color)
        {
            this.sideA = FilterSize(sideA);
            this.sideB = FilterSize(sideB);
            this.sideC = FilterSize(sideC);

            // Автоматически корректируем стороны если треугольник невалидный
            if (!IsValidTriangle())
            {
                RevalidateSides();
                Console.WriteLine("Внимание: Стороны треугольника автоматически скорректированы для создания валидного треугольника");
            }
        }

        private bool IsValidTriangle()
        {
            return sideA + sideB > sideC &&
                   sideA + sideC > sideB &&
                   sideB + sideC > sideA;
        }

        private void RevalidateSides()
        {
            // Делаем треугольник равносторонним с самой большой стороной
            double maxSide = Math.Max(sideA, Math.Max(sideB, sideC));
            sideA = sideB = sideC = maxSide;
        }

        public override double GetArea()
        {
            // Формула Герона
            double p = GetPerimeter() / 2;
            return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
        }

        public override double GetPerimeter() => SideA + SideB + SideC;

        public override void Draw(PaintEventArgs e)
        {
            try
            {
                PointF[] points = CalculateTrianglePoints();

                Pen pen = new Pen(Color, LineWidth);
                SolidBrush brush = new SolidBrush(Color.FromArgb(100, Color));

                e.Graphics.DrawPolygon(pen, points);
                e.Graphics.FillPolygon(brush, points);

                DrawBoundingBox(e);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при рисовании треугольника: {ex.Message}");
            }
        }

        private PointF[] CalculateTrianglePoints()
        {
            // Используем сторону A как основание, вычисляем высоту
            float baseLength = (float)SideA;
            float height = (float)(Math.Sqrt(3) / 2 * SideA); // Высота равностороннего треугольника

            return new PointF[]
            {
                new PointF(StartX, StartY + height), // Левая нижняя
                new PointF(StartX + baseLength, StartY + height), // Правая нижняя
                new PointF(StartX + baseLength / 2, StartY) // Верхняя вершина
            };
        }

        public override void Info(PaintEventArgs e)
        {
            try
            {
                Console.WriteLine($"Треугольник:");
                Console.WriteLine($"  Сторона A: {SideA}");
                Console.WriteLine($"  Сторона B: {SideB}");
                Console.WriteLine($"  Сторона C: {SideC}");
                Console.WriteLine($"  Координаты: ({StartX}, {StartY})");
                base.Info(e);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выводе информации о треугольнике: {ex.Message}");
            }
        }

        protected override double GetBoundingWidth() => SideA;
        protected override double GetBoundingHeight() => (Math.Sqrt(3) / 2 * SideA);
    }
}