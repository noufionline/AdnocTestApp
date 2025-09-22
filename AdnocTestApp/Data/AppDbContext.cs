

using AdnocTestApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AdnocTestApp.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        
        public DbSet<Department> Departments { get; set; }  
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<Employee>().ToTable("Employees");
            model.Entity<Employee>().HasKey(e=> e.Id);
            model.Entity<Employee>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();

            model.Entity<Employee>().Property(e => e.FullName).HasMaxLength(100).IsRequired();
            model.Entity<Employee>().HasIndex(e => new { e.DepartmentId, e.FullName }).HasDatabaseName("IX_Employees_DepartmentId_FullName");
           


            model.Entity<Department>().ToTable("Departments");
            model.Entity<Department>().HasKey(d => d.Id);
            model.Entity<Department>().Property(d=> d.Id).IsRequired().ValueGeneratedOnAdd();


            model.Entity<Department>().Property(d => d.Name).IsRequired().HasMaxLength(100);
            model.Entity<Department>().Property(d => d.Location).HasMaxLength(200);
            model.Entity<Department>()
                    .HasMany(x => x.Employees)
                    .WithOne(x => x.Department)
                    .HasForeignKey(x => x.DepartmentId);


            model.Entity<Department>().HasIndex(d => d.Name).HasDatabaseName("IX_Departments_Name");

        }


    }
}
