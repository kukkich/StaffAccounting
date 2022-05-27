using StaffAccounting.Models.Company;
using System.Reflection;

namespace StaffAccounting.Models
{
    public class EmployeeNotationFactory
    {
        private readonly IEnumerable<Type> _employeeTypes;

        public EmployeeNotationFactory()
        {
            _employeeTypes = Assembly.GetAssembly(typeof(Employee))?.GetTypes()
                .Where(type =>
                    type.IsSubclassOf(typeof(Employee)) &&
                    type.GetCustomAttribute<NotationAttribute>() != null
                );
        }

        public Employee CreateEmployee(Type type, EmployeeCreationModel creationModel)
        {
            var constuructor = type.GetConstructors()
                .First(ctor => 
                    ctor.GetParameters().Length == 1 &&
                    ctor.GetParameters()[0].ParameterType == typeof(EmployeeCreationModel)
                );
            
            return (Employee)constuructor.Invoke(new object[] { creationModel });
        }

        public IEnumerable<string> GetNotations() => 
            _employeeTypes.Select(type => type.GetCustomAttribute<NotationAttribute>().Name);

        public Type GetTybeByNotation(string notation) =>
            _employeeTypes.First(type =>
                    type.GetCustomAttribute<NotationAttribute>()?.Name == notation
                   );

        public string GetClassNameByNotation(string attributeName) =>
            GetTybeByNotation(attributeName).Name;
    }
}
