namespace Infrastructure.Interfaces
{
    /// <summary>
    /// Defines a contract for objects that can be saved and queried for their ability to be saved.
    /// </summary>
    /// <remarks>Implementations of this interface should provide logic to determine whether the object can be
    /// saved  and to perform the save operation. The <see cref="CanSave"/> method should be called before invoking 
    /// <see cref="Save"/> to ensure the object is in a valid state for saving.</remarks>
    public interface IAmSavable
    {
        /// <summary>
        /// Determines whether the current state allows saving.
        /// </summary>
        /// <returns><see langword="true"/> if saving is permitted in the current state; otherwise, <see langword="false"/>.</returns>
        bool CanSave();

        /// <summary>
        /// Saves the current state or data to the underlying storage.
        /// </summary>
        /// <remarks>This method commits any pending changes to the storage medium. Ensure that all
        /// required  data is properly set before calling this method. If an error occurs during the save  operation, an
        /// exception may be thrown.</remarks>
        void Save();
    }
}
