using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.Notation;
using StaffAccounting.Models.Filtration;
using StaffAccounting.Models.ViewProviders;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Directors")]
    [Notation("Директор")]
    public class Director : Employee
    {
        public List<Accountant> Accountants { get; set; } = new();
        public List<DepartmentHead> DepartmentHeads { get; set; } = new();

        public override bool CanBeRaised => false;

        public Director() { }

        public override ViewResult GetView(IViewProvider viewProvider, HTTPActions action)
        {
            return viewProvider.Director(this, action);
        }

        public override void JoinFromDatabase(CompanyContext context) { }

        public override bool IsMatch(RelationFilterOption option) => false;

        public override Employee GetRisedEmployee()
        {
            throw new InvalidOperationException();
        }

        protected override void UnlinkRelatedEntities()
        {
            foreach (Accountant accountant in Accountants) accountant.DirectorId = null;
            foreach (DepartmentHead departmentHead in DepartmentHeads) departmentHead.DirectorId = null;
        }
    }
}
