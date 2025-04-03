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

[QueryProperty(nameof(StudentJson), "student")]
public partial class AddingStundentsViewModel : ObservableObject
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
    Student selectedStudent;

    public string StudentJson
    {
        get => _studentJson;
        set
        {
            // Decode and store the raw JSON
            _studentJson = WebUtility.UrlDecode(value);

            // Deserialize into a Book object
            SelectedStudent = JsonConvert.DeserializeObject<Student>(_studentJson);

            // Notify that SelectedBook has changed
            OnPropertyChanged(nameof(SelectedStudent));
        }
    }
    private string _studentJson;

    private readonly ApiServices _apiService = ApiServices.Instance;

    [ObservableProperty]
    string actionButtonText = "Save"; // e.g., switch to "Edit" in edit mode

    [RelayCommand]
    public async Task SaveStudentAsync()
    {
        IsLoading = true;

        try
        {
            if (!string.IsNullOrWhiteSpace(selectedStudent?.LibraryCardNum.ToString()))
            {
                // Check if the book already exists (edit mode)
                var existing = await _apiService.GetStudentAsync(selectedStudent.LibraryCardNum);
                if (existing != null)
                {
                    var success = await _apiService.UpdateStudentAsync(selectedStudent.LibraryCardNum, SelectedStudent);
                    Debug.WriteLine(success ? "Student updated." : "Update failed.");
                }
                else
                {
                    var created = await _apiService.GetStudentAsync(selectedStudent.LibraryCardNum);
                    Debug.WriteLine(created != null ? "Student created." : "Create failed.");
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
