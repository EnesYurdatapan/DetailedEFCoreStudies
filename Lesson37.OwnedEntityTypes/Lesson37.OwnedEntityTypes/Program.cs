#region Owned Entity Types Nedir ?

// EF Core entity sınıflarını parçalayarak propertylerini kümesel olarak farklı sınıflarda barındırmamızı ve tüm bu sınıfları ilgili entity'de birleştirip bütünsel olarak çalışmamıza izin vermektedir.
// Böylece bir entity, sahip olunan(owned) birden fazla alt sınıfın birleşmesiyle meydana gelebilmektedir.


//Domain Driven Design yaklaşımında Value Object'lere karşılık olarak Owned Entity Types'lar kullanılır.
#endregion

#region Owned Entity Types Nasıl Uygulanır ?

//Normal bir entityde farklı sınıfların referans edilmesi primary key vs. gibi hatalara sebebiyet verir. Çünkü direkt sınıfın referans olarak alınması ef core tarafından ilişkisel bir tasarım olarak algılanır. Bizlerin entity içerisindeki propertyleri kümesel olarak barındıran sınıfları o entitynin bir parçası olduğunu bildirmemiz gerekir.

class Employee
{
    public int Id { get; set; }
    //public string Name { get; set; }
    //public string MiddleName { get; set; }
    //public string LastName { get; set; }
    //public string StreetAddress { get; set; }
    //public string Location { get; set; }
    public bool IsActive { get; set; }
    public EmployeeName EmployeeName { get; set; }
    public Address Address { get; set; }
}

class EmployeeName
{
    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
}

class Address
{
    public string StreetAddress { get; set; }
    public string Location { get; set; }
}

#region OwnsOne Metodu

//modelBuilder.Entity<Employee>.OwnsOne(e=>e.EmployeeName, builder=>builder.Property(e=>e.Name).HasColumnName("Name"));
//modelBuilder.Entity<Employee>.OwnsOne(e=>e.Address);

#endregion

#region Owned Attribute

//[Owned]
//class EmployeeName
//{
//    public string Name { get; set; }
//    public string MiddleName { get; set; }
//    public string LastName { get; set; }
//}

//[Owned]
//class Address
//{
//    public string StreetAddress { get; set; }
//    public string Location { get; set; }
//}

#endregion

#region  IEntityTypeConfiguration<T>

//class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
//{
//    public void Configure(EntityTypeBuilder<Employee> builder)
//    {
//        //builder.OwnsOne(e=>e.EmployeeName, builder=>builder.Property(e=>e.Name).HasColumnName("Name"));
//        //builder.Entity<Employee>.OwnsOne(e=>e.Address);
//    }
//}

//OnModelCreating fonksiyonu içinde : 
//    modelBuilder.ApplyConfiguration(new EmployeeConfiguration());


#endregion

#region OwnsMany metodu


//Entitynin farklı özelliklerine başka bir sınıftan ICollection türünde Navigation Property aracılığı ile ilişkisel olarak erişebilmemizi sağlayan bir işleve sahiptir.
//Normalde Has ilişkisi olarak kurulabilecek bu ilişkinin temel farkı, Has ilişkisi DbSet propertysi gerektirirken, OwnsMany metodu ise DbSet'e ihtiyaç duymaksızın gerçekleştirmemizi sağlar.

class Employee2
{
    public ICollection<Order> Orders { get; set; }
}
class Order
{
    public string OrderDate { get; set; }
    public int Price { get; set; }
    public int MyProperty { get; set; }
    public Employee2 Employee { get; set; }
}

//modelBuilder.Entity<Employee>().OwnsMany(e=>e.Orders, builder=>
//{
//builder.WithOwner().HasForeginKey("OwnedEmployeeId");
//builder.Property<int>("Id");
//builder.HasKey("Id");
//};

#endregion

#endregion

#region Sınırlılıklar

//Herhangi bir owned entity type için DbSet propertysine ihtiyaç yoktur.
//OnModelCreating fonksiyonunda Entity<T> metodu ile Owned Entity Type türünden bir sınıf üzerinde herhangi bir konf. gerçekleştirilemez.

//Owned Entity Type'ların kalıtımsal hiyerarşi desteği yoktur.

#endregion

