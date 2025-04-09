using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        HashSet<int> uniqueNumbers = new HashSet<int>();
        int inputNumber;

        Console.WriteLine("Введите числа (ввод 0 завершает ввод):");

        while (true)
        {
            inputNumber = Convert.ToInt32(Console.ReadLine());

            if (inputNumber == 0)
                break;

            if (uniqueNumbers.Add(inputNumber))
            {
                Console.WriteLine("Число добавлено: " + inputNumber);
            }
            else
            {
                Console.WriteLine("Число уже существует: " + inputNumber);
            }
        }

        Console.WriteLine("Уникальные числа:");
        foreach (var number in uniqueNumbers)
        {
            Console.WriteLine(number);
        }
    }
}
