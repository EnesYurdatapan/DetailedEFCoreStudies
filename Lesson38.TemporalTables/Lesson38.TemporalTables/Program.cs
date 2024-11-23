#region Temporal Tables Nedir ?

// Veri değişikliği süreçlerinde kayıtları depolayan ve zaman içinde farklı noktalardaki tablo verilerinin analizi için kullanılan ve sistem tarafından yönetilen tablolardır.

#endregion


#region Temporal Tables özelliğiyle nasıl çalışılır ?

//EF Core'daki migration yapıları sayesinde temporal tablelar oluşturulup veritabanında üretilebilmektedir.
// Mevcut tablolar migrationlar aracılığıyla temporal tablelara dönüşebilir.
//Herhangi bir tablonun verisel olarak geçmişini rahatlıkla sorgulayabiliriz.
//Herhangi bir tablodaki bir verinin geçmişteki herhangi bir T anındaki hali/verileri geri getirilebilir.

#endregion

#region Temporal Table nasıl uygulanır ?

#region IsTemporal Yapılandırması
//EF Core bu yapılandırma fonksiyonu sayesinde ilgili entity'e karşılık üretilecek tabloda temporal table oluşturacağını anlatmaktadır. Veya önceden ilgili tablo üretilmişse, onu temporal table'a çevirecektir.
class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}

class DbContext
{
    //


    // OnModelCreating {
    // modelBuilder.Entity<Employee>.ToTable("Employees", builder => builder.IsTemporal());
}
#endregion

#endregion

#region Temporal Table üzerinden geçmiş verisel izleri sorgulama

#region TemporalAsOf

//Belirli bir zaman için değişikliğe uğrayan tüm öğeleri döndüren bir fonksiyondur.

//context.Persons.TemporalAsOf(2024,05,09,11,22,53).Select(p=> new { p.Id, p.Name, EF.Property<DateTime>(p,"PeriodStart") }).ToListAsync();
// PeriodStart ve PeriodEnd shadow property olduğu için o şekilde aldık.
#endregion

#region TemporalAll

// Güncellenmiş yahut silinmiş olan tüm verilerin geçmiş sürümlerini veya geçerli durumlarını döndüren bir fonksiyondur.
//context.Persons.TemporalAll().Select(p=> new { p.Id, p.Name, EF.Property<DateTime>(p,"PeriodStart") }).ToListAsync();
#endregion

#region TemporalFtomTo

//Belirli bir zaman aralığı içerisindeki verileri döndüren fonksiyondur. Başlangıç ve bitiş zamanı dahil değildir.
//var baslangic = new DateTime(2022,12,09,06,23,25);
//var bitis = new DateTime(2022,12,09,07,23,25);

//context.Persons.TemporalFromTo(baslangic,bitis).Select(p=> new { p.Id, p.Name, EF.Property<DateTime>(p,"PeriodStart") }).ToListAsync();


#endregion

#region Temporal Between

//Belirli bir zaman aralığı içerisindeki verileri döndüren fonksiyondur. Başlangıç dahil deği, bitiş zamanı dahil.


#endregion

#region TemporalContainedIn

//Belirli bir zaman aralığı içerisindeki verileri döndüren fonksiyondur. Başlangıç ve bitiş zamanı dahil.


#endregion

#endregion

#region Silinmiş bir veriyi temporal table'dan geri getirme

//Silinmiş bir veriyi temporal table'dan geri getirebilmek için öncelikle iligli verinin silindiği tarihi bulmamız gerekir. Ardından TemporalAsOf fonksiyonu ile silinen verinin geçmiş değeri elde edilebilir ve fiziksel tabloya bu veri taşınabilir.

//context.Persons.TemporalAll()
//.Where(p=> p.Id==3)
//.OrderByDescending(p=>EF.Property<DateTime>(p,"PeriodEnd"))
//.Select(p=>EF.Property<DateTime>(p,"PeriodEnd")).FirstAsync();

// var deletedPerson = await context.Persons.TemporalAsOf(dateOfDelete.AddMiliSeconds(-1))
//.FirstOrDefaultAsync(p=>p.Id==3);


//await context.AddAsync(deletedPerson);

//await context.Database.OpenConnectionAsync();
//await context.Database.ExecuteSqlInterpolatedAsync($"SET_IDENTITY_INSERT dbo.Persons ON");
//await context.SaveChangesAsync();
//await context.Database.ExecuteSqlInterpolatedAsync($"SET_IDENTITY_INSERT dbo.Persons OFF");


#region SET_IDENTITY_INSERT konfigürasyonu

//Id bazlı veri ekleme sürecinde  ilgili verinin id sütununa kayıt işleyebilmek içn veriyi fiziksel tabloya taşıma işleminden önce veritabanı seviyesinde SET IDENTITY_INSEERT komutu eşliğinde id bazlı veri ekleme işlemi aktifleştirilmelidir. 

#endregion



#endregion