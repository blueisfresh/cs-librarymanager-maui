using LibraryManagementMaui.ViewModels;

namespace LibraryManagementMaui.View;

public partial class BooksPage : ContentPage
{
    public BooksViewModel BooksVM { get; set; }
    public BooksPage()
	{
		InitializeComponent();
        BooksVM = new BooksViewModel();
        BindingContext = BooksVM;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is BooksViewModel vm)
        {
            await vm.LoadBooksAsync();
        }
    }

}