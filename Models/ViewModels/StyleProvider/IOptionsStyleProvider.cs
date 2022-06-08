using StaffAccounting.Models.ViewModels.StyleProvider.Styles;

namespace StaffAccounting.Models.ViewModels.StyleProvider
{
    public interface IOptionsStyleProvider : IOptionStyle
    {
        public void SetNext();
    }
}
