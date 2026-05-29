using System;
using LogisticsSystem.Domain.Contracts;
using LogisticsSystem.Domain.Entities;

namespace LogisticsSystem.Domain.Services;

// Модель результату для збереження SRP
public record FinancialReport(double Revenue, double FuelCost, double DriverSalary, double OperatorFee, double NetProfit, bool IsProfitable);

public class FinancialService
{
    // Патерн Observer: подія, яка спрацює у разі фінансових ризиків
    public event EventHandler<string>? OnUnprofitableRouteDetected;

    private readonly IFinancialStrategy _tariffStrategy;
    private const double FUEL_PRICE = 90.0;

    // DIP: Залежимо від абстракції стратегії, а не від реалізації
    public FinancialService(IFinancialStrategy tariffStrategy)
    {
        _tariffStrategy = tariffStrategy;
    }

    public FinancialReport GenerateReport(Truck truck, Route route, double hours, double hourlyRate, double operatorFee)
    {
        double revenue = _tariffStrategy.CalculateRevenue(route.DistanceKm, truck.CurrentLoadKg * 2); // умовна оцінка доходу
        
        double consumption = truck.CalculateFuelConsumptionPer100Km();
        double fuelCost = (route.DistanceKm / 100.0) * consumption * FUEL_PRICE;
        double driverSalary = hours * hourlyRate;
        
        double totalExpenses = fuelCost + driverSalary + operatorFee;
        double netProfit = revenue - totalExpenses;
        bool isProfitable = netProfit > 0;

        if (!isProfitable)
        {
            // Генеруємо сповіщення підписникам (Observer)
            OnUnprofitableRouteDetected?.Invoke(this, $"УВАГА! Маршрут {route.StartPoint}->{route.EndPoint} збитковий на {Math.Abs(netProfit):F2} грн!");
        }

        return new FinancialReport(revenue, fuelCost, driverSalary, operatorFee, netProfit, isProfitable);
    }
}