namespace Entities
{
    public class Product
    {
        // EF Core aşağıdaki gibi name conventionlara sahip propertyleri PK olarak kabul eder.
        //public int ID { get; set; }
        //public int Id { get; set; }
        //public int ProductId { get; set; }
        public int Id { get; set; }

        public string ProductName { get; set; }
        public int Price { get; set; }
    }
}


