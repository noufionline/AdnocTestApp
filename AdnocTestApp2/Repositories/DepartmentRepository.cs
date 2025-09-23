using AdnocTestApp2.Data;
using Microsoft.EntityFrameworkCore;

namespace AdnocTestApp2.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Department> CreateAsync(Department department)
        {
            _context.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task DeleteAsync(Department department)
        {
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
        }

        public Task<bool> DepartmentExists(int id)
        {
            return _context.Department.AnyAsync(x => x.Id == id);
        }

        public Task<Department?> FindAsync(int id)
        {
            return _context.Department
                 .FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<List<Department>> GetAllAsync()
        {
            return _context.Department.ToListAsync();
        }

        public async Task<Department> UpdateAsync(Department department)
        {
            _context.Update(department);
            await _context.SaveChangesAsync();
            return department;
        }
    }
}
