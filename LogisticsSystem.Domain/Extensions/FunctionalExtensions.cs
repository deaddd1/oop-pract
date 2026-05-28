using System;
using System.Collections.Generic;

namespace LogisticsSystem.Domain.Extensions;

public static class FunctionalExtensions
{
    // Аналог ForEach на базі Action
    public static void CustomForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source) action(item);
    }

    // Аналог Map (Select) на базі Func
    public static IEnumerable<R> CustomMap<T, R>(this IEnumerable<T> source, Func<T, R> transform)
    {
        foreach (var item in source) yield return transform(item);
    }

    // Аналог Reduce (Aggregate) на базі Func
    public static TResult CustomReduce<T, TResult>(this IEnumerable<T> source, TResult seed, Func<TResult, T, TResult> accumulator)
    {
        TResult result = seed;
        foreach (var item in source)
        {
            result = accumulator(result, item);
        }
        return result;
    }
}