using Entities;
using Lesson1;
using Microsoft.EntityFrameworkCore;

ExampleDbContext context = new();


#region Method Syntax
// var products = await context.Products.ToListAsync();
#endregion

#region Query Syntax
// var products2 = await (from product in context.Products
//                      select product).ToListAsync();
#endregion

#region IQueryable ve IEnumerable farkları
// IQueryable : sorguya karşılık gelir, Ef core üzerinden yapılmış olan sorgunun execute edilmemiş halini ifade eder.

// IEnumerable : sorgunun execute edilip verilerin in memorye yüklenmiş halini ifade eder.

// ToList fonksiyonu IQueryable'ı execute ederek IEnumerable'a çevirir. Yani artık veriler elimizdedir.

var products = context.Products; //ToListAsync'e kadar olan kısım IQueryable, ToListAsync'ten sonra IEnumerable'a döner.
var products2 = await context.Products.ToListAsync();
#endregion


#region Foreach
foreach (var product in products)
{
    Console.WriteLine(product.ProductName);
}

// ToList kullanılmadan/haricinde, foreach'i tetiklediğimizde de IQueryable IEnumerable'a çevrilir yani sorgu execute edilir.
#endregion

#region Deferred Execution(Ertelenmiş çalışma)
int productId = 5;
var products3 = from product in context.Products
                where product.Id > productId
                select product;
productId = 15;

foreach (var product in products3)
{
    Console.WriteLine(product.ProductName);
}

// burada productId'yi 15 olarak alır. Çünkü foreach execute edilene kadar o sorgu işlenmemiştir ve değişken değişiklikleri işlenir.
/// IQueryable çalışmalarında ilgili kod yazıldığı yerde tetiklenmez/çalıştırılmaz. Queryable üzerine işlem yapılan yerde(ToList, Foreach) execute edilir.
#endregion


#region Single ve SingleOrDefault farkı

//Single : Sorgu sonucunda birden fazla veri geliyorsa veya hiç veri gelmiyorsa hata fırlatır.
//SingleOrDefault : Sorgu sonucunda birden fazla veri geliyorsa hata, hiç veri gelmiyorsa null verir.

#endregion

#region Find
// PK kolonuna özel, hızlı arama yapmamızı sağlayan fonksiyon.
var productFind= await context.Products.FindAsync(5);

// ** ÖNEMLİ ** Sorgulama sürecinde önce context içerisini kontrol eder, orada yoksa veritabanına gider. Performanslıdır.
#endregion

#region Count fonksiyonları için performans optimizasyonu
// Count fonksiyonunu ToList'ten sonra çağırmak gereksiz, çünkü sorguyu execute edip tüm veriyi belleğe getirdikten sonra tekrar sayma işlemi yapar.
// Fakat ToList yazmadan direkt Count çağırırsak, sorguya Count'ı ekleyip direkt execute ederiz.

var productsCount = await context.Products.CountAsync(p=>p.Price >5); // paranteze şart da yazabiliriz..
#endregion

#region Sorgu sonucu dönüşüm fonksiyonları

#region ToDictionary

// Sorgu neticesinde gelecek olan veriyi bir dictionary olarak elde etmek istiyorsak kullanırız.
var productsDict = await context.Products.ToDictionaryAsync(p=>p.Id, p=>p.ProductName); // key ve value değerlerinin hangi kolonlar olacağını belirledik

#endregion

#region ToArray
var productsArray = await context.Products.ToArrayAsync(); // Verilen obje türünde array'e çevirir.
#endregion

#endregion

#region Select
// Select fonksiyonu, çekilecek kolonları ayarlamamızı sağlar.
var productsSelect = context.Products.Select(p => new Product
{
    Id=p.Id,
    Price=p.Price,
}).ToListAsync();
// Aynı zamanda anonim bir tip oluşturup yine istediğimiz kolonları döndürebiliriz.
var productsSelectAnon = context.Products.Select(p => new 
{
    Id=p.Id,
    Price=p.Price,
}).ToListAsync(); // bu kullanımda null olan değerler dönmez, performanslıdır

#endregion

#region SelectMany

// Select ile aynı amaca hizmet eder fakat ilişkisel tablolar neticesinde gelen koleksiyonel verileri de tekilleştirip elde etmemizi sağlar.
#endregion

#region GroupBy

#region Method Syntax
var datas = await context.Products.GroupBy(p => p.Price).Select(group => new
{
    Count = group.Count(),
    Price = group.Key
}).ToListAsync();
//Önce hangi kolona göre gruplayacağımızı belirttik, daha sonra gruplandırdığımız verilerin istediğimiz kolonlarını aldık.Burada key değerimiz gruplandırma yaptığımız kolon.
#endregion

#region QuerySyntax
var datasQuery = await (from product in context.Products
                 group product by product.Price
                 into @group
                 select new
                 {
                     Price = @group.Key,
                     Count = @group.Count(),
                 }).ToListAsync();
#endregion
#endregion