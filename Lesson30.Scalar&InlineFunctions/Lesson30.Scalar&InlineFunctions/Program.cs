#region Scalar Functions nedir ? 

// Geriye herhangi bir değer döndüren SQL fonksiyonlarıdır.


#endregion

#region Scalar Functions Oluşturma

//1. Adım : Boş bir migration oluşturulmalı.
//2. Adım : Bu migration içerisinde Up metodunda SQL metodu eşliğinde fonksiyonun create kodları, down metodu içerisinde de Drop kodları yazılacaktır.
//3. Adım : migrate edilmeli

//protected override void Up(MigrationBuilder migrationBuilder)
//{
//migrationBuilder.Sql($@"
//CREATE FUNCTION getPersonTotalOrderPrice(@personId INT)
//  RETURNS INT
//AS
//BEGIN
// DECLARE @totalPrice INT
// SELECT @totalPrice = SUM(o.Price) FROM Persons p
// JOIN Orders o
//      ON p.PersonId= o.PersonId
// WHERE p.PersonId=@personId
// RETURN @totalPrice
//END");
//}

//protected override void Down(MigrationBuilder migrationBuilder)
//{
//migrationBuilder.Sql($@" DROP FUNCTION getPersonTotalOrderPrice");
//}

#endregion

#region Scalar Function'ı EF Core üzerinde kullanma - HasDbFunction Metodu

// Veritabanı seviyesindeki herhangi bir fonksiyonu EF Core/yazılım kısmında bir metoda bind etmemizi sağlayan bir fonksiyondur.

// var persons = (from person in context.Persons where context.GetPersonTotalOrderPrice(person.PersonId) > 500 select person).ToListAsync();

class Context
{
    //public int GetPersonTotalOrderPrice(int personId)
    //      =>throw new Exception();
}

//modelBuilder.HasDbFunction(typeof(Context).GetMethod(nameOf(Context.GetPersonTotalOrderPrice),new[] {typeof(int)})).HasName("getPersonTotalOrderPrice");
#endregion

#region Inline Functions Nedir ?

//Geriye bir değer değil, tablo döndüren fonksiyonlardır.

#endregion

#region Inline Function oluşturma


//1. Adım : Boş bir migration oluşturulmalı.
//2. Adım : Bu migration içerisinde Up metodunda SQL metodu eşliğinde fonksiyonun create kodları, down metodu içerisinde de Drop kodları yazılacaktır.
//3. Adım : migrate edilmeli

//protected override void Up(MigrationBuilder migrationBuilder)
//{
//migrationBuilder.Sql($@"
//CREATE FUNCTION bestSellingStaff(@totalOrderPrice INT=0)
//  RETURNS TABLE
//AS
//BEGIN
// DECLARE @totalPrice INT
// SELECT TOP 1 p.Name, COUNT(*) OrderCount, SUM(o.Price) TotalOrderPrice FROM Persons p
// JOIN Orders o
//      ON p.PersonId= o.PersonId
// GROUP BY p.Name
// HAVING SUM(o.Price) > @totalOrderPrice
// ORDER By OrderCount DESC
//END");
//}

//protected override void Down(MigrationBuilder migrationBuilder)
//{
//migrationBuilder.Sql($@" DROP FUNCTION getPersonTotalOrderPrice");
//}

#endregion

#region Inline Function'ı EF Core'a entegre etme

// Bize bir tablo olarak döndüğü için bir model üzerinden verileri karşılamamız gerekir.

//var persons = await context.BestSellingStaff(500).ToListAsync();

class BestSellingStaff
{
    public string Name { get; set; }
    public int OrderCount { get; set; }
    public int TotalOrderPrice { get; set; }
}

//public DbSet<BestSellingStaff> BestSellingStaffs;

// public IQueryable<BestSellingStaff> BestSellingStaff(int totalOrderPrice=0)
//      =>FromExpression(()=>BestSellingStaff(totalOrderPrice));

//modelBuilder.HasDbFunction(typeof(Context).GetMethod(nameOf(Context.BestSellingStaff),new[] {typeof(int)})).HasName("bestSellingStaff");
// modelBuilder.Entity<BestSellingStaff>().HasNoKey();

#endregion