using LibraryManagementMaui.ViewModels;

namespace LibraryManagementMaui.View;

public partial class AddingStudentPage : ContentPage
{
	public AddingStudentPage()
	{
		InitializeComponent();
		BindingContext = new AddingStundentsViewModel();
	}
}