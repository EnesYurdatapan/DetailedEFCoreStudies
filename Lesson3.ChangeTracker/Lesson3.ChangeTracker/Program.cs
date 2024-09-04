using Entities;
using Lesson1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Xml.Serialization;

ExampleDbContext exampleDbContext = new ExampleDbContext();

#region Change Tracking nedir ?

//Context nesnesi üzerinden gelen tüm veriler otomatik olarak bir takip mekanizması tarafından izlenirler.
// Bu takip mekanizmasına Change Tracker denir. Change Tracker ile nesneler üzerindeki değişiklikler/işlemler takip edilerek bu işlemlere uygun sql sorguları oluşturulur. Bu işleme Change Tracking denir.

#endregion


#region ChangeTracker Property

// Takip edilen nesnelere erişebilmemizi ve gerektiğinde üzerinde işlemler yapabilmemizi sağlayan bir prop.
// Context sınıfının base class'ı olan DbContext sınıfının bir memberıdır.

var products = await exampleDbContext.Products.ToListAsync();
products[6].Price = 532; // update komutu oluşturur ve state Modified olur
products[7].ProductName ="product1"; // update komutu oluşturur ve state Modified olur
exampleDbContext.Products.Remove(products[8]); // Delete komutu oluşturur ve state Removed olur 

var datas = exampleDbContext.ChangeTracker.Entries();
await exampleDbContext.SaveChangesAsync();
Console.WriteLine();
#endregion

#region DetectChanges

// Yapılan değişiklikler db'ye gönderilmeden önce algılandığından emin olmak gerekir. SaveChanges methodu çağırıldığında nesneler EF Core tarafından otomatik kontrol edilirler.
// Ancak yapılan operasyonlarda güncel tracking verilerinden emin olmak için değişikliklerin algılanmasını opsiyonel olarak gerçekleştirmek isteyebiliriz.
// İşte bunun için DetectChanges fonksiyonunu SaveChanges'a bırakmadan kendimiz de manuel olarak tetikleyebiliriz.

var product = await exampleDbContext.Products.FirstOrDefaultAsync(u => u.Id == 3);
product.Price = 12423;
exampleDbContext.ChangeTracker.DetectChanges();
await exampleDbContext.SaveChangesAsync();
// Çok gerekli değildir, fakat bazen async programlamada vs sorunlar olabiliyor.

#endregion


#region AutoDetectChangesEnabled Property

//İgili metotlar(SaveChanges, Entries) tarafından DetectChanges metodunun otomatik olarak tetiklenmesinin konfigürasyonunu yapmamızı sağlayan property.
// Eğer DetectChanges fonksiyonunun kullanımını irademizle yönetmek ve maliyet/ğperformans optimizasyonu yapmak istiyorsak "false" yaparak kapatabiliriz.


#endregion


#region Entries metodu

//Contextteki Entry metodunun koleksiyonel versiyonudur.
// ChangeTracker mekanizması tarafından izlenen her entity nesnesinin bilgisini EntityEntry türünden elde etmemizi sağlar ve belirli işlemler yapabilmemize olanak sağlar.
// Entries metodu, DetectChanges metodunu tetikler. Bu durumda tıpkı SaveChanges'ta olduğu gibi bir maliyettir.
//AutoDetectChangesEnabled false yapılarak maliyetten tasarruf edilebilir.

var products2 = await exampleDbContext.Products.ToListAsync();
products.FirstOrDefault(p=>p.Id==7).Price = 532;
products.FirstOrDefault(p => p.Id == 8).ProductName = "product2"; 
exampleDbContext.Products.Remove(products.FirstOrDefault(p => p.Id == 9));

exampleDbContext.ChangeTracker.Entries().ToList().ForEach(p =>
{
    if (p.State == EntityState.Unchanged)
    {
        // burda istediğimiz işlemleri yaparak veritabanına kaydedilmeden değişiklikler yapabiliriz.
    }
    else if (p.State == EntityState.Modified)
    {
        // burda istediğimiz işlemleri yaparak veritabanına kaydedilmeden değişiklikler yapabiliriz.

    }
});

#endregion



#region AcceptAllChanges metodu

//SaveChanges() veya SaveChanges(true) şeklinde tetiklendiğinde EF Core her şeyin yolunda gittiğini varsayarak track ettiği verilerin takibini keser. Böyle bir durumda beklenmeyen bir durum veya hata söz konusu olursa EF Core takip ettiği nesneleri bırakacağı için bir düzeltme söz konusu olamayacaktır.

// SaveChanges(false) başarılı olsa da başarısız olsa da takip etmeyi bırakmaz. AcceptAllChanges'ı çağırırsak takibi bırakmasını manuel olarak söylemiş oluruz.

var products3 = await exampleDbContext.Products.ToListAsync();
products3[6].Price = 532; // update komutu oluşturur ve state Modified olur
products3[7].ProductName = "product1"; // update komutu oluşturur ve state Modified olur
exampleDbContext.Products.Remove(products3[8]); // Delete komutu oluşturur ve state Removed olur 

