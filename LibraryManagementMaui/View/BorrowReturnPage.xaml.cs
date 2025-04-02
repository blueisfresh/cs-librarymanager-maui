namespace LibraryManagementMaui.View;
using LibraryManagementMaui.ViewModels;

public partial class BorrowReturnPage : ContentPage
{
	public BorrowReturnPage()
	{
		InitializeComponent();
		BindingContext = new BorrowReturnViewModel();
	}
}