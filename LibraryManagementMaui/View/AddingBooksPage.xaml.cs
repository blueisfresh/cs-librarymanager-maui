using LibraryManagementMaui.ViewModels;

namespace LibraryManagementMaui.View;

public partial class AddingBooksPage : ContentPage
{
	public AddingBooksPage()
	{
		InitializeComponent();
        BindingContext = new AddingBooksViewModel();
	}
}