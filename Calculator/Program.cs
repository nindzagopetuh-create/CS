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
            // string expression = "3+4*2/(1-5)^2";

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
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentException("Выражение не может быть пустым");

            // Удаляем пробелы и заменяем запятые на точки
            expression = expression.Replace(" ", "").Replace(",", ".");

            // Обрабатываем отрицательные числа в начале выражения
            if (expression[0] == '-')
                expression = "0" + expression;

            Stack<double> numbers = new Stack<double>();
            Stack<char> operators = new Stack<char>();

            for (int i = 0; i < expression.Length; i++)
            {
                char current = expression[i];

                if (current == ' ') continue;

                // Обработка чисел (включая десятичные)
                if (char.IsDigit(current) || current == '.')
                {
                    string numberStr = ReadNumber(expression, ref i);
                    if (double.TryParse(numberStr, out double number))
                    {
                        numbers.Push(number);
                    }
                    else
                    {
                        throw new ArgumentException($"Неверный формат числа: {numberStr}");
                    }
                }
                // Обработка открывающей скобки
                else if (current == '(')
                {
                    operators.Push(current);
                }
                // Обработка закрывающей скобки
                else if (current == ')')
                {
                    ProcessClosingBracket(numbers, operators);
                }
                // Обработка операторов
                else if (IsOperator(current))
                {
                    ProcessOperator(current, numbers, operators);
                }
                else
                {
                    throw new ArgumentException($"Неизвестный символ: {current}");
                }
            }

            // Применяем оставшиеся операции
            ProcessRemainingOperations(numbers, operators);

            if (numbers.Count != 1 || operators.Count > 0)
            {
                throw new ArgumentException("Неверное выражение");
            }

            return numbers.Pop();
        }

        // Чтение числа из строки (оптимизированная версия)
        static string ReadNumber(string expression, ref int index)
        {
            int start = index;
            bool hasDecimalPoint = false;

            while (index < expression.Length)
            {
                char c = expression[index];

                if (char.IsDigit(c))
                {
                    index++;
                }
                else if (c == '.' && !hasDecimalPoint)
                {
                    hasDecimalPoint = true;
                    index++;
                }
                else
                {
                    break;
                }
            }

            string number = expression.Substring(start, index - start);
            index--; // Корректируем индекс для основного цикла

            return number;
        }

        // Обработка закрывающей скобки
        static void ProcessClosingBracket(Stack<double> numbers, Stack<char> operators)
        {
            bool foundOpeningBracket = false;

            while (operators.Count > 0 && operators.Peek() != '(')
            {
                if (numbers.Count < 2)
                    throw new ArgumentException("Недостаточно операндов для операции");

                numbers.Push(ApplyOperation(operators.Pop(), numbers.Pop(), numbers.Pop()));
            }

            if (operators.Count > 0 && operators.Peek() == '(')
            {
                operators.Pop(); // Удаляем открывающую скобку
                foundOpeningBracket = true;
            }

            if (!foundOpeningBracket)
                throw new ArgumentException("Несбалансированные скобки");
        }

        // Обработка операторов
        static void ProcessOperator(char currentOperator, Stack<double> numbers, Stack<char> operators)
        {
            // Обработка отрицательных чисел после операторов или открывающих скобок
            if (currentOperator == '-' && (operators.Count == 0 || operators.Peek() == '(') && numbers.Count == operators.Count)
            {
                // Это унарный минус, добавляем 0 в стек чисел
                numbers.Push(0);
            }

            // Применяем операции с более высоким или равным приоритетом
            while (operators.Count > 0 && operators.Peek() != '(' &&
                   HasHigherOrEqualPrecedence(operators.Peek(), currentOperator))
            {
                if (numbers.Count < 2)
                    throw new ArgumentException("Недостаточно операндов для операции");

                numbers.Push(ApplyOperation(operators.Pop(), numbers.Pop(), numbers.Pop()));
            }

            operators.Push(currentOperator);
        }

        // Обработка оставшихся операций
        static void ProcessRemainingOperations(Stack<double> numbers, Stack<char> operators)
        {
            while (operators.Count > 0)
            {
                if (operators.Peek() == '(')
                    throw new ArgumentException("Несбалансированные скобки");

                if (numbers.Count < 2)
                    throw new ArgumentException("Недостаточно операндов для операции");

                numbers.Push(ApplyOperation(operators.Pop(), numbers.Pop(), numbers.Pop()));
            }
        }

        static bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        static bool HasHigherOrEqualPrecedence(char op1, char op2)
        {
            int priority1 = GetOperatorPriority(op1);
            int priority2 = GetOperatorPriority(op2);
            return priority1 >= priority2;
        }

        static int GetOperatorPriority(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return 0;
            }
        }

        static double ApplyOperation(char operation, double b, double a)
        {
            switch (operation)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    if (Math.Abs(b) < double.Epsilon)
                        throw new DivideByZeroException("Деление на ноль");
                    return a / b;
                default:
                    throw new ArgumentException($"Неизвестная операция: {operation}");
            }
        }
    }
}
