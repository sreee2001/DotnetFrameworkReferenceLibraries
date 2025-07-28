namespace Infrastructure.Interfaces
{
    public interface IEditable
    {
        bool IsDirty { get; }
    }
}
