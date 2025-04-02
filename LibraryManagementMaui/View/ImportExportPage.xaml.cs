namespace LibraryManagementMaui.View;
using LibraryManagementMaui.ViewModels;

public partial class ImportExportPage : ContentPage
{
	public ImportExportPage()
	{
		InitializeComponent();
		BindingContext = new ImportExportViewModel();
	}
}