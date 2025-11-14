using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractGeometry
{
    class ClassDiagram
    {
        public static void PrintDiagram()
        {
            Console.WriteLine("=== ДИАГРАММА КЛАССОВ ===");
            Console.WriteLine();

            Console.WriteLine("Интерфейсы:");
            Console.WriteLine("├── IHaveDiagonal");
            Console.WriteLine("│   ├── double GetDiagonal()");
            Console.WriteLine("│   └── void PrintDiagonal()");
            Console.WriteLine("├── IHaveDiameter");
            Console.WriteLine("│   ├── double GetDiameter()");
            Console.WriteLine("│   └── void PrintDiameter()");
            Console.WriteLine("└── IHaveHeight");
            Console.WriteLine("    ├── double GetHeight()");
            Console.WriteLine("    └── void PrintHeight()");
            Console.WriteLine();

            Console.WriteLine("Абстрактный класс:");
            Console.WriteLine("└── Shape");
            Console.WriteLine("    ├── Поля: startX, startY, lineWidth, Color");
            Console.WriteLine("    ├── Свойства: StartX, StartY, LineWidth, Color");
            Console.WriteLine("    ├── Абстрактные методы:");
            Console.WriteLine("    │   ├── double GetArea()");
            Console.WriteLine("    │   ├── double GetPerimeter()");
            Console.WriteLine("    │   └── void Draw(PaintEventArgs e)");
            Console.WriteLine("    └── Виртуальные методы:");
            Console.WriteLine("        └── void Info(PaintEventArgs e)");
            Console.WriteLine();

            Console.WriteLine("Конкретные классы:");
            Console.WriteLine("├── Square : Shape, IHaveDiagonal, IHaveHeight");
            Console.WriteLine("├── Rectangle : Shape, IHaveDiagonal, IHaveHeight");
            Console.WriteLine("├── Circle : Shape, IHaveDiameter");
            Console.WriteLine("├── Triangle : Shape");
            Console.WriteLine("├── EquilateralTriangle : Triangle, IHaveHeight");
            Console.WriteLine("└── IsoscelesTriangle : Triangle, IHaveHeight");
        }
    }
}
