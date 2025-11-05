using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Academy
{
    class Program
    {
        static readonly string delimiter = "\n-----------------------------------\n";
        static readonly string fileName = "group.txt";

        static void Main(string[] args)
        {
            //Base-class pointers:
            //Generalisation (Upcast - приведение дочернего объекта к базовому типу)
            Human[] group =
            {
                new Student("Pinkman", "Jessie", 22, "Chemistry", "WW_220", 90, 95),
                new Teacher("White", "Walter", 50, "Chemistry", 25),
                new Graduate("Schreder", "Hank", 40,"Criminalistic", "OBN", 50, 60, "How to catch Heisenberg"),
                new Student("Vercetty", "Tommy", 30, "Theft", "Vice", 98, 99),
                new Teacher("Diaz", "Ricardo", 50, "Weapons distribution", 20),
                new Teacher("Schwazenegger", "Arnold", 78, "Heavy Metal", 65)
            };

            Console.WriteLine(delimiter);
            //Specialisation:
            for (int i = 0; i < group.Length; i++)
            {
                //group[i].Info();
                Console.WriteLine(group[i].ToString());
                Console.WriteLine(delimiter);
            }

            // Запись группы в файл
            SaveGroupToFile(group, fileName);
            Console.WriteLine("Группа сохранена в файл: " + fileName);
            Console.WriteLine(delimiter);

            // Загрузка группы из файла
            Human[] loadedGroup = LoadGroupFromFile(fileName);
            Console.WriteLine("Группа загружена из файла:");
            Console.WriteLine(delimiter);

            for (int i = 0; i < loadedGroup.Length; i++)
            {
                Console.WriteLine(loadedGroup[i].ToString());
                Console.WriteLine(delimiter);
            }
        }
        // Метод для загрузки группы из файла
        static void SaveGroupToFile(Human[] group, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Human person in group)
                {
                    // Сохраняем тип и данные через разделитель
                    if (person is Student student)
                    {
                        if (person is Graduate graduate)
                        {
                            writer.WriteLine($"Graduate|{graduate.LastName}|{graduate.FirstName}|{graduate.Age}|" +
                                           $"{graduate.Speciality}|{graduate.Group}|{graduate.Rating}|{graduate.Attendance}|{graduate.Subject}");
                        }
                        else
                        {
                            writer.WriteLine($"Student|{student.LastName}|{student.FirstName}|{student.Age}|" +
                                           $"{student.Speciality}|{student.Group}|{student.Rating}|{student.Attendance}");
                        }
                    }
                    else if (person is Teacher teacher)
                    {
                        writer.WriteLine($"Teacher|{teacher.LastName}|{teacher.FirstName}|{teacher.Age}|" +
                                       $"{teacher.Speciality}|{teacher.Experience}");
                    }
                }
            }
        }

        // Метод для загрузки группы из файла (улучшенная версия)
        static Human[] LoadGroupFromFile(string filename)
        {
            List<Human> group = new List<Human>();

            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    Human person = ParseHumanFromDelimitedString(line);
                    if (person != null)
                    {
                        group.Add(person);
                    }
                }
            }

            return group.ToArray();
        }

        // Упрощенный парсинг с разделителями
        static Human ParseHumanFromDelimitedString(string data)
        {
            try
            {
                string[] parts = data.Split('|');
                if (parts.Length < 2) return null;

                string type = parts[0];

                switch (type)
                {
                    case "Student":
                        if (parts.Length >= 8)
                        {
                            return new Student(parts[1], parts[2], int.Parse(parts[3]),
                                             parts[4], parts[5], double.Parse(parts[6]), double.Parse(parts[7]));
                        }
                        break;

                    case "Teacher":
                        if (parts.Length >= 6)
                        {
                            return new Teacher(parts[1], parts[2], int.Parse(parts[3]),
                                             parts[4], int.Parse(parts[5]));
                        }
                        break;

                    case "Graduate":
                        if (parts.Length >= 9)
                        {
                            return new Graduate(parts[1], parts[2], int.Parse(parts[3]),
                                              parts[4], parts[5], double.Parse(parts[6]),
                                              double.Parse(parts[7]), parts[8]);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при парсинге: {ex.Message}");
            }

            return null;
        }
    }
}