namespace StaffAccounting.Models.Filtration
{
    public interface IFiltrable
    {
        public bool IsMatch(RelationFilterOption option);
    }
}
