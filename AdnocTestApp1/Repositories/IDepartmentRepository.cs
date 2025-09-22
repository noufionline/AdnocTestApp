using AdnocTestApp1.Data;
using AdnocTestApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace AdnocTestApp1.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department> CreateAsync(Department department);
        Task<Department?> FindAsync(int id);
        Task<Department> UpdateAsync(Department department);
        Task DeleteAsync(Department department);
        Task<List<Department>> GetAllAsync();
    }

    public class DepartmentRepository : IDepartmentRepository
{
    private readonly AppDbContext _context;

    public DepartmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Department> CreateAsync(Department department)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        return department;
    }

    public async Task<Department?> FindAsync(int id)
    {
        return await _context.Departments.FindAsync(id);
    }

    public async Task<Department> UpdateAsync(Department department)
    {
        _context.Departments.Update(department);
        await _context.SaveChangesAsync();
        return department;
    }

    public async Task DeleteAsync(Department department)
    {
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Department>> GetAllAsync()
    {
        return await _context.Departments.ToListAsync();
    }
}
}
