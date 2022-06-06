using StaffAccounting.Models.Company;
using StaffAccounting.Models.Notation;
using System.Reflection;

namespace StaffAccounting.Models
{
    public class EmployeeNotationFactory
    {
        public readonly EmployeeNotationProvider Provider;

        public EmployeeNotationFactory()
        {
            Provider = new EmployeeNotationProvider();
        }

        public Employee CreateEmployee(string notation)
        {
            ConstructorInfo constuructor = Provider.GetConstructor(
                    notation,
                    ctor => ctor.GetParameters().Length == 0
                );

            return (Employee)constuructor.Invoke(Array.Empty<object>());
        }
    }
}
