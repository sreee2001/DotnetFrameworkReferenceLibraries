using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Infrastructure.Base
{
    /// <summary>
    /// Provides a base implementation of the <see cref="INotifyDataErrorInfo"/> interface, enabling derived classes  to
    /// manage and notify data validation errors for properties.
    /// </summary>
    /// <remarks>This class maintains a collection of validation errors for property names and provides
    /// mechanisms to query  errors and notify changes. Derived classes can use the <see cref="RaiseErrorsChanged"/>
    /// method to trigger  the <see cref="ErrorsChanged"/> event when validation errors for a property are
    /// updated.</remarks>
    public abstract class NotifyPropertyDataErrorInfoBase : INotifyDataErrorInfo
    {
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

        #region Helper Methods

        /// <summary>
        /// Raises the ErrorsChanged event for the specified property name.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion

    }
}

