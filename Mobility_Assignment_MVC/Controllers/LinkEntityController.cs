using Microsoft.AspNetCore.Mvc;
using Mobility_Assignment_MVC.Models;
using Mobility_Assignment_MVC.Models.Data;

namespace Mobility_Assignment_MVC.Controllers
{
    public class LinkEntityController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly HttpClient _httpClient;
        public LinkEntityController(ApplicationDbContext applicationDbContext, IHttpClientFactory httpClientFactory)
        {
            _applicationDbContext = applicationDbContext;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7272/swagger/index.html");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        public IActionResult Index()
        {
            return View();
        }

        //Get For Add
        public IActionResult Add()
        {
            return View();
        }

        //Post Action Methd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPost(Person person)
        {
            if (ModelState.IsValid)
            {
                _applicationDbContext.Add(person);
                _applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["SuccessMessage"] = "Person deleted successfully.";
            return View(person);
        }

        //Get - Delete Record
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        //Post - Handle Deletion Logic
        [HttpPost]
        public IActionResult Delete(string firstName, string lastName)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                var person = _applicationDbContext.Persons.FirstOrDefault(x => x.First_Name == firstName && x.Last_Name == lastName);
                if (person != null)
                {
                    return View("ConfirmDelete", person);
                }
                else
                {
                    ViewBag.ErrorMessage = "Person not found!";
                }
            }
            return View();
        }

        //Get - Delete Record
        [HttpGet]
        public IActionResult ConfirmDelete()
        {
            return View();
        }

        //Confirm Delte
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var personToDelete = _applicationDbContext.Persons.Find(id);
            if (personToDelete == null)
            {
                return NotFound();
            }

            _applicationDbContext.Persons.Remove(personToDelete);
            _applicationDbContext.SaveChanges();

            TempData["SuccessMessage"] = "Person deleted successfully.";

            return RedirectToAction("Index");
        }
        //Get List of People
        public IActionResult List()
        {
            IEnumerable<Person> personsList = _applicationDbContext.Persons;
            return View(personsList);
        }

        //Get - Delete Record
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string searchTerm)
        {
            // Perform the search operation based on the searchTerm
            // For example, if you're searching for a person by name:
            var searchResults = _applicationDbContext.Persons
                                    .Where(p => p.First_Name.Contains(searchTerm) || p.Last_Name.Contains(searchTerm))
                                    .ToList();

            // Redirect to the GET action method to display the search results
            return RedirectToAction("SearchResults", new { searchTerm = searchTerm });


        }

        [HttpGet]
        public IActionResult SearchResults(string searchTerm)
        {
            // Perform the search operation based on the searchTerm
            // For example, if you're searching for a person by name:
            var searchResults = _applicationDbContext.Persons
                                    .Where(p => p.First_Name.Contains(searchTerm) || p.Last_Name.Contains(searchTerm))
                                    .ToList();

            // Pass the search results to the view
            return View(searchResults);
        }

        //public async Task<IActionResult> AddRecordViaAPI(Person person)
        //{
        //    try
        //    {
        //        var response = await _httpClient.PostAsJsonAsync("api/WebServiceHome/AddRecord", person);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Record added successfully
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            // Handle failure scenario
        //            // You might want to log the error or display an error message to the user
        //            TempData["ErrorMessage"] = "Failed to add record via API.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions
        //        TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
        //    }

        //    return RedirectToAction("Add"); // Redirect back to the add record page
        //}

        public IActionResult WebService()
        {
            // Redirect to the Web API endpoint or perform any other logic to access the API service
            return Redirect("https://localhost:7272/swagger/index.html"); // Replace with your actual Web API URL
        }
    }


}
