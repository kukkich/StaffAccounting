using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.VieweProviders;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Accountants")]
    [Notation("Бухгалтер")]
    public class Accountant : Employee
    {
        public int DirectorId { get; set; }
        public Director Director { get; set; }

        public Accountant() { }

        public Accountant (EmployeeCreationModel model)
            :base(model)
        {
            DirectorId = model.DirectorId;
        }

        public override ViewResult GetView(IViewProvider viewProvider, HTTPActions action)
        {
            return viewProvider.Accountant(this, action);
        }
    }
}


