using System;
using LogisticsSystem.Domain.Contracts;

namespace LogisticsSystem.Domain.Entities;

public class Cargo : ICargoComponent
{
    // Реалізація інтерфейсу ICargoComponent (та базового IEntity)
    public string Id { get; }
    public string Description { get; }
    public double WeightKg { get; }
    public double VolumeM3 { get; }
    public double Price { get; } // Ціна багажу (Розділ ІІ/ІІІ)

    // Параметризований конструктор з валідацією (Розділ І - Захист інваріантів)
    public Cargo(string description, double weightKg, double volumeM3, double price)
    {
        if (string.IsNullOrWhiteSpace(description)) 
            throw new ArgumentException("Опис вантажу не може бути порожнім.");
        if (weightKg <= 0) 
            throw new ArgumentException("Вага вантажу повинна бути більшою за 0 кг.");
        if (volumeM3 <= 0) 
            throw new ArgumentException("Об'єм вантажу повинен бути більшим за 0 м³.");
        if (price < 0) 
            throw new ArgumentException("Оголошена вартість вантажу не може бути від'ємною.");

        Id = Guid.NewGuid().ToString().Substring(0, 8);
        Description = description;
        WeightKg = weightKg;
        VolumeM3 = volumeM3;
        Price = price;
    }

    // Конструктор копіювання (Розділ І - Життєвий цикл об'єктів)
    public Cargo(Cargo other)
    {
        if (other == null) 
            throw new ArgumentNullException(nameof(other));

        Id = Guid.NewGuid().ToString().Substring(0, 8);
        Description = other.Description + " (Копія)";
        WeightKg = other.WeightKg;
        VolumeM3 = other.VolumeM3;
        Price = other.Price;
    }

    // Перевантаження оператора "+" для злиття вантажів (Розділ І - Агрегація даних)
    public static Cargo operator +(Cargo left, Cargo right)
    {
        if (left == null || right == null) 
            throw new ArgumentNullException("Вантажі для об'єднання не можуть бути null.");

        return new Cargo(
            $"{left.Description} + {right.Description}", 
            left.WeightKg + right.WeightKg, 
            left.VolumeM3 + right.VolumeM3, 
            left.Price + right.Price
        );
    }
}