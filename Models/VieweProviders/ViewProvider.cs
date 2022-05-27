using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.Company;

namespace StaffAccounting.Models.VieweProviders
{
    [NonController]
    public class ViewProvider : Controller, IViewProvider
    {
        private const string ViewPath = "Views";
        private readonly Dictionary<HTTPActions, string> _pathsByHttpAction = new Dictionary<HTTPActions, string>
        {
            [HTTPActions.Read] = "Details",
            [HTTPActions.Create] = "Create",
            [HTTPActions.Update] = "Edit",
            [HTTPActions.Delete] = "Delete"
        };

        public ViewResult Accountant(Accountant accountant, HTTPActions action)
        {
            return View(GetViewPath(nameof(Accountant), action));
        }

        public ViewResult DepartmentHead(DepartmentHead head, HTTPActions action)
        {
            return View(GetViewPath(nameof(DepartmentHead), action));
        }

        public ViewResult Director(Director director, HTTPActions action)
        {
            return View(GetViewPath(nameof(Director), action));
        }

        public ViewResult Manager(Manager manager, HTTPActions action)
        {
            return View(GetViewPath(nameof(Manager), action));
        }

        public ViewResult Worker(Worker worker, HTTPActions action)
        {
            return View(GetViewPath(nameof(Worker), action));
        }

        private string GetViewPath(string viewName, HTTPActions actions) =>
            _pathsByHttpAction[actions] + "/" + viewName;
    }
}
