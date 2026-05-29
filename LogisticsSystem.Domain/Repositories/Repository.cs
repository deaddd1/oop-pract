using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LogisticsSystem.Domain.Contracts;

namespace LogisticsSystem.Domain.Repositories;

public class Repository<T> where T : class, LogisticsSystem.Domain.Contracts.IEntity
{
    private Dictionary<string, T> _storage = new();
    private readonly string _filePath;

    public Repository()
    {
        // 1. Шукаємо шлях до базової папки проєкту LogisticsSystem
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        
        // Піднімаємося на рівень вище з папки bin/Debug/net9.0 до папки проєкту LogisticsSystem.App
        string projectDir = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\..\LogisticsSystem.App"));

        // Якщо раптом структура інша, використовуємо поточну папку
        if (!Directory.Exists(projectDir))
        {
            projectDir = Directory.GetCurrentDirectory();
        }

        // 2. Формуємо повний шлях, щоб файл створювався прямо в папці додатку
        _filePath = Path.Combine(projectDir, $"{typeof(T).Name.ToLower()}_storage.json");
        
        LoadFromFile();
    }

    public void Add(T entity)
    {
        _storage[entity.Id] = entity;
        SaveToFile(); 
    }

    public T? GetById(string id) => _storage.TryGetValue(id, out var entity) ? entity : null;
    
    public IEnumerable<T> GetAll() => _storage.Values;

    private void SaveToFile()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(_storage, options);
        File.WriteAllText(_filePath, jsonString);
    }

    private void LoadFromFile()
    {
        if (File.Exists(_filePath))
        {
            string jsonString = File.ReadAllText(_filePath);
            var data = JsonSerializer.Deserialize<Dictionary<string, T>>(jsonString);
            if (data != null)
            {
                _storage = data;
            }
        }
    }
}