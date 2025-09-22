using AdnocTestApp1.Data;
using AdnocTestApp1.Models;
using AdnocTestApp1.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdnocTestApp1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IDepartmentRepository _repository;

        public HomeController(ILogger<HomeController> logger,IDepartmentRepository repository)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] Department department)
        {
            if(ModelState.IsValid)
            {
                var item = await _repository.CreateAsync(department);
                return Json(new { success = true, data = item });
            }

            return Json(new 
            { 
                 success = false, 
                 errors = ModelState
                            .Values
                            .SelectMany(x=> x.Errors)
                            .Select(x=> x.ErrorMessage)
                            .ToList() 
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDepartment([FromBody] Department department)
        {
            if (ModelState.IsValid)
            {
                var existing = await _repository.FindAsync(department.Id);
                if (existing == null) return Json(new { success = false, message = "Not found" });

                existing.Name = department.Name;
                existing.Location = department.Location;

                var updatedDepartment = await _repository.UpdateAsync(existing);

                return Json(new { success = true, data = updatedDepartment });
            }

            return Json(new
            {
                success = false,
                errors = ModelState.Values
                                                    .SelectMany(v => v.Errors)
                                                    .Select(e => e.ErrorMessage)
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDepartment([FromBody] int id)
        {
            var dept = await _repository.FindAsync(id);
            if (dept == null)
            {
                return Json(new { success = false, message = "Department not found." });
            }

            await _repository.DeleteAsync(dept);

            return Json(new { success = true, id = id });
        }

        public async Task<IActionResult> Index()
        {
            var items = await _repository.GetAllAsync();
            return View(items);
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
}
