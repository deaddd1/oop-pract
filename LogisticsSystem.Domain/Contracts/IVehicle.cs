using LogisticsSystem.Domain.Entities;

namespace LogisticsSystem.Domain.Contracts;

public interface IVehicle : IEntity, IDisposable
{
    // Рядок string Id { get; } видалено, бо він уже є в IEntity
    double MaxCapacityKg { get; }
    double CurrentLoadKg { get; }
    bool IsTrackingActive { get; }
    
    void LoadCargo(Cargo cargo);
    void UnloadAll();
    void Drive(Route route);
}