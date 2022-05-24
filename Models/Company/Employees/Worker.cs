using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Workers")]
    [Name("Рабочий")]
    public class Worker : Employee
    {
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }

        public int RankId { get; set; }
        public Rank Rank { get; set; }

        public Worker() { }

        public Worker(EmployeeCreationModel model) 
            : base(model)
        {
            ManagerId = model.ManagerId;
            RankId = model.RankId;
        }
    }
}
