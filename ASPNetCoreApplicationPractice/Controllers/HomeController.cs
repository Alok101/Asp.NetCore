using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreApplicationPractice.Models;
using ASPNetCoreApplicationPractice.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetCoreApplicationPractice.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        private IHostingEnvironment _hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            HomeDefaultViewsModel homeDefaultViewsModel = new HomeDefaultViewsModel()
            {
                AllEmployee = _employeeRepository.GetAllEmployee(),
                Title = "All Employee List"
            };
            return View("../Home/Default", homeDefaultViewsModel);
        }
        [AllowAnonymous]
        public ViewResult View(int? id)
        {
            Employee employee = _employeeRepository.GetEmployeeById(id.Value);
            if (employee == null)
            {
                return View("../Error/Error", id);
            }
            return View("../Home/View", _employeeRepository.GetEmployeeById(id ?? 1));
        }
        [HttpGet]
        public ViewResult Edit(int Id)
        {
            Employee employee = _employeeRepository.GetEmployeeById(Id);
            EmployeeEditViewModels employeeEditViewModels = new EmployeeEditViewModels
            {
                Id = employee.Id,
                Name = employee.Name,
                Address = employee.Address,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath,
                Salary = employee.Salary
            };
            return View("../Home/Edit", employeeEditViewModels);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModels model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployeeById(model.Id);
                employee.Name = model.Name;
                employee.Address = model.Address;
                employee.Department = model.Department;
                employee.Salary = model.Salary;
                employee.PhotoPath = model.ExistingPhotoPath;
                if (model.Photo != null)
                {
                    employee.PhotoPath = NewFile(model);
                    if (model.ExistingPhotoPath != null)
                    {
                        string existingFilePath = Path.Combine(Path.Combine(_hostingEnvironment.WebRootPath, "images"), model.ExistingPhotoPath);
                        System.IO.File.Delete(existingFilePath);
                    }

                }
                _employeeRepository.Update(employee);
                return RedirectToAction("view", "home", new { id = model.Id });
            }
            return View();
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModels model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    uniqueFileName = NewFile(model);
                    //string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    //uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    //string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    //model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Address = model.Address,
                    Department = model.Department,
                    Salary = model.Salary,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.AddNewEmployee(newEmployee);
                return RedirectToAction("View", "Home", new { id = newEmployee.Id });
            }
            return View();
        }
        [NonAction]
        public string NewFile(EmployeeCreateViewModels model)
        {

            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream= new FileStream(filePath, FileMode.Create))
            {
                model.Photo.CopyTo(fileStream);
            }
               
            return uniqueFileName;
        }
    }
}