using System.ComponentModel.DataAnnotations;

namespace StaffAccounting.Models.Company
{
    public class Rank : IDataJoinable
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; } = null!;

        public List<Worker> Workers { get; set; } = new();

        public void JoinFromDatabase(CompanyContext context)
        {
            Workers = context.Workers.Where(worker => worker.RankId == Id).ToList();
        }

        public Task JoinFromDatabaseAsync(CompanyContext context)
            => Task.Run(() => JoinFromDatabase(context));

        public void BeforeDeletion(CompanyContext context)
        {
            JoinFromDatabase(context);
            UnlinkRelatedEntities();
        }

        private void UnlinkRelatedEntities()
        {
            foreach (Worker worker in Workers) worker.RankId = null;
        }
    }
}
