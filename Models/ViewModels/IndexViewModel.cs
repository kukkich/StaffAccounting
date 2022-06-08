using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using StaffAccounting.Models.Company;
using StaffAccounting.Models.Filtration;
using StaffAccounting.Models.Notation;

namespace StaffAccounting.Models.ViewModels
{
    public class IndexViewModel
    {
        public Pagination<Employee> Pagination { get; set; }
        public RelationFilterOption FilterParameters { get; set; }
        public Dictionary<string, string> QueryData { get; set; }
        public EmployeeNotationProvider NotationProvider { get; }

        private const int _pageSize = 10;

        public IndexViewModel (CompanyContext context, int page, IQueryCollection query, string requiredNotation)
        {
            QueryData = query.ToDictionary(x => x.Key, x => (string)x.Value);

            PositionFilter positionFilter = new(requiredNotation, context);
            NotationProvider = positionFilter.Provider;

            IEnumerable<Employee> items = positionFilter.RequiredEmployees;

            FilterParameters = new(query);
            if (FilterParameters.IsEmpty())
                Pagination = new(items, _pageSize);
            else
                Pagination = new(
                    items.AsEnumerable()
                        .Filter(FilterParameters),
                    _pageSize
                    );

            Pagination.PageNumber = page;
        }
    }
}
