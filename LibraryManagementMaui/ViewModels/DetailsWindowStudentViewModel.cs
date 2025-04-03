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

[QueryProperty(nameof(StudentJson), "student")]
public partial class DetailsWindowStudentViewModel : ObservableObject
{
    [ObservableProperty]
    Student selectedStudent;

    public string StudentJson
    {
        get => _studentJson;
        set
        {
            _studentJson = WebUtility.UrlDecode(value);

            SelectedStudent = JsonConvert.DeserializeObject<Student>(_studentJson);

            OnPropertyChanged(nameof(SelectedStudent));
        }
    }
    private string _studentJson;
}
