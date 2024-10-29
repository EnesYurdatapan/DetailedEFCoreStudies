using System.ComponentModel.DataAnnotations.Schema;
//Store Procedure, view'ler gibi kompleks sorgularımızı daha basit bir şekilde tekrar kullanılabilir bir hale getirmemizi sağlayan veritabanı nesnesidir.
//View'ler tablo gibi bir davranış sergilerken ; SP ise fonksiyonel bir davranış sergilerler. Birçok + yanı vardır.

#region EF Core ile Stored Procedure Kullanımı

#region Store Procedure oluşturma

//1. Adım : Boş bir Migration oluşturulur.
//2. Adım : Migration içerisindeki Up fonksiyonuna SP'ın Create komutları yazılır. Down fonksiyonuna ise Drop komutları yazılır.
//3. Adım : Migration basılır.

//protected override void Up(MigrationBuilder migrationBuilder)
//{
//migrationBuilder.Sql($@" CREATE PROCEDURE sp_PersonOrders AS  ****** Buraya istenen SQL sorgusu yazılacak ******);
//}

//protected override void Down(MigrationBuilder migrationBuilder)
//{
//migrationBuilder.Sql($@" DROP PROCEDURE sp_PersonOrders AS  ****** Buraya istenen SQL sorgusu yazılacak ******);
//}

#endregion

#region Store Procedure kullanma

//SP kullanabilmek için bir entity'e ihtiyacımız vardır. Bu entity'nin DbSet propertysi olarak context nesnesine de eklenmesi gerekir.
// Bu DbSet propertysi üzerinden FromSql fonksiyonunu kullanarak "EXEC ......" komutu eşliğinde SP yapılanmasını çalıştırıp sorgu sonucunu elde ederiz.

#region FromSql


[NotMapped]
class PersonOrder
{
    public string Name { get; set; }
    public int Count { get; set; }
}
class Context
{
    //public DbSet<PersonOrder> PersonOrders { get; set; }
}
// modelBuilder.Entity<PersonOrder>().HasNoKey();


// var datas = await context.PersonOrders.FromSql($"EXEC sp_PersonOrders").ToListAsync();

#endregion

#endregion

#region Geriye Değer Döndüren Stored Procedure Kullanma

//SqlParameter countParameter = new(){ ParameterName="count", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Output };
// context.Database.ExecuteSqlRawAsync($"EXEC @count = sp_bestSellingStaff", countParameter);
// Console.WriteLine(countParameter.Value);

#endregion

#region Parametreli Stored Procedure kullanımı

//protected override void Up(MigrationBuilder migrationBuilder)
//{
//migrationBuilder.Sql($@" CREATE PROCEDURE sp_PersonOrders2 (@PersonId INT, @Name NVARCHAR(MAX) OUTPUT)
//                          AS
//                          SELECT @Name = p.Name FROM Persons p
//                          JOIN Orders o
//                          ON p.PersonId = o.PersonId
//                          WHERE p.PersonId=@PersonId");
//}

//protected override void Down(MigrationBuilder migrationBuilder)
//{
//migrationBuilder.Sql($@" DROP PROCEDURE sp_PersonOrders2 ");
//}

#region Input Parametreli Stored Procedure kullanımı

//SqlParameter countParameter = new(){ ParameterName="name", SqlDbType = System.Data.SqlDbType.NVarChar, Direction = System.Data.ParameterDirection.Output, Size=1000 };

// context.Database.ExecuteSqlRawAsync($"EXECUTE sp_PersonOrders2 5,@name OUTPUT",nameParameter);
// Console.WriteLine(nameParemeter.Value);

#endregion

#region Output Parametreli Stored Procedure kullanımı



#endregion


#endregion

#endregion