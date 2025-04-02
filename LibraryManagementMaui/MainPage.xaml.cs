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

    }

}
