using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models;
using System.Diagnostics;
using StaffAccounting.Models.Company;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using StaffAccounting.Extensions;
using System.Threading.Tasks;
using StaffAccounting.Models.VieweProviders;

namespace StaffAccounting.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly CompanyContext _companyContext;
        private readonly EmployeeNotationFactory _factory;
        private readonly IViewProvider _viewProvider;

        public EmployeeController(ILogger<EmployeeController> logger, CompanyContext companyContext)
        {
            _viewProvider = new ViewProvider();
            _logger = logger;
            _companyContext = companyContext;
            _factory = new EmployeeNotationFactory();
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _companyContext.Employees.ToListAsync();
            return View(employees);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Route("Employee/Create/{employeeNotation}")]
        public IActionResult Create(string employeeNotation)
        {
            string employeeClassName = _factory.GetClassName(employeeNotation.DecodeAsUrl());
            ViewBag.Company = _companyContext;
            return View("Create" + employeeClassName);
        }

        [HttpPost]
        [Route("Employee/Create/{employeeNotation}")]
        public IActionResult Create(EmployeeCreationModel employeeModel, string employeeNotation)
        {
            Type employeeType = _factory.GetTybeByNotation(employeeNotation.DecodeAsUrl());

            ViewBag.Company = _companyContext;
            if (!ModelState.IsValid)
                return View("Create" + employeeType.Name, employeeModel);

            Employee newEmployee = _factory.CreateEmployee(employeeType, employeeModel);

            _companyContext.Employees.Add(newEmployee);

            _companyContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult SelectType()
        {
            List<string> notations = _factory.Notations.ToList();
            ViewBag.EmployeeTypeNames = notations;
            return View();
        }

        [HttpPost]
        public IActionResult SelectType(string employeeType)
        {
            return Redirect("/Employee/Create/" + employeeType.EncodeAsUrl());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Employee employee = (await _companyContext.Employees
                    .ToListAsync())
                    .FirstOrDefault(employee => employee.Id == id);

                if (employee != null)
                {
                    employee.JoinFromDatabase(_companyContext);
                    ViewResult view = employee.GetView(_viewProvider, HTTPActions.Read);
                    return view;
                }
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Employee employee = await _companyContext.Employees
                    .FirstOrDefaultAsync(employee => employee.Id == id);
                if (employee != null)
                {
                    employee.JoinFromDatabase(_companyContext);
                    ViewResult view = employee.GetView(_viewProvider, HTTPActions.Update);
                    view.ViewData.Add("Company", _companyContext);
                    return view;
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditAccountant(Accountant employee)
        {
            return await UpdateEmployee(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditDepartmentHead(DepartmentHead employee)
        {
            return await UpdateEmployee(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditDirector(Director employee)
        {
            return await UpdateEmployee(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditManager(Manager employee)
        {
            return await UpdateEmployee(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditWorker(Worker employee)
        {
            return await UpdateEmployee(employee);
        }

        [NonAction]
        private async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            _companyContext.Employees.Update(employee);
            await _companyContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}