using Microsoft.Maui.Controls;

namespace LibraryManagementMaui;

public partial class MainContainer : ContentPage
{
	public MainContainer()
	{
		InitializeComponent();

    }

    public void SwitchToView(Microsoft.Maui.Controls.View view)
    {
        ContentRegion.Content = view;
    }
}