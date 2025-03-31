using LibraryManagementMaui.View;

namespace LibraryManagementMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NestedPage1), typeof(NestedPage1));
        }
    }
}
