using System.ComponentModel.DataAnnotations;

namespace StaffAccounting.Models.Company
{
    public class Department : IDataJoinable
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; } = null!;

        public List<DepartmentHead> Heads { get; set; } = new();

        public void JoinFromDatabase(CompanyContext context)
        {
            Heads = context.DepartmentHeads.Where(head => head.DepartmentId == Id).ToList();
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
            foreach (DepartmentHead head in Heads) head.DepartmentId = null;
        }
    }
}
