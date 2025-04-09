using System;
using System.Collections.Generic;

class Attraction
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public int VisitCount { get; private set; }

    public Attraction(string name, int capacity, decimal price)
    {
        Name = name;
        Capacity = capacity;
        Price = price;
        VisitCount = 0;
    }

    public void Attend()
    {
        VisitCount++;
    }
}

class Visitor
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> VisitedAttractions { get; private set; }

    public Visitor(string name, int age)
    {
        Name = name;
        Age = age;
        VisitedAttractions = new List<string>();
    }

    public void BuyTicket(Attraction attraction)
    {
        VisitedAttractions.Add(attraction.Name);
        attraction.Attend();
    }
}

class Ticket
{
    public static int Counter = 1;
    public int ID { get; private set; }
    public Attraction Attraction { get; private set; }
    public Visitor Visitor { get; private set; }

    public Ticket(Attraction attraction, Visitor visitor)
    {
        ID = Counter++;
        Attraction = attraction;
        Visitor = visitor;
    }

    public void PrintTicket()
    {
        Console.WriteLine($"Билет #{ID}: {Visitor.Name} → {Attraction.Name} (Цена: {Attraction.Price} руб.)");
    }
}

class Program
{
    static List<Attraction> attractions = new List<Attraction>();
    static List<Visitor> visitors = new List<Visitor>();
    static List<Ticket> tickets = new List<Ticket>();

    static void Main()
    {
        SeedData();

        while (true)
        {
            Console.WriteLine("\n1. Добавить посетителя\n2. Показать аттракционы\n3. Купить билет\n4. Показать билеты\n0. Выход");
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddVisitor();
                    break;
                case "2":
                    ShowAttractions();
                    break;
                case "3":
                    SellTicket();
                    break;
                case "4":
                    ShowTickets();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }
        }
    }

    static void SeedData()
    {
        attractions.Add(new Attraction("Американские горки", 20, 250));
        attractions.Add(new Attraction("Колесо обозрения", 30, 150));
        attractions.Add(new Attraction("Комната страха", 10, 200));
    }

    static void AddVisitor()
    {
        Console.Write("Введите имя: ");
        string name = Console.ReadLine();
        Console.Write("Введите возраст: ");
        int age = int.Parse(Console.ReadLine());

        visitors.Add(new Visitor(name, age));
        Console.WriteLine("Посетитель добавлен.");
    }

    static void ShowAttractions()
    {
        Console.WriteLine("\nСписок аттракционов:");
        for (int i = 0; i < attractions.Count; i++)
        {
            var a = attractions[i];
            Console.WriteLine($"{i + 1}. {a.Name} — Цена: {a.Price} руб., Посещений: {a.VisitCount}");
        }
    }

    static void SellTicket()
    {
        if (visitors.Count == 0)
        {
            Console.WriteLine("Нет зарегистрированных посетителей.");
            return;
        }

        Console.WriteLine("\nВыберите посетителя:");
        for (int i = 0; i < visitors.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {visitors[i].Name} (Возраст: {visitors[i].Age})");
        }

        int visitorIndex = int.Parse(Console.ReadLine()) - 1;
        Visitor visitor = visitors[visitorIndex];

        ShowAttractions();
        Console.Write("Выберите аттракцион: ");
        int attrIndex = int.Parse(Console.ReadLine()) - 1;
        Attraction attraction = attractions[attrIndex];

        visitor.BuyTicket(attraction);
        var ticket = new Ticket(attraction, visitor);
        tickets.Add(ticket);

        Console.WriteLine("Билет куплен:");
        ticket.PrintTicket();
    }

    static void ShowTickets()
    {
        Console.WriteLine("\nСписок билетов:");
        foreach (var ticket in tickets)
        {
            ticket.PrintTicket();
        }
    }
}
