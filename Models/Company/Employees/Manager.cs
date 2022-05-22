using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Managers")]
    [Name("Менеджер")]
    public class Manager : Employee
    {
        public int DepartmentHeadId { get; set; }
        public DepartmentHead? DepartmentHead { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public List<Worker> Workers { get; set; } = new();
    }
}
