using Microsoft.AspNetCore.Mvc;

namespace StaffAccounting.Models.ViewProviders
{
    public interface IViewable
    {
        public ViewResult GetView(IViewProvider viewProvider, HTTPActions action);
    }
}
