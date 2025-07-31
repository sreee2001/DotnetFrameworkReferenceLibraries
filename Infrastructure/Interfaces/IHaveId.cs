namespace Infrastructure.Interfaces
{
    /// <summary>
    /// Represents an entity that has a unique identifier.
    /// </summary>
    /// <remarks>This interface is typically implemented by types that require an integer-based identifier to
    /// distinguish instances. The <see cref="Id"/> property can be used to get or set the unique identifier for the
    /// implementing object.</remarks>
    public interface IHaveId
    {
        int Id { get; set; }
    }
}
