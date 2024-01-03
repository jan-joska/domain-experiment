namespace Experiments.Domain;

public abstract class Entity<T> : IEquatable<Entity<T>> where T : IEquatable<T>
{
    public readonly List<IDomainEvent> _domainEvents = new();

    protected Entity(T identity)
    {
        Identity = identity ?? throw new ArgumentNullException(nameof(identity));
    }

    public T Identity { get;  };

    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public bool Equals(Entity<T>? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return EqualityComparer<T>.Default.Equals(Identity, other.Identity);
    }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        _domainEvents.Add(domainEvent);
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

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Entity<T>) obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(Identity);
    }

    public static bool operator ==(Entity<T>? left, Entity<T>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<T>? left, Entity<T>? right)
    {
        return !Equals(left, right);
    }

    public override string ToString()
    {
        return $"[Entity {GetType().Name} Identity:{Identity}]";
    }
}