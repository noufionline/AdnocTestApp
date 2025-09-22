using AdnocTestApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace AdnocTestApp.Components
{
    public class DepartmentListViewComponent : ViewComponent
    {
        readonly AppDbContext _dbContext;
        public DepartmentListViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<IViewComponentResult> InvokeAsync()
        {
            var departments = _dbContext.Departments.ToList();
            return Task.FromResult<IViewComponentResult>(View(departments));
        }
    }
}
