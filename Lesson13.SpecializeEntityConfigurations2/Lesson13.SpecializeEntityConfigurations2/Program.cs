#region Composite Key

// Bir tabloda birden fazla kolonu bileşik olarak primary key yapmak istiyorsak buna composite key denir.

class A
{
    public int Id { get; set; }
    public int Id2 { get; set; }
}

// modelBuilder.Entity<A>()
// .HasKey("Id","Id2");

#endregion

#region HasDefaultSchema

// EF Core üzerinden inşa edilen herhangi bir veritabanı nesnesi default olarak dbo şemasına sahiptir.
//Bunu özelleştirmek için yapılan bir yapılandırmadır.

// modelBuilder.HasDefaultSchema("ahmet");





#endregion


#region Property

#region HasDefaultValue

//Bir kolona default değer vermek için kullanırız.

//modelBuilder.Entity<Person>()
//.Property(p=>p.Salary)
//.HasDefaultValue(100);

#endregion


#region HasDefaultValueSql

// Tablodaki herhangi bir kolonun değer gönderilmediği durumlarda default olarak hangi sql cümleciğinden değeri alacağını belirler.
// Yani değer ataması veritabanı sunucusunda, verdiğimiz parametredeki SQL sorgusuyla yapılır.

//modelBuilder.Entity<Person>()
//.Property(p=>p.CreatedDate)
//.HasDefaultValueSql("GETDATE()");


#endregion


#endregion


#region HasComputedColumnSql

// Tablolarda birden fazla kolondaki verileri işleyerek değerini oluşturan kolonlara denir.
//EF ccore üzerinden bu tarz computed column oluşturabilmek için kullanılan yapılandırmadır.

class Example
{
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Computed { get; set; }

}



//modelBuilder.Entity<Example>()
//.Property(e=>e.Computed)
//.HasComputedColumnSql("[X] + [Y]");

#endregion


#region HasConstraintName

// EF Core üzerinden oluşturulan constraintlere default isim yerine özelleştirilmiş isim vermek için kullanılan yapılandırmadır.


//modelBuilder.Entity<Person>()
//.HasOne(p=>p.Department)
//.WithMany(p=>p.Persons)
//.HasForeignKey(p=>p.DepartmentId)
//.HasConstraintName("ahmet");

#endregion


#region HasData

// Seed Data isimli bir kavram var. Bu, migrate sürecinde veritabanını inşa ederken bir yandan da yazılım üzerine hazır veriler oluşturmak istiyorsak kullanırız.
//HasData konfigürasyonu bu operasyonun yapılandırma ayağıdır.

//modelBuilder.Entity<Person>()
//.HasData(new Person {Id=2, Department = new(){ Name="asd"}, Name="ahmet",Surname="Yılmaz", Salary = 150000, CreatedDate = DateTime.Now})


//Burada person migrate ederken verdiğimiz verilerin de tabloya atılması gerektiğini bildirdik.
//HasData ile migrate sürecinde oluşturulacak olan verilerin PK olan id kolonlarına değer girilmesi mecburdur.
#endregion


#region HasDiscriminator

//Table Per Hierarchy, Table Per Type senaryolarında Discriminator isimli bir kolon oluşturulur.
//Bu kolonun ismini ve hatta tipini özelleştirmek için kullanılan konfigürasyondur.

 class Entity
{
    public int Id { get; set; }
    public string X { get; set; }
}
class D : Entity
{
    public int Y { get; set; }
}

class E : Entity
{
    public int Z { get; set; }
}

//public DbSet<A> As { get; set; }
//public DbSet<B> Bs { get; set; }
//public DbSet<Entity> Entities { get; set; }

//modelBuilder.Entity<Entity>()
//.HasDiscriminator<string>("Ayırıcı");


#endregion



#region HasField

//Backing Field özelliğini kullanmamız için uyguladığımız bir yapılandırmadır.

////modelBuilder.Entity<Person>()
//.Property(p=>p.Name)
//.HasField("nameof(Person._name))");

#endregion


#region HasNoKey

//Normal şartlarda EF Core'da tüm entitylerin bir PK kolonu olmak zorunda.
//Eğer ki entityde PK kolonu olmayacaksa bunun bildirilmesi gerekir.


//modelBuilder.Entity<Person>()
//.HasNoKey();

#endregion



#region  HasIndex

// EF Core'da index yapılanmasını ayarlamak için kullanılan konfiügrasyon

//modelBuilder.Entity<Person>()
//.HasIndex(p=>p.Name);

#endregion


#region HasQueryFilter

// Temeldeki görevi bir entitye karşılık uygulama bazında bir filtre koymaktır.

//modelBuilder.Entity<Person>()
//.HasQueryFilter(p=>p.CreatedDate.Year==DateTime.Now.Year);

// örneğin .ToList sorgusuna .Where il gizliden yukardaki filtreyi de ekler.

#endregion