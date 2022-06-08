namespace StaffAccounting.Models.Filtration
{
    public class RelationFilterOption
    {
        public int? DepartmentId;
        public int? ProjectId;
        public int? RankId;
        public int? DepartmentHeadId;
        public int? DirectorId;
        public int? ManagerId;

        public RelationFilterOption(IQueryCollection query)
        {
            DepartmentId = TryParseNullableToInt(query[nameof(DepartmentId)]);
            ProjectId = TryParseNullableToInt(query[nameof(ProjectId)]);
            RankId = TryParseNullableToInt(query[nameof(RankId)]);
            DepartmentHeadId = TryParseNullableToInt(query[nameof(DepartmentHeadId)]);
            DirectorId = TryParseNullableToInt(query[nameof(DirectorId)]);
            ManagerId = TryParseNullableToInt(query[nameof(ManagerId)]);
        }

        public bool IsEmpty() =>
            DepartmentId == null
            && ProjectId == null
            && RankId == null
            && DepartmentHeadId == null
            && DirectorId == null
            && ManagerId == null;

        private static int? TryParseNullableToInt(string value)
        {
            if (value == null) return null;
            return int.Parse(value);
        }
    }
}
