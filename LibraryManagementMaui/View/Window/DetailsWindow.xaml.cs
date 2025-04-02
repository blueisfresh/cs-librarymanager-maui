using LibraryManagementMaui.ViewModels;

namespace LibraryManagementMaui.View.Window;

public partial class DetailsWindow : ContentPage
{
	public DetailsWindow()
	{
		InitializeComponent();
        BindingContext = new DetailsWindowViewModel();
    }
}