using EmployeePortal.web.Models.Domain;

namespace EmployeePortal.web.Models
{
    public class IndexViewModel
    {
        public List<Employee>? Employees { get; set; }
        public List<string>? Suggestions { get; set; }
    }
}
