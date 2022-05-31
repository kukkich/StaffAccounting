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
                Rank rank = (await _companyContext.Ranks
                    .ToListAsync())
                    .FirstOrDefault(rank => rank.Id == id);
                if (rank != null)
                {
                    await rank.JoinFromDatabaseAsync(_companyContext);
                    return View(rank);
                }
            }

            return NotFound();
        }
    }
}
