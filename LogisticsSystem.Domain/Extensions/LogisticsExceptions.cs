using System;

namespace LogisticsSystem.Domain.Exceptions;

public class LogisticsException : Exception
{
    public LogisticsException(string message) : base(message) {}
}

public class RouteNotFoundException : LogisticsException
{
    public RouteNotFoundException(string routeName) : base($"Маршрут '{routeName}' не знайдено або він заблокований через негоду.") {}
}