using System;

namespace Infrastructure.Entities
{
    public abstract class Entity : AuditableEntityBase
    {
        // This class can be extended with additional properties or methods specific to the application domain.
    }

    /// <summary>
    /// Generic entity class that includes sorting functionality.
    /// </summary>
    public class SortableEntity : Entity, ISortable
    {
        private int? _sortOrder;

        public int? SortOrder 
        {
            get => _sortOrder;
            set => SetField(ref _sortOrder, value); 
        }
        // Additional properties or methods can be added here if needed
    }
}
