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
            DepartmentId = TryParseInt(query[nameof(DepartmentId)]);
            ProjectId = TryParseInt(query[nameof(ProjectId)]);
            RankId = TryParseInt(query[nameof(RankId)]);
            DepartmentHeadId = TryParseInt(query[nameof(DepartmentHeadId)]);
            DirectorId = TryParseInt(query[nameof(DirectorId)]);
            ManagerId = TryParseInt(query[nameof(ManagerId)]);
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
