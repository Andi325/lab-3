using System;
using System.Collections.Generic;
using System.Linq;

class Товар
{
    public string Назва { get; set; }
    public decimal Ціна { get; set; }
    public string Опис { get; set; }
    public string Категорія { get; set; }
}

class Користувач
{
    public string Логін { get; set; }
    public string Пароль { get; set; }
    public List<Товар> ІсторіяПокупок { get; set; } = new List<Товар>();
}

class Замовлення
{
    public List<Товар> Товари { get; set; } = new List<Товар>();
    public int Кількість { get; set; }
    public decimal ЗагальнаВартість => Товари.Sum(товар => товар.Ціна * Кількість);
    public string Статус { get; set; }
}

interface ISearchable
{
    List<Товар> ПошукЗаКритеріями(decimal мінімальнаЦіна, decimal максимальнаЦіна, string категорія);
}

class Магазин : ISearchable
{
    public List<Товар> Товари { get; set; } = new List<Товар>();
    public List<Користувач> Користувачі { get; set; } = new List<Користувач>();
    public List<Замовлення> Замовлення { get; set; } = new List<Замовлення>();

    public List<Товар> ПошукЗаКритеріями(decimal мінімальнаЦіна, decimal максимальнаЦіна, string категорія)
    {
        return Товари
            .Where(товар => товар.Ціна >= мінімальнаЦіна && товар.Ціна <= максимальнаЦіна && товар.Категорія == категорія)
            .ToList();
    }

    
}

class Program
{
    static void Main()
    {
        Магазин магазин = new Магазин();

        
        магазин.Товари.Add(new Товар { Назва = "Продукт 1", Ціна = 10.50m, Категорія = "Категорія 1" });
        магазин.Товари.Add(new Товар { Назва = "Продукт 2", Ціна = 15.75m, Категорія = "Категорія 2" });
        магазин.Користувачі.Add(new Користувач { Логін = "user1", Пароль = "password1" });
        Замовлення замовлення = new Замовлення();
        замовлення.Товари.Add(магазин.Товари[0]);
        замовлення.Кількість = 2;

        магазин.Замовлення.Add(замовлення);
        
        decimal мінімальнаЦіна = 10;
        decimal максимальнаЦіна = 20;
        string категорія = "Категорія 1";

        List<Товар> знайденіТовари = магазин.ПошукЗаКритеріями(мінімальнаЦіна, максимальнаЦіна, категорія);

        Console.WriteLine($"Знайдені товари у категорії '{категорія}' з ціною від {мінімальнаЦіна} до {максимальнаЦіна}:");
        foreach (Товар товар in знайденіТовари)
        {
            Console.WriteLine($"Назва: {товар.Назва}, Ціна: {товар.Ціна}, Категорія: {товар.Категорія}");
        }
    }
}