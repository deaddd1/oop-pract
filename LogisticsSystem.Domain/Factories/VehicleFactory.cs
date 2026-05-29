using System;
using LogisticsSystem.Domain.Entities;
using LogisticsSystem.Domain.Contracts;

namespace LogisticsSystem.Domain.Factories;

public class VehicleFactory : IVehicleFactory
{
    private static VehicleFactory? _instance;
    private static readonly object _lock = new();

    private VehicleFactory() {}

    public static VehicleFactory Instance
    {
        get
        {
            lock (_lock)
            {
                return _instance ??= new VehicleFactory();
            }
        }
    }

    public Truck CreateVehicle(string type, string id, double maxCapacity)
    {
        return type.ToLower() switch
        {
            "truck" => new Truck(id, maxCapacity),
            "van" => new Truck(id, maxCapacity * 0.5),
            _ => throw new ArgumentException($"Невідомий тип транспорту: {type}")
        };
    }
}