using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace Infrastructure.UI.Behaviors
{
    /// <summary>
    /// Use this class to get the readonly SelectedItem and SelectedItems from Any class that Implements MultiSelector
    /// </summary>
    public abstract class SelectedItemsBehavior<T> : Behavior<T> where T : MultiSelector
    {
        #region Overrides of Behavior

        /// <inheritdoc />
        protected override void OnAttached()
        {
            AssociatedObject.SelectionChanged += AssociatedObjectOnSelectionChanged;
        }

        #region Overrides of Behavior

        /// <inheritdoc />
        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= AssociatedObjectOnSelectionChanged;
        }

        #endregion

        private void AssociatedObjectOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            SelectedItem = AssociatedObject.SelectedItem;
            SelectedItems = AssociatedObject.SelectedItems;
        }


        /// <summary>
        /// Dependency Property of Selected Items of the Data Grid
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(IList), typeof(SelectedItemsBehavior<T>),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        /// <summary>
        /// Returns the Selected Items of Data Grid
        /// </summary>
        public IList SelectedItems
        {
            get => (IList)GetValue(SelectedItemsProperty);
            set
            {
                if (SelectedItems != value)
                    SetValue(SelectedItemsProperty, value);
            }
        }

        /// <summary>
        /// Dependency Property of Selected Items of the Data Grid
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(SelectedItemsBehavior<T>),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        /// <summary>
        /// Returns the Selected Items of Data Grid
        /// </summary>
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set
            {
                if (SelectedItem != value)
                    SetValue(SelectedItemProperty, value);
            }
        }

        #endregion
    }
}
