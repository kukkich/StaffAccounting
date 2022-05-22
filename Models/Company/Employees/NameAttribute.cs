namespace StaffAccounting.Models.Company
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NameAttribute : Attribute
    {
        public readonly string Name;
        public NameAttribute(string name)
        {
            Name = name;
        }
    }
}
