﻿using Infrastructure.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Infrastructure.Base
{

    /// <summary>
    /// Base class the provided Change Notifications, Validation, and Errors Notifications
    /// </summary>
    /// <remarks>
    /// IsDirty flag is used to indicate if the model has unsaved changes.
    /// PostSetAction is an action that can be executed after a property is set.
    /// RaisePropertyChanged method validates the model asynchronously before raising the PropertyChanged event.
    /// SetField method sets the field only if the new value is different from the old value, and raises a notify of property change.
    /// Setter method is useful if you intend to set the member of another object.
    /// ValidateAsync method runs the validation rules on the model asynchronously.
    /// Validate method runs the validation rules on the model synchronously.
    /// AddErrors method adds a list of errors for the specified property name.
    /// ClearErrors method clears all errors for the specified property name.
    /// </remarks>
    public abstract class PropertyChangedBase : NotifyPropertyBase, IAmEditable
    {
        private readonly ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();
        private bool _isDirty;

        /// <summary>
        /// Dirty flag to tell if needs saving
        /// </summary>
        [NotMapped]
        public bool IsDirty
        {
            get => _isDirty;
            set
            {
                if (_isDirty == value)
                    return;
                _isDirty = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Action to be executed after a property is set.
        /// </summary>
        public Action<string> PostSetAction = propertyName => { };

        /// <summary>
        /// Raises the PropertyChanged event for the specified property name.
        /// Validates the model asynchronously before raising the event.
        /// </summary>
        /// <param name="propertyName"></param>
        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            ValidateAsync();
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Sets the field only if the new value is different from old value, and raises a notify of property change
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <typeparam name="T"></typeparam>
        /// <usage>
        ///  private string name;  public string Name
        ///  {
        ///     get { return name; }
        ///     set { SetField(ref name, value); }
        ///  }
        /// </usage>
        /// <returns>true if value is set and false if new value is same as old value</returns>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            OnPropertyChanging(propertyName);
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            //if (typeof(ComboBoxDropDownItem).IsAssignableFrom(typeof(T)) &&
            //    string.IsNullOrWhiteSpace((value as ComboBoxDropDownItem)?.Name))
            //    value = default;
            field = value;
            PostSetAction?.Invoke(propertyName);
            IsDirty = true;
            RaisePropertyChanged(propertyName);
            return true;
        }


        /// <summary>
        /// Sets the field only if the new value is different from old value, and raises a notify of property change
        /// Useful if you intend to set the member of another object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="setValue"></param>
        /// <param name="propertyName"></param>
        /// <usage>
        /// public class Person
        /// {
        ///     public string Name {get; set;}
        ///     public int Id {get; set;}
        /// };
        /// private Person person;
        /// public string Name
        /// {
        ///     get { return person.Name; }
        ///     set { Setter(person.Name, value, b => person.Name = b); }
        /// }
        /// </usage>
        /// <returns></returns>
        protected bool Setter<T>(T field, T value, Action<T> setValue, [CallerMemberName] string propertyName = "")
        {
            OnPropertyChanging(propertyName);
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            setValue(value);
            PostSetAction?.Invoke(propertyName);
            IsDirty = true;
            RaisePropertyChanged(propertyName);
            return true;
        }


        /// <summary>
        /// Runs the validation asynchronously
        /// </summary>
        /// <returns></returns>
        public Task ValidateAsync()
        {
            return Task.Run(() => Validate());
        }

        private readonly object _lock = new object();

        /// <summary>
        /// Run the Validation rules on the model
        /// </summary>
        public void Validate()
        {
            lock (_lock)
            {
                ValidationContext validationContext = new ValidationContext(this, null, null);
                List<ValidationResult> validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);

                foreach (KeyValuePair<string, List<string>> keyValuePair in _errors.ToList())
                {
                    if (validationResults.All(b => b.MemberNames.All(m => m != keyValuePair.Key)))
                    {
                        _errors.TryRemove(keyValuePair.Key, out List<string> result);
                        RaiseErrorsChanged(keyValuePair.Key);
                    }
                }

                IEnumerable<IGrouping<string, ValidationResult>> groupings =
                    from validationResult in validationResults
                    from name in validationResult.MemberNames
                    group validationResult by name into grouping
                    select grouping;

                foreach (IGrouping<string, ValidationResult> grouping in groupings)
                {
                    List<string> errorMessages = grouping.Select(r => r.ErrorMessage).ToList();

                    if (_errors.ContainsKey(grouping.Key))
                    {
                        _errors.TryRemove(grouping.Key, out List<string> result);
                        RaiseErrorsChanged(grouping.Key);
                    }
                    _errors.TryAdd(grouping.Key, errorMessages);
                    RaiseErrorsChanged(grouping.Key);
                }
            }
        }

        /// <summary>
        /// Adds list of errors for the specified property name.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="errorMessages"></param>
        public void AddErrors(string propertyName, List<string> errorMessages)
        {
            _errors.TryAdd(propertyName, errorMessages);
            RaiseErrorsChanged(propertyName);
        }

        /// <summary>
        /// Clears all errors for the specified property name.
        /// </summary>
        /// <param name="propertyName"></param>
        public void ClearErrors(string propertyName)
        {
            _errors.TryRemove(propertyName, out List<string> result);
            RaiseErrorsChanged(propertyName);
        }
    }
}

