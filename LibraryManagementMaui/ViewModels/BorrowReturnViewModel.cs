using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagementMaui.Models;
using LibraryManagementMaui.Services;
using LibraryManagementMaui.View;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementMaui.ViewModels;

public partial class BorrowReturnViewModel : ObservableObject
{
    // Indicates whether the action is Borrowing.
    [ObservableProperty]
    private bool isBorrowing = true;

    // Indicates whether the action is Returning.
    [ObservableProperty]
    private bool isReturning = false;

    // The entered book number.
    [ObservableProperty]
    private string bookNumber;

    // The entered student library card number.
    [ObservableProperty]
    private string studentLibraryCardNum;

    // Optionally, you can bind a DueDate (if needed for further UI purposes).
    [ObservableProperty]
    private DateTime? dueDate;

    // The collection of books selected for borrowing/returning.
    public ObservableCollection<Book> SelectedBooks { get; } = new ObservableCollection<Book>();

    // Expose whether the Student ID input should be visible.
    // Here we assume the student ID field is only needed when borrowing.
    public bool IsStudentIdVisible => IsBorrowing;

    partial void OnIsBorrowingChanged(bool value)
    {
        OnPropertyChanged(nameof(IsStudentIdVisible));
    }
    partial void OnIsReturningChanged(bool value)
    {
        OnPropertyChanged(nameof(IsStudentIdVisible));
    }

    // Command to add a book to the SelectedBooks list.
    [RelayCommand]
    async Task AddBookAsync()
    {
        if (!string.IsNullOrWhiteSpace(BookNumber))
        {
            // Fetch the book from your API service using the provided BookNumber.
            var book = await ApiServices.Instance.GetBookAsync(BookNumber);
            if (book != null)
            {
                SelectedBooks.Add(book);
                BookNumber = string.Empty;
            }
            else
            {
                bool addBook = await Shell.Current.DisplayAlert("Book Not Found",
                    "Book not found. Would you like to add this book?", "Yes", "No");
                if (addBook)
                {
                    // Navigate to the AddBook page (adjust route as needed).
                    await Shell.Current.GoToAsync(nameof(AddingBooksPage));
                }
            }
        }
    }

    // Command to confirm the borrow or return action.
    [RelayCommand]
    async Task ConfirmActionAsync()
    {
        if (IsBorrowing)
        {
            // Validate the student library card number.
            if (string.IsNullOrWhiteSpace(StudentLibraryCardNum) ||
                !int.TryParse(StudentLibraryCardNum, out int studentCardNum))
            {
                await Shell.Current.DisplayAlert("Invalid Student",
                    "Please enter a valid Student Library Card Number.", "OK");
                return;
            }

            // Retrieve the student via the API.
            var student = await ApiServices.Instance.GetStudentAsync(studentCardNum);
            if (student == null)
            {
                bool addStudent = await Shell.Current.DisplayAlert("Student Not Found",
                    "Student not found. Would you like to add this student?", "Yes", "No");
                if (addStudent)
                {
                    await Shell.Current.GoToAsync(nameof(AddingStudentPage));
                }
                return;
            }

            // Process borrowing for each selected book.
            foreach (var book in SelectedBooks)
            {
                var borrow = new Borrow
                {
                    // Use the correct property names from your models.
                    BookBookNum = book.BookNum,
                    StudentLibraryCardNum = student.LibraryCardNum,
                    BorrowDate = DateTime.Now,
                    DueDate = DateTime.Now.AddMonths(3),
                    ReturnDate = null
                };

                var createdBorrow = await ApiServices.Instance.CreateBorrowAsync(borrow);
                if (createdBorrow != null)
                {
                    await Shell.Current.DisplayAlert("Success",
                        $"Borrowed {book.Title} until {borrow.DueDate?.ToShortDateString()}.", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error",
                        $"Failed to borrow {book.Title}.", "OK");
                }
            }
        }
        else if (IsReturning)
        {
            // Process returning for each selected book.
            foreach (var book in SelectedBooks)
            {
                // Retrieve the active borrow record for the given book.
                var activeBorrow = await ApiServices.Instance.GetActiveBorrowByBookNumAsync(book.BookNum);
                if (activeBorrow != null)
                {
                    bool success = await ApiServices.Instance.DeleteBorrowAsync(activeBorrow.BorrowID);
                    if (success)
                    {
                        await Shell.Current.DisplayAlert("Success",
                            $"Returned {book.Title} successfully.", "OK");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error",
                            $"Failed to return {book.Title}.", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Not Found",
                        $"No borrow record found for {book.Title}.", "OK");
                }
            }
        }

        // Clear the selected books and reset the due date.
        SelectedBooks.Clear();
        DueDate = null;
    }


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
