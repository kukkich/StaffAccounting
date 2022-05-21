using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Workers")]
    public class Worker : Employee
    {
        public int ManagerId { get; set; }
        public Manager? Manager { get; set; }

        public int RankId { get; set; }
        public Rank? Rank { get; set; }
    }
}
