using System;
using System.Collections.Generic;
using System.Linq;

namespace LogisticsSystem.Domain.Entities;

public class Route
{
    private readonly List<string> _stops = new();
    public double DistanceKm { get; } // НОВА ЗМІННА: Скільки км їхати

    public string StartPoint => _stops.Count > 0 ? _stops[0] : throw new InvalidOperationException("Маршрут порожній.");
    public string EndPoint => _stops.Count > 1 ? _stops[^1] : throw new InvalidOperationException("Маршрут не має кінцевої точки.");

    public Route(IEnumerable<string> stops, double distanceKm)
    {
        if (stops == null || stops.Count() < 2)
            throw new ArgumentException("Маршрут повинен містити мінімум 2 точки.");
        if (distanceKm <= 0) 
            throw new ArgumentException("Відстань має бути більшою за 0 км.");

        _stops.AddRange(stops);
        DistanceKm = distanceKm;
    }

    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= _stops.Count) throw new IndexOutOfRangeException("Зупинки не існує.");
            return _stops[index];
        }
    }
}