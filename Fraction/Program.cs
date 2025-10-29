using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FractionProject;

namespace FractionProject
{
    public class Fraction : IComparable<Fraction>, IEquatable<Fraction>
    {
        public long Numerator { get; private set; }
        public long Denominator { get; private set; }

        public Fraction(long numerator, long denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Denominator cannot be zero.");

            Numerator = numerator;
            Denominator = denominator;
            Normalize();
        }

        public Fraction(long wholeNumber) : this(wholeNumber, 1) { }

        // Нормализация дроби (сокращение и знак)
        private void Normalize()
        {
            if (Denominator < 0)
            {
                Numerator = -Numerator;
                Denominator = -Denominator;
            }

            long gcd = GCD(Math.Abs(Numerator), Math.Abs(Denominator));
            if (gcd > 1)
            {
                Numerator /= gcd;
                Denominator /= gcd;
            }
        }

        // Наибольший общий делитель (алгоритм Евклида)
        private static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Наименьшее общее кратное
        private static long LCM(long a, long b)
        {
            if (a == 0 || b == 0) return 0;
            return (a / GCD(a, b)) * b;
        }

        // Арифметические операции
        public static Fraction operator +(Fraction a, Fraction b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("Fractions cannot be null");

            long lcm = LCM(a.Denominator, b.Denominator);
            long numerator = a.Numerator * (lcm / a.Denominator) + b.Numerator * (lcm / b.Denominator);
            return new Fraction(numerator, lcm);
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("Fractions cannot be null");

            long lcm = LCM(a.Denominator, b.Denominator);
            long numerator = a.Numerator * (lcm / a.Denominator) - b.Numerator * (lcm / b.Denominator);
            return new Fraction(numerator, lcm);
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("Fractions cannot be null");

            return new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("Fractions cannot be null");
            if (b.Numerator == 0)
                throw new DivideByZeroException("Cannot divide by zero fraction.");

            return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }

        // Унарные операторы
        public static Fraction operator +(Fraction fraction)
        {
            return fraction ?? throw new ArgumentNullException(nameof(fraction));
        }

        public static Fraction operator -(Fraction fraction)
        {
            if (fraction is null)
                throw new ArgumentNullException(nameof(fraction));

            return new Fraction(-fraction.Numerator, fraction.Denominator);
        }

        public static Fraction operator ++(Fraction fraction)
        {
            if (fraction is null)
                throw new ArgumentNullException(nameof(fraction));

            return fraction + new Fraction(1);
        }

        public static Fraction operator --(Fraction fraction)
        {
            if (fraction is null)
                throw new ArgumentNullException(nameof(fraction));

            return fraction - new Fraction(1);
        }

        // Операторы сравнения
        public static bool operator ==(Fraction a, Fraction b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Numerator == b.Numerator && a.Denominator == b.Denominator;
        }

        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }

        public static bool operator <(Fraction a, Fraction b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("Fractions cannot be null for comparison");
            return a.CompareTo(b) < 0;
        }

        public static bool operator >(Fraction a, Fraction b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("Fractions cannot be null for comparison");
            return a.CompareTo(b) > 0;
        }

        public static bool operator <=(Fraction a, Fraction b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("Fractions cannot be null for comparison");
            return a.CompareTo(b) <= 0;
        }

        public static bool operator >=(Fraction a, Fraction b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("Fractions cannot be null for comparison");
            return a.CompareTo(b) >= 0;
        }

        // Преобразование в другие типы
        public double ToDouble()
        {
            return (double)Numerator / Denominator;
        }

        public decimal ToDecimal()
        {
            return (decimal)Numerator / Denominator;
        }

        // Преобразование из других типов
        public static implicit operator Fraction(long value)
        {
            return new Fraction(value);
        }

        public static explicit operator double(Fraction fraction)
        {
            return fraction?.ToDouble() ?? throw new ArgumentNullException(nameof(fraction));
        }

        public static explicit operator decimal(Fraction fraction)
        {
            return fraction?.ToDecimal() ?? throw new ArgumentNullException(nameof(fraction));
        }

        // Реализация интерфейсов
        public int CompareTo(Fraction other)
        {
            if (other is null) return 1;

            long left = Numerator * other.Denominator;
            long right = other.Numerator * Denominator;

            return left.CompareTo(right);
        }

        public bool Equals(Fraction other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Fraction);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Numerator.GetHashCode() * 397) ^ Denominator.GetHashCode();
            }
        }

        // Строковое представление
        public override string ToString()
        {
            if (Denominator == 1)
                return Numerator.ToString();

            return $"{Numerator}/{Denominator}";
        }

        // Парсинг из строки
        public static Fraction Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentException("String cannot be null or empty.");

            string[] parts = s.Split('/');
            if (parts.Length == 1)
            {
                if (long.TryParse(parts[0].Trim(), out long numerator))
                    return new Fraction(numerator);
            }
            else if (parts.Length == 2)
            {
                if (long.TryParse(parts[0].Trim(), out long numerator) &&
                    long.TryParse(parts[1].Trim(), out long denominator))
                    return new Fraction(numerator, denominator);
            }

            throw new FormatException("Invalid fraction format. Expected format: 'numerator/denominator' or 'wholeNumber'");
        }

        public static bool TryParse(string s, out Fraction fraction)
        {
            fraction = null;
            try
            {
                fraction = Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Математические методы
        public Fraction Reciprocal()
        {
            if (Numerator == 0)
                throw new InvalidOperationException("Cannot get reciprocal of zero fraction.");

            return new Fraction(Denominator, Numerator);
        }

        public Fraction Abs()
        {
            return new Fraction(Math.Abs(Numerator), Math.Abs(Denominator));
        }

        public Fraction Pow(int exponent)
        {
            if (exponent == 0)
                return new Fraction(1);

            if (exponent < 0)
                return Reciprocal().Pow(-exponent);

            Fraction result = new Fraction(1);
            for (int i = 0; i < exponent; i++)
            {
                result *= this;
            }

            return result;
        }
    }
}
namespace FractionProject
{
    class Program
{
    static void Main(string[] args)
    {
        // Создание дробей
        Fraction f1 = new Fraction(1, 2);
        Fraction f2 = new Fraction(3, 4);
        Fraction f3 = new Fraction(2);

        Console.WriteLine($"f1 = {f1}"); // 1/2
        Console.WriteLine($"f2 = {f2}"); // 3/4
        Console.WriteLine($"f3 = {f3}"); // 2

        // Арифметические операции
        Console.WriteLine($"f1 + f2 = {f1 + f2}"); // 5/4
        Console.WriteLine($"f1 - f2 = {f1 - f2}"); // -1/4
        Console.WriteLine($"f1 * f2 = {f1 * f2}"); // 3/8
        Console.WriteLine($"f1 / f2 = {f1 / f2}"); // 2/3

        // Сравнение
        Console.WriteLine($"f1 < f2: {f1 < f2}"); // True
        Console.WriteLine($"f1 == f2: {f1 == f2}"); // False

        // Преобразование
        Console.WriteLine($"f1 as double: {(double)f1}"); // 0.5
        Console.WriteLine($"f2 as decimal: {(decimal)f2}"); // 0.75

        // Парсинг
        Fraction f4 = Fraction.Parse("5/8");
        Console.WriteLine($"Parsed: {f4}"); // 5/8

        // Математические методы
        Console.WriteLine($"Reciprocal of f1: {f1.Reciprocal()}"); // 2
        Console.WriteLine($"Absolute of -f1: {(-f1).Abs()}"); // 1/2
        Console.WriteLine($"f1^3: {f1.Pow(3)}"); // 1/8
    }
}
}