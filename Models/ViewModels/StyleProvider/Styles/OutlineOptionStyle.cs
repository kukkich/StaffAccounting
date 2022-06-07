namespace StaffAccounting.Models.ViewModels.StyleProvider.Styles
{
    public class OutlineOptionStyle : IOptionStyle
    {
        public string Details => "btn btn-outline-info btn-sm";
        public string Edit => "btn btn-outline-secondary btn-sm";
        public string Delete => "btn btn-outline-danger btn-sm";
        public IOptionStyle Next => new FilledOptionStyle();
    }
}
