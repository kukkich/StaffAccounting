using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Accountants")]
    public class Accountant : Employee
    {
        public int DirectorId { get; set; }
        public Director? Director { get; set; }
    }
}
