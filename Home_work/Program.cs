using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Информация о числовых типах в C#");
        Console.WriteLine("================================\n");

        // Целочисленные типы со знаком
        PrintTypeInfo<sbyte>("sbyte");
        PrintTypeInfo<short>("short");
        PrintTypeInfo<int>("int");
        PrintTypeInfo<long>("long");

        // Целочисленные типы без знака
        PrintTypeInfo<byte>("byte");
        PrintTypeInfo<ushort>("ushort");
        PrintTypeInfo<uint>("uint");
        PrintTypeInfo<ulong>("ulong");

        // Типы с плавающей точкой
        PrintTypeInfo<float>("float");
        PrintTypeInfo<double>("double");
        PrintTypeInfo<decimal>("decimal");

        // Символьный тип (технически не числовой, но часто используется с числами)
        PrintTypeInfo<char>("char");
    }

    static void PrintTypeInfo<T>(string typeName) where T : struct
    {
        Type type = typeof(T);

        Console.WriteLine($"Тип: {typeName}");
        Console.WriteLine($"  Полное имя: {type.FullName}");
        Console.WriteLine($"  Размер в байтах: {GetSize<T>()}");
        Console.WriteLine($"  Минимальное значение: {GetMinValue<T>()}");
        Console.WriteLine($"  Максимальное значение: {GetMaxValue<T>()}");
        Console.WriteLine($"  Является примитивным: {type.IsPrimitive}");
        Console.WriteLine($"  Является значением: {type.IsValueType}");
        Console.WriteLine();
    }

    static int GetSize<T>() where T : struct
    {
        return Type.GetTypeCode(typeof(T)) switch
        {
            TypeCode.SByte or TypeCode.Byte => sizeof(sbyte),
            TypeCode.Int16 or TypeCode.UInt16 => sizeof(short),
            TypeCode.Int32 or TypeCode.UInt32 => sizeof(int),
            TypeCode.Int64 or TypeCode.UInt64 => sizeof(long),
            TypeCode.Single => sizeof(float),
            TypeCode.Double => sizeof(double),
            TypeCode.Decimal => sizeof(decimal),
            TypeCode.Char => sizeof(char),
            _ => 0
        };
    }

    static string GetMinValue<T>() where T : struct
    {
        return Type.GetTypeCode(typeof(T)) switch
        {
            TypeCode.SByte => sbyte.MinValue.ToString(),
            TypeCode.Byte => byte.MinValue.ToString(),
            TypeCode.Int16 => short.MinValue.ToString(),
            TypeCode.UInt16 => ushort.MinValue.ToString(),
            TypeCode.Int32 => int.MinValue.ToString(),
            TypeCode.UInt32 => uint.MinValue.ToString(),
            TypeCode.Int64 => long.MinValue.ToString(),
            TypeCode.UInt64 => ulong.MinValue.ToString(),
            TypeCode.Single => float.MinValue.ToString("G"),
            TypeCode.Double => double.MinValue.ToString("G"),
            TypeCode.Decimal => decimal.MinValue.ToString(),
            TypeCode.Char => ((int)char.MinValue).ToString(),
            _ => "N/A"
        };
    }

    static string GetMaxValue<T>() where T : struct
    {
        return Type.GetTypeCode(typeof(T)) switch
        {
            TypeCode.SByte => sbyte.MaxValue.ToString(),
            TypeCode.Byte => byte.MaxValue.ToString(),
            TypeCode.Int16 => short.MaxValue.ToString(),
            TypeCode.UInt16 => ushort.MaxValue.ToString(),
            TypeCode.Int32 => int.MaxValue.ToString(),
            TypeCode.UInt32 => uint.MaxValue.ToString(),
            TypeCode.Int64 => long.MaxValue.ToString(),
            TypeCode.UInt64 => ulong.MaxValue.ToString(),
            TypeCode.Single => float.MaxValue.ToString("G"),
            TypeCode.Double => double.MaxValue.ToString("G"),
            TypeCode.Decimal => decimal.MaxValue.ToString(),
            TypeCode.Char => ((int)char.MaxValue).ToString(),
            _ => "N/A"
        };
    }
}