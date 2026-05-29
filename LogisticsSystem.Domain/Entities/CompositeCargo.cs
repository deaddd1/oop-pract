using System;
using System.Collections.Generic;
using System.Linq;
using LogisticsSystem.Domain.Contracts;

namespace LogisticsSystem.Domain.Entities;

public class CompositeCargo : ICargoComponent
{
    public string Id { get; } = Guid.NewGuid().ToString().Substring(0, 8);
    public string Description { get; }
    
    private readonly List<ICargoComponent> _components = new();

    public CompositeCargo(string description)
    {
        Description = description;
    }

    public void Add(ICargoComponent component) => _components.Add(component);
    public void Remove(ICargoComponent component) => _components.Remove(component);

    // Патерн Composite делегує підрахунок своїм дітям
    public double WeightKg => _components.Sum(c => c.WeightKg);
    public double VolumeM3 => _components.Sum(c => c.VolumeM3);
    public double Price => _components.Sum(c => c.Price);
}