using LibraryManagementMaui.Models;
using System;
using System.Collections.Generic;
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


    public async Task<List<Student>> GetStudentsAsync()
    {
        return await _http.GetFromJsonAsync<List<Student>>("Students") ?? new List<Student>();
    }

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


}
