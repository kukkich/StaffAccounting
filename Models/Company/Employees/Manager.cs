using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.VieweProviders;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Managers")]
    [Notation("Менеджер")]
    public class Manager : Employee
    {
        public int DepartmentHeadId { get; set; }
        public DepartmentHead DepartmentHead { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public List<Worker> Workers { get; set; } = new();

        public Manager() { }

        public Manager(EmployeeCreationModel model)
            : base(model)
        {
            DepartmentHeadId = model.DepartmentHeadId;
            ProjectId = model.ProjectId;
        }

        public override ViewResult GetView(IViewProvider viewProvider, HTTPActions action)
        {
            return viewProvider.Manager(this, action);
        }
    }
}
