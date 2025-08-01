using System.Windows;

namespace Infrastructure.UI.Extensions
{
    /// <summary>
    /// Provides attached properties and methods to manage focus behavior for <see cref="DependencyObject"/> instances.
    /// </summary>
    /// <remarks>This class defines the <see cref="IsFocusedProperty"/> attached property, which can be used
    /// to programmatically set or retrieve the focus state of a <see cref="DependencyObject"/>. When the property is
    /// set to <see langword="true"/>, the associated <see cref="UIElement"/> will receive focus.</remarks>
    public static class DependencyObjectFocusExtension
    {
        /// <summary>
        /// Gets a value indicating whether the specified <see cref="DependencyObject"/> is currently focused.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> to check for focus.</param>
        /// <returns><see langword="true"/> if the specified <see cref="DependencyObject"/> is focused; otherwise, <see
        /// langword="false"/>.</returns>
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="IsFocusedProperty"/> attached property for the specified <see
        /// cref="DependencyObject"/>.
        /// </summary>
        /// <remarks>This method allows you to programmatically set the focus state of a <see
        /// cref="DependencyObject"/> by modifying the <see cref="IsFocusedProperty"/>.</remarks>
        /// <param name="obj">The <see cref="DependencyObject"/> on which to set the attached property value.</param>
        /// <param name="value">A <see langword="true"/> value to indicate the object is focused; otherwise, <see langword="false"/>.</param>
        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        /// <summary>
        /// Identifies the IsFocused attached property, which indicates whether a UI element is currently focused.
        /// </summary>
        /// <remarks>This attached property can be used to track the focus state of a UI element. It is
        /// primarily intended for use in XAML bindings or code-behind scenarios.</remarks>
        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached("IsFocused", typeof(bool), typeof(DependencyObjectFocusExtension),
                new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));


        /// <summary>
        /// Handles changes to the <see cref="IsFocused"/> dependency property.
        /// </summary>
        /// <remarks>If the new value of the <see cref="IsFocused"/> property is <see langword="true"/>,
        /// the method  ensures the element loses focus before setting focus to it.</remarks>
        /// <param name="d">The <see cref="DependencyObject"/> on which the property value has changed.</param>
        /// <param name="e">Provides data about the property change, including the old and new values.</param>
        private static void OnIsFocusedPropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var uie = (UIElement)d;
            if ((bool)e.NewValue)
            {
                OnLostFocus(uie, null);
                uie.Focus();
            }
        }

        /// <summary>
        /// Handles the <see cref="UIElement.LostFocus"/> event and updates the focus state of the sender.
        /// </summary>
        /// <remarks>This method sets the <see cref="IsFocusedProperty"/> of the sender to <see
        /// langword="false"/> when the sender loses focus. Ensure that the <paramref name="sender"/> is a <see
        /// cref="UIElement"/> before invoking this method.</remarks>
        /// <param name="sender">The source of the event, which must be a <see cref="UIElement"/>.</param>
        /// <param name="e">The event data associated with the <see cref="UIElement.LostFocus"/> event.</param>
        private static void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender is UIElement)
            {
                (sender as UIElement).SetValue(IsFocusedProperty, false);
            }
        }
    }
}
