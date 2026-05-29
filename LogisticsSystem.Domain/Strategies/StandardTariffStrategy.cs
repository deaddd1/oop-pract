namespace LogisticsSystem.Domain.Strategies;

public class StandardTariffStrategy : Contracts.IFinancialStrategy
{
    public double CalculateRevenue(double distanceKm, double cargoPrice) 
        => (distanceKm * 55.0) + (cargoPrice * 0.01); // 55 грн/км + 1% страховки
}

public class ExpressTariffStrategy : Contracts.IFinancialStrategy
{
    public double CalculateRevenue(double distanceKm, double cargoPrice) 
        => (distanceKm * 85.0) + (cargoPrice * 0.02); // 85 грн/км + 2% за швидкість
}