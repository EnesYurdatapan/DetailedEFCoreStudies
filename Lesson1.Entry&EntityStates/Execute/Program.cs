
using Entities;
using Lesson1;

ExampleDbContext context = new();

Product product = new()
{
    Price= 15,
    ProductName="Kalem"
};


#region Entry State & SaveChanges
Console.WriteLine(context.Entry(product).State); // Detached, ne yapılacağını bilmiyor henüz. 

await context.Products.AddAsync(product);   //await context.AddAsync(product); >>> ikisi de aynı fakat tip güvenli/tip güvensiz farkı var.
Console.WriteLine(context.Entry(product).State); // Added, çünkü ekleme işleminden sonra çağırdık.
await context.SaveChangesAsync(); // bütün crud işlemlerinde sorguları oluşturup bir transaction eşliğinde veritabanına gönderip execute eden fonksiyondur. Eğer ki oluşturulan sorgulardan birisi başarısız olursa tüm işlemleri geri alır(rollback).

Console.WriteLine(context.Entry(product).State); // Unchanged, çünkü artık işlemi veritabanına kaydettik.
#endregion




#region AddRange 
Product product1 = new()
{
    Price = 15,
    ProductName = "Kalem"
};
Product product2 = new()
{
    Price = 20,
    ProductName = "Silgi"
};
Product product3 = new()
{
    Price = 25,
    ProductName = "Masa"
};

await context.Products.AddRangeAsync(product1, product2, product3); // toplu ekleme
#endregion