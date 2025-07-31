using System.Windows;
using System.Windows.Controls;

namespace Infrastructure.UI.Behaviors
{
    /// <summary>
    /// Provides an attached behavior that automatically brings a selected <see cref="TreeViewItem"/> into view when it
    /// is selected.
    /// </summary>
    /// <remarks>This behavior can be applied to <see cref="TreeViewItem"/> elements by setting the attached
    /// property <see cref="IsBringSelectedIntoViewProperty"/> to <see langword="true"/>. When enabled, the behavior
    /// ensures that the selected item is scrolled into view within its container.</remarks>
    public class BringSelectedItemIntoViewBehavior
    {
        /// <summary>
        /// Identifies the IsBringSelectedIntoView attached property, which determines whether the selected item in a
        /// control should be automatically brought into view.
        /// </summary>
        /// <remarks>This attached property can be applied to controls such as ListBox or ListView to
        /// enable automatic scrolling of the selected item into view.</remarks>
        public static readonly DependencyProperty IsBringSelectedIntoViewProperty = DependencyProperty.RegisterAttached(
            "IsBringSelectedIntoView", typeof(bool), typeof(BringSelectedItemIntoViewBehavior), new PropertyMetadata(default(bool), PropertyChangedCallback));

        /// <summary>
        /// Sets a value indicating whether the selected item in a container should be automatically brought into view.
        /// </summary>
        /// <remarks>This method sets the value of the attached property
        /// <c>IsBringSelectedIntoViewProperty</c>  on the specified <paramref name="element"/>. Use this to control
        /// whether the selected item  in a container, such as a <see cref="System.Windows.Controls.ListBox"/>, is
        /// automatically  scrolled into view when selected.</remarks>
        /// <param name="element">The <see cref="DependencyObject"/> on which to set the value.</param>
        /// <param name="value"><see langword="true"/> to enable automatic scrolling to bring the selected item into view;  otherwise, <see
        /// langword="false"/>.</param>
        public static void SetIsBringSelectedIntoView(DependencyObject element, bool value)
        {
            element.SetValue(IsBringSelectedIntoViewProperty, value);
        }

        /// <summary>
        /// Gets a value indicating whether the specified element is configured to bring the selected item into view.
        /// </summary>
        /// <param name="element">The <see cref="DependencyObject"/> from which to retrieve the value.</param>
        /// <returns><see langword="true"/> if the element is set to bring the selected item into view; otherwise, <see
        /// langword="false"/>.</returns>
        public static bool GetIsBringSelectedIntoView(DependencyObject element)
        {
            return (bool)element.GetValue(IsBringSelectedIntoViewProperty);
        }

        /// <summary>
        /// Handles changes to a dependency property and attaches event handlers to a <see cref="TreeViewItem"/> when
        /// the property's value is changed.
        /// </summary>
        /// <remarks>This method attaches the <see cref="TreeViewItem.Unloaded"/> and <see
        /// cref="TreeViewItem.Selected"/> event handlers to the <see cref="TreeViewItem"/> when the dependency
        /// property's value is changed. If the <paramref name="dependencyObject"/> is not a <see cref="TreeViewItem"/>,
        /// the method does nothing.</remarks>
        /// <param name="dependencyObject">The <see cref="DependencyObject"/> whose property value has changed. This is expected to be a <see
        /// cref="TreeViewItem"/>.</param>
        /// <param name="dependencyPropertyChangedEventArgs">Provides data about the property change, including the old and new values of the dependency property.</param>
        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var treeViewItem = dependencyObject as TreeViewItem;
            if (treeViewItem == null)
            {
                return;
            }

            if (!((bool)dependencyPropertyChangedEventArgs.OldValue) &&
                ((bool)dependencyPropertyChangedEventArgs.NewValue))
            {
                treeViewItem.Unloaded += TreeViewItemOnUnloaded;
                treeViewItem.Selected += TreeViewItemOnSelected;
            }
        }

        /// <summary>
        /// This method detaches event handlers for the 
        /// <see cref="TreeViewItem.Unloaded"/> and <see cref="TreeViewItem.Selected"/>
        /// events from the specified <see cref="TreeViewItem"/> to prevent memory leaks.
        /// </summary>
        /// <remarks></remarks>
        /// <param name="sender">The source of the event, expected to be a <see cref="TreeViewItem"/>.</param>
        /// <param name="routedEventArgs">The event data associated with the <see cref="TreeViewItem.Unloaded"/> event.</param>
        private static void TreeViewItemOnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var treeViewItem = sender as TreeViewItem;
            if (treeViewItem == null)
            {
                return;
            }

            treeViewItem.Unloaded -= TreeViewItemOnUnloaded;
            treeViewItem.Selected -= TreeViewItemOnSelected;
        }

        /// <summary>
        /// Handles the selection event for a <see cref="TreeViewItem"/> and ensures the selected item is brought into
        /// view.
        /// </summary>
        /// <remarks>This method is triggered when a <see cref="TreeViewItem"/> is selected. If the
        /// selected item is valid, it scrolls into view to ensure it is visible to the user.</remarks>
        /// <param name="sender">The source of the event, typically the <see cref="TreeView"/> containing the selected item.</param>
        /// <param name="routedEventArgs">The event data associated with the selection event.</param>
        private static void TreeViewItemOnSelected(object sender, RoutedEventArgs routedEventArgs)
        {
            //if (routedEventArgs.OriginalSource == routedEventArgs.Source)
            {
                var treeViewItem = routedEventArgs.OriginalSource as TreeViewItem;
                if (treeViewItem != null)
                    treeViewItem.BringIntoView();

            }
        }
    }
}
