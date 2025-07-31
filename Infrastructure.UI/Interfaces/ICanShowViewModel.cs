using System.Windows.Input;

namespace Infrastructure.UI.Interfaces
{
    /// <summary>
    /// Defines functionality for displaying and closing a view model, as well as providing a command to show the view
    /// model.
    /// </summary>
    /// <remarks>This interface is typically implemented by classes that manage the lifecycle of a view model,
    /// including showing and closing the associated view. It also exposes a command that can be used  to trigger the
    /// display of the view model.</remarks>
    public interface ICanShowViewModel
    {
        /// <summary>
        /// Show the View Model
        /// </summary>
        void Show();

        /// <summary>
        /// Close the view Model
        /// </summary>
        void Close();

        /// <summary>
        /// Command to Show the View Model
        /// </summary>
        ICommand ShowCommand { get; }
    }
}
