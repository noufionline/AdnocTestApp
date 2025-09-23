using AdnocTestApp2.Data;

namespace AdnocTestApp2.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department> CreateAsync(Department department);    
        Task<Department> UpdateAsync(Department department);
        Task DeleteAsync(Department department);
        Task<Department?> FindAsync(int id);
        Task<List<Department>> GetAllAsync();

        Task<bool> DepartmentExists(int id);
       
    }
}
