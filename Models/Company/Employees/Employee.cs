using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.VieweProviders;
using System;
using System.ComponentModel.DataAnnotations;

namespace StaffAccounting.Models.Company
{
    public abstract class Employee : IViewable, IDataJoinable
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public Sex Sex { get; set; }
        public string FullName => $"{LastName} {FirstName} {MiddleName}";

        protected Employee () { }

        protected Employee (EmployeeCreationModel model)
        {
            FirstName = model.FirstName;
            MiddleName = model.MiddleName;
            LastName = model.LastName;
            Birthday = model.Birthday;
            Sex = model.Sex;
        }

        public abstract ViewResult GetView(IViewProvider viewProvider, HTTPActions action);
        public abstract void JoinFromDatabase(CompanyContext context);
    }
}
