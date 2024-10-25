#region Eager Loading Nedir ?

// Generate edilen bir sorguya ilişkisel verilerin parça parça eklenmesini sağlayan ve bunu yaparken iradeli bir şekilde yapmamızı sağlayan bir yöntemdir.
//Eager Loading, arkaplanda üretilen sorguya Join uygular.

#endregion


#region Include

//Eager Loading operasyonunuzu yapmamızı sağlayan bir fonksiyondur.
//Yani üretilen bir sorguya diğer ilişkisel tabloların dahil edilmesini sağlayan bir işleve sahiptir.

// var employees = await context.Employees.Include(e=>e.Orders).ToListAsync();
// var employees = await context.Employees.Include("Orders").ToListAsync();

// var employees = await context.Employees.Include(e=>e.Orders).Include(e=>e.Regions).ToListAsync();

//Include'lar arasına Where vb. sorgular da ekleyebiliriz. EF sorguyu komple görüp sorguya çevirdiği için sıralamaları önemli değildir.
#endregion

#region ThenInclude

//Üretilen sorguda Include eidlen tabloların ilişkili olduğu diğer tabloları da sorguya ekleyebilmek için kullanılan bir fonksiyondur.
//Eğer ki, üretilen sorguya include edilen navigation property kolleksiyonel bir property ise o zaman bu property üzerinden diğer ilişkisel tabloya erişilemez. Böyle bir durumda koleksiyonel propertylerin türlerine erişip, o tür ile ilişkili diğer tabloları da sorguya eklememizi sağlayan fonksiyondur.

//var orders = await context.Orders.Include(o=>o.Employee).ThenInclude(e=>e.Orders).ToListAsync();



#endregion


#region FilteredInclude

//Sorgulama süreçelerinde Include yparken sonuçlar üzerinde filtreleme ve sıralama gerçekleştirmemizi sağlayan bir özelliktir.

//var regions = await context.Regions.Include(r=>r.Employees.Where(e=>e.Name.Contains("a")).ToListAsync();


// OrderByDescending, Where, ThenBy, ThenByDescending, Skip, Take gibi fonksiyonları destekler.
// Change Tracker'ın aktif olduğu durumlarda Include edilmiş sorgular üzerindeki filtreleme sonuçları beklenmeyen olabilir. Bu durum, daha önce sorgulanmış ve Change Tracker tarafından takip edilmiş veriler arasında filtrenin gereksinimi dışında kalan veriler için söz konusu olacaktır. Bundan dolayı sağlıklı bir filtered include operasyonu için change trackerın kullanılmadığı sorguları tercih etmeyi düşüneblirsiniz.
#endregion


#region Eager Loading için Kritik bir bilgi

// EF Core, önceden üretilmiş ve execute edilerek verileri belleğe alınmış sorguların verilerini sonraki sorgularda KULLANIR !


// var orders = await context.Orders.ToListAsync();

//var employees = await context.Employees.ToListAsync();

// Bu sorguların sonucunda include atmasak bile Employees verileri içinde ilişkili orders verileri de gelir. Yani bu tarz senaryolarda Include kullanmamıza gerek yoktur. Daha verimli olur.


#endregion


#region AutoInclude

//Uygulama seviyesinde bir entitye karşılık yapılan tüm sorgularda kesinlikle bir tabloya Include işlemi gerçekleştirilecekse bunu her bir sorgu için Include kullanarak yapmaktansa merkezileştirilerek konfigüre edilir.


//modelBuilder.Entity<Employee>()
//              .Navigation(e=>e.Region)
//              .AutoInclude();

#endregion


#region IgnoreAutoIncldues

//AutoInclude ile verdiğimiz merkezi include talimatını bu fonksiyonla sorgu seviyesinde ignorelayabiliriz.

//var employees = await context.Employees.IgnoreAutoIncludes().ToListAsync();

#endregion

#region Birbirleri üzerinden türetilmiş Entityler arasında Include 

class Employee:Person
{
    public Order Order { get; set; }
}

class Person
{

}

class Order
{

}

#region as Operatörü ile sorgulama

// var persons = context.Persons.Include(p=>((Employee)p).Orders);

#endregion

#region Cast Operatörü ile Sorgulama
//var persons2 = context.Persons.Include(p=>((p as Employee).Orders);


#endregion

#region 2. Overload ile Include
// var persons3 = context.Persons.Include("Orders");

#endregion

#endregion