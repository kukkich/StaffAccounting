namespace StaffAccounting.Models.Filtration
{
    public static class FilterExtension
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, RelationFilterOption option)
            where T : IFiltrable
        {
            return collection.Where(item => item.IsMatch(option));
        }
    }
}
