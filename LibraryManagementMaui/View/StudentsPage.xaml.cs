namespace LibraryManagementMaui.View;
using LibraryManagementMaui.ViewModels;

public partial class StudentsPage : ContentPage
{
	public StudentsPage()
	{
		InitializeComponent();
		BindingContext = new StudentsViewModel();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is StudentsViewModel vm)
        {
            await vm.LoadStudentsAsync();
        }
    }
}