using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Infrastructure.Base
{
    /// <summary>
    /// Base class for models that implement INotifyPropertyChanged, INotifyPropertyChanging, and INotifyDataErrorInfo
    /// </summary>
    public abstract class NotifyPropertyBase: INotifyPropertyChanged, INotifyPropertyChanging, INotifyDataErrorInfo
    {
        #region Implementation of INotifyPropertyChanged

        /// <inheritdoc />
        public virtual event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Implementation of INotifyDataErrorInfo

        private readonly ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();

        /// <inheritdoc />
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return null;
            _errors.TryGetValue(propertyName, out List<string> errorsForName);
            return errorsForName;
        }

        /// <inheritdoc />
        public bool HasErrors => _errors.Any(b => b.Value != null && b.Value.Count > 0);

        /// <inheritdoc />
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


        #endregion

        #region Implementation of INotifyPropertyChanging

        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        /// <summary>
        /// Raises the PropertyChanged event for the specified property name.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanging event for the specified property name.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the ErrorsChanged event for the specified property name.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

    }
}

