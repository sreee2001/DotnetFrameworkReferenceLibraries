using System.ComponentModel;

namespace Infrastructure.Base
{
    /// <summary>
    /// Provides a base class for implementing the <see cref="INotifyPropertyChanged"/> interface,  enabling property
    /// change notifications for derived classes.
    /// </summary>
    /// <remarks>This class simplifies the implementation of the <see cref="INotifyPropertyChanged"/>
    /// interface  by providing a virtual <see cref="OnPropertyChanged(string)"/> method to raise the  <see
    /// cref="PropertyChanged"/> event. Derived classes can call this method to notify subscribers  of property
    /// changes.</remarks>
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        #region Implementation of INotifyPropertyChanged

        /// <inheritdoc />
        public virtual event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Helper Methods

        /// <summary>
        /// Raises the PropertyChanged event for the specified property name.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

