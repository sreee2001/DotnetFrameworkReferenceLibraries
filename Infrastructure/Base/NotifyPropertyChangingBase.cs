using System.ComponentModel;

namespace Infrastructure.Base
{
    /// <summary>
    /// Provides a base class for objects that notify subscribers when a property value is about to change.
    /// </summary>
    /// <remarks>This class implements the <see cref="INotifyPropertyChanging"/> interface, enabling derived
    /// classes to raise  the <see cref="PropertyChanging"/> event when a property is about to change. Derived classes
    /// can use the  <see cref="OnPropertyChanging(string)"/> method to trigger the event.</remarks>
    public abstract class NotifyPropertyChangingBase : INotifyPropertyChanging
    {
        #region Implementation of INotifyPropertyChanging

        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        #region Helper Methods

        /// <summary>
        /// Raises the PropertyChanging event for the specified property name.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        #endregion

    }
}

