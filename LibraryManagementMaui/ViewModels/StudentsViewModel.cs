using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagementMaui.Models;
using LibraryManagementMaui.Services;
using LibraryManagementMaui.View;
using LibraryManagementMaui.View.Window;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagementMaui.ViewModels;

public partial class StudentsViewModel : ObservableObject
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

    public StudentsViewModel()
    {
        Students = new ObservableCollection<Student>();
        _ = LoadStudentsAsync();
        IsPopupVisible = false;
    }

    private List<Student> allStudents = new();

    private readonly ApiServices _apiService = ApiServices.Instance;

    [ObservableProperty]
    ObservableCollection<Student> students;

    [ObservableProperty]
    private string search;

    partial void OnSearchChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Students = new ObservableCollection<Student>(allStudents);
        }
        else
        {
            // Filter case-insensitively
            var filtered = allStudents
                .Where(b => b.FirstName.Contains(value, StringComparison.OrdinalIgnoreCase))
            .ToList();

            Students = new ObservableCollection<Student>(filtered);
        }
    }

    [ObservableProperty]
    private bool isPopupVisible;

    public async Task LoadStudentsAsync()
    {
        IsLoading = true;
        try
        {
            var studentList = await _apiService.GetStudentsAsync();
            allStudents = studentList.Where(s => !string.Equals(s.FirstName, "Anonym", StringComparison.OrdinalIgnoreCase))
            .ToList();

            Students.Clear();
            foreach (var student in allStudents)
            {
                Students.Add(student);
            }
        }
        catch (Exception ex)
        {
            // Log the exception or notify the user
            System.Diagnostics.Debug.WriteLine($"Error loading students: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    async Task AddStudents()
    {
        //await Shell.Current.GoToAsync(nameof(AddingStudentPage));
    }

    [RelayCommand]
    async Task DeleteStudents(Student selectedStudent)
    {
        await _apiService.DeleteStudentAsync(selectedStudent.LibraryCardNum);
        ShowPopup();
    }

    [RelayCommand]
    async Task EditStudent(Student selectedStudent)
    {
        var studentJson = JsonConvert.SerializeObject(selectedStudent);
        var encodedStudentJson = WebUtility.UrlEncode(studentJson);
        await Shell.Current.GoToAsync($"{nameof(AddingStudentPage)}?student={encodedStudentJson}");
    }

    [RelayCommand]
    async Task ShowDetail(Student selectedStudent)
    {
        var studentJson = JsonConvert.SerializeObject(selectedStudent);
        var encodedStudentJson = WebUtility.UrlEncode(studentJson);
        await Shell.Current.GoToAsync($"{nameof(DetailsWindowStudent)}?student={encodedStudentJson}");
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
}
