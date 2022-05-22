using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models;
using System.Diagnostics;
using StaffAccounting.Models.Company;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace StaffAccounting.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly CompanyContext _companyContext;
        private readonly IEnumerable<Type> employeeTypes;

        public EmployeeController(ILogger<EmployeeController> logger, CompanyContext companyContext)
        {
            _logger = logger;
            _companyContext = companyContext;

            employeeTypes = Assembly.GetAssembly(typeof(Employee))?.GetTypes()
                .Where(type => 
                    type.IsSubclassOf(typeof(Employee)) && 
                    type.GetCustomAttribute<NameAttribute>() != null
                );
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
        [Route("Employee/Create/{employeeTypeName}")]
        public IActionResult Create(string employeeTypeName)
        {
            string employeeClassName = GetEmployeeTypeByAttributeName(employeeTypeName).Name;

            return View("Create" + employeeClassName);
        }

        [HttpPost]
        [Route("Employee/Create/{employeeTypeName}")]
        public IActionResult Create(EmployeeCreationModel employeeModel, string employeeTypeName)
        {
            string employeeClassName = GetEmployeeTypeByAttributeName(employeeTypeName).Name;
            if (!ModelState.IsValid)
                return View("Create" + employeeClassName, employeeModel);

            return Redirect("Index");
        }

        public IActionResult SelectType()
        {
            List<string> types = employeeTypes.Select(employeeType => employeeType.GetCustomAttribute<NameAttribute>().Name).ToList();
            ViewBag.EmployeeTypes = types;
            return View();
        }

        [HttpPost]
        public IActionResult SelectType(string employeeType)
        {
            return Redirect("/Employee/Create/" + System.Net.WebUtility.UrlEncode(employeeType));
        }

        [NonAction]
        private Type GetEmployeeTypeByAttributeName(string employeeTypeName)
        {
            return employeeTypes.First(type =>
                    type.GetCustomAttribute<NameAttribute>()?.Name == employeeTypeName
                    );
        }
    }
}