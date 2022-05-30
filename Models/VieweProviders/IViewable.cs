using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.Company;

namespace StaffAccounting.Models.VieweProviders
{
    public interface IViewable
    {
        public ViewResult GetView(IViewProvider viewProvider, HTTPActions action);
    }
}
