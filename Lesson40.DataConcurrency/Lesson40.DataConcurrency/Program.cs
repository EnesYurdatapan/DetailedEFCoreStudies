#region Data Concurrency Nedir ?

// Geliştirdiğimiz uygulamalarda zaman zaman verisel olarak tutarsızlıklar meydana gelebilmektedir.
// Örneğin ; birden fazla uygulamanın veya client'ın aynı veritabanı üzerinde eşzamanlı olarak çalışıtğı durumlarda verisel anlamda uygulamadan uygulamaya yahut client'tan clienta tutarsızlıklar meydana gelebilir.

// Data Concurrency kavramı, uygulamalardaki veri tutarsızlığı durumlarına karşılık yönetilebilirlği sağlayacak olan davranışları kapsayan bir kavramdır.

//Bir uygulamada veri tutarsızlığının olması demek, o uygulamayı kullanan kullanıcıları yanıltmak demektir.

//Veri tutarsızlığınını olduğu uygulamalarda istatistiksel olarak yanlış sonuçlar elde edilebilir.

#endregion

#region Stale & Dirty Data Nedir ?

//Stale Data : Veri tutatsızlığına sebebiyet verebilecek güncellenmemiş veya zamanı geçmiş olan verileri ifade eder.
// Örneğin bir ürünün stok durumu sıfırlandığı halde arayüz üzerinde bunu ifade eden bir güncelleme durumu söz konusu değilse işte bu stale data durumuna bir örnektir.

//Dirty Data : Veri tutatsızlığına sebebiyet verebilecek verinin hatalı yahut yanlış olduğunu ifade etmektedir. 
// Örneğin adı 'Ahmet' olan bir kullanıcının veritabanında 'Mehmet' olarak tutulması dirty data örneklendirmesidir.

#endregion

#region Last In Wins (Son Gelen Kazanır)

// Bir veri yapısında son yapılan aksiyona göre en güncel verinin en üstte bulunmasını ifade eden bir deyimsel terimdir.

#endregion

#region Pessimistic Lock (Kötümser Kilitleme)

// Bir transaction sürecinde elde edilen veriler üzerinde farklı sorgularla değişiklik yapılmasını engellemek için ilgili verilerin kilitlenmesini sağlayarak değişikliğe karşı direnç oluşturulmasını ifade eden bir yöntemdir.

//Bu verilerin kilitlenmesi durumu ilgili transactionın commit ya da rollback edilmesi ile sınırlıdır.


#region Deadlock Nedir ?

//Kitlenmiş olan bir verinin veritabanı seviyesinde meydana gelen sistemsel bir hatadan dolayı kilidinin çözülememesi yahut döngüsel olarak kilitlenme durumunun meydana gelmesini ifade eden bir terimdir.

//Pessimistic Lock yönteminde deadlock durumunu yaşamanız bir ihtimaldir. O yüzden değerlendirilmesi gereken ve iyi düşünülerek tercih edilmesi gereken bir yaklaşımdır.

#endregion


#region WITH (XLOCK)

//await using var transaction = await context.Database.BeginTransactionAsync();

// context.Persons.FromSql($"SELECT * FROM Persons WITH (XLOCK) WHERE PersonId=5");

//await transaction.CommitAsync(); >> Commit edildiği için Lock'u salar.

#endregion


#endregion

#region Optimistic Lock (İyimse Kilitleme)

//Bir verinin stale olup olmadığını anlamak için herhangi bir locking işlemi olmaksızın versiyon mantığında çalışmamızı sağlayan yaklaşımdır.
//Optimistic Lock yönteminde, Pessimistic lock'da olduğu gibi veriler üzerinde tutarsızlığa sebep olabilecek değişiklikler fiziksel olarak engellenmemektedir. Yani veriler tutarsızlığı sağlayacak şekilde değişitirlebilir.
//Fakat Optimistic Lock yaklaşımı ile bu veriler üzerindeki tutarsızlık durumunu takip edebilmek için versiyon bilgisini kullanıyoruz.
//Her bir veriye karşılık bir versiyon bilgisi üretiliyor. Bu bilgi ister metinsel ister sayısal olabilir. Bu versiyon bilgisi veri üzerinde yapılan her bir değişiklik neticesinde güncellenecektir. Dolayısıyla bu güncellemeyi kolay bir şekilde gerçekleştirebilmek için sayısal olmasını tercih ederiz.
// EF Core üzerinden verileri sorgularken ilgili verilerin versiyon bilgilierini de in-memory'e alıyoruz. Ardından veri üzerinde bir değişiklik yapılırsa bu in-memorydeki versiyon bilgisi ile veritabanındaki versiyon bilgisi karşılaştırılır. Eğerki bu karşılaştırma doğrulanıyorsa yapılan aksiyon geçerli olacaktır, yok eğer doğrulanmıyorsa demek ki verinin değeri değişmiş anlamına gelecek yani bir tutarsızlık durumu olduğu anlaşılacaktır. İşte bu durumda bir hata fırlatılacak ve aksiyon gerçekleştirilmeyecektir.

//EF Core Optimistic Lock yaklaşımı için genetiğinde yapısal bir özellik barındırmaktadır.

#region Property Based Configuration

//Verisel tutarlılığın kontrol edilmek istendiği propertyler ConcurrencyCheck attribute ile işaretlenir. Bu işaretleme neticesinde her entity'nin instance'ı için in-memoryde bir token değeri üretilecektir. Üretilen bu token değeri, alınan aksiyon süreçlerinde EF Core tarafından doğrulanacak ve eğer ki herhangi bir değişiklik yoksa aksiyon başarıyla sonlandırılmış olacaktır.
// Eğer transaction sürecinde ilgili veri üzerinde (ConcurrencyCheck attribute ü ile işaretlenmiş propertylerde) herhangi bir değişiklik durumu söz konusuysa o taktirde üretilen token da değiştirilecek ve haliyle doğrulama sürecinde geçerli olmayacağı anlaşılacağı için veri tutarsızlığı durumu olduğu anlaşılacak ve hata fırlatılacaktır.

using System.ComponentModel.DataAnnotations;

class Person
{
    public int Id { get; set; }
    [ConcurrencyCheck]
    public string Name { get; set; }
}

// VEYA FluentAPI ile 
//modelBuilder.Entity<Person>().Property(p=>p.Name).IsConcurrencyToken();
#endregion

#region RowVersion Column

//Bu yaklaşımda ise veritabanındaki her bir satıra karşılık versiyon bilgisi fiziksel olarak oluşturulmaktadır.
class Person2
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
}

//modelBuilder.Entity<Person>().Property(p=>p.RowVersion).IsRowVersion();

#endregion

#endregion