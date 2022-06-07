namespace StaffAccounting.Models.ViewModels.StyleProvider
{
    public interface IOptionsStyleProvider
    {
        public string Details { get; }
        public string Edit { get; }
        public string Delete { get; }
        public void SetNext();
    }
}
