using Infrastructure.Interfaces;

namespace Infrastructure.EntitiesPOCO
{
    #region POCO classes for Database opereations
    public abstract class PocoEntityBaseWithName : PocoEntityBase, IEntityBaseWithName
    {
        public string Name { get; set; }
        // Additional properties or methods can be added here if needed
    }
    #endregion
}
