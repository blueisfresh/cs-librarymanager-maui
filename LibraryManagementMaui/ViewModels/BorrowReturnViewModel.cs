using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagementMaui.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementMaui.ViewModels;

public partial class BorrowReturnViewModel : ObservableObject
{
    [RelayCommand]
    async Task GoToStatisticsPage()
    {
        await Shell.Current.GoToAsync(nameof(StatisticsPage));
    }
    [RelayCommand]
    async Task GoToBorrowedBooksPage()
    {
       await Shell.Current.GoToAsync(nameof(BorrowedBooksPage));
    }
}
