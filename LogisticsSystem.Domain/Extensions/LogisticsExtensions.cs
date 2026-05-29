using System.Collections.Generic;
using System.Linq;
using LogisticsSystem.Domain.Entities;

namespace LogisticsSystem.Domain.Extensions;

public static class LogisticsExtensions
{
    // Швидка фільтрація важких вантажів
    public static IEnumerable<Cargo> GetHeavyCargo(this IEnumerable<Cargo> cargos, double minWeight)
    {
        return cargos.Where(c => c.WeightKg >= minWeight);
    }

    // ВИПРАВЛЕНО: Тепер групуємо об'єкти Truck замість старого класу VehicleBase
    public static IEnumerable<IGrouping<bool, Truck>> GroupVehiclesByLoad(this IEnumerable<Truck> vehicles)
    {
        return from v in vehicles
               group v by v.CurrentLoadKg > 0;
    }
}