namespace StaffAccounting.Models.Company
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<DepartmentHead> Heads { get; set; } = new();
    }
}
