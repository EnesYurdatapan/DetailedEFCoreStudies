#region Data Seeding Nedir ?
//EF Core ile inşa edilen veritabanı içerisinde veritabanı nesneleri olabileceği gibi verilerin de migrate
//sürecinde üretilmesini isteyebiliriz.

//İşte bu ihtiyaca istinaden Seed Data özelliği ile EF Core üzerinden migrationlarda veriler oluşturulabilir ve migrate ederken bu verileri hedef tablolarımıza basabiliriz.

//Migrate sürecinde Seed datalar migrate sürecinde hazır verileri tablolara basabilmek için bunları  yazılım kısmında tutmamızı gerektirmektedir.Böylece bu veriler üzerinde veritabanı seviyesinde istenilen manipülasyonlar gönül rahatlığıyla gerçekleştirilebilir.


//Data Seeding özelliği şu noktalarda kullanılabilir ;

// > Test için geçici verilere ihtiyaç varsa,
// > Asp.NET Core'daki Identity yapılanmasındaki roller gibi static değerler de tutulabilir.
// > Yazılım için temel konfigürasyonel değerler.
#endregion


#region Seed Data Ekleme
//OnModelCreating metodu içerisinde Entity fonksiyonundan sonra çağırılan HasData fonksiyonu ilgili entitye karşılık Seed Dataları eklememizi sağlayan bir fonksiyondur.

//PK kolonu manuel olarak verilmelidir.


// modelBuilder.Entity<Blog>()
//              .HasData( new Blog(){ Id=1, Url = "www.bilmemne.com"},);
//                        new Blog(){ Id=2, Url = "www.bilmemne2.com"};

#endregion


#region İlişkisel verilerde seed data ekleme

// İlişkisel senaryolarda dependent table'a veri eklerken foreign key kolonuun propertyisi varsa eğer ona ilişkisel değeri vererek ekleme işlemini yapıyoruz. Aksi takdirde hata verecktir.


// modelBuilder.Entity<Post>()
//              .HasData( new Post(){ Id=1, BlogId=1,Title = "A"},);
//                        new Post(){ Id=2,BlogId=1, Title = "B"};

#endregion


#region Seed Data'nın PK'ini değiştirme
// Eğer ki migrate edilen herhangi bir seed datanın sonrasında PK'i değiştirilirse bu datayla ilişkili veriler(varsa) cascade davranışı göstereceklerdir(yani silineceklerdir.)


#endregion