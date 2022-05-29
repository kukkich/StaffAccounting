namespace StaffAccounting.Models.Company
{
    public class Project : IDataJoinable
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<Manager> Managers { get; set; } = new();

        public void JoinFromDatabase(CompanyContext context)
        {
            Managers = context.Managers.Where(manager => manager.ProjectId == Id).ToList();
        }

        public Task JoinFromDatabaseAsync(CompanyContext context)
            => Task.Run(() => JoinFromDatabase(context));
    }
}
