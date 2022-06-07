namespace StaffAccounting.Models.ViewModels.StyleProvider.Styles
{
    public interface IOptionStyle : IIterable<IOptionStyle>
    {
        public string Details { get; }
        public string Edit { get; }
        public string Delete { get; }
    }
}
