namespace Infrastructure.Interfaces
{
    /// <summary>
    /// Represents an entity that can be sorted based on a specified order.
    /// </summary>
    /// <remarks>The <see cref="SortOrder"/> property determines the relative position of the entity in a
    /// sorted collection. Entities with lower values are considered to have higher priority in the sort order. A
    /// <c>null</c> value indicates that the entity does not participate in sorting.</remarks>
    public interface IAmSortable
    {
        int? SortOrder { get; set; }
    }
}
