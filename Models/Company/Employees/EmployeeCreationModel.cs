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

        public int DirectorId { get; set; }
        public int DepartmentId { get; set; }
        public int ProjectId { get; set; }
        public int DepartmentHeadId { get; set; }
        public int RankId { get; set; }
        public int ManagerId { get; set; }
    }
}
