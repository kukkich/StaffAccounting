using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.Company;

namespace StaffAccounting.Models.VieweProviders
{
    public interface IViewProvider
    {
        public ViewResult Accountant(Accountant accountant, HTTPActions action);
        public ViewResult DepartmentHead(DepartmentHead head, HTTPActions action);
        public ViewResult Director(Director director, HTTPActions action);
        public ViewResult Manager(Manager manager, HTTPActions action);
        public ViewResult Worker(Worker worker, HTTPActions action);
    }
}
