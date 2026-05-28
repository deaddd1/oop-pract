using LogisticsSystem.Domain.Base;

namespace LogisticsSystem.Domain.Entities;

public class Truck : VehicleBase
{
    public int AxlesCount { get; }

    public Truck(string id, double maxCapacityKg, int axlesCount) : base(id, maxCapacityKg)
    {
        if (axlesCount < 2) throw new ArgumentException("Вантажівка повинна мати мінімум 2 осі.");
        AxlesCount = axlesCount;
    }

    public override void Drive(Route route)
    {
        base.Drive(route);
        Console.WriteLine($"[Truck] Вантажівка з {AxlesCount} осями проходить ваговий контроль.");
    }
}