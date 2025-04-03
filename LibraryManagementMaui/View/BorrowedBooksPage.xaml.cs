using LibraryManagementMaui.ViewModels;

namespace LibraryManagementMaui.View;

public partial class BorrowedBooksPage : ContentPage
{
	public BorrowedBooksPage()
	{
		InitializeComponent();
		BindingContext = new BorrowedBooksViewModel();
	}
}