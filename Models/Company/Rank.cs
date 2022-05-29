namespace StaffAccounting.Models.Company
{
    public class Rank : IDataJoinable
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<Worker> Workers { get; set; } = new();

        public void JoinFromDatabase(CompanyContext context)
        {
            Workers = context.Workers.Where(worker => worker.RankId == Id).ToList();
        }

        public Task JoinFromDatabaseAsync(CompanyContext context)
            => Task.Run(() => JoinFromDatabase(context));
    }
}
