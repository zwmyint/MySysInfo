namespace MyAttributes.ConsoleApp
{
    public class ValueChecker
    {
        [ValueCheck(true)]
        public int PositiveValue { get; set; }
        [ValueCheck(false)]
        public int NegativeValue { get; set; }


        public bool CheckValue(object obj)
        {
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(ValueCheckAttribute), false);
                if (attributes.Length > 0)
                {
                    var attribute = (ValueCheckAttribute)attributes[0];
                    var value = (int?)property.GetValue(obj);
                    if ((value >= 0 && attribute.IsPositive) || (value < 0 && !attribute.IsPositive))
                    {
                        Console.WriteLine($"{property.Name} is {(attribute.IsPositive ? "positive" : "negative")}");
                    }
                }
            }

            return true;
        }
    }

    

}
