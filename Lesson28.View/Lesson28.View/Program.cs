// Oluşturduğumuz kompleks sorguları ihtiyaç durumlarında daha rahat bir şekilde kullanabilmek için basitleştiren bir veritabanı objesidir.

#region View Oluşturma

// 1. Adım : Boş bir migration oluşturulmalıdır.
// 2. Adım : Migration içerisindeki Up fonksiyonunda viewın create komutları, down fonksiyonunda ise drop fonksiyonları yazılmalıdır.
// 3. Adım : Migration basılır.

//protected override void Up(MigrationBuilder migrationBuilder)
//{
//migrationBuilder.Sql($@" CREATE VIEW vm_PersonOrders AS  ****** Buraya istenen SQL sorgusu yazılacak ******);
//}

//protected override void Down(MigrationBuilder migrationBuilder)
//{
//migrationBuilder.Sql($@" DROP VIEW vm_PersonOrders AS  ****** Buraya istenen SQL sorgusu yazılacak ******);
//}

#endregion

#region EF Core ile View kullanımı

//View'i EF Core üzerinden sorgulayabilmek için view sonucunu karşılayabilecek bir entity oluşturulması ve bu entity üzerinden DbSet propertysinin eklenmesi gerekir.

class PersonOrder
{
    public int Count { get; set; }
    public string Name { get; set; }
}

class Context
{
    //public DbSet<PersonOrder> PersonOrders { get; set; }
}

#region DbSet'in View olduğunu bildirmek
// modelBuilder.Entity<PersonOrder>().ToView("vm_PersonOrders").HasNoKey();
#endregion

// var personOrders = await context.PersonOrders.ToListAsync();

#endregion

#region EF Core'da View özellikleri

// Viewlerde PK olmaz. Bu yüzden ilgili DbSet'in HasNoKey ile işaretlenmesi gerekir.

// View neticesinde gelen veriler Change Tracker ile takip edilmezler. Haliyle üzerlerinde yapılan değişiklikleri EF Core veritabanına uygulamaz.

#endregion