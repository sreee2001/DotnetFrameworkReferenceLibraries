using System;

namespace Infrastructure.Interfaces
{
    /// <summary>
    /// Defines a contract for entities that support audit tracking, including creation and modification metadata.
    /// </summary>
    /// <remarks>Implement this interface to provide information about when and by whom an entity was created
    /// or last modified. This is commonly used in systems that require audit trails for data changes.</remarks>
    public interface IAmAuditable
    {
        DateTime CreatedOn { get; set; }
        DateTime? ModifiedOn { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}
