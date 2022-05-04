using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Skillbox_Lesson_06
{
    internal class Program
    {
        private const string EmployeesDataPath = "EmployeesData.txt";

        private static void Main()
        {
            ProgramStart();
        }

        private static void ProgramStart()
        {
            Console.WriteLine("Здравствуйте, выберите операцию:\n");
            Console.WriteLine("1 - вывести список сотрудников.\n" +
                "2 - добавить сотрудника.\n");

            while (true)
            {
                string input = Console.ReadLine();
                if (byte.TryParse(input, out byte numOperation))
                {

                    switch (numOperation)
                    {
                        case 1:
                            ReadData(EmployeesDataPath);
                            return;
                        case 2:
                            AddEmployee(EmployeesDataPath);
                            return;
                    }

                    Console.Clear();
                    Console.WriteLine("Ошибка!\n" +
                                "Введите:\n\t1 - вывести список сотрудников;\n" +
                                "\t2 - добавить сотрудника\n");
                }
            }
        }

        private static void ReadData(string path)
        {
            Console.Clear();
            Console.WriteLine("СПИСОК СОТРУДНИКОВ:\n");

            string[] employeesData = GetSplittedFile(path);
            if (employeesData != null)
            {
                for (int i = 0; i < employeesData.Length - 1; i++)
                {
                    string data = employeesData[i].Replace('#', '|');
                    Console.WriteLine(data);
                }
            }
            else { Console.WriteLine("Ошибка! Файла не существует"); }
            Console.ReadKey();
        }

        private static void AddEmployee(string path)
        {
            Console.Clear();
            Console.WriteLine("ДОБАВЛЕНИЕ СОТРУДНИКА:\n");

            int newId = CheckId(path);

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine();

                writer.Write(newId + ".#" + DateTime.Now.ToString() + "#");

                Console.Write("Введите ФИО нового сотрудника: ");
                writer.Write(Console.ReadLine() + "#");

                Console.Write("Введите возраст: ");
                writer.Write(Console.ReadLine() + "#");

                Console.Write("Введите рост: ");
                writer.Write(Console.ReadLine() + "#");

                Console.Write("Введите дату рождения: ");
                writer.Write(Console.ReadLine() + "#");

                Console.Write("Введите город: ");
                writer.Write("город " + Console.ReadLine());
            }
        }

        private static int CheckId(string path)
        {
            string[] employeesData = GetSplittedFile(path);

            int[] ids = new int[employeesData.Length - 1];
            for (int i = 0; i < ids.Length; i++)
            {
                ids[i] = int.Parse(employeesData[i].Substring(0, employeesData[i].IndexOf('.')));
            }

            return ids.Max() + 1;
        }

        private static string[] GetSplittedFile(string path)
        {
            StringBuilder builder = new StringBuilder();
            string[] employeesData = null;

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        builder.AppendLine(reader.ReadLine());
                    }
                }

                employeesData = builder.ToString().Split('\n');
            }

            return employeesData;
        }
    }
}