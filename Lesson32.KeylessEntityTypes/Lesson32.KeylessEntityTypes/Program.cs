// Normal entity typlere ek olarak primary key içermeyen querylere karşı veritabanı sorguları yürütmek için kullanılan bir özelliktir.

//Genellikle aggregate operasyonların yapıldığı group by veya pivot table gibi işlemler neticesinde elde edilen istatistiksel sonuçlar primary key kolonu barındırmazlar. Bizler bu tarz sorguları Keyless Entity Types özelliği ile sanki bir entity'e karşılık geliyormuş gibi çalıştırabiliriz.


#region Keyless Entity Types Tanımlama

//1. : Hangi sorgu olursa olsun EF Core üzerinden bu sorgunun bir entitye karşılık geliyormuş gibi işleme tabi tutulabilmesi için o sorgunun sonucunda bir entitynin yine de tasarlanması gerekmektedir.
//2. : Bu entity'nin DbSet Propertysi olarak DbContext nesnesine eklenmesi gerekmektedir.
//3. : Tanımlamış olduğumuz entity'e OnModelCreating fonksiyonu içinde bunun bir keyi olmadığı ve hangi sorgunun çalıştırılacağı da ToView vs. gibi işlemlerle konfigüre edilir.

//protected override void Up(MigrationBuilder migrationBuilder)
//{
//migrationBuilder.Sql($@"CREATE VIEW vw_PersonOrderCount
//    AS
//        SELECT  p.Namei COUNT(*) Count FROM Persons p
//        JOIN Orders o
//                ON p.PersonId = o.PersonId
//        GROUP BY p.Name);
////}

//protected override void Down(MigrationBuilder migrationBuilder)
//{
//migrationBuilder.Sql($@" DROP VIEW vw_PersonOrderCount );
//}

// var datas = await context.PersonOrderCounts.ToListAsync();
class PersonOrderCount
{
    public int Count { get; set; }
    public string Name { get; set; }
}

class Context
{
    //public DbSet<PersonOrderCount> PersonOrderCounts { get; set; }
}


//modelBuilder.Entity<PersonOrderCount>().HasNoKey().ToView("vw_PersonOrderCount");

#endregion

#region Keyless Attribute
//[Keyless]
//class PersonOrderCount
//{
//    public int Count { get; set; }
//    public string Name { get; set; }
//}

#endregion

#region Keyless Entity Types Özellikleri

//Primary Key kolonu olmaz !
//Change Tracker mekanizması aktif olmayacaktır.
//TPH olarak entity hiyerarşilerinde kullanılabilir fakat diğer kalıtımsal ilişkilerde kullanılamaz.

#endregion

