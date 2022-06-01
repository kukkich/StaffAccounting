using Microsoft.EntityFrameworkCore;

namespace StaffAccounting.Models.ViewModels
{
    public class Pagination<TItems>
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

        private readonly Paging _paging;
        private readonly IQueryable<TItems> _source;

        public Pagination(IQueryable<TItems> collection, int pageSize)
        {
            _source = collection;
            PageSize = pageSize;
            _paging = new Paging(collection.Count(), PageSize, 1);
        }

        public List<TItems> GetItems()
        {
            return _source.Skip((_paging.PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }

        public async Task<List<TItems>> GetItemsAsync()
        {
            return await _source.Skip((_paging.PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
