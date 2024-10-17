#region Sequence nedir ?

//Veritabanında benzersiz ve ardışık sayısal değerler üreten veritabanı nesnesidir.
//Sequence herhangi bir tablonun özelliği değildir. Veritabanı nesnesidir. Birden fazla tablo üzerinde kullanılabilir.

#endregion

#region Sequence Tanımlama

//Sequenceler üzerinden değer oluştururken veritabanına özgü çalışma yapılması zaruridir. SQL Server'a özel yazılan Sequence tanımı örneğin Oracle'da hata verebilir.

// modelBuilder.HasSequence("EC_Sequence");
//modelBuilder.Entity<Employee>()
//             .Property(e=>e.Id)
//             .HasDefaultValueSql("NEXT VALUE FOR EC_Sequence");

#endregion

#region Sequence Yapılandırması

#region StartsAt & IncrementsBy

// Kaç ile başlayıp ne kadar artış göstereceğini gösteren konfigürasyonlardır

// modelBuilder.HasSequence("EC_Sequence")
//             .StarsAt(10);
//              .IncrementsBy(2);

#endregion

#endregion


#region Sequence ile Identity Farkı

// Squence bir veritabanı nesnesiyken, identity tablo özelliğidir.
// Yani Sequence herhangi bir tabloya bağımlı değildir.
//Identity bir sonraki değeri diskten alırken Sequence ise RAM'den alır. Bu yüzden önemli ölçüde Sequence daha hızlı ve az maliyetlidir.

#endregion