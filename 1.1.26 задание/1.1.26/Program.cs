using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите общее количество учеников в классе: ");
        int totalStudents = int.Parse(Console.ReadLine());

        Console.Write("Насколько девочек больше, чем мальчиков: ");
        int difference = int.Parse(Console.ReadLine());

        if ((totalStudents - difference) % 2 != 0 || (totalStudents - difference) < 0)
        {
            Console.WriteLine("Ошибка: невозможно определить корректное количество мальчиков и девочек.");
        }
        else
        {
            int boys = (totalStudents - difference) / 2;
            int girls = boys + difference;

            Console.WriteLine("\nРезультат:");
            Console.WriteLine("Количество мальчиков: " + boys);
            Console.WriteLine("Количество девочек: " + girls);
        }
    }
}
