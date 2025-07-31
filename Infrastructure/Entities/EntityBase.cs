using Infrastructure.Base;
using Infrastructure.Interfaces;

namespace Infrastructure.Entities
{
    #region Entity classes for application domain with change tracking and validation and notification
    
    public abstract class EntityBase : PropertyChangedBase, IHaveId
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }
    }
    #endregion
}
