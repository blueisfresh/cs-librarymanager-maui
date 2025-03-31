using LibraryManagementMaui.ViewModels;

namespace LibraryManagementMaui
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel MainPageVM { get; set; }

        public MainPage()
        {
            InitializeComponent();
            MainPageVM = new MainPageViewModel();
            BindingContext = MainPageVM;
        }

        private async void navigateBtn_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NestedPage1));
        }

    }

}
