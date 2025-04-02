using LibraryManagementMaui.View;
using LibraryManagementMaui.View.Window;

namespace LibraryManagementMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NestedPage1), typeof(NestedPage1));
            // Register the "DetailsWindow" route
            Routing.RegisterRoute(nameof(DetailsWindow), typeof(DetailsWindow));
        }
    }
}
