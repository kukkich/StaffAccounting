using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Workers")]
    [Name("Рабочий")]
    public class Worker : Employee
    {
        public int ManagerId { get; set; }
        public Manager? Manager { get; set; }

        public int RankId { get; set; }
        public Rank? Rank { get; set; }
    }
}
