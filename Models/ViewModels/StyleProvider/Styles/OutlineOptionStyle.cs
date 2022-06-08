namespace StaffAccounting.Models.ViewModels.StyleProvider.Styles
{
    public class OutlineOptionStyle : IIterableOptionStyle
    {
        public string Details => "btn btn-outline-info btn-sm";
        public string Edit => "btn btn-outline-secondary btn-sm";
        public string Delete => "btn btn-outline-danger btn-sm";
        public IIterableOptionStyle Next => new FilledOptionStyle();
    }
}
