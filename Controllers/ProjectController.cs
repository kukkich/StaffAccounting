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
    public class ProjectController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly CompanyContext _companyContext;

        public ProjectController(ILogger<EmployeeController> logger, CompanyContext companyContext)
        {
            _logger = logger;
            _companyContext = companyContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _companyContext.Projects.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Project project = (await _companyContext.Projects
                    .ToListAsync())
                    .FirstOrDefault(project => project.Id == id);
                if (project != null)
                {
                    await project.JoinFromDatabaseAsync(_companyContext);
                    return View(project);
                }
            }

            return NotFound();
        }
    }
}
