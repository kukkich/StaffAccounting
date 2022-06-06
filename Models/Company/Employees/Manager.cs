using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.Notation;
using StaffAccounting.Models.Filtration;
using StaffAccounting.Models.ViewProviders;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Managers")]
    [Notation("Менеджер")]
    public class Manager : Employee
    {
        public int? DepartmentHeadId { get; set; }
        public DepartmentHead DepartmentHead { get; set; }

        public int? ProjectId { get; set; }
        public Project Project { get; set; }

        public List<Worker> Workers { get; set; } = new();

        public Manager() { }

        public override ViewResult GetView(IViewProvider viewProvider, HTTPActions action)
        {
            return viewProvider.Manager(this, action);
        }

        public override void JoinFromDatabase(CompanyContext context)
        {
            Project = context.Projects.FirstOrDefault(project => project.Id == ProjectId);
            DepartmentHead = context.DepartmentHeads.FirstOrDefault(departmentHead => departmentHead.Id == DepartmentHeadId);
        }

        public override bool IsMatch(RelationFilterOption option)
        {
            return (option.DepartmentHeadId is not null && option.DepartmentHeadId == DepartmentHeadId)
                || (option.ProjectId is not null && option.ProjectId == ProjectId);
        }
    }
}
