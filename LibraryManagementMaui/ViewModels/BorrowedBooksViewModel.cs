using CommunityToolkit.Mvvm.ComponentModel;
using LibraryManagementMaui.Models;
using LibraryManagementMaui.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementMaui.ViewModels;

public partial class BorrowedBooksViewModel : ObservableObject
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

    private readonly ApiServices _apiService = ApiServices.Instance;

    [ObservableProperty]
    private ObservableCollection<Borrow> borrowedBooks = new();

    [ObservableProperty]
    private bool isEmpty;

    public BorrowedBooksViewModel()
    {
        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            var allBooks = await _apiService.GetBorrowsAsync();
            BorrowedBooks = new ObservableCollection<Borrow>(
    allBooks.Where(b => b.ReturnDate == null)
);  

        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error loading borrowed books: " + ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }
}
