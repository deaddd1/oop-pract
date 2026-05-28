using LogisticsSystem.Domain.Contracts;
using LogisticsSystem.Domain.Entities;

namespace LogisticsSystem.Domain.Base;

public abstract class VehicleBase : IVehicle
{
    private bool _disposed = false;

    public string Id { get; }
    public double MaxCapacityKg { get; }
    public double CurrentLoadKg { get; private set; }
    public bool IsTrackingActive { get; private set; }

    protected VehicleBase(string id, double maxCapacityKg)
    {
        if (maxCapacityKg <= 0) throw new ArgumentException("Вантажопідйомність має бути більшою за 0.");
        Id = id;
        MaxCapacityKg = maxCapacityKg;
        IsTrackingActive = true; 
    }

    public void LoadCargo(Cargo cargo)
    {
        if (cargo == null) throw new ArgumentNullException(nameof(cargo));
        
        if (CurrentLoadKg + cargo.WeightKg > MaxCapacityKg)
            throw new InvalidOperationException($"Перевантаження! Неможливо завантажити вантаж вагою {cargo.WeightKg} кг.");

        CurrentLoadKg += cargo.WeightKg;
    }

    public void UnloadAll() => CurrentLoadKg = 0;

    public virtual void Drive(Route route)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(VehicleBase));
        Console.WriteLine($"Транспорт {Id} вирушив за маршрутом: {route.StartPoint} -> {route.EndPoint}.");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                IsTrackingActive = false;
                Console.WriteLine($"[Ресурси] GPS-трекер для авто {Id} успішно деактивовано.");
            }
            _disposed = true;
        }
    }

    ~VehicleBase() => Dispose(false);
}