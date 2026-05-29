using System;
using LogisticsSystem.Domain.Contracts;

namespace LogisticsSystem.Domain.Entities;

public class Truck : IEntity
{
    // Реалізація інтерфейсу IEntity
    public string Id { get; }
    
    // Максимальна вантажопідйомність авто
    public double MaxCapacityKg { get; }
    
    // Поточна вага вантажу в машині
    public double CurrentLoadKg { get; private set; }

    public Truck(string id, double maxCapacityKg)
    {
        if (string.IsNullOrWhiteSpace(id)) 
            throw new ArgumentException("Номерний знак автомобіля не може бути порожнім.");
        if (maxCapacityKg <= 0) 
            throw new ArgumentException("Вантажопідйомність має бути більшою за 0 кг.");

        Id = id;
        MaxCapacityKg = maxCapacityKg;
        CurrentLoadKg = 0; // Нова машина приїжджає порожньою
    }

    // Метод для завантаження посилок в авто (ПЗ 2 - Інкапсуляція та захист інваріантів)
    public void LoadCargo(Cargo cargo)
    {
        if (cargo == null) 
            throw new ArgumentNullException(nameof(cargo));
        
        if (CurrentLoadKg + cargo.WeightKg > MaxCapacityKg)
            throw new InvalidOperationException($"Перевантаження автомобіля! Неможливо завантажити {cargo.Description}.");

        CurrentLoadKg += cargo.WeightKg;
    }

    // Математика розрахунку витрати пального (Розділ ІІ)
    // Базова витрата порожньої фури — 25л на 100км. Кожні 100кг вантажу додають 0.5л палива.
    public double CalculateFuelConsumptionPer100Km()
    {
        double baseConsumption = 25.0; 
        double extraConsumption = (CurrentLoadKg / 100.0) * 0.5; 
        return baseConsumption + extraConsumption;
    }
}