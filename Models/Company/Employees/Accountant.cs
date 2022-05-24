using System.ComponentModel.DataAnnotations.Schema;

namespace StaffAccounting.Models.Company
{
    [Table("Accountants")]
    [Name("Бухгалтер")]
    public class Accountant : Employee
    {
        public int DirectorId { get; set; }
        public Director Director { get; set; }

        public Accountant() { }


        public Accountant (EmployeeCreationModel model)
            :base(model)
        {
            DirectorId = model.DirectorId;
        }
    }
}
