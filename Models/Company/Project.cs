using System.ComponentModel.DataAnnotations;

namespace StaffAccounting.Models.Company
{
    public class Project : IDataJoinable
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; } = null!;

        public List<Manager> Managers { get; set; } = new();

        public void JoinFromDatabase(CompanyContext context)
        {
            Managers = context.Managers.Where(manager => manager.ProjectId == Id).ToList();
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
            foreach (Manager manager in Managers) manager.ProjectId = null;
        }
    }
}
