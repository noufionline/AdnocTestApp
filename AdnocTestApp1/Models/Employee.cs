namespace AdnocTestApp1.Models
{
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
