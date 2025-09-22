using AdnocTestApp1.Data;
using AdnocTestApp1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdnocTestApp1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] Department department)
        {
            if(ModelState.IsValid)
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                return Json(new { success = true, data = department });
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
                var existing = await _context.Departments.FindAsync(department.Id);
                if (existing == null) return Json(new { success = false, message = "Not found" });

                existing.Name = department.Name;
                existing.Location = department.Location;

                await _context.SaveChangesAsync();

                return Json(new { success = true, data = existing });
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
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null)
            {
                return Json(new { success = false, message = "Department not found." });
            }

            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync();

            return Json(new { success = true, id = id });
        }

        public IActionResult Index()
        {
            var items = _context.Departments.ToList();
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
