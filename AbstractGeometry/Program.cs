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
            Console.WriteLine("=== ГЕОМЕТРИЧЕСКИЕ ФИГУРЫ ===");
            Console.WriteLine("Создание изображения с фигурами...\n");

            try
            {
                // Создаем bitmap для рисования
                using (Bitmap bitmap = new Bitmap(1000, 800))
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // Настраиваем графику для лучшего качества
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    graphics.Clear(Color.White);

                    PaintEventArgs e = new PaintEventArgs(graphics, new System.Drawing.Rectangle(0, 0, 1000, 800));

                    // Рисуем фигуры с разными позициями
                    Console.WriteLine("Рисую квадрат...");
                    Square square = new Square(100, 50, 50, 3, Color.Red);
                    square.Draw(e);
                    square.Info(e);

                    Console.WriteLine("Рисую прямоугольник...");
                    Rectangle rectangle = new Rectangle(150, 80, 200, 150, 2, Color.Blue);
                    rectangle.Draw(e);
                    rectangle.Info(e);

                    Console.WriteLine("Рисую круг...");
                    Circle circle = new Circle(50, 400, 100, 2, Color.Green);
                    circle.Draw(e);
                    circle.Info(e);

                    Console.WriteLine("Рисую треугольник...");
                    // Используем валидные стороны для треугольника
                    Triangle triangle = new Triangle(120, 120, 120, 600, 100, 2, Color.Orange);
                    triangle.Draw(e);
                    triangle.Info(e);

                    string filename = "geometric_shapes.png";
                    bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                    Console.WriteLine($"Рисунок успешно сохранен как '{filename}'");
                    Console.WriteLine($"Размер изображения: {bitmap.Width}x{bitmap.Height} пикселей");

                    string fullPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), filename);
                    Console.WriteLine($"Полный путь: {fullPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                Console.WriteLine($"Детали: {ex.StackTrace}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();

        }
    }
}