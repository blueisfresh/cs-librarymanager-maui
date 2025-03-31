using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LibraryManagementMaui.ViewModels;

public partial class BooksViewModel : ObservableObject
{
    public BooksViewModel()
    {
        Books = new ObservableCollection<string>{
            "The Great Gatsby",
            "1984",
            "To Kill a Mockingbird",
            "Pride and Prejudice",
            "Moby Dick"
        };
    }

    [ObservableProperty]
    ObservableCollection<string> books;

    [ObservableProperty]
    string search;

    [RelayCommand]
    void Add()
    {
        // Open the add books window
    }
}
