using System;
using Microsoft.AspNetCore.Mvc;
using EmployeePortal.web.Models;
using EmployeePortal.web.Models.Domain;
using EmployeePortal.web.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;




namespace EmployeePortal.web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        private int currentId = 0;

        [BindProperty]
        public List<int> selectedIds { get; set; }

        public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchInput, string searchType)
        {
            var employees = await mvcDemoDbContext.Employees.OrderBy(e => e.Id).ToListAsync();

            if (!string.IsNullOrEmpty(searchInput))
            {
                if (searchType == "name")
                {

                    switch ("name")
                    {
                        case "First Name":
                            employees = employees.Where(n => n.FirstName.ToLower().Contains(searchInput.ToLower(), StringComparison.CurrentCultureIgnoreCase)).ToList();

                            break;
                        case "Last Name":
                            employees = employees.Where(n => n.LastName.ToLower().Contains(searchInput.ToLower())).ToList();
                            break;
                    }

                }
                else if (searchType == "department")
                {
                    employees = employees.Where(n => n.Department.ToLower().Contains(searchInput.ToLower())).ToList();

                }
            }

            return View(new IndexViewModel { Employees = employees, Suggestions = employees.Select(e => e.FirstName).ToList() });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                //Id = GuId.NewGuId(),
                DateCreated = DateTime.Now.ToLocalTime(),
                FirstName = addEmployeeRequest.FirstName,
                LastName = addEmployeeRequest.LastName,
                Email = addEmployeeRequest.Email,
                Department = addEmployeeRequest.Department,
                Salary = addEmployeeRequest.Salary,
                JobTitle = addEmployeeRequest.JobTitle,
                HireDate = addEmployeeRequest.HireDate


            };
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                // You can display error messages using ViewData or TempData
                return Add();
            }

            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Add");

        }


        [HttpGet]
        public async Task<IActionResult> View(int Id)
        {
            var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync( x => x.Id == Id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    //Id = employee.Id,
                    DateCreated = employee.DateCreated,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Department = employee.Department,
                    Salary = employee.Salary,
                    JobTitle = employee.JobTitle,
                    HireDate = employee.HireDate   
                };
                    
                return await Task.Run(() => View("View", viewModel));
            }
            

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Email = model.Email;
                employee.Department = model.Department;
                employee.Salary = model.Salary;
                employee.JobTitle = model.JobTitle;
                employee.HireDate = model.HireDate;
                

                await mvcDemoDbContext.SaveChangesAsync();


                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Employee model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteSelected(int[] selectedIds)
        {
            if (selectedIds != null)
            {
                foreach (var id in selectedIds)
                {
                    var employee = mvcDemoDbContext.Employees.Find(id);
                    // Your logic to delete the employee by id
                    mvcDemoDbContext.Employees.Remove(employee);
                    mvcDemoDbContext.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            //if (selectedIds == null || selectedIds.Length == 0)
            //{
            //    // throw error
            //    ModelState.AddModelError("", "No item selected to delete");
            //    return View();

            //}

            ////bind the task collection into list
            //var selectedIdArray = selectedIds;
            //List<int> TaskIds = selectedIdArray.Select(id => Int32.Parse(selectedIds)).ToList();


            //for (var i = 0; i < TaskIds.Count(); i++)
            //{
            //    var todo = mvcDemoDbContext.Employees.Find(TaskIds[i]);
            //    //remove the record from the database
            //    mvcDemoDbContext.Employees.Remove(todo);
            //    //call save changes action otherwise the table will not be updated
            //    mvcDemoDbContext.SaveChanges();

            //}
            // Find employees by their IDs
            //var employeesToDelete = await mvcDemoDbContext.Employees
            //                               .Where(e => selectedIds.Contains(e.Id))
            //                               .ToListAsync();
            //if (selectedIds != null && selectedIds.Any())
            //{
            //    if (IsConfirmedToDelete(selectedIds)) // Updated function name
            //    {
            //        // Remove the found employees
            //        mvcDemoDbContext.Employees.RemoveRange(employeesToDelete);
            //        await mvcDemoDbContext.SaveChangesAsync();
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        // Handle cancellation (e.g., redirect back to the index page)
            //        return RedirectToAction("Index");
            //    }
            //}

            // Handle no selected employees
            TempData["ErrorMessage"] = "Please select at least one employee to delete.";
            return RedirectToAction("Index");
        }

        private bool IsConfirmedToDelete(List<int> selectedIds)
        {
            var confirmationMessage = $"Are you sure you want to delete {selectedIds.Count} employees?";
            return this.HttpContext.Request.Query["confirmDelete"] == "true"; // Check for confirmation query string parameter
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAll()
        {
            // Get all employees from the database
            var allEmployees = await mvcDemoDbContext.Employees.ToListAsync();

            // Check if any employees were found
            if (allEmployees.Any())
            {
                // Remove all employees
                mvcDemoDbContext.Employees.RemoveRange(allEmployees);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Redirect back to the Index action
            return RedirectToAction("Index");
        }
    }
}
