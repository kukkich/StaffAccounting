using Microsoft.EntityFrameworkCore;

namespace StaffAccounting.Models.ViewModels
{
    public class Pagination<TItems>
        where TItems : class
    {
        public int PageSize { get; private set; }
        public int PageNumber
        {
            get => _paging.PageNumber;
            set => _paging.PageNumber = value;
        }
        public int TotalPages => _paging.TotalPages;
        public bool HasNextPage => _paging.HasNextPage;
        public bool HasPreviousPage => _paging.HasPreviousPage;

        private int HowManySkip => (_paging.PageNumber - 1) * PageSize;
        private readonly Paging _paging;
        private readonly IEnumerable<TItems> _source;

        public Pagination(IEnumerable<TItems> collection, int pageSize)
        {
            _source = collection;
            PageSize = pageSize;
            _paging = new Paging(collection.Count(), PageSize, 1);
        }

        public List<TItems> GetItems()
        {
            // if there is no filter parameters in controller
            // for query optimisation
            if (_source is DbSet<TItems>)
            {
                return _source
                    .AsQueryable()
                    .Skip(HowManySkip)
                    .Take(PageSize)
                    .ToList();
            }
            return _source
                .Skip(HowManySkip)
                .Take(PageSize)
                .ToList();
        }

        public async Task<List<TItems>> GetItemsAsync()
        {
            // if there is no filter parameters in controller
            // for query optimisation
            if (_source is DbSet<TItems>)
            {
                return await _source
                    .AsQueryable()
                    .Skip(HowManySkip)
                    .Take(PageSize)
                    .ToListAsync();
            }
            return await Task.Run(() =>
                    _source
                        .Skip(HowManySkip)
                        .Take(PageSize)
                        .ToList()
                );
        }

        public bool HasPage(int page) => _paging.IsCorrectPageNumber(page);

    }
}
