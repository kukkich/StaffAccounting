using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffAccounting.Models.Company;
using StaffAccounting.Models.ViewModels;

namespace StaffAccounting.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly CompanyContext _companyContext;
        private const int _pageSize = 10;

        public ProjectController(ILogger<EmployeeController> logger, CompanyContext companyContext)
        {
            _logger = logger;
            _companyContext = companyContext;
        }

        public IActionResult Index(int page = 1)
        {
            if (page < 0)
            {
                return NotFound();
            }
            try
            {
                Pagination<Project> viewModel = new(_companyContext.Projects, _pageSize);
                viewModel.PageNumber = page;
                return View(viewModel);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Project project = await _companyContext.Projects
                    .FirstOrDefaultAsync(project => project.Id == id);

                if (project != null)
                {
                    await project.JoinFromDatabaseAsync(_companyContext);
                    return View(project);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Project project = await _companyContext.Projects
                    .FirstOrDefaultAsync(project => project.Id == id);
                if (project != null)
                {
                    project.BeforeDeletion(_companyContext);
                    _companyContext.Projects.Remove(project);
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
        public async Task<IActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _companyContext.Projects.Add(project);
                await _companyContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Project project = await _companyContext.Projects
                    .FirstOrDefaultAsync(project => project.Id == id);
                if (project != null)
                {
                    return View(project);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Project project)
        {
            if (ModelState.IsValid)
            {
                _companyContext.Projects.Update(project);
                await _companyContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Edit", project);
        }
    }
}
