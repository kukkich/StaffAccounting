namespace StaffAccounting.Models.Company
{
    public interface IDataJoinable
    {
        public void JoinFromDatabase(CompanyContext context);
        public Task JoinFromDatabaseAsync(CompanyContext context)
            => Task.Run(() => JoinFromDatabase(context));
    }
}
