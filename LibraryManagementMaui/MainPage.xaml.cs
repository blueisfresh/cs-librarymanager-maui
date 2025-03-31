using LibraryManagementMaui.ViewModels;

namespace LibraryManagementMaui
{
    public partial class MainPage : ContentPage
    {
        public BurgerHeaderViewModel BurgerHeaderVM { get; set; }

        public MainPage()
        {
            InitializeComponent();
            BurgerHeaderVM = new BurgerHeaderViewModel();
            // Optionally, assign the BindingContext for the entire page if needed:
            BindingContext = this;
        }

    }

}
