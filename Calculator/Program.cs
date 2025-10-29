//#define CALC_IF
//#define CALC_SWITCH
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите арифметическое выражение: ");
            string expression = Console.ReadLine();
            // string expression = "22+33-44/2+8*3";
            // string expression = "(2+3)*4-10/2";

            try
            {
                double result = EvaluateExpression(expression);
                Console.WriteLine($"Результат: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.ReadKey();
        }

        static double EvaluateExpression(string expression)
        {
            // Удаляем пробелы и заменяем запятые на точки
            expression = expression.Replace(" ", "").Replace(",", ".");

            // Используем стек для чисел и операций
            Stack<double> numbers = new Stack<double>();
            Stack<char> operators = new Stack<char>();

            for (int i = 0; i < expression.Length; i++)
            {
                char current = expression[i];

                // Если текущий символ - цифра или точка, извлекаем полное число
                if (char.IsDigit(current) || current == '.')
                {
                    string numberStr = "";
                    while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.'))
                    {
                        numberStr += expression[i];
                        i++;
                    }
                    i--; // Возвращаемся на один символ назад

                    if (double.TryParse(numberStr, out double number))
                    {
                        numbers.Push(number);
                    }
                    else
                    {
                        throw new ArgumentException($"Неверный формат числа: {numberStr}");
                    }
                }
                // Если текущий символ - открывающая скобка, добавляем в стек операторов
                else if (current == '(')
                {
                    operators.Push(current);
                }
                // Если текущий символ - закрывающая скобка, вычисляем выражение в скобках
                else if (current == ')')
                {
                    while (operators.Count > 0 && operators.Peek() != '(')
                    {
                        numbers.Push(ApplyOperation(operators.Pop(), numbers.Pop(), numbers.Pop()));
                    }

                    if (operators.Count == 0)
                    {
                        throw new ArgumentException("Несбалансированные скобки");
                    }

                    operators.Pop(); // Удаляем открывающую скобку
                }
                // Если текущий символ - оператор
                else if (IsOperator(current))
                {
                    // Применяем операции с более высоким приоритетом
                    while (operators.Count > 0 && HasHigherPrecedence(operators.Peek(), current))
                    {
                        numbers.Push(ApplyOperation(operators.Pop(), numbers.Pop(), numbers.Pop()));
                    }

                    operators.Push(current);
                }
                else
                {
                    throw new ArgumentException($"Неизвестный символ: {current}");
                }
            }

            // Применяем оставшиеся операции
            while (operators.Count > 0)
            {
                numbers.Push(ApplyOperation(operators.Pop(), numbers.Pop(), numbers.Pop()));
            }

            if (numbers.Count != 1)
            {
                throw new ArgumentException("Неверное выражение");
            }

            return numbers.Pop();
        }

        static bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        static bool HasHigherPrecedence(char op1, char op2)
        {
            if (op1 == '(' || op1 == ')')
                return false;

            if ((op2 == '*' || op2 == '/') && (op1 == '+' || op1 == '-'))
                return false;

            return true;
        }

        static double ApplyOperation(char operation, double b, double a)
        {
            switch (operation)
            {
                case '+': return a + b;
                case '-': return a - b;
                case '*': return a * b;
                case '/':
                    if (b == 0)
                        throw new DivideByZeroException("Деление на ноль");
                    return a / b;
                default:
                    throw new ArgumentException($"Неизвестная операция: {operation}");
            }
        }
    }
}

