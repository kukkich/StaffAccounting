namespace StaffAccounting.Models.ViewModels
{
    public class Paging
    {
        public int PageNumber
        {
            get => _pageNumber;
            set
            {
                if (!IsCorrectPageNumber(value))
                    throw new ArgumentOutOfRangeException(nameof(value));
                _pageNumber = value;
            }
        }
        public int TotalPages { get; private set; }
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;

        private int _pageNumber;

        public Paging(int totalCount, int pageSize, int pageNumber)
        {
            if (totalCount < 0) throw new ArgumentOutOfRangeException(nameof(totalCount));
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            PageNumber = pageNumber;
        }

        public bool IsCorrectPageNumber(int pageNumber)
        {
            return pageNumber > 0 && pageNumber <= TotalPages;
        }
    }
}
