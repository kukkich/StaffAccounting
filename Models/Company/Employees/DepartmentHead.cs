﻿using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.Notation;
using StaffAccounting.Models.Filtration;
using StaffAccounting.Models.ViewProviders;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("DepartmentHeads")]
    [Notation("Глава департамента")]
    public class DepartmentHead : Employee, IDataJoinable
    {
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public int? DirectorId { get; set; }
        public Director Director { get; set; }

        public List<Manager> Managers { get; set; } = new();

        public DepartmentHead() { }

        public override ViewResult GetView(IViewProvider viewProvider, HTTPActions action)
        {
            return viewProvider.DepartmentHead(this, action);
        }

        public override void JoinFromDatabase(CompanyContext context)
        {
            Department = context.Departments.FirstOrDefault(department => department.Id == DepartmentId);
            Director = context.Directors.FirstOrDefault(department => department.Id == DirectorId);
        }

        public override bool IsMatch(RelationFilterOption option)
        {
            return (option.DepartmentId is not null && option.DepartmentId == DepartmentId)
                || (option.DirectorId is not null && option.DirectorId == DirectorId);
        }
    }
}
