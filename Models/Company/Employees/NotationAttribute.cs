namespace StaffAccounting.Models.Company
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
