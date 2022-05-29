namespace StaffAccounting.Models.Company
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<DepartmentHead> Heads { get; set; } = new();

        public void JoinFromDatabase(CompanyContext context)
        {
            Heads = context.DepartmentHeads.Where(head => head.DepartmentId == Id).ToList();
        }

        public Task JoinFromDatabaseAsync(CompanyContext context)
            => Task.Run(() => JoinFromDatabase(context));
    }
}
