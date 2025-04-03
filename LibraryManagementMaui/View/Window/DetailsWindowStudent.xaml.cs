using LibraryManagementMaui.ViewModels;

namespace LibraryManagementMaui.View.Window;

public partial class DetailsWindowStudent : ContentPage
{
	public DetailsWindowStudent()
	{
		InitializeComponent();
		BindingContext = new DetailsWindowStudentViewModel();
	}
}