using System.ComponentModel.DataAnnotations;

namespace StaffAccounting.Models.Company
{
    public class EmployeeCreationModel
    {
        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Введите отчество")]
        public string MiddleName { get; set; } = null!;
        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "Введите дату рождения")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Выберите пол")]
        public Sex Sex { get; set; }

        public Director Director { get; set; }
        public Department Department { get; set; }
        public Project Project { get; set; }
        public DepartmentHead DepartmentHead { get; set; }
        public Rank Rank { get; set; }
        public Manager Manager { get; set; }
    }
}
