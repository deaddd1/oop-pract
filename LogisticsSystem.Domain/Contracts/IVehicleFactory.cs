using LogisticsSystem.Domain.Entities;

namespace LogisticsSystem.Domain.Contracts;

public interface IVehicleFactory
{
    Truck CreateVehicle(string type, string id, double maxCapacity);
}