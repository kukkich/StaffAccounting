namespace StaffAccounting.Models.Filtration
{
    public interface IFiltrable
    {
        public bool IsMatch(FilterOption option);
    }
}
