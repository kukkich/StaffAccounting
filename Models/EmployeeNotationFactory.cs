using StaffAccounting.Models.Company;
using StaffAccounting.Models.Company.Attributes;
using System.Reflection;

namespace StaffAccounting.Models
{
    public class EmployeeNotationFactory
    {
        public IEnumerable<string> Notations => _employeeTypes
            .Select(type => type.GetCustomAttribute<NotationAttribute>().Name);

        private readonly IEnumerable<Type> _employeeTypes;

        public EmployeeNotationFactory()
        {
            _employeeTypes = Assembly.GetAssembly(typeof(Employee))?.GetTypes()
                .Where(type =>
                    type.IsSubclassOf(typeof(Employee)) &&
                    type.GetCustomAttribute<NotationAttribute>() != null
                );
        }

        public Employee CreateEmployee(string notation)
        {
            Type type = GetTybeByNotation(notation);
            var constuructor = type.GetConstructors()
                .First(ctor =>
                    ctor.GetParameters().Length == 0
                );

            return (Employee)constuructor.Invoke(Array.Empty<object>());
        }

        private Type GetTybeByNotation(string notation) =>
            _employeeTypes.First(type =>
                    type.GetCustomAttribute<NotationAttribute>()?.Name == notation
                   );
    }
}
