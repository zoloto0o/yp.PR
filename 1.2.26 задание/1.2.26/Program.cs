using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите координату клетки (например, a1): ");
        string input = Console.ReadLine().ToLower();

        if (input.Length != 2 ||
            input[0] < 'a' || input[0] > 'h' ||
            input[1] < '1' || input[1] > '8')
        {
            Console.WriteLine("Неверный формат ввода. Введите от a1 до h8.");
            return;
        }

        char columnChar = input[0];
        int row = int.Parse(input[1].ToString());
        int column = columnChar - 'a' + 1;

        bool isBlack = (column + row) % 2 == 0;

        string color = isBlack ? "Чёрная" : "Белая";
        Console.WriteLine($"Клетка {input} — {color}");
    }
}
