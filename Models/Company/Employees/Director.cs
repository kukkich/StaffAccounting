using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Directors")]
    [Name("Директор")]
    public class Director : Employee
    {
        public List<Accountant> Accountants { get; set; } = new();
        public List<DepartmentHead> DepartamentHeads { get; set; } = new();

        public Director () { }

        public Director(EmployeeCreationModel model) 
            : base(model)
        { }
    }
}
