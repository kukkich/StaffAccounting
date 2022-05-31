using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.Company.Attributes;
using StaffAccounting.Models.VieweProviders;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Directors")]
    [Notation("Директор")]
    public class Director : Employee
    {
        public List<Accountant> Accountants { get; set; } = new();
        public List<DepartmentHead> DepartamentHeads { get; set; } = new();

        public Director () { }

        public override ViewResult GetView(IViewProvider viewProvider, HTTPActions action)
        {
            return viewProvider.Director(this, action);
        }

        public override void JoinFromDatabase(CompanyContext context) {}
    }
}
