using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffAccounting.Models;
using StaffAccounting.Models.Company;
using StaffAccounting.Models.Filtration;
using StaffAccounting.Models.ViewModels;
using StaffAccounting.Models.ViewProviders;
using System.Diagnostics;

namespace StaffAccounting.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly CompanyContext _companyContext;
        private readonly EmployeeNotationFactory _factory;
        private readonly IViewProvider _viewProvider;
        private const int _pageSize = 6;
        public EmployeeController(ILogger<EmployeeController> logger, CompanyContext companyContext)
        {
            _viewProvider = new ViewProvider();
            _logger = logger;
            _companyContext = companyContext;
            _factory = new EmployeeNotationFactory();
        }

        public IActionResult Index(int page = 1, [FromQuery] string requiredNotation = null)
        {
            if (page < 0)
            {
                return NotFound();
            }
            try
            {
                IndexViewModel viewModel = new(_companyContext, page, Request.Query, requiredNotation);
                return View(viewModel);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Create
        // TODO concatinate in one Get method
        [HttpGet]
        public IActionResult SelectType()
        {
            List<string> notations = _factory.Provider.Notations.ToList();
            ViewBag.EmployeeTypeNames = notations;
            return View();
        }

        [HttpPost]
        public IActionResult SelectType(string employeeNotation)
        {
            Employee employee = _factory.CreateEmployee(employeeNotation);
            ViewResult view = employee.GetView(_viewProvider, HTTPActions.Create);
            view.ViewData.Add("Company", _companyContext);
            return view;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountant(Accountant employee)
        {
            if (!ModelState.IsValid)
            {
                ViewResult view = employee.GetView(_viewProvider, HTTPActions.Create);
                view.ViewData.Add("Company", _companyContext);
                return view;
            }
            return await Create(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartmentHead(DepartmentHead employee)
        {
            if (!ModelState.IsValid)
            {
                ViewResult view = employee.GetView(_viewProvider, HTTPActions.Create);
                view.ViewData.Add("Company", _companyContext);
                return view;
            }
            return await Create(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDirector(Director employee)
        {
            if (!ModelState.IsValid)
            {
                ViewResult view = employee.GetView(_viewProvider, HTTPActions.Create);
                view.ViewData.Add("Company", _companyContext);
                return view;
            }
            return await Create(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateManager(Manager employee)
        {
            if (!ModelState.IsValid)
            {
                ViewResult view = employee.GetView(_viewProvider, HTTPActions.Create);
                view.ViewData.Add("Company", _companyContext);
                return view;
            }
            return await Create(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorker(Worker employee)
        {
            if (!ModelState.IsValid)
            {
                ViewResult view = employee.GetView(_viewProvider, HTTPActions.Create);
                view.ViewData.Add("Company", _companyContext);
                return view;
            }
            return await Create(employee);
        }
        #endregion Create

        #region Edit
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
            if (!ModelState.IsValid)
            {
                ViewResult view = employee.GetView(_viewProvider, HTTPActions.Update);
                view.ViewData.Add("Company", _companyContext);
                return view;
            }
            return await Update(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditDepartmentHead(DepartmentHead employee)
        {
            if (!ModelState.IsValid)
            {
                ViewResult view = employee.GetView(_viewProvider, HTTPActions.Update);
                view.ViewData.Add("Company", _companyContext);
                return view;
            }
            return await Update(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditDirector(Director employee)
        {
            if (!ModelState.IsValid)
            {
                ViewResult view = employee.GetView(_viewProvider, HTTPActions.Update);
                view.ViewData.Add("Company", _companyContext);
                return view;
            }
            return await Update(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditManager(Manager employee)
        {
            if (!ModelState.IsValid)
            {
                ViewResult view = employee.GetView(_viewProvider, HTTPActions.Update);
                view.ViewData.Add("Company", _companyContext);
                return view;
            }
            return await Update(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditWorker(Worker employee)
        {
            if (!ModelState.IsValid)
            {
                ViewResult view = employee.GetView(_viewProvider, HTTPActions.Update);
                view.ViewData.Add("Company", _companyContext);
                return view;
            }
            return await Update(employee);
        }
        #endregion Edit

        [HttpPost]
        public async Task<IActionResult> Raise(int? id)
        {
            if (id != null)
            {
                Employee employee = await _companyContext.Employees.FirstOrDefaultAsync(employee => employee.Id == id);
                if (employee != null && employee.CanBeRaised)
                {
                    Employee raised = employee.GetRisedEmployee();
                    employee.JoinFromDatabase(_companyContext);
                    _companyContext.Employees.Remove(employee);
                    _companyContext.Employees.Add(raised);
                    await _companyContext.SaveChangesAsync();
                    return await Details(raised.Id);
                }
            }
            return NotFound();
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

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Employee employee = await _companyContext.Employees.FirstOrDefaultAsync(employee => employee.Id == id);
                if (employee != null)
                {
                    _companyContext.Employees.Remove(employee);
                    await _companyContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        [NonAction]
        private async Task<IActionResult> Create(Employee employee)
        {
            _companyContext.Employees.Add(employee);
            await _companyContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [NonAction]
        private async Task<IActionResult> Update(Employee employee)
        {
            _companyContext.Employees.Update(employee);
            await _companyContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}