using Microsoft.EntityFrameworkCore;
using StaffAccounting.Models.Company;
using StaffAccounting.Models.Notation;
using System.Reflection;

namespace StaffAccounting.Models.Filtration
{
    public class PositionFilter
    {
        public IEnumerable<Employee> RequiredEmployees =>
            (IEnumerable<Employee>)_employeesContext
                    .GetGetMethod()
                    .Invoke(_companyContext, Array.Empty<object>());

        public readonly EmployeeNotationProvider Provider;

        private readonly PropertyInfo _employeesContext;
        private readonly CompanyContext _companyContext;

        public PositionFilter(string requiredNotation, CompanyContext companyContext)
        {
            _companyContext = companyContext;

            Provider = new();
            Type requiredType = Provider.TryGetTybeByNotation(requiredNotation);

            PropertyInfo[] contextProperties = companyContext.GetType().GetProperties();
            IEnumerable<PropertyInfo> employeeContexts = contextProperties
                .Where(p =>
                    {
                        Type propertyType = p.PropertyType;
                        if (propertyType.IsGenericType && propertyType.GetGenericArguments().Length == 1)
                        {
                            var genericType = propertyType.GetGenericArguments()[0];

                            return (genericType.IsSubclassOf(typeof(Employee)) &&
                                genericType.GetCustomAttribute<NotationAttribute>() != null)
                                ||
                                genericType == typeof(Employee);
                        }
                        return false;
                    }
                ).ToList();
            if (requiredNotation == null)
            {
                _employeesContext = employeeContexts.First(p =>
                    p.PropertyType.GetGenericArguments()[0] == typeof(Employee)
                );
            }
            else
            {
                _employeesContext = employeeContexts.First(p =>
                    p.PropertyType.GetGenericArguments()[0] == requiredType
                );
            }
        }
    }
}
