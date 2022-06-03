namespace StaffAccounting.Models.Filtration
{
    public class FilterOption
    {
        public int? DepartmentId;
        public int? ProjectId;
        public int? RankId;
        public int? DepartmentHeadId;
        public int? DirectorId;
        public int? ManagerId;

        // ASP .Net require this constructor
        public FilterOption () { }

        public FilterOption (IQueryCollection query)
        {
            DepartmentId = TryParseInt(query.FirstOrDefault(p => p.Key == nameof(DepartmentId)).Value);
            ProjectId = TryParseInt(query.FirstOrDefault(p => p.Key == nameof(ProjectId)).Value);
            RankId = TryParseInt(query.FirstOrDefault(p => p.Key == nameof(RankId)).Value);
            DepartmentHeadId = TryParseInt(query.FirstOrDefault(p => p.Key == nameof(DepartmentHeadId)).Value);
            DirectorId = TryParseInt(query.FirstOrDefault(p => p.Key == nameof(DirectorId)).Value);
            ManagerId = TryParseInt(query.FirstOrDefault(p => p.Key == nameof(ManagerId)).Value);
        }

        public bool IsEmpty() =>
            DepartmentId == null
            && ProjectId == null
            && RankId == null
            && DepartmentHeadId == null
            && DirectorId == null
            && ManagerId == null;

        private static int? TryParseInt(string value)
        {
            if (value == null) return null;
            return int.Parse(value);
        }
    }
}
