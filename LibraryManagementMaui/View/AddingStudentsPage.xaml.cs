namespace LibraryManagementMaui.View;
using LibraryManagementMaui.ViewModels;

public partial class AddingStudentsPage : ContentPage
{
	public AddingStudentsPage()
	{
		InitializeComponent();
		BindingContext = new AddingStundentsViewModel();
	}
}