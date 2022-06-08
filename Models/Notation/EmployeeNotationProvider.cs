using StaffAccounting.Models.Company;
using System.Reflection;

namespace StaffAccounting.Models.Notation
{
    public class EmployeeNotationProvider
    {
        public IEnumerable<string> Notations => _employeeTypes
            .Select(type => type.GetCustomAttribute<NotationAttribute>().Name);

        private readonly IEnumerable<Type> _employeeTypes;

        public EmployeeNotationProvider()
        {
            _employeeTypes = Assembly
                .GetAssembly(typeof(Employee))
                ?.GetTypes()
                .Where(type =>
                    type.IsSubclassOf(typeof(Employee)) &&
                    type.GetCustomAttribute<NotationAttribute>() != null
                );
        }

        public ConstructorInfo GetConstructor(string notation, Func<ConstructorInfo, bool> additionalSelector = null)
        {
            Type type = GetTybeByNotation(notation);

            return type.GetConstructors()
                .First(additionalSelector == null
                    ? ctor => true
                    : ctor => additionalSelector(ctor)
                );
        }

        public Type GetTybeByNotation(string notation) =>
            _employeeTypes.First(type =>
                    type.GetCustomAttribute<NotationAttribute>()?.Name == notation
                   );

        public Type TryGetTybeByNotation(string notation) =>
            _employeeTypes.FirstOrDefault(type =>
                    type.GetCustomAttribute<NotationAttribute>()?.Name == notation
                   );
    }
}
