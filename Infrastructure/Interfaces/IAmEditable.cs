namespace Infrastructure.Interfaces
{
    public interface IAmEditable
    {
        /// <summary>
        /// I have changes that need to be saved
        /// </summary>
        bool IsDirty { get; }
    }
}
