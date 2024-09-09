using System.ComponentModel.DataAnnotations;

namespace EmployeePortal.web.Models.Domain
{
    public class Employee
    {
        public int Id { get; set; } // Assuming an auto-incrementing integer ID
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; } = "";
        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; } = "";
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; } = "";
        [Required(ErrorMessage = "Job title is required")]
        public string JobTitle { get; set; } = "";
        [Required(ErrorMessage = "Salary is required")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "Date Hired is required")]
        public DateTime HireDate { get; set; }
    }
}
