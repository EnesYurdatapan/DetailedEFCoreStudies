#region Global Query Filters Nedir?

// Bir entity'e özel uygulama seviyesinde genel/ön kabullu şartlar oluşturmamızı sağlayan ve böylece verileri global bir şekilde filtrelememizi sağlayan bir özelliktir.

//Böylece belirtilen entity üzerinde yapılan tüm sorgulamalarda ekstradan bir şart ifadesine gerek kalmaksızın filtreleri otomatik uygulayarak hızlıca sorgulama yapmamızı sağlamaktadır.

//Genellikle uygulama bazında IsActive gibi verilerle çalışıldığı durumlarda,
//MultiTenancy uygulamalarda TenantId tanımlarken vs. kullanılabilir.
#endregion

#region Global Query Filters Nasıl Uygulanır ?



// modelBuilder.Entity<Person>.HasQueryFilter(p=>p.IsActive);

#endregion

#region Navigation Property üzerinden global query filter

// modelBuilder.Entity<Person>.HasQueryFilter(p=>p.Orders.Count>0);


#endregion

#region Global Query Filters nasıl ignore edilir ?

//var person = await context.Persons.IgnoreQueryFilters().ToListAsync();

#endregion

#region Dikkat edilmesi gereken husus!

//Global Query Filter uygulanan kolona farkında olmaksızın şart uygulanabilmektedir. Bu duruma dikkat edilmelidir.
//Yani globalde verilen filtre unutulup kod esnasında tekrar yazılabilme durumu.

#endregion
