using MathNet.Numerics;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#region EF Core'da neden konfigürasyonlara ihtiyaç duyarız ?

//Default davranışları yeri geldiğinde geçersiz kılmak ve özelleştirmek isteyebiliriz.

// Örneğin EFCore'da veritabanı tabloları isimlerini DbSet Propertysinden alır.
// Mesela bunu özelleştirip farklı bir yerden almasını isteyebiliriz. Gibi..

#endregion

#region OnModelCreating Metodu

//EF Core'da yapılandırma denilince akla ilk gelen metot OnModelCreating metodudur.
// Bu metot Context sınıfı içinde virtual olarak ayarlanmış bir metottur.
// Biz bu metodu kullanarak modellarımızla ilgili temel konfigürasyonel davranışları(Fluent API) sergileyebiliriz.

// Bir model'ın yaratılışıyla ilgili tüm konfigürasyonları burada gerçekleştirebiliriz.

#region GetEntityTypes
//EF core'da kullanılan entity'leri elde etmek, programatik olarak öğrenmek istiyorsak kullanırız.

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//   var entities = modelBuilder.Model.GetEntityTypes();
//}


#endregion
#endregion

#region Table - ToTable
// Generate edilecek tablonun ismini belirlememizi sağlayan yapılandırmadır.
// EF Core normal şartlarda generate edeceği tablonun adını DbSet property'sinden almaktadır.
// Eğer bunu özelleştirmek istiyorsak Table attribute'ünü veya ToTable api'ını kullanabiliriz.

// -------------------- DATA ANNOTATIONS ------------------------------


[Table("Kisiler")]
class Person
{
    public int Id { get; set; }
}


// ------------ FLUENT API ----------------------

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//   modelBuilder.Entity<Person>().ToTable("Kisiler");
//}


#endregion

#region Column - HasColumnName, HasColumnType, HasColumnOrder

//EF Core'a tabloların kolonları entity sınıfları içindeki propertylere karşılık gelir
// Default olarak property'lerin adı kolon adıyken, türleri/tipleri kolon türleridir.
//Bunları da özelleştirmek istiyorsak :

// ----------------DATA ANNOTATIONS ------------------------

class Department
{
    public int Id { get; set; }
    [Column("Adı",TypeName ="Ahmet", Order = 5)]
    public string Name { get; set; } // Ahmet tipinde bir "Adı" kolonu
}

// ----------------------Fluent API ------------------------

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//   modelBuilder.Entity<Department>()
//      .Property(d=>d.Name)
//      .HasColumnName("Adı")
//      .HasColumnType("asfgdgdf")
//      .HasColumnOrder(5);
//}

#endregion


#region ForeignKey - HasForeignKey

// İlişkisel tablo tasarımlarında, bağımlı tabloda esas tabloya karşılık gelecek verilerin tutulduğu kolonu foreign key olarak temsil ederiz.
//EF Core'da foreign key kolonu genellikle otomatik oluşturulur, fakat biz farklı bir name convention ile oluşturmak istiyorsak özelleştirebiliriz.


// Örneğini daha önceki derslerde data annotations ve fluent API üzerinde görmüştük.

#endregion


#region NotMapped - Ignore

// EF Core entity sınıfları içindeki tüm propertyleri default olarak modellenen tabloya kolon olarak migrate eder.
//Bazen entity sınıfları içinde tabloda bir kolona karşılık gelmeyen propertyler tanımlamak zorunda kalabiliriz.

// Bu Propertylerin EF Core tarafından kolon olarak map edilmesini istemediğimizi şu şekilde belirtebiliriz :


// ------------ Data Annotations ------------------
class A
{
    public int Id { get; set; }
    public string Name { get; set; }
    [NotMapped] // Görmezden gel, kolon değil bu demek
    public string Zamazingo { get; set; }
}

// ---------------------- Fluent API ------------------------

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//      modelBuilder.Entity<A>()
//          .Ignore(a=>a.Zamazingo);
//}

#endregion


#region Key-HasKey

//EF Core'da default convention olarak bir entitynin içindeki Id,ID, EntityId gibi tanımlanan tüm propertylere varsayılan olarak primary key constraint'i uygulanır
//Key attribute ile ya da HasKey apisi yapılanmalarıyla primary key propertysini istediğimiz gibi ayarlayabiliriz.

class B
{
    [Key]
    public int Ahmet { get; set; }
}

// Fluent API -- 
// .HasKey(b=>b.Ahmet);

#endregion


#region Timestamp - IsRowVersion

// Veri tutarlılığı için satırın versiyonunu tutmak gerekir.
// Verinin versiyonunu oluşturmamız için yapmamız gereken konfigürasyonlardır.

class C
{
    public int ID { get; set; }
    public string Name { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
}



// modelBuilder.Entity<C>()
// .Property(c=>c.RowVersion)
// .IsRowVersion();

#endregion


#region Required - IsRequired

//Bir kolonun nullable ya da not nullable olmasını belirleriz.
// EF Core'da bir property default olarak not null olarak tanımlanır. 
//Eğer ki property'i nullable yapmak istiyorsak türü üzerinde ?(nullable) operatörü ile nitelendiririz.

class D
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } // eğer string? yazsaydık Nullable olurdu
}


// modelBuilder.Entity<D>()
// .Property(d=>d.Name)
// .IsRequired();

#endregion


#region MaxLength - HasMaxLength - StringLength

class E
{
    public int Id { get; set; }
    [MaxLength(100)]
   // [StringLength(100)]
    public string Name { get; set; }

}

// modelBuilder.Entity<E>()
// .Property(e=>e.RowVersion)
// .MaxLength(100);

#endregion


#region Unicode - IsUnicode

// kolon içerisinde unicode karakterler kullanılacaksa kullanılır.

class G
{
    public int Id { get; set; }
   // [UniCode]
    public string Name { get; set; }
}

#endregion


#region Comment - HasComment

//EF Core üzerinden oluşturulmuş olan veritabanı nesneleri üzerinde bir açıklama/yorum yapmak istiyorsanız Comment'i kullanabilirsiniz.

// Veritabanında da bu açıklama gözükür.
class H
{
    public int Id { get; set; }
    [Comment("Bu bir propertydir.")]
    public int MyProperty { get; set; }
}


// modelBuilder.Entity<H>()
//  .HasComment("Bu tablo şuna yaramaktadır..")
// .Property(c=>c.MyProperty)
// .HasComment("Bu bir propertydir.");

#endregion


#region ConcurrencyCheck - IsConcurrencyToken

//Verinin bütünsel olarak tutarlılığını sağlayan concurrency token denilen bir yapı vardır.

class I
{
    public int Id { get; set; }
    [ConcurrencyCheck]
    public int MyProperty { get; set; }
}


// modelBuilder.Entity<I>()
// .Property(i=>i.MyProperty)
// .IsConcurrencyToken();

#endregion


#region InverseProperty

//2 Entity arasında birden fazla ilişki varsa eğer bu ilişkilerin hangi navigation propertyler üzerinden olduğunu anlayacağımız propertylerdir.


class Flight
{
    public int Id { get; set; }
    public Airport DepartureAirport { get; set; }
    public Airport ArrivalAirport { get; set; }

}
class Airport
{
    public int Id { get; set; }
    [InverseProperty(nameof(Flight.DepartureAirport))]

    public ICollection<Flight> DepartureFlights { get; set; }
    [InverseProperty(nameof(Flight.ArrivalAirport))]
    public ICollection<Flight> ArrivalFlights { get; set; }
}

#endregion