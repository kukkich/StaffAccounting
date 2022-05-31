namespace StaffAccounting.Models.Company.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NotationAttribute : Attribute
    {
        public readonly string Name;
        public NotationAttribute(string name)
        {
            Name = name;
        }
    }
}
