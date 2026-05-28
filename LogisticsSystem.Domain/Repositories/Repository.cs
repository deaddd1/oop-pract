using System.Collections.Generic;
using LogisticsSystem.Domain.Contracts;

namespace LogisticsSystem.Domain.Repositories;

public class Repository<T> where T : class, IEntity
{
    // Оптимальний вибір для пошуку по Id за O(1)
    private readonly Dictionary<string, T> _storage = new();

    public void Add(T entity) => _storage[entity.Id] = entity;

    public T? GetById(string id) => _storage.TryGetValue(id, out var entity) ? entity : null;

    public IEnumerable<T> GetAll() => _storage.Values;
}