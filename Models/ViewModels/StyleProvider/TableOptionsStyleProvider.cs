using StaffAccounting.Models.ViewModels.StyleProvider.Styles;

namespace StaffAccounting.Models.ViewModels.StyleProvider
{
    public class TableOptionsStyleProvider : IOptionsStyleProvider
    {
        public string Details => _currentStyle.Details;
        public string Edit => _currentStyle.Edit;
        public string Delete => _currentStyle.Delete;

        private IIterableOptionStyle _currentStyle;

        public TableOptionsStyleProvider()
        {
            _currentStyle = new FilledOptionStyle();
        }

        public void SetNext()
        {
            _currentStyle = _currentStyle.Next;
        }
    }
}
