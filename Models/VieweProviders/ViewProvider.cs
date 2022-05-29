using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.Company;

namespace StaffAccounting.Models.VieweProviders
{
    [NonController]
    public class ViewProvider : Controller, IViewProvider
    {
        private readonly Dictionary<HTTPActions, string> _pathsByHttpAction = new Dictionary<HTTPActions, string>
        {
            [HTTPActions.Read] = "Details",
            [HTTPActions.Create] = "Create",
            [HTTPActions.Update] = "Edit",
            [HTTPActions.Delete] = "Delete"
        };

        public ViewResult Accountant(Accountant accountant, HTTPActions action)
        {
            return View(GetViewPath(nameof(Accountant), action), accountant);
        }

        public ViewResult DepartmentHead(DepartmentHead head, HTTPActions action)
        {
            return View(GetViewPath(nameof(DepartmentHead), action), head);
        }

        public ViewResult Director(Director director, HTTPActions action)
        {
            return View(GetViewPath(nameof(Director), action), director);
        }

        public ViewResult Manager(Manager manager, HTTPActions action)
        {
            return View(GetViewPath(nameof(Manager), action), manager);
        }

        public ViewResult Worker(Worker worker, HTTPActions action)
        {
            return View(GetViewPath(nameof(Worker), action), worker);
        }

        private string GetViewPath(string viewName, HTTPActions actions) =>
            _pathsByHttpAction[actions] + "/" + viewName;
    }
}
