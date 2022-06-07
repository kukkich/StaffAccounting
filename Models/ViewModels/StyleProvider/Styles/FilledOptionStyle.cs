namespace StaffAccounting.Models.ViewModels.StyleProvider.Styles
{
    public class FilledOptionStyle : IOptionStyle
    {
        public string Details => "btn btn-info btn-sm";
        public string Edit => "btn btn-secondary btn-sm";
        public string Delete => "btn btn-danger btn-sm";
        public IOptionStyle Next => new OutlineOptionStyle();
    }
}