await exampleDbContext.SaveChangesAsync(false);
exampleDbContext.ChangeTracker.AcceptAllChanges();


#endregion


#region HasChanges metodu

//Takip edilen nesneler arasından değişiklik yapılanların olup olmadığının bilgisini verir.
//arka planda DetectChanges metodunu tetikler.
#endregion


#region Entity States

// entity nesnelerinin durumlarını ifade eder.

#region Detached

//ChangeTracker tarafından takip edilmeyen nesnenin durumu.
Product product2 = new Product();
Console.WriteLine(exampleDbContext.Entry(product2).State);

#endregion

#region Added && Modified && Deleted
//DB'ye eklenecek nesneyi ifade eder. Fakat nesne henüz veritabanına işlenmemiştir.SaveChanges çağırıldığında insert komutu oluşturulacağı anlamına gelir.
Product product3 = new Product() { Price = 5, ProductName="xyfd"};
Console.WriteLine(exampleDbContext.Entry(product3).State);// detached
await exampleDbContext.Products.AddAsync(product3); 
Console.WriteLine(exampleDbContext.Entry(product3).State);// added
await exampleDbContext.SaveChangesAsync();
product3.Price = 324;
Console.WriteLine(exampleDbContext.Entry(product3).State); // modified
await exampleDbContext.SaveChangesAsync();
exampleDbContext.Remove(product3);
Console.WriteLine(exampleDbContext.Entry(product3).State); // Deleted
await exampleDbContext.SaveChangesAsync();



#endregion

#region Unchanged

//Db'den sorgulandığından beri nesne üzerinde herhangi bir değişiklik yapılmadığını ifade eder. Sorgu neticesinde elde edilen tüm nesneler başlangıçta bu state değerindedir.
var products4 = await exampleDbContext.Products.ToListAsync();
var data2= exampleDbContext.ChangeTracker.Entries();
Console.WriteLine();

#endregion

#endregion


#region Context nesnesi üzerinden Change Tracker

#region OriginalValues Property'si
// değiştirilmiş verinin orijinalini getirir.

var product5 = await exampleDbContext.Products.FirstOrDefaultAsync(p=>p.Id == 5);
product5.Price = 3252;
product5.ProductName = "Silgi"; // Modified

exampleDbContext.Entry(product5).OriginalValues.GetValue<float>(nameof(Product.Price));
#endregion

#region CurrentValues Property'si
// eğer entity'nin türünü bilmiyorsan yani generic çalışıyorsan şu anki değerleri bu şekilde elde edersin
var productName = exampleDbContext.Entry(product5).CurrentValues.GetValue<string>(nameof(Product.ProductName));
// heapteki nesnenin değerini getirdi, Db'den değil.

#endregion

//GetDatabaseValues de var, veritabanındaki güncel verileri getirir.

#endregion


#region SaveChanges'ın interceptor olarak çağırılması

//public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
//{
//    var entries = ChangeTracker.Entries();
//    foreach (var entry in Entries)
//    {
//        if (entry.State==EntityState.Added)
//        {
//            //....
//        }
//    }
//}

#endregion


#region AsNoTracking

// Context'ten çektiğimiz nesnelerin change tracker ile takip edilmesi beraberinde maliyet gerektirir.
// AsNoTracking metodu, context üzerinden sorgu sonucu gelen verilerin Change Tracker tarafından takip edilmesini engeller.
// Bu sayede tracker ihtiyacının olmadığı verilerdeki maliyetten kurtulmuş oluruz.(Örneğin sadece listeleme yapacaksak.)

var productsAsNoTracking = exampleDbContext.Products.AsNoTracking().ToList();

#endregion


#region AsNoTrackingWithIdentityResolution

//Change Tracker mekanizması sayesinde yinelenen veriler aynı instanceları kullanırlar.
// Örneğin 2 kullanıcı ve bunlara ait rollerin instanceları çekilecek. İkisinin de rolü adminse sadece 1 admin instance'ı çekilir.
//Bu örneği AsNoTracking kullandığımızda ise 2 kullanıcı için 2 ayrı admin instance'ı oluşturulur. Bu sefer de maliyeti arttırmıış oluruz.
// İşte bu tarz ilişkili verileri çektiğimiz operasyonlarda AsNoTrackingWithIdentityResolution kullanmalıyız.


var productsANTWI = await exampleDbContext.Products.Include(p=>p.ProductName).AsNoTrackingWithIdentityResolution().ToListAsync();
#endregion

#region UseQueryTrackingBehavior

//EF Core seviyesinde/uygulama seviyesinde ilgili contextten gelen verilerin üzerinde Change Tracker mekanizmasının davranışını temel seviyede belirlememizi sağlar.
// Yani bir konfigürasyon fonksiyonudur. 
// optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

#endregion