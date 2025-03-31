using LibraryManagementMaui.View;

namespace LibraryManagementMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register a route that points to StudentPage
            Routing.RegisterRoute("StudentPage", typeof(StudentPage));
        }
    }
}
