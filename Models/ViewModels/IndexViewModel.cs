using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using StaffAccounting.Models.Company;
using StaffAccounting.Models.Filtration;

namespace StaffAccounting.Models.ViewModels
{
    public class IndexViewModel
    {
        public Pagination<Employee> Pagination { get; set; }
        public FilterOption FilterParameters { get; set; }
        public Dictionary<string, string> QueryData { get; set; }
        private const int _pageSize = 2;

        public IndexViewModel (DbSet<Employee> items, int page, IQueryCollection query)
        {
            QueryData = query.ToDictionary(x => x.Key, x => (string)x.Value);
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
