namespace LibraryManagementMaui.View;
using LibraryManagementMaui.ViewModels;

public partial class StudentsPage : ContentPage
{
	public StudentsPage()
	{
		InitializeComponent();
		BindingContext = new StundentsViewModel();
	}
}