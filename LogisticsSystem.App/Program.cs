using System;
using LogisticsSystem.Domain.Entities;
using LogisticsSystem.Domain.Services;
using LogisticsSystem.Domain.Contracts;

namespace LogisticsSystem.App;

class Program
{
    static void Main(string[] args)
    {
        // cd .\LogisticsSystem\; dotnet run --project LogisticsSystem.App
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        LogisticsFacade facade = new LogisticsFacade();

        Console.WriteLine("==================================================================");
        Console.WriteLine("⚡ СИСТЕМА УПРАВЛІННЯ ЛОГІСТИКОЮ: ВСТАНОВЛЕННЯ ПАРАМЕТРІВ ВАНТАЖУ ⚡");
        Console.WriteLine("==================================================================");

        // ВВЕДЕННЯ ДАНИХ ВАНТАЖУ КОРИСТУВАЧЕМ
        Console.WriteLine("\n--- КРОК 1: ДАНІ ВАНТАЖУ ---");
        
        Console.Write("Введіть назву/опис вантажу: ");
        string cargoDescription = Console.ReadLine() ?? "Секретний вантаж";

        Console.Write("Введіть вагу вантажу (кг): ");
        double cargoWeight = double.Parse(Console.ReadLine() ?? "500");

        Console.Write("Введіть об'єм вантажу (м³): ");
        double cargoVolume = double.Parse(Console.ReadLine() ?? "3");

        Console.Write("Введіть оголошену вартість вантажу (грн): ");
        double cargoPrice = double.Parse(Console.ReadLine() ?? "50000");

        // Створюємо вантаж на основі твоїх даних
        Cargo userCargo = new Cargo(cargoDescription, cargoWeight, cargoVolume, cargoPrice);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n📦 Вантаж успішно створено: '{userCargo.Description}' ({userCargo.WeightKg} кг)");
        Console.ResetColor();


        // ВВЕДЕННЯ ДАНИХ РЕЙСУ КОРИСТУВАЧЕМ
        Console.WriteLine("\n--- КРОК 2: НАЛАШТУВАННЯ ПАРАМЕТРІВ РЕЙСУ ---");
        
        Console.Write("Введіть тип авто (truck / van): ");
        string type = Console.ReadLine() ?? "truck";

        Console.Write("Введіть тип тарифу (1 - Стандартний, 2 - Експрес): ");
        string tariffChoice = Console.ReadLine() ?? "1";
        
        // Вибір стратегії тарифу
        IFinancialStrategy strategy = tariffChoice == "2" 
            ? new ExpressTariffStrategy() 
            : new StandardTariffStrategy();

        Console.Write("Введіть відстань рейсу (км): ");
        double distance = double.Parse(Console.ReadLine() ?? "300");

        Console.Write("Введіть час в дорозі (годин): ");
        double hours = double.Parse(Console.ReadLine() ?? "5");

        Console.Write("Погодинна ставка водія (грн/год): ");
        double driverRate = double.Parse(Console.ReadLine() ?? "250");

        Console.Write("Послуги оператора супроводу (грн): ");
        double opFee = double.Parse(Console.ReadLine() ?? "1000");

        // Обробник події ризику (Observer)
        EventHandler<string> alertManager = (sender, msg) => 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n🚨 [ALERT SYSTEM]: {msg}");
            Console.ResetColor();
        };

        // ЗАПУСК РОЗРАХУНКУ
        FinancialReport report = facade.RunLogisticsCalculation(
            type, "BC7777CB", 2000, userCargo,
            "Київ", "Одеса", distance, hours,
            strategy, driverRate, opFee, alertManager
        );

        // ВИВЕДЕННЯ ТАБЛИЦІ (SRP)
        PrintFinancialTable(report);

        Console.WriteLine("\nНатисніть Enter для завершення...");
        Console.ReadLine();
    }

    static void PrintFinancialTable(FinancialReport r)
    {
        Console.WriteLine("\n=====================================================================");
        Console.WriteLine("📊 ФІНАНСОВИЙ ЗВІТ ПО РЕЙСУ");
        Console.WriteLine("=====================================================================");
        Console.WriteLine(string.Format("| {0,-35} | {1,25} |", "Стаття витрат/доходів", "Сума (грн)"));
        Console.WriteLine("---------------------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(string.Format("| {0,-35} | {1,25:N2} |", "💰 Розрахований Фрахт (Дохід)", r.Revenue));
        Console.ResetColor();
        Console.WriteLine(string.Format("| {0,-35} | {1,25:N2} |", "⛽ Паливо", r.FuelCost));
        Console.WriteLine(string.Format("| {0,-35} | {1,25:N2} |", "🧑‍✈️ Зарплата водія", r.DriverSalary));
        Console.WriteLine(string.Format("| {0,-35} | {1,25:N2} |", "🎧 Operator підтримки", r.OperatorFee));
        Console.WriteLine("---------------------------------------------------------------------");

        if (r.IsProfitable)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine(string.Format("| {0,-35} | {1,25:N2} |", " ЧИСТИЙ ПРИБУТОК КОМПАНІЇ", r.NetProfit));
            Console.ResetColor();
            Console.WriteLine("\n📢 РЕЗУЛЬТАТ: Проєкт рентабельний. Запускаємо рейс у роботу.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format("| {0,-35} | {1,25:N2} |", " ЗБИТОК КОМПАНІЇ", r.NetProfit));
            Console.ResetColor();
            Console.WriteLine("\n❌ РЕЗУЛЬТАТ: Заявку відхилено через фінансову недоцільність.");
        }
        Console.WriteLine("=====================================================================");
    }
}