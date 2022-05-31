using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.Company.Attributes;
using StaffAccounting.Models.ViewProviders;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Accountants")]
    [Notation("Бухгалтер")]
    public class Accountant : Employee
    {
        public int? DirectorId { get; set; }
        public Director Director { get; set; }

        public Accountant() { }

        public override ViewResult GetView(IViewProvider viewProvider, HTTPActions action)
        {
            return viewProvider.Accountant(this, action);
        }

        public override void JoinFromDatabase(CompanyContext context)
        {
            Director = context.Directors.FirstOrDefault(director => director.Id == DirectorId);
        }
    }
}


