using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagementMaui.Models;
using LibraryManagementMaui.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementMaui.ViewModels;

[QueryProperty(nameof(BookJson), "book")]
public partial class AddingBooksViewModel : ObservableObject
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

    [ObservableProperty]
    Book selectedBook;

    public string BookJson
    {
        get => _bookJson;
        set
        {
            // Decode and store the raw JSON
            _bookJson = WebUtility.UrlDecode(value);

            // Deserialize into a Book object
            SelectedBook = JsonConvert.DeserializeObject<Book>(_bookJson);

            // Notify that SelectedBook has changed
            OnPropertyChanged(nameof(SelectedBook));
        }
    }
    private string _bookJson;

    private readonly ApiServices _apiService = ApiServices.Instance;

    [ObservableProperty]
    string actionButtonText = "Save"; // e.g., switch to "Edit" in edit mode

    [RelayCommand]
    public async Task SaveBookAsync()
    {
        IsLoading = true;

        try
        {
            if (!string.IsNullOrWhiteSpace(SelectedBook?.BookNum))
            {
                // Check if the book already exists (edit mode)
                var existing = await _apiService.GetBookAsync(SelectedBook.BookNum);
                if (existing != null)
                {
                    var success = await _apiService.UpdateBookAsync(SelectedBook.BookNum, SelectedBook);
                    Debug.WriteLine(success ? "Book updated." : "Update failed.");
                }
                else
                {
                    var created = await _apiService.CreateBookAsync(SelectedBook);
                    Debug.WriteLine(created != null ? "Book created." : "Create failed.");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error saving book: " + ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }

}
