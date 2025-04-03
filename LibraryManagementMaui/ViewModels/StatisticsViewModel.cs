using CommunityToolkit.Mvvm.ComponentModel;
using LibraryManagementMaui.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementMaui.ViewModels;

public partial class StatisticsViewModel : ObservableObject
{
    private bool isLoading;
    public bool IsLoading
    {
        get => isLoading;
        set
        {
            // SetProperty updates the value and raises PropertyChanged if needed.
            if (SetProperty(ref isLoading, value))
            {
                // Manually notify that IsNotLoading has changed.
                OnPropertyChanged(nameof(IsNotLoading));
            }
        }
    }

    // Computed property that reflects the inverse of IsLoading.
    public bool IsNotLoading => !IsLoading;

    public ObservableCollection<KeyValuePair<string, int>> MostBorrowedBooks { get; set; }
        = new ObservableCollection<KeyValuePair<string, int>>();


    private readonly ApiServices _apiService = ApiServices.Instance;
    public StatisticsViewModel()
    {
        _ = LoadData();
    }

    private async Task LoadData()
    {
        IsLoading = true;
        try
        {

            // Replace with your actual API endpoint URL
            var result = await _apiService.GetTopBorrowedBooksAsync(10);
            if (result != null)
            {
                foreach (var item in result)
                {
                    MostBorrowedBooks.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching most borrowed books: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
}
