using System.ComponentModel.DataAnnotations;

namespace StaffAccounting.Models.Company
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AdultAgeAttribute : ValidationAttribute
    {
        public AdultAgeAttribute() { }

        public override bool IsValid(object value)
        {
            if (value is DateTime birthday)
            {
                var today = DateTime.Today;
                var age = today.Year - birthday.Year;

                if (birthday.Date > today.AddYears(-age)) age--;
                return age >= 18 && age <= 90;
            }

            return false;
        }
    }
}
