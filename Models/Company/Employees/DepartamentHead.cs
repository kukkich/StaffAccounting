﻿using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.VieweProviders;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("DepartmentHeads")]
    [Notation("Глава департамента")]
    public class DepartmentHead : Employee
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int DirectorId { get; set; }
        public Director Director { get; set; }

        public List<Manager> Managers { get; set; } = new();

        public DepartmentHead() { }

        public DepartmentHead(EmployeeCreationModel model)
            : base(model)
        {
            DepartmentId = model.DepartmentId;
            DirectorId = model.DirectorId;
        }

        public override ViewResult GetView(IViewProvider viewProvider, HTTPActions action)
        {
            return viewProvider.DepartmentHead(this, action);
        }
    }
}
