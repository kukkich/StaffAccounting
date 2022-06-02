using Microsoft.AspNetCore.Mvc;
using StaffAccounting.Models.Filtration;
using StaffAccounting.Models.ViewProviders;
using System.ComponentModel.DataAnnotations;

namespace StaffAccounting.Models.Company
{
    public abstract class Employee : IViewable, IDataJoinable, IFiltrable
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Введите отчество")]
        public string MiddleName { get; set; } = null!;
        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "Введите дату рождения")]
        public DateTime Birthday { get; set; }
        public Sex Sex { get; set; }
        public string FullName => $"{LastName} {FirstName} {MiddleName}";
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - Birthday.Year;

                if (Birthday.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        protected Employee() { }

        public abstract ViewResult GetView(IViewProvider viewProvider, HTTPActions action);
        public abstract void JoinFromDatabase(CompanyContext context);
    }
}
