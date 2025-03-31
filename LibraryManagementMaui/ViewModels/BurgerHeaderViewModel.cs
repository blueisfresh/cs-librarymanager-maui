using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementMaui.ViewModels
{
    public partial class BurgerHeaderViewModel: ObservableObject
    {

        // The ObservableProperty attribute will automatically generate 
        // the property with a backing field and call OnPropertyChanged.
        [ObservableProperty]
        private bool isMenuVisible;

        public ICommand ToggleMenuCommand { get; }
        public ICommand NavigateCommand { get; }

        public BurgerHeaderViewModel()
        {
            ToggleMenuCommand = new RelayCommand(() => IsMenuVisible = !IsMenuVisible);

            // Implement your navigation command here.
            NavigateCommand = new RelayCommand<string>((page) =>
            {
                // Example navigation logic:
                // Shell.Current.GoToAsync(page);
                // Or use your custom navigation service.
            });
        }
    }
}
