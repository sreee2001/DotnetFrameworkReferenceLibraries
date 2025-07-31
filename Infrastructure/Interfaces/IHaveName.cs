namespace Infrastructure.Interfaces
{
    /// <summary>
    /// Represents an entity that has a name.
    /// </summary>
    /// <remarks>This interface defines a contract for objects that expose a <see cref="Name"/> property,
    /// allowing them to be identified or described by a string name.</remarks>
    public interface IHaveName
    {
        string Name { get; set; }
    }
}
