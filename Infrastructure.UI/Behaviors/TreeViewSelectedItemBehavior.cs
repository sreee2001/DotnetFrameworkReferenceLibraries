using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Infrastructure.UI.Behaviors
{
    /// <summary>
    /// Use this class to get the readonly SelectedItem from TreeView
    /// </summary>
    public class TreeViewSelectedItemBehavior : Behavior<TreeView>
    {
        /// <summary>
        /// Dependency Property of Selected Item
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(TreeViewSelectedItemBehavior),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedItemChanged));

        /// <summary>
        /// Returns the Selected Item
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

        #region Overrides of Behavior

        /// <inheritdoc />
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectedItemChanged += AssociatedObjectOnSelectedItemChanged;
        }

        /// <inheritdoc />
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectedItemChanged -= AssociatedObjectOnSelectedItemChanged;
        }

        #endregion

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TreeViewItem item = e.NewValue as TreeViewItem;
            item?.SetValue(TreeViewItem.IsSelectedProperty, true);
        }

        private void AssociatedObjectOnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> routedPropertyChangedEventArgs)
        {
            SelectedItem = AssociatedObject.SelectedItem;
            //SelectedItem = routedPropertyChangedEventArgs.NewValue;
        }
    }
}
