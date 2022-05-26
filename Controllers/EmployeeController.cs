using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models;
using System.Diagnostics;
using StaffAccounting.Models.Company;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using StaffAccounting.Extensions;

namespace StaffAccounting.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly CompanyContext _companyContext;
        private readonly EmployeeTypesProvider _typesProvider;

        public EmployeeController(ILogger<EmployeeController> logger, CompanyContext companyContext)
        {
            _logger = logger;
            _companyContext = companyContext;
            _typesProvider = new EmployeeTypesProvider();
        }

        public async Task<IActionResult> Index()
        {
            return View(await _companyContext.Employees.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpGet]
        [Route("Employee/Create/{employeeAttributeName}")]
        public IActionResult Create(string employeeNotation)
        {
            string employeeClassName = _typesProvider.GetClassNameByNotation(employeeNotation.DecodeAsUrl());
            ViewBag.Company = _companyContext;
            return View("Create" + employeeClassName);
        }

        [HttpPost]
        [Route("Employee/Create/{employeeAttributeName}")]
        public IActionResult Create(EmployeeCreationModel employeeModel, string employeeNotation)
        {
            Type employeeType = _typesProvider.GetTybeByNotation(employeeNotation.DecodeAsUrl());

            ViewBag.Company = _companyContext;
            if (!ModelState.IsValid)
                return View("Create" + employeeType.Name, employeeModel);

            Employee newEmployee = _typesProvider.CreateEmployee(employeeType, employeeModel);
            
            _companyContext.Employees.Add(newEmployee);

            _companyContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult SelectType()
        {
            List<string> notations = _typesProvider.GetNotations().ToList();
            ViewBag.EmployeeTypeNames = notations;
            return View();
        }

        [HttpPost]
        public IActionResult SelectType(string employeeType)
        {
            return Redirect("/Employee/Create/" + employeeType.EncodeAsUrl());
        }
    }
}