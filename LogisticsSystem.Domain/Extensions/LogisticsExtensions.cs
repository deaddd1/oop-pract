using System.Collections.Generic;
using System.Linq;
using LogisticsSystem.Domain.Entities;

namespace LogisticsSystem.Domain.Extensions;

public static class LogisticsExtensions
{
    // Extension method для швидкої фільтрації важких вантажів
    public static IEnumerable<Cargo> GetHeavyCargo(this IEnumerable<Cargo> cargos, double minWeight)
    {
        return cargos.Where(c => c.WeightKg >= minWeight);
    }

    // ВИПРАВЛЕНО: Замінено 'var' на чіткий тип для групування LINQ
    public static IEnumerable<IGrouping<bool, Base.VehicleBase>> GroupVehiclesByLoad(this IEnumerable<Base.VehicleBase> vehicles)
    {
        return from v in vehicles
               group v by v.CurrentLoadKg > 0;
    }
}