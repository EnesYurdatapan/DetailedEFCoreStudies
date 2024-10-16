#region Table Per Concrete Type (TPC) Nedir?

//TPC davranışı, kalıtımsal ilişkiye sahip olan entitylerin olduğu çalışmalarda sadece concrete olan entitylere karşılık bir tablo oluşturacak davranış modelidir.
//TPC, TPT'nin daha performanslı versiyonudur.
// TPT gibi bire bir ilişki bulunmaz. Her tabloda kalıtım aldığı entitynin kolonları bulunur.
#endregion

#region TPC nasıl uygulanır ?

//Hiyerarşik düzlemde abstract olan yapılar üzerinden OnModelCreating Entity fonksiyonuyla konfigürasyona girip ardından UseTpcMappingStrategy fonksiyonu eşliğinde davranışın TPC olacağını belirleyebiliriz.

// modelBuilder.Entity<Person>().UseTpcMappingStrategy();

#endregion

#region TPC'de veri ekleme, silme, güncelleme, sorgulama

// CRUD işlemlerinde TPH,TPT'de de olduğu gibi normal süreçten hiçbir fark yoktur.

#endregion