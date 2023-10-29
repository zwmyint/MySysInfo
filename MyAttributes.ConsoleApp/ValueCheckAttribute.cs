namespace MyAttributes.ConsoleApp
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValueCheckAttribute : Attribute
    {
        public bool IsPositive { get; }
        public ValueCheckAttribute(bool isPositive = true)
        {
            IsPositive = isPositive;
        }
    }
}
