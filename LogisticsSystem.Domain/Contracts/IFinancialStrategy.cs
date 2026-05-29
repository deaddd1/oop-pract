//*Strategy метод
namespace LogisticsSystem.Domain.Contracts;

public interface IFinancialStrategy
{
    double CalculateRevenue(double distanceKm, double cargoPrice);
}