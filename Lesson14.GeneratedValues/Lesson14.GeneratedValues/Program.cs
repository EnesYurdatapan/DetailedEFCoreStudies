#region GeneratedValue nedir ?

// EF Core'da üretilen değerlerle ilgili çeşitli modellerin ayrıntılarını yapılandırmamızı sağlayan bir konfigürasyondur.

#endregion

#region Default Values

//EF Core'da herhangi bir tablonun herhangi bir kolonuna yazılım tarafından bir değer gönderilmediği taktirde bu kolona hangi değerin atanacağını belirleyen yapılandırmadır.

#region HasDefaultValue
//Static veri verir.

//modelBulder.Entity<Person>()
//.Property(p=>p.Salary)
//.HasDefaultValue(150000);
#endregion

#region HasDefaultValueSql
// Veritabanı üzerinden verilen sorguyla ekler.


//modelBulder.Entity<Person>()
//.Property(p=>p.Salary)
//.HasDefaultValueSql("FLOOR(RAND()*1000)");

#endregion

#endregion


#region Computed Columns

#region HasComputedColumnSql

//Tablo içerisindeki kolonlar üzerinden yapılan aritmetik işlemler sonucunda üretilen kolon.

//modelBulder.Entity<Person>()
//.Property(p=>p.TotalGain)
//.HasComputedColumnSql("([Salary] + [Premium]) * 10");

#endregion


#endregion


#region Value Generation

#region Primary Keys

// Herhangi bir tablodaki satırları kimlik olarak tanımlayan, tekil olan sütun veya sütunlardır.

#endregion


#region Identity

//Yalnızca otomatik olarak artan bir sütundur.Bir sütun, PK olmadan da identity olarak tanımlanabilir.
//Bir tablo içinde identity kolonu sadece 1 tane olabilir.

#endregion

//Bu iki özellik genellikle birlikte kullanılır. O yüzden EF Core PK olan bir kolonu otomatik olarak Identity olacak şekilde yapılandırır.
// Ancak bu bir gereklilik değildir. PK identity olmayabilir.

#endregion


#region Database Generated

#region DatabaseGeneratedOption.None - ValueGeneratedNever
//Bir kolonda değer üretilmeyecekse eğer None ile işaretleriz.
//EF Core'un default olarak PK kolonolarda getirdiği Identity özelliğini kaldırmak istiyorsak None'ı kullanabiliriz.


// --------------- FLUENT API ------------------------

//modelBulder.Entity<Person>()
//.Property(p=>p.PersonId)
//.ValueGeneratedNever();


// ------------------- Data Annotations -------------

using System.ComponentModel.DataAnnotations.Schema;

class A
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int AId { get; set; }
}
#endregion

#region DatabaseGeneratedOption.Identity - ValueGeneratedOnAdd
//Kolonun ardışık artmasını istiyorsak kullanırız.

#region Sayısal Türlerde
//Eğer ki Identity özelliği sayısal olan bir kolonda kullanılacaksa o duruumda ilgili tablodaki PK kolonundan iradeli bir şekilde identity özelliğinin kaldırılması gerekir.
class B
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int BId { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BCode { get; set; }
}

#endregion

#region Sayısal Olmayan Türlerde

//Eğer ki Identity özelliği sayısal olmayan bir kolonda kullanılacaksa o durumda PK'dan identity özelliğini elle kaldırmamıza gerek yoktur.


class C
{
    public int CId { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid CCode { get; set; }
}

// Eğer bu yeni türün herhangi bir arttırma mekanizması yoksa diye HasDefaultValueSql ile default value atmamız gerekir.

//modelBulder.Entity<Person>()
//.Property(p=>p.Salary)
//.ValueGeneratedOnAdd() >>>>>>> Identity Annotation'ın muadili
//.HasDefaultValueSql("NEWID()");

#endregion

#endregion

#region DatabaseGeneratedOption.Computed - ValueGeneratedOnAddOrUpdate

//EF Core üzerinde bir kolon Computed Column ise ister Computed olarak belirleyebiliriz, belirlemeden de kullanabiliriz.



#endregion

#endregion