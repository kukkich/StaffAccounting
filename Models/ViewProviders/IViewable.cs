using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.Company;

namespace StaffAccounting.Models.ViewProviders
{
    public interface IViewable
    {
        public ViewResult GetView(IViewProvider viewProvider, HTTPActions action);
    }
}
