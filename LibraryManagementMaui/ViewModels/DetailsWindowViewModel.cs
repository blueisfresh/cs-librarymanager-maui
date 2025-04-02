using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagementMaui.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementMaui.ViewModels;

[QueryProperty(nameof(BookJson), "book")]
public partial class DetailsWindowViewModel : ObservableObject
{
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

    [RelayCommand]
    void SaveBook()
    {
        if (SelectedBook != null) // und es existiert schon
        {
            // update with api
        }
        else { 
            // create a new row with api
        }
    }
}
