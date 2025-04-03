using LibraryManagementMaui.View;
using LibraryManagementMaui.View.Window;

namespace LibraryManagementMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DetailsWindow), typeof(DetailsWindow));
            Routing.RegisterRoute(nameof(AddingBooksPage), typeof(AddingBooksPage));
            Routing.RegisterRoute(nameof(StatisticsPage), typeof(StatisticsPage));
            Routing.RegisterRoute(nameof(BorrowedBooksPage), typeof(BorrowedBooksPage));
            Routing.RegisterRoute(nameof(DetailsWindowStudent), typeof(DetailsWindowStudent));
            Routing.RegisterRoute(nameof(AddingStudentPage), typeof(AddingStudentPage));
            
        }
    }
}
