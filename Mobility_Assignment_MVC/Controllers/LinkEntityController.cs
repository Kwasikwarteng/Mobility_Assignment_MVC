using Microsoft.AspNetCore.Mvc;
using Mobility_Assignment_MVC.Models;
using Mobility_Assignment_MVC.Models.Data;

namespace Mobility_Assignment_MVC.Controllers
{
    public class LinkEntityController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public LinkEntityController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
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
                    return View("DeleteConfirmation", person);
                }
                else
                {
                    ViewBag.ErrorMessage = "Person not found!";
                }
            }
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

            return RedirectToAction("Index");
        }
        //Get List of People
        public IActionResult List()
        {
            IEnumerable<Person> personsList = _applicationDbContext.Persons;
            return View(personsList);
        }


    }
}
