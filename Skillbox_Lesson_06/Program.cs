using System;
using System.IO;
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
                            ReadData();
                            return;
                        case 2:
                            AddEmployee();
                            return;
                    }

                    Console.Clear();
                    Console.WriteLine("Ошибка!\n" +
                                "Введите:\n\t1 - вывести список сотрудников;\n" +
                                "\t2 - добавить сотрудника\n");
                }
            }
        }

        private static void ReadData()
        {
            Console.Clear();
            Console.WriteLine("СПИСОК СОТРУДНИКОВ:\n");

            StringBuilder builder = new StringBuilder();
            int strCount = 0;
            if (File.Exists(EmployeesDataPath))
            {
                using (StreamReader reader = new StreamReader(EmployeesDataPath))
                {
                    while (!reader.EndOfStream)
                    {
                        builder.AppendLine(reader.ReadLine());
                        strCount++;
                    }
                }

                string[] employeesData = builder.ToString().Split('\n');

                for (int i = 0; i < employeesData.Length - 1; i++)
                {
                    string data = employeesData[i].Replace('#', '|');
                    Console.WriteLine(data);
                }
            }
            else { Console.WriteLine("Ошибка! Файла не существует"); }
            Console.ReadKey();
        }

        private static void AddEmployee()
        {
            Console.Clear();
            Console.WriteLine("ДОБАВЛЕНИЕ СОТРУДНИКА:\n");
            using (StreamWriter writer = new StreamWriter(EmployeesDataPath, true))
            {
                writer.WriteLine();

                Console.Write("Введите ID сотрудника: ");
                writer.Write(Console.ReadLine() + ".#" + DateTime.Now.ToString() + "#");


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
    }
}