using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagementMaui.Models;
using LibraryManagementMaui.View.Window;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using LibraryManagementMaui.View;
using LibraryManagementMaui.Services;
using System.Diagnostics;

namespace LibraryManagementMaui.ViewModels;

public partial class BooksViewModel : ObservableObject
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
    public BooksViewModel()
    {
        //Books = new ObservableCollection<Book>(booksDictionary.Values);
        Books = new ObservableCollection<Book>();
        _ = LoadBooksAsync();
        IsPopupVisible = false;
    }

    private List<Book> allBooks = new(); // Unfiltered list to restore when clearing search

    private readonly ApiServices _apiService = ApiServices.Instance;

    [ObservableProperty]
    ObservableCollection<Book> books;

    [ObservableProperty]
    private string search;

    partial void OnSearchChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            // Show all books when search is cleared
            Books = new ObservableCollection<Book>(allBooks);
        }
        else
        {
            // Filter case-insensitively
            var filtered = allBooks
                .Where(b => b.Title.Contains(value, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Books = new ObservableCollection<Book>(filtered);
        }
    }

    [ObservableProperty]
    private bool isPopupVisible;

    public async Task LoadBooksAsync()
    {
        IsLoading = true;
        try
        {
            var booksList = await _apiService.GetBooksAsync();
            allBooks = booksList;

            Books.Clear();
            foreach (var book in allBooks)
            {
                Books.Add(book);
            }
        }
        catch (Exception ex)
        {
            // Log the exception or notify the user
            System.Diagnostics.Debug.WriteLine($"Error loading books: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    async Task Add()
    {
        await Shell.Current.GoToAsync(nameof(AddingBooksPage));
    }

    [RelayCommand]
    async Task DeleteBook(Book selectedBook)
    {
        await _apiService.DeleteBookAsync(selectedBook.BookNum);
        ShowPopup();
    }

    [RelayCommand]
    async Task EditBook(Book selectedBook)
    {
        // Serialize the Book object to JSON.
        var bookJson = JsonConvert.SerializeObject(selectedBook);
        // Escape the JSON string.
        var encodedBookJson = WebUtility.UrlEncode(bookJson);
        await Shell.Current.GoToAsync($"{nameof(AddingBooksPage)}?book={encodedBookJson}");
    }

    [RelayCommand]
    async Task ShowDetail(Book selectedBook)
    {
        // Serialize the Book object to JSON.
        var bookJson = JsonConvert.SerializeObject(selectedBook);
        // Escape the JSON string.
        var encodedBookJson = WebUtility.UrlEncode(bookJson);
        await Shell.Current.GoToAsync($"{nameof(DetailsWindow)}?book={encodedBookJson}");
    }

    // Command to show the popup overlay
    [RelayCommand]
    void ShowPopup()
    {
        IsPopupVisible = true;
    }

    // Command to close the popup overlay
    [RelayCommand]
    void ClosePopup()
    {
        IsPopupVisible = false;
    }

    // Test Data before API got connected
    Dictionary<string, Book> booksDictionary = new Dictionary<string, Book>
    {
        // Note: The row with test values (BookNum "00001-2022") is omitted
        ["00001-2024"] = new Book
        {
            BookNum = "00001-2024",
            Title = "Der alte Mann und das Meer",
            Author = "Elias Guerematchi-Vrabl",
            Publisher = "Rowohlt Verlaga",
            ISBN = "9783499551399",
            PublicationPlace = "Hamburg",
            PublicationDate = new DateTime(1952, 9, 1)
        },
        ["00002-2024"] = new Book
        {
            BookNum = "00002-2024",
            Title = "1984",
            Author = "George Orwell",
            Publisher = "Ullstein Verlag",
            ISBN = "9783548234106",
            PublicationPlace = "Berlin",
            PublicationDate = new DateTime(1949, 6, 8)
        },
        ["00003-2024"] = new Book
        {
            BookNum = "00003-2024",
            Title = "Stolz und Vorurteil",
            Author = "Jane Austen",
            Publisher = "Insel Verlag",
            ISBN = "9783458361528",
            PublicationPlace = "Frankfurt",
            PublicationDate = new DateTime(1813, 1, 28)
        },
        ["00004-2024"] = new Book
        {
            BookNum = "00004-2024",
            Title = "Krieg und Frieden",
            Author = "Leo Tolstoi",
            Publisher = "Hanser Verlag",
            ISBN = "9783446234208",
            PublicationPlace = "München",
            PublicationDate = new DateTime(1869, 1, 1)
        },
        ["00005-2023"] = new Book
        {
            BookNum = "00005-2023",
            Title = "Design Patterns: Elements of Reusable Object-Oriented Software",
            Author = "Erich Gamma",
            Publisher = "Addison-Wesley",
            ISBN = "9780201633610",
            PublicationPlace = "New York",
            PublicationDate = new DateTime(2007, 1, 1)
        },
        ["00005-2024"] = new Book
        {
            BookNum = "00005-2024",
            Title = "Die Verwandlung",
            Author = "Franz Kafka",
            Publisher = "Fischer Verlag",
            ISBN = "9783100590115",
            PublicationPlace = "Frankfurt",
            PublicationDate = new DateTime(1915, 10, 1)
        },
        ["00006-2024"] = new Book
        {
            BookNum = "00006-2024",
            Title = "Faust",
            Author = "Johann Wolfgang von Goethe",
            Publisher = "Reclam Verlag",
            ISBN = "9783150000000",
            PublicationPlace = "Stuttgart",
            PublicationDate = new DateTime(1808, 1, 1)
        },
        ["00007-2024"] = new Book
        {
            BookNum = "00007-2024",
            Title = "Don Quijote",
            Author = "Miguel de Cervantes",
            Publisher = "Suhrkamp Verlag",
            ISBN = "9783518458508",
            PublicationPlace = "Berlin",
            PublicationDate = new DateTime(1753, 1, 1)
        },
        ["00008-2024"] = new Book
        {
            BookNum = "00008-2024",
            Title = "Die Brüder Karamasow",
            Author = "Fjodor Dostojewski",
            Publisher = "Diogenes Verlag",
            ISBN = "9783257069286",
            PublicationPlace = "Zürich",
            PublicationDate = new DateTime(1880, 1, 1)
        },
        ["00009-2024"] = new Book
        {
            BookNum = "00009-2024",
            Title = "Moby Dick",
            Author = "Herman Melville",
            Publisher = "Anaconda Verlag",
            ISBN = "9783730600006",
            PublicationPlace = "Köln",
            PublicationDate = new DateTime(1851, 10, 18)
        },
        ["00010-2024"] = new Book
        {
            BookNum = "00010-2024",
            Title = "Anna Karenina",
            Author = "Leo Tolstoi",
            Publisher = "Ullstein Verlag",
            ISBN = "9783548287782",
            PublicationPlace = "Berlin",
            PublicationDate = new DateTime(1878, 1, 1)
        },
        ["00011-2024"] = new Book
        {
            BookNum = "00011-2024",
            Title = "Der große Gatsby",
            Author = "F. Scott Fitzgerald",
            Publisher = "dtv",
            ISBN = "9783423130039",
            PublicationPlace = "München",
            PublicationDate = new DateTime(1925, 4, 10)
        },
        ["00012-2024"] = new Book
        {
            BookNum = "00012-2024",
            Title = "Schuld und Sühne",
            Author = "Fjodor Dostojewski",
            Publisher = "Aufbau Verlag",
            ISBN = "9783351025002",
            PublicationPlace = "Berlin",
            PublicationDate = new DateTime(1866, 1, 1)
        },
        ["00013-2024"] = new Book
        {
            BookNum = "00013-2024",
            Title = "Der Prozess",
            Author = "Franz Kafka",
            Publisher = "Fischer Verlag",
            ISBN = "9783100590016",
            PublicationPlace = "Frankfurt",
            PublicationDate = new DateTime(1925, 1, 1)
        }
    };

}
