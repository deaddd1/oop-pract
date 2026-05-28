using Xunit;
using System;
using LogisticsSystem.Domain.Entities;
using LogisticsSystem.Domain.Extensions;
using LogisticsSystem.Domain.Utils;
using LogisticsSystem.Domain.Exceptions;

namespace LogisticsSystem.Tests;

public class Section2Tests
{
    [Fact]
    public void CustomMapReduce_ShouldCalculateCorrectSum()
    {
        // Arrange
        var list = new[] { new Cargo("А", 100, 1), new Cargo("Б", 200, 2) };

        // Act
        var weights = list.CustomMap(c => c.WeightKg);
        double total = weights.CustomReduce(0.0, (acc, x) => acc + x);

        // Assert
        Assert.Equal(300, total);
    }

    [Fact]
    public void RetryPolicy_ShouldEventuallySucceed()
    {
        // Arrange
        int attempts = 0;

        // Act
        string result = RetryPolicy.Execute(() =>
        {
            attempts++;
            if (attempts < 2) throw new RouteNotFoundException("Тест");
            return "OK";
        });

        // Assert
        Assert.Equal("OK", result);
        Assert.Equal(2, attempts);
    }
}