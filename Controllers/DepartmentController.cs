using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffAccounting.Models.Company;

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
                Department department = await _companyContext.Departments
                    .FirstOrDefaultAsync(department => department.Id == id);

                if (department != null)
                {
                    await department.JoinFromDatabaseAsync(_companyContext);
                    return View(department);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Department department = await _companyContext.Departments.FirstOrDefaultAsync(department => department.Id == id);
                if (department != null)
                {
                    department.BeforeDeletion(_companyContext);
                    _companyContext.Departments.Remove(department);
                    await _companyContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _companyContext.Departments.Add(department);
                await _companyContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Department department = await _companyContext.Departments
                    .FirstOrDefaultAsync(department => department.Id == id);
                if (department != null)
                {
                    return View(department);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Department department)
        {
            if (ModelState.IsValid)
            {
                _companyContext.Departments.Update(department);
                await _companyContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Edit", department);
        }
    }
}
