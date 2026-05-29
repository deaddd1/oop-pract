using LogisticsSystem.Domain.Contracts;

namespace LogisticsSystem.Domain.Services;

public class StandardTariffStrategy : IFinancialStrategy
{
    public double CalculateRevenue(double distanceKm, double cargoPrice) => (distanceKm * 55.0) + (cargoPrice * 0.01);
}

public class ExpressTariffStrategy : IFinancialStrategy
{
    public double CalculateRevenue(double distanceKm, double cargoPrice) => (distanceKm * 85.0) + (cargoPrice * 0.02);
}