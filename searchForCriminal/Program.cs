using System;
using System.Collections.Generic;
using System.Linq;

namespace searchForCriminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataBaseCriminal dataBaseCriminal = new DataBaseCriminal();

            dataBaseCriminal.Start();
        }
    }
}

class DataBaseCriminal
{
    private List<Criminal> _criminals = new List<Criminal>();

    public DataBaseCriminal()
    {
        _criminals.Add(new Criminal("Петров Иван Василиевич", "Русский", 179, 92, false));
        _criminals.Add(new Criminal("Аликбеков Нурсултан Магомедович", "Дагестанец", 184, 78, true));
        _criminals.Add(new Criminal("Старикевич Тимур Константинович", "Русский", 188, 110, false));
        _criminals.Add(new Criminal("Рогозин Олег петрович", "Русский", 181, 70, false));
        _criminals.Add(new Criminal("Жусупов Карэн Ибрагирмович", "Дагестанец", 175, 64, false));
        _criminals.Add(new Criminal("Лукашенко Алектандр Батькович", "Беларус", 169, 108, true));
        _criminals.Add(new Criminal("Краснов Антон Геннадиевич", "Русский", 160, 54, false));
    }

    public void Start()
    {
        ConsoleKey commandKeyExit = ConsoleKey.Escape;
        ConsoleKey userInput;
        bool isWork = true;

        while (isWork)
        {
            Console.Clear();

            SearchCriminals();

            Console.WriteLine($"Нажмите чтобы - продолжить / Выйти: любая клавиша / {commandKeyExit}");

            userInput = Console.ReadKey().Key;

            if (userInput == commandKeyExit)
            {
                isWork = false;
            }
        }
    }

    private void SearchCriminals()
    {
        Console.Write("Введите рост: ");

        int growth = UserUtils.GetNumber();

        Console.Write("\nВведите вес: ");

        int weight = UserUtils.GetNumber();

        Console.Write("\nВведите национальность: ");

        string nationality = Console.ReadLine();

        Console.Clear();

        var filteredCriminals = _criminals.Where(criminal => criminal.Growth == growth && criminal.Weight == weight && criminal.Nationality.ToLower() == nationality.ToLower() && criminal._isInCustody == false);

        if (filteredCriminals.Count() == 0)
        {
            Console.WriteLine("Преступников по заданым параметрам не найдено...");
        }
        else
        {
            foreach (var criminal in filteredCriminals)
            {
                criminal.ShowInfo();
            }
        }
    }
}

class Criminal
{
    private string _fullName;
    private string _status;

    public Criminal(string fullName, string nationality, int growth, int weight, bool isInCustody)
    {
        _fullName = fullName;
        Nationality = nationality;
        Growth = growth;
        Weight = weight;
        _isInCustody = isInCustody;

        if (_isInCustody)
        {
            _status = "Заключенный";
        }
        else
        {
            _status = "В розыске";
        }
    }

    public string Nationality { get; private set; }

    public int Growth { get; private set; }

    public int Weight { get; private set; }

    public bool _isInCustody { get; private set; }

    public void ShowInfo()
    {
        Console.WriteLine($"Полное имя: {_fullName}\nНациональность: {Nationality}\nРост: {Growth}\nВес: {Weight}\nСтатус: {_status}\n");
    }
}

static class UserUtils
{
    public static int GetNumber()
    {
        bool isNumberWork = true;
        int userNumber = 0;

        while (isNumberWork)
        {
            bool isNumber;
            string userInput = Console.ReadLine();

            if (isNumber = int.TryParse(userInput, out int number))
            {
                userNumber = number;
                isNumberWork = false;
            }
            else
            {
                Console.WriteLine($"Не правильный ввод данных!!!  Повторите попытку");
            }
        }
        return userNumber;
    }
}