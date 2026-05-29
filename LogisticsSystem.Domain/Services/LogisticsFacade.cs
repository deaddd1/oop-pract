using System;
using LogisticsSystem.Domain.Entities;
using LogisticsSystem.Domain.Contracts;

namespace LogisticsSystem.Domain.Services;

public class LogisticsFacade
{
    public FinancialReport RunLogisticsCalculation(
        string vehicleType, string vehicleId, double maxCap, ICargoComponent cargo,
        string start, string end, double distance, double hours,
        IFinancialStrategy strategy, double driverRate, double opFee,
        EventHandler<string> riskHandler)
    {
        // Створення авто через Фабрику-Сінглтон (ПЗ 10)
        Truck truck = Factories.VehicleFactory.Instance.CreateVehicle(vehicleType, vehicleId, maxCap);
        
        // Перетворюємо інтерфейсний вантаж у конкретний для завантаження в авто
        var deliveryCargo = new Cargo(cargo.Description, cargo.WeightKg, cargo.VolumeM3, cargo.Price);
        truck.LoadCargo(deliveryCargo);

        Route route = new Route(new[] { start, end }, distance);

        FinancialService finService = new FinancialService(strategy);
        finService.OnUnprofitableRouteDetected += riskHandler; // Налаштування Observer (ПЗ 11)
        
        FinancialReport report = finService.GenerateReport(truck, route, hours, driverRate, opFee);
        
        finService.OnUnprofitableRouteDetected -= riskHandler; // Захист від Memory Leak

        return report;
    }
}