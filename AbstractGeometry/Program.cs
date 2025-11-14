using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace AbstractGeometry
{
    class Program
    {
        static void Main(string[] args)
        {
            // Показываем диаграмму классов
            ClassDiagram.PrintDiagram();
            Console.WriteLine("\n" + new string('=', 50) + "\n");

            Console.WriteLine("=== ДЕМОНСТРАЦИЯ ВСЕХ ГЕОМЕТРИЧЕСКИХ ФИГУР ===");
            Console.WriteLine("Создание изображения с фигурами...\n");

            try
            {
                using (Bitmap bitmap = new Bitmap(1200, 1000))
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    graphics.Clear(Color.White);

                    PaintEventArgs e = new PaintEventArgs(graphics, new System.Drawing.Rectangle(0, 0, 1200, 1000));

                    // Демонстрация всех фигур
                    Console.WriteLine("1. Квадрат:");
                    Square square = new Square(100, 50, 50, 3, Color.Red);
                    square.Draw(e);
                    square.Info(e);

                    Console.WriteLine("2. Прямоугольник:");
                    Rectangle rectangle = new Rectangle(150, 80, 200, 150, 2, Color.Blue);
                    rectangle.Draw(e);
                    rectangle.Info(e);

                    Console.WriteLine("3. Круг:");
                    Circle circle = new Circle(50, 400, 100, 2, Color.Green);
                    circle.Draw(e);
                    circle.Info(e);

                    Console.WriteLine("4. Треугольник:");
                    Triangle triangle = new Triangle(120, 100, 140, 600, 100, 2, Color.Orange);
                    triangle.Draw(e);
                    triangle.Info(e);

                    Console.WriteLine("5. Равносторонний треугольник:");
                    EquilateralTriangle eqTriangle = new EquilateralTriangle(100, 50, 300, 2, Color.Purple);
                    eqTriangle.Draw(e);
                    eqTriangle.Info(e);

                    Console.WriteLine("6. Равнобедренный треугольник:");
                    IsoscelesTriangle isosTriangle = new IsoscelesTriangle(120, 100, 600, 300, 2, Color.DarkCyan);
                    isosTriangle.Draw(e);
                    isosTriangle.Info(e);

                    // Дополнительные фигуры
                    Console.WriteLine("7. Дополнительные фигуры:");
                    Square square2 = new Square(70, 50, 500, 2, Color.DarkRed);
                    square2.Draw(e);

                    Circle circle2 = new Circle(40, 400, 500, 2, Color.DarkGreen);
                    circle2.Draw(e);

                    // Сохраняем результат
                    string filename = "all_geometric_shapes.png";
                    bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Png);

                    Console.WriteLine($"\n✅ Все фигуры успешно нарисованы и сохранены в '{filename}'");
                    Console.WriteLine($"📍 Файл находится в: {Directory.GetCurrentDirectory()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}