using LibraryManagementMaui.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementMaui.Services;

public class ApiServices
{
    private static readonly Lazy<ApiServices> _instance = new Lazy<ApiServices>(() => new ApiServices());
    public static ApiServices Instance => _instance.Value;

    private readonly HttpClient _http;

    // Private constructor prevents external instantiation.
    private ApiServices()
    {
        // Handler that bypasses SSL certificate errors (DEV ONLY!)
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
        };

        // Pass the handler into the HttpClient constructor
        _http = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://10.0.2.2:7117/api/")
        };
    }


    // -----------------------
    // STUDENTS METHODS
    // -----------------------

    // GET: api/Students
    public async Task<List<Student>> GetStudentsAsync()
    {
        return await _http.GetFromJsonAsync<List<Student>>("Students") ?? new List<Student>();
    }

    // GET: api/Students/{id}
    public async Task<Student> GetStudentAsync(int id)
    {
        try
        {
            return await _http.GetFromJsonAsync<Student>($"Students/{id}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in GetStudentAsync: " + ex.Message);
            return null;
        }
    }

    // PUT: api/Students/{id}
    public async Task<bool> UpdateStudentAsync(int id, Student student)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"Students/{id}", student);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in UpdateStudentAsync: " + ex.Message);
            return false;
        }
    }

    // POST: api/Students
    public async Task<Student> CreateStudentAsync(Student student)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("Students", student);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Student>();
            }
            else
            {
                Debug.WriteLine("Error in CreateStudentAsync: " + response.ReasonPhrase);
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in CreateStudentAsync: " + ex.Message);
            return null;
        }
    }

    // DELETE: api/Students/{id}
    public async Task<bool> DeleteStudentAsync(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"Students/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in DeleteStudentAsync: " + ex.Message);
            return false;
        }
    }

    // -----------------------
    // BOOKS METHODS
    // -----------------------

    // GET: api/Books
    public async Task<List<Book>> GetBooksAsync()
    {
        try
        {
            var books = await _http.GetFromJsonAsync<List<Book>>("Books");
            return books ?? new List<Book>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in GetBooksAsync: " + ex.Message);
            return new List<Book>();
        }
    }

    // GET: api/Books/{id}
    public async Task<Book> GetBookAsync(string id)
    {
        try
        {
            return await _http.GetFromJsonAsync<Book>($"Books/{id}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in GetBookAsync: " + ex.Message);
            return null;
        }
    }

    // PUT: api/Books/{id}
    public async Task<bool> UpdateBookAsync(string id, Book book)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"Books/{id}", book);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in UpdateBookAsync: " + ex.Message);
            return false;
        }
    }

    // POST: api/Books
    public async Task<Book> CreateBookAsync(Book book)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("Books", book);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Book>();
            }
            else
            {
                Debug.WriteLine("Error in CreateBookAsync: " + response.ReasonPhrase);
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in CreateBookAsync: " + ex.Message);
            return null;
        }
    }

    // DELETE: api/Books/{id}
    public async Task<bool> DeleteBookAsync(string id)
    {
        try
        {
            var response = await _http.DeleteAsync($"Books/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in DeleteBookAsync: " + ex.Message);
            return false;
        }
    }

    // -----------------------
    // BORROWS METHODS
    // -----------------------

    // GET: api/Borrows
    public async Task<List<Borrow>> GetBorrowsAsync()
    {
        try
        {
            var borrows = await _http.GetFromJsonAsync<List<Borrow>>("Borrows");
            return borrows ?? new List<Borrow>();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in GetBorrowsAsync: " + ex.Message);
            return new List<Borrow>();
        }
    }

    // GET: api/Borrows/{id}
    public async Task<Borrow> GetBorrowAsync(int id)
    {
        try
        {
            return await _http.GetFromJsonAsync<Borrow>($"Borrows/{id}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in GetBorrowAsync: " + ex.Message);
            return null;
        }
    }

    // PUT: api/Borrows/{id}
    public async Task<bool> UpdateBorrowAsync(int id, Borrow borrow)
    {
        try
        {
            var response = await _http.PutAsJsonAsync($"Borrows/{id}", borrow);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in UpdateBorrowAsync: " + ex.Message);
            return false;
        }
    }

    // POST: api/Borrows
    public async Task<Borrow> CreateBorrowAsync(Borrow borrow)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("Borrows", borrow);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Borrow>();
            }
            else
            {
                Debug.WriteLine("Error in CreateBorrowAsync: " + response.ReasonPhrase);
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in CreateBorrowAsync: " + ex.Message);
            return null;
        }
    }

    // DELETE: api/Borrows/{id}
    public async Task<bool> DeleteBorrowAsync(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"Borrows/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in DeleteBorrowAsync: " + ex.Message);
            return false;
        }
    }

    // GET: api/Borrows/bookNum/{bookNum} (Active Borrow)
    public async Task<Borrow> GetActiveBorrowByBookNumAsync(string bookNum)
    {
        try
        {
            var response = await _http.GetAsync($"Borrows/bookNum/{bookNum}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Borrow>();
            }
            else
            {
                Debug.WriteLine("Error in GetActiveBorrowByBookNumAsync: " + response.ReasonPhrase);
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in GetActiveBorrowByBookNumAsync: " + ex.Message);
            return null;
        }
    }

    // GET: api/Borrows/top/{topCount}
    public async Task<List<KeyValuePair<string, int>>> GetTopBorrowedBooksAsync(int topCount)
    {
        try
        {
            var response = await _http.GetAsync($"Borrows/top/{topCount}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<KeyValuePair<string, int>>>();
            }
            else
            {
                Debug.WriteLine("Error in GetTopBorrowedBooksAsync: " + response.ReasonPhrase);
                return new List<KeyValuePair<string, int>>();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error in GetTopBorrowedBooksAsync: " + ex.Message);
            return new List<KeyValuePair<string, int>>();
        }
    }
}
