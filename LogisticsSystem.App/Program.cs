using System;
using System.Collections.Generic;
using System.Linq;
using LogisticsSystem.Domain.Entities;
using LogisticsSystem.Domain.Repositories;
using LogisticsSystem.Domain.Extensions;
using LogisticsSystem.Domain.Exceptions;
using LogisticsSystem.Domain.Utils;

namespace LogisticsSystem.App;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== Розділ ІІ. Структури даних, LINQ та Обробка помилок ===");

        // 1. Тест Generics & Сховища (ПЗ 5)
        var cargoRepository = new Repository<Cargo>();
        cargoRepository.Add(new Cargo("Обладнання", 1200, 5));
        cargoRepository.Add(new Cargo("Текстиль", 200, 2));
        cargoRepository.Add(new Cargo("Будівельні матеріали", 3500, 10));

        Console.WriteLine($"\n[Generics] У сховище додано вантажів: {cargoRepository.GetAll().Count()}");

        // 2. Тест Делегатів Custom ForEach, Map, Reduce (СР 5)
        var cargos = cargoRepository.GetAll();
        Console.WriteLine("\n[Делегати] Список вантажів через CustomForEach:");
        cargos.CustomForEach(c => Console.WriteLine($" - {c.Description}: {c.WeightKg} кг"));

        double totalWeight = cargos.CustomMap(c => c.WeightKg).CustomReduce(0.0, (sum, w) => sum + w);
        Console.WriteLine($"[Делегати] Загальна вага через Map/Reduce: {totalWeight} кг");

        // 3. Тест LINQ та Extension Methods (ПЗ 7 & СР 7)
        var heavyCargos = cargos.GetHeavyCargo(1000);
        Console.WriteLine("\n[LINQ Extension] Вантажі важче 1000 кг:");
        foreach (var hc in heavyCargos)
        {
            Console.WriteLine($" - {hc.Description} ({hc.WeightKg} кг)");
        }

        // 4. Тест Custom Exception & Retry Policy з експоненційною затримкою (ПЗ 8 & СР 8)
        Console.WriteLine("\n[Retry Policy] Симуляція запиту до GPS-навігатора через шторм...");
        int callCount = 0;
        
        try
        {
            string finalRoute = RetryPolicy.Execute(() =>
            {
                callCount++;
                if (callCount < 3) 
                    throw new RouteNotFoundException("Київ-Чоп"); // Падає перші 2 рази
                
                return "Маршрут Київ-Чоп успішно побудовано в об'їзд!";
            });
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[Успіх] Навігатор відповів: {finalRoute}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Катастрофа] Операція остаточно провалена: {ex.Message}");
        }

        PrintPerformanceTable();
        Console.ReadLine();
    }

    // Теоретичне обґрунтування для СР 6
    static void PrintPerformanceTable()
    {
        Console.WriteLine("\n📊 СР 6. Обґрунтування вибору структур даних (.NET Collections):");
        Console.WriteLine("| Колекція          | Пошук (ID) | Вставка    | Коли використовувати?                     |");
        Console.WriteLine("|-------------------|------------|------------|-------------------------------------------|");
        Console.WriteLine("| List<T>           | O(n)       | O(1)       | Для простих переліків та ітерацій        |");
        Console.WriteLine("| Dictionary<K, V>  | O(1)       | O(1)       | Для швидкого доступу до сутностей за Key |");
        Console.WriteLine("| HashSet<T>        | O(1)       | O(1)       | Для унікальних записів (напр. унікальні ID)|");
    }
}
