#region EF Core'da In-Memory Database ile Çalışmanın Gereği Nedir?

//EF Core, fiziksel veritabanlarından ziyade in-memory'de database oluşturup üzerinde birçok işlemi yapmamızı sağlayabilmektedir. İşte bu özellik ile gerçek uygulamaların dışında test gibi operasyonları hızlıca yürütebileceğimiz imkanlar elde edebilmekteyiz.
// Yeni çıkan EF Core özelliklerini test edebilmek için kullanılabilir.

// In-memory database üzerinde çalışırken migration oluşturmaya gerek yoktur.
// In-memory'de oluşturulmuş olan database uygulama sona erdiği/kapatıldı taktirde bellekten silinecektir.
#endregion

#region Avantajları nelerdir ?

//Test ve pre-prod uygulamalarda gerçek/fiziksel veritabanları oluşturmak ve yapılandırmak yerine tüm veritabanını bellekte modelleyebilir ve gerekli işlemleri sanki gerçek bir veritabanında çalışıyor gibi orada gerçekleştirebiliriz.

//Bellekte çalışmak geçici bir deneyim olacağı için veritabanı serverlarında test amaçlı üretilmiş olan veritabanlarının lüzumsuz yer işgal etmesini engellemiş olacaktır.
// bellekte veritabanını modellemek kodun hızlı bir şekilde test edilebilmesini sağlayacaktır.

#endregion

#region Dezavantajları nelerdir ?

// In-Memory'de yapılacak olan veritabanı işlevlerinde ilişkisel modellemeler yapılamamaktadır. Bu durumdan dolayı veri tutarlılığı sekteye uğrayabilir ve istatistiksel açıdan yanlış sonuçlar elde edilebilir.

#endregion

#region Örnek çalışma

//Microsoft.EntityFrameworkCore.InMemory kütüphanesi uygulamaya yüklenmelidir.


// optionsBuilder.UseInMemoryDatabase("exampleDatabase");

#endregion