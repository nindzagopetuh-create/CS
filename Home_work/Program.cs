using System;

class Program
{
    static void Main()
    {
        Console.WriteLine();

        // 1) Прямоугольник 5x5
        Console.WriteLine("1)");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Console.Write("* ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        // 2) Прямоугольный треугольник (возрастающий)
        Console.WriteLine("2)");
        for (int i = 1; i <= 5; i++)
        {
            for (int j = 1; j <= i; j++)
            {
                Console.Write("* ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        // 3) Прямоугольный треугольник (убывающий)
        Console.WriteLine("3)");
        for (int i = 5; i >= 1; i--)
        {
            for (int j = 1; j <= i; j++)
            {
                Console.Write("* ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        // 4) Треугольник со смещением вправо (убывающий)
        Console.WriteLine("4)");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < i; j++)
            {
                Console.Write("  ");
            }
            for (int j = 0; j < 5 - i; j++)
            {
                Console.Write("* ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        // 5) Треугольник со смещением вправо (возрастающий)
        Console.WriteLine("5)");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4 - i; j++)
            {
                Console.Write("  ");
            }
            for (int j = 0; j <= i; j++)
            {
                Console.Write("* ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        // 6) Ромб/песочные часы
        Console.WriteLine("6)");
        // Верхняя часть
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4 - i; j++)
            {
                Console.Write(" ");
            }
            Console.Write("/");
            for (int j = 0; j < i * 2; j++)
            {
                Console.Write(" ");
            }
            Console.Write("\\");
            Console.WriteLine();
        }
        // Нижняя часть
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < i; j++)
            {
                Console.Write(" ");
            }
            Console.Write("\\");
            for (int j = 0; j < (4 - i) * 2; j++)
            {
                Console.Write(" ");
            }
            Console.Write("/");
            Console.WriteLine();
        }
        Console.WriteLine();

        // 7) Шахматная доска 5x5
        Console.WriteLine("7)");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if ((i + j) % 2 == 0)
                    Console.Write("+ ");
                else
                    Console.Write("- ");
            }
            Console.WriteLine();
        }
    }
}