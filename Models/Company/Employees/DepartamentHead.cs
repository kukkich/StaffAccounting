using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("DepartmentHeads")]
    [Name("Глава департамента")]
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
            Department = model.Department;
            Director = model.Director;
        }
    }
}
