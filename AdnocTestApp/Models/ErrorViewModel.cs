using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdnocTestApp.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }



    public class DepartmentListViewModel
    {
        public List<Department> Departments { get; set; } = new();
        public Department Department { get; set; } = new() {  Name = "Logistics",Location="Code Block"};

    }

    public class EditDepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Location { get; set; }
    }



        public class Department
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string? Location { get; set; }

            public List<Employee> Employees { get; set; } = new();
        }




 
        public class Employee
        {
            public int Id { get; set; }
            public int DepartmentId { get; set; }
            public string FullName { get; set; } = "";
            public string? Title { get; set; }
            public string? Email { get; set; }

            public Department? Department { get; set; }
        }
    


}
