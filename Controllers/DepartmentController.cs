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
    public class DepartmentController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly CompanyContext _companyContext;

        public DepartmentController(ILogger<EmployeeController> logger, CompanyContext companyContext)
        {
            _logger = logger;
            _companyContext = companyContext;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _companyContext.Departments.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Department department = (await _companyContext.Departments
                    .ToListAsync())
                    .FirstOrDefault(department => department.Id == id);

                if (department != null)
                {
                    await department.JoinFromDatabaseAsync(_companyContext);
                    return View(department);
                }
            }

            return NotFound();
        }
    }
}
