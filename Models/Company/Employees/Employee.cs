using System;
using System.ComponentModel.DataAnnotations;

namespace StaffAccounting.Models.Company
{
    public abstract class Employee
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
            MiddleName = model.LastName;
            LastName = model.LastName;
            Birthday = model.Birthday;
            Sex = model.Sex;
        }
    }
}
