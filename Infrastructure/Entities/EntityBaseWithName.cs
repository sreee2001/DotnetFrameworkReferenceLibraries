using Infrastructure.Interfaces;

namespace Infrastructure.Entities
{
    public abstract class EntityBaseWithName : EntityBase, IEntityBaseWithName
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
