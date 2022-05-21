namespace StaffAccounting.Models.Company
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<Manager> Managers { get; set; } = new();
    }
}
