namespace LogisticsSystem.Domain.Contracts;

public interface ICargoComponent : LogisticsSystem.Domain.Contracts.IEntity
{
    string Description { get; }
    double WeightKg { get; }
    double VolumeM3 { get; }
    double Price { get; }
}
