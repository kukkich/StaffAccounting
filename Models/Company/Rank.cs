namespace StaffAccounting.Models.Company
{
    public class Rank
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<Worker> Workers { get; set; } = new();
    }
}
