namespace Experiments.Domain;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetAtomicValues();

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var value in GetAtomicValues()) hash.Add(value.GetHashCode());
        return hash.ToHashCode();
    }

    public bool Equals(ValueObject? other)
    {
        if (other is null)
        {
            return false;
        }
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return Equals((ValueObject) obj);
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !Equals(left, right);
    }

    public override string ToString()
    {
        return $"[ValueObject {GetType().Name} Values: {GetValuesSeparatedByComma()}]";
    }

    private string GetValuesSeparatedByComma()
    {
        return string.Join(",", GetAtomicValues());
    }
}