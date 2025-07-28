using Infrastructure.Interfaces;

namespace Infrastructure.Entities
{
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
