namespace Infrastructure.UI.Interfaces
{
    /// <summary>
    /// Represents a view model that has a parent view model.
    /// </summary>
    /// <remarks>This interface is typically implemented by view models that are part of a hierarchical
    /// structure, where each view model can reference its parent. The parent view model is expected to implement the
    /// <see cref="ICanShowViewModel"/> interface.</remarks>
    public interface IHaveParentViewModel
    {
        ICanShowViewModel ParentViewModel { get; }
    }
}
