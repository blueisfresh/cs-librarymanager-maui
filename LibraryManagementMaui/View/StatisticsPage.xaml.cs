using LibraryManagementMaui.ViewModels;

namespace LibraryManagementMaui.View;

public partial class StatisticsPage : ContentPage
{
	public StatisticsPage()
	{
		InitializeComponent();
		BindingContext = new StatisticsViewModel();

    }
}