using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        // Ścieżka do pliku JSON 
        string filePath = "pojazdy.json";

        // Wczytanie danych z pliku
        string jsonData = File.ReadAllText(filePath);
        List<Pojazd> pojazdy = JsonConvert.DeserializeObject<List<Pojazd>>(jsonData);

        // 1. Grupowanie pojazdów według marki i liczenie ich ilości
        var liczbaPojazdówNaMarkę = pojazdy
            .GroupBy(p => p.Marka)
            .Select(g => new { Marka = g.Key, Liczba = g.Count() })
            .OrderByDescending(m => m.Liczba);

        // 2. Pobranie unikalnych marek
        var unikalneMarki = pojazdy
            .Select(p => p.Marka)
            .Distinct()
            .OrderBy(m => m);

        // 3. Pobranie marek, które występują więcej niż raz
        var markiWielePojazdow = liczbaPojazdówNaMarkę
            .Where(m => m.Liczba > 1)
            .Select(m => m.Marka);

        // Wyświetlenie wyników
        Console.WriteLine("Liczba pojazdów dla każdej marki:");
        foreach (var item in liczbaPojazdówNaMarkę)
        {
            Console.WriteLine($"{item.Marka}: {item.Liczba}");
        }

        Console.WriteLine("\nUnikalne marki pojazdów:");
        Console.WriteLine(string.Join(", ", unikalneMarki));

        Console.WriteLine("\nMarki, które występują więcej niż raz:");
        Console.WriteLine(string.Join(", ", markiWielePojazdow));
    }
}

// Klasa reprezentująca pojazd
class Pojazd
{
    public int ID { get; set; }
    public string Marka { get; set; }
    public string Model { get; set; }
    public int Rocznik { get; set; }
    public string VIN { get; set; }
}
