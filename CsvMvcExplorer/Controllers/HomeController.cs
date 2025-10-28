using Microsoft.AspNetCore.Mvc;
using CsvMvcExplorer.Helpers;
using CsvMvcExplorer.Models;
using System.Linq;
using System.Diagnostics; // For error view if needed

namespace CsvMvcExplorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<Person> _people;
        private readonly ILogger<HomeController> _logger; // Not used much, but standard

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            // Load CSV—path relative to wwwroot
            _people = CsvReader.ReadCsv("wwwroot/data/people.csv");
        }

        public IActionResult Index()
        {
            return View(_people);
        }

        [HttpGet]
        public IActionResult FilterByCity(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return RedirectToAction("Index");
            }

            // LINQ: Case-insensitive filter (Where requirement)
            var filtered = _people
                .Where(p => p.City.Contains(city, StringComparison.OrdinalIgnoreCase))
                .ToList();

            ViewBag.Query = $"Filtered by city containing '{city}' ({filtered.Count} results)";
            return View("Index", filtered);
        }

        [HttpGet]
        public IActionResult SortByAge(bool descending = false)
        {
            // LINQ: Order by age
            var sorted = descending
                ? _people.OrderByDescending(p => p.Age).ToList()
                : _people.OrderBy(p => p.Age).ToList();

            ViewBag.Query = descending ? "Sorted by age (oldest first)" : "Sorted by age (youngest first)";
            return View("Index", sorted);
        }

        [HttpGet]
        public IActionResult AverageAgeByCity()
        {
            // LINQ: Group and average
            var averages = _people
                .GroupBy(p => p.City)
                .Select(g => new CityAverage
                {
                    City = g.Key,
                    AverageAge = Math.Round(g.Average(p => p.Age), 1)
                })
                .OrderBy(a => a.AverageAge)
                .ToList();

            ViewBag.Query = "Average age per city";
            return View(averages);
        }

        [HttpGet]
        public IActionResult SelectCities()
        {
            // LINQ Select: Pull just the City property from all people (Select requirement)
            var cities = _people
                .Select(p => p.City)
                .Distinct()  // Extra: Distinct() for unique cities
                .OrderBy(c => c)
                .ToList();

            ViewBag.Query = $"Unique cities selected from dataset ({cities.Count} total)";
            return View("SelectResult", cities);  // New view for strings list
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    // Simple view model for averages (anonymous types don't play nice with views)
    public class CityAverage
    {
        public string City { get; set; } = string.Empty;
        public double AverageAge { get; set; }
    }
}