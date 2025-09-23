using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdnocTestApp2.Data;
using AdnocTestApp2.Repositories;

namespace AdnocTestApp2.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentsController(IDepartmentRepository repository)
        {
           _repository = repository;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _repository.FindAsync(id.Value);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Department department)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateAsync(department);
                return Json(new { success = true, data=department });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x=> x.Errors).Select(x=> x.ErrorMessage).ToList()});
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _repository.FindAsync(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _repository.UpdateAsync(department);
                    return Json(new { success = true, data = department });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DepartmentExists(department.Id))
                    {
                        return Json(new { success = false, errors = $"This department ({department.Id}) not found in the database" });

                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() });
         


        }

        // GET: Departments/Delete/5

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var department = await _repository.FindAsync(id.Value);
        //    if (department == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(department);
        //}

        // POST: Departments/Delete/5
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var department = await _repository.FindAsync(id);
            if (department != null)
            {
              await _repository.DeleteAsync(department);
            }

            
            return RedirectToAction(nameof(Index));
        }

        private Task<bool> DepartmentExists(int id)
        {
            return _repository.DepartmentExists(id);
        }
    }
}
