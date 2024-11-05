#region QueryTags nedir?

//EF Core ile generate edilen sorgulara açıklama eklememizi sağlayarak; SQL Profiler, Query Log vs gibi yapılarda bu açıklamalar eşliğinde sorguları gözlemlememizi sağlayan bir özelliktir.


#endregion

#region TagWith Metodu

//ToList ile sorgu execute edilmeden önce çağırılıp sorguya eklenir. 

//await context.Persons.TagWith("Örnek açıklama...").ToListAsync();

#endregion

#region Multiple TagWith

//Bir sorguya birden fazla açıklama eklenebilir.
//await context.Persons.TagWith("Örnek açıklama...").Include(p=>p.Orders).TagWith("Personeller ilişkilendirildi...").ToListAsync();


#endregion

#region TagWithCallSite Metodu

//Oluşturulan sorguya açıklama satırı ekler, ek olarak bu sorgunun bu .cs dosyasında hangi satırda üretildiği bilgisini de verir.

//await context.Persons.TagWithCallSite("Örnek açıklama...").ToListAsync();


#endregion