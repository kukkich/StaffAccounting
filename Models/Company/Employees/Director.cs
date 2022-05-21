using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Directors")]
    public class Director : Employee
    {
        public List<Accountant> Accountants { get; set; } = new();
        public List<DepartmentHead> DepartamentHeads { get; set; } = new();
    }
}
