using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Text.Json;
using Movie.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Movie.Controllers
{
    public class MovieController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public MovieController(IConfiguration configuration)
        {
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // Bypass SSL errors for local dev
            };

            _httpClient = new HttpClient(handler);
            _apiUrl = $"{configuration["ApiSettings:BaseUrl"]}/Movie"; // Fetch API URL from appsettings.json
        }

        public async Task<IActionResult> Index()
        {
            List<MovieModel> movies = new List<MovieModel>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    movies = JsonSerializer.Deserialize<List<MovieModel>>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    ViewData["Error"] = $"API Error: {response.StatusCode} - {error}";
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Exception: {ex.Message}";
            }

            return View(movies);
        }

        public async Task<IActionResult> Detail(int id)
        {
            MovieModel movie = null;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    movie = JsonSerializer.Deserialize<MovieModel>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    ViewData["Error"] = $"API Error: {response.StatusCode} - {error}";
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Exception: {ex.Message}";
            }

            return View(movie);
        }
    }
}
