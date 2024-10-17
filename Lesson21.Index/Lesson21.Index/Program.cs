#region Index Nedir ?

//Index,  bir sütuna dayalı sorgulamaları daha verimli ve performanslı hale getirmek için kullanılan yapıdır.


#endregion

#region Indexleme nasıl yapılır ?

//PK,FK ve AK olan kolonlar otomatik olarak indexlenir.

// --------------- Attribute ---------------------

//[Index(nameof(Name))]
class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

// --------------------- FluentAPI --------------------

// modelBuilder.Entity<Employee>()
//             .HasIndex(x=>x.Name);
#endregion

#region Composite Index

//[Index(nameof(Name),(nameof(Surname))]


// modelBuilder.Entity<Employee>()
//             .HasIndex(x=>new{x.Name,x.Surname});

//Composite Indexlemede sorgu da composite olmalıdır.
//Yani "context.Employees.Where(e=>e.Name=="rsgrdgd");" gibi bir sorguda işe yaramaz, "context.Employees.Where(e=>e.Name=="rsgrdgd" && e.Surname=="sgkdfkhd");" gibi olmalıdır.
#endregion

#region Birden Fazla Kolonu Ayrı Ayrı Indexleme

//[Index(nameof(Name))]   >> "context.Employees.Where(e=>e.Name=="rsgrdgd");"
//[Index(nameof(Surname))] >> "context.Employees.Where(e=>e.Surname=="rsgrdgd");"
//[Index(nameof(Name),(nameof(Surname))]  >> "context.Employees.Where(e=>e.Name=="rsgrdgd" && e.Surname=="sgkdfkhd");"

#endregion

#region Indexi Unique yapma

// attribute ile yaptığımızda daha önce gördüğümüz gibi IsUnique = true parametresiyle yaparız.

// FluentAPI karşılığı da HasIndex()'ten sonra .IsUnique() fonksiyonuyla konfigüre edilmesidir.

#endregion

#region Index Sort Order - Sıralama düzeni

#region AllDescending - Attribute

//Tüm indexlemelerde descending davranışının bütünsel olarak konfigürasyonunu sağlar.
//[Index(nameof(Name), AllDescending=true)]

#endregion

#region IsDescending - Attribute

//Composite Indexleme sürecindeki her bir kolona göre sıralama davranışını özel olarak ayarlamak istiyorsak kullanılır.

//[Index(nameof(Name), (nameof(Surname), IsDescending = new[]{true,false})]

#endregion

#region IsDescending FluentAPI

// modelBuilder.Entity<Employee>()
//             .HasIndex(x=>x.Name)
//              .IsDescending();

// modelBuilder.Entity<Employee>()
//             .HasIndex(x=> new{x.Name,x.Surname})
//              .IsDescending(true,false);

#endregion

#endregion

#region IndexFilter

// Attribute'ü yok, sadece HasFilter metodu var.
// Indexlenecek verinin filtrelenmesini sağlayıp hacmini düşürebiliriz.

// modelBuilder.Entity<Employee>()
//             .HasIndex(x=>x.Name)
//              .HasFilter("[NAME] IS NOT NULL");

#endregion

#region Included Columns

// Indexlenmiş propertyler üzerinden sorgu yapılırken, sorguya indexlenmemiş bir property de eklenirse normalde performans düşer. Fakat bu fonksiyonla biz indexlenmemiş propertyler sorguya eklendiğinde performans kaybını önleriz.

// modelBuilder.Entity<Employee>()
//             .HasIndex(x=> new {x.Name, x.Surname})
//             .IncludeProperties(x=>x.Salary);

//context.Employees.Where(x=>x.Name=="drht" && x.Surname ="dhrthjrt" && x.Salary=500);
#endregion