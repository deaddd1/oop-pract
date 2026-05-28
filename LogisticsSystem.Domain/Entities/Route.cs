namespace LogisticsSystem.Domain.Entities;

public class Route
{
    private readonly List<string> _stops = new();

    public string StartPoint => _stops.Count > 0 ? _stops[0] : throw new InvalidOperationException("Маршрут порожній.");
    public string EndPoint => _stops.Count > 1 ? _stops[^1] : throw new InvalidOperationException("Маршрут не має кінцевої точки.");

    public Route(IEnumerable<string> stops)
    {
        if (stops == null || stops.Count() < 2)
            throw new ArgumentException("Маршрут повинен містити мінімум 2 точки.");
        _stops.AddRange(stops);
    }

    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= _stops.Count) throw new IndexOutOfRangeException("Зупинки не існує.");
            return _stops[index];
        }
    }
}