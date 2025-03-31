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
            NavigateCommand = new RelayCommand<string>(async (page) =>
            {
                switch (page)
                {
                    case "StudentsPage":
                        await Shell.Current.GoToAsync("StudentPage");
                        break;
                    case "BooksPage":
                        await Shell.Current.GoToAsync("/Views/BooksPage.xaml");
                        break;
                    case "BorrowReturnPage":
                        await Shell.Current.GoToAsync("/Views/BorrowReturnPage.xaml");
                        break;
                    case "StatisticsPage":
                        await Shell.Current.GoToAsync("/Views/StatisticsPage.xaml");
                        break;
                    case "ImportExportPage":
                        await Shell.Current.GoToAsync("/Views/ImportExportPage.xaml");
                        break;
                }
            });
        }
    }
}
