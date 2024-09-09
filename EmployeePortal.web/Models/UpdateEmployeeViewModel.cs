using System.ComponentModel.DataAnnotations;

namespace EmployeePortal.web.Models
{
    public class UpdateEmployeeViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be greater than or equal to 0.")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Job Title is required.")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Hire Date is required.")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
    }
}