namespace Infrastructure.Interfaces
{
    public interface IEntityBaseWithName : IEntityBase
    {
        string Name { get; set; }
    }
}
