namespace MyWebApp.Razor.Models
{
    public class Product
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public long? SupplierId { get; set; }
        public long? CategoryId { get; set; }
        public string? Unit { get; set; }
        public byte[]? Price { get; set; }

    }
}


//public long ProductId { get; set; }
//public string ProductName { get; set; } = null!;
//public long? SupplierId { get; set; }
//public long? CategoryId { get; set; }
//public string? QuantityPerUnit { get; set; }
//public byte[]? UnitPrice { get; set; }
//public long? UnitsInStock { get; set; }
//public long? UnitsOnOrder { get; set; }
//public long? ReorderLevel { get; set; }
//public bool Discontinued { get; set; }
//public DateTime? DiscontinuedDate { get; set; }