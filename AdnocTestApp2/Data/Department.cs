using Microsoft.EntityFrameworkCore;
using AdnocTestApp2.Data;

namespace AdnocTestApp2.Data
{
    public class Department
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }

    }


    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var department = modelBuilder.Entity<Department>();

            department.ToTable("Departments");
            department.HasKey(d => d.Id);    
            department.Property(x=> x.Id).ValueGeneratedOnAdd();
            department.Property(x => x.Name).HasMaxLength(100);
            department.Property(x=> x.Location).HasMaxLength(100);

            
        }

public DbSet<AdnocTestApp2.Data.Department> Department { get; set; } = default!;
        
    }
}
