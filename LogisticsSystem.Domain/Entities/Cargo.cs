namespace LogisticsSystem.Domain.Entities;

public class Cargo : Contracts.IEntity
{
    private double _weightKg;
    private double _volumeM3;

    public string Id { get; }
    public string Description { get; }

    public double WeightKg
    {
        get => _weightKg;
        private set
        {
            if (value <= 0) throw new ArgumentException("Вага вантажу повинна бути більшою за 0.");
            _weightKg = value;
        }
    }

    public double VolumeM3
    {
        get => _volumeM3;
        private set
        {
            if (value <= 0) throw new ArgumentException("Об'єм вантажу повинен бути більшим за 0.");
            _volumeM3 = value;
        }
    }

    public Cargo(string description, double weightKg, double volumeM3)
    {
        Id = System.Guid.NewGuid().ToString().Substring(0, 8);
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Опис не може бути порожнім.");
        Description = description;
        WeightKg = weightKg;
        VolumeM3 = volumeM3;
    }

    public Cargo(Cargo other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        Description = other.Description + " (Копія)";
        WeightKg = other.WeightKg;
        VolumeM3 = other.VolumeM3;
    }

    public static Cargo operator +(Cargo left, Cargo right)
    {
        if (left == null || right == null) throw new ArgumentNullException("Вантажі не можуть бути null.");
        return new Cargo($"{left.Description} + {right.Description}", left.WeightKg + right.WeightKg, left.VolumeM3 + right.VolumeM3);
    }

    public static bool operator ==(Cargo? left, Cargo? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.WeightKg == right.WeightKg && left.VolumeM3 == right.VolumeM3 && left.Description == right.Description;
    }

    public static bool operator !=(Cargo? left, Cargo? right) => !(left == right);
    public override bool Equals(object? obj) => obj is Cargo other && this == other;
    public override int GetHashCode() => HashCode.Combine(Description, WeightKg, VolumeM3);
}