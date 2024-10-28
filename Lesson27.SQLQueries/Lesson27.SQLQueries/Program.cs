//Eğer ki sorguyu LINQ ile ifade edemiyorsak, veya LINQ'in ürettiği sorguya nazaran daha optimize bir sorguyu manuel geliştirmek ve EF Core üzerinden execute etmek istersek EF Core bu davranışı destekler.
//Tarafımızca oluşturulmuş sorguları EF Core tarafından execute edebilmek için o sorgunun sonucunu karşılayacak bir entity modelinin tasarlanmış ve bunun DbSet olarak context nesnesine tanımlanmış olması gerekiyor.

using System.Reflection;

class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public ICollection<Order> Order { get; set; }
}
class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }
    public Person Person { get; set; }
}

#region FromSqlInterpolated

//EF Core 7.0 sürümünden önce ham sorguları execute edebildiğimiz fonksiyondur.
//var persons = await context.Persons.FromSqlInterpolated($"SELECT * FROM Persons").ToListAsync();

#endregion

#region FromSql

//EF Core 7.0 ile gelen metottur. FromSqlInterpolated ile aynı işlevi görür.

#region Query Execute

//var persons = await context.Persons.FromSql($"SELECT * FROM Persons").ToListAsync();

#endregion

#region Stored Procedure Execute

//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons NULL").ToListAsync();


#endregion

#region Parametreli Sorgu Oluşturma

//------------------- Örnek 1 -------------------------

//int personId=3;
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons Where PersonId={personId}").ToListAsync();

//Burada sorguya geçirilen personId değişkeni arka planda bir DbParameter türüne dönüştürülerek o şekilde sorguya dahil edilir.

// ------------------------- Örnek 2 ----------------------------------

//var persons = await context.Persons.FromSql($"EXECUTE dbo.sp_GetAllPersons @PersonId = {personId}").ToListAsync();


//--------------------------- Örnek 3 ------------------------------------------

//SqlParameter personId = new("PersonId","3");
//personId.DbType= System.Data.DbType.Int32;
//personId.Direction= System.Data.ParameterDirection.Input;
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons Where PersonId={personId}").ToListAsync();

//Bu örnekte kendimiz SqlParameter olarak verdiğimiz için dönüşüme gerek kalmaz. Ufak da olsa performans optimizasyonu sağlar.


#endregion

#endregion

#region Dynamic SQL Oluşturma ve Parametre Girme - FromSqlRaw

//string columnName= "PersonId" , value="3";
//var persons = await context.Persons.FromSql($"SELECT * FROM Persons Where {columnName} = {value}").ToListAsync();

//Yukardaki kod ÇALIŞMAZ !
//Ef Core dinamik olarak oluşturulan sorgularda özellikle kolon isimleri parametreleştirilmişsse o sorguyu çalıştırmaz !

//string columnName= "PersonId";
//SqlParameter value = new("PersonId","3");
//var persons = await context.Persons.FromSqlRaw($"SELECT * FROM Persons Where {columnName} = @PersonId",value).ToListAsync();

//FromSql ve FromSqlInterpolated metotlarında SQL injection vs gibi güvenlik önlemleri alınmış vaziyettedir. Lakin dinamik olarak sorguları oluşturuyorsanız eğer burada güvenlik geliştirici tarafından sağlanmalıdır. Yani gelen sorguda yorumlar, noktalı virgüller veya SQL'e özel karakterlerin algılanması ve bunların temizlenmesi geliştirici tarafından sağlanır.

#endregion

#region SqlQuery - Entity olmayan Scalar Sorguların Çalıştırılması - Non Entity

//Entitysi olmayan scalar sorguların çalıştırılıp sonucunu elde etmeemizi sağlayan yeni bir fonksiyondur.
//var data = await context.Database.SqlQuery<int>($"SELECt PersonId FROM Persons").ToListAsync();

//SqlQuery'de LINQ operatörleriyle sorguya ekstradan katkıda bulunmak istiyorsanız eğer bu sorgu neticesinde gelecek olan kolonun adını Value olarak bildirmeniz gerekir. Çünkü, SqlQuery sorguyu bir subquery olarak generate etmektedir. Haliyle bu durumdan dolayı LINQ ile verilen şart ifadeleri statik olarak Value kolonuna göre tasarlanmıştır. O yüzden b şekilde bir çalışma zorunlu hale gelmektedir.
//var data = await context.Database.SqlQuery<int>($"SELECt PersonId Value FROM Persons").Where(x=>x>5).ToListAsync();


#endregion


#region ExecuteSql

// context.Database.ExecuteSqlAsync($"UPDATE Persons SET Name='Fatma' WHERE PersonId="1");

#endregion

#region Sınırlılıklar

//Queryler entity türünün tüm özellikleri için kolonlarda değer döndürmelidir.
//var persons = await context.Persons.FromSql($"SELECT Name FROM Persons").ToListAsync(); >> Sadece Name döndürüldüğü için hata verir.

// Sütun isimleri property isimleriyle aynı olmalıdır.

//SQL sorgusu Join yapısı içeremez !! Bu tarz ihtiyaç noktalarında include fonksiyonu kullanılmalıdır.


#endregion