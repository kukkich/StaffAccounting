using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffAccounting.Models.Company;

namespace StaffAccounting.Controllers
{
    public class RankController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly CompanyContext _companyContext;

        public RankController(ILogger<EmployeeController> logger, CompanyContext companyContext)
        {
            _logger = logger;
            _companyContext = companyContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _companyContext.Ranks.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Rank rank = await _companyContext.Ranks
                    .FirstOrDefaultAsync(rank => rank.Id == id);
                if (rank != null)
                {
                    await rank.JoinFromDatabaseAsync(_companyContext);
                    return View(rank);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Rank rank = await _companyContext.Ranks
                    .FirstOrDefaultAsync(rank => rank.Id == id);
                if (rank != null)
                {
                    rank.BeforeDeletion(_companyContext);
                    _companyContext.Ranks.Remove(rank);
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
        public async Task<IActionResult> Create(Rank rank)
        {
            if (ModelState.IsValid)
            {
                _companyContext.Ranks.Add(rank);
                await _companyContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rank);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Rank rank = await _companyContext.Ranks
                    .FirstOrDefaultAsync(rank => rank.Id == id);
                if (rank != null)
                {
                    return View(rank);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Rank rank)
        {
            if (ModelState.IsValid)
            {
                _companyContext.Ranks.Update(rank);
                await _companyContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Edit", rank);
        }
    }
}
