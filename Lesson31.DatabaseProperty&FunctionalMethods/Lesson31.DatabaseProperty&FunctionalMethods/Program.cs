#region Database Property'si

//Database Propertysi veritabanını temsil eden ve Ef Core'un bazı işlevlerinin detaylarına erişmemizi sağlayan property'dir.

#endregion

#region BeginTransaction

//EF Core, transaction yönetimini otomatik bir şekilde kendisi gerçekleştirmektedir. Eğer ki transaction yönetimini manuel olarak anlık ele almak istiyorsak BeginTransaction fonks. kullanabiliriz.
// IDbContextTransaction transaction = context.Database.BeginTransaction();

#endregion

#region CommitTransaction

// Yapılan işlem sonucunda transactionı başarılı kılmamızı sağlayan metottur. Yani commit etmemizi sağlar. Fazla kullanılmaz.
//context.Database.CommitTransaction();

#endregion

#region RollbackTransaction

//EF Core üzerinde yapılan çalışmaların rollback edilebilmesi için kullanılan bir fonksiyondur.
// context.Database.RollbackTransaction();

#endregion

#region CanConnect

// Verilen connection stringe karşılık bağlantı kurulabilir bir veritabanı var mı yok mu bilgisini bool cinsinden verir.
//bool isConnect = context.Database.CanConnect();

#endregion

#region EnsureCreated

//EF Core'da tasarlanan veritabanını migration kullanmaksızın, runtime'da yani kod üzerinde veritabanı sunucusuna inşaa edebilmek için kullanılan bir fonksiyondur.
//context.Database.EnsureCreated();

#endregion

#region EnsureDeleted


//İnşaa edilmiş veritabanını runtimeda silen bir fonksiyondur.
//context.Database.EnsureDeleted();

#endregion

#region GenerateCreateScript

//Context nesnesinde yapılmış olan VT tasarımı her ne ise ona uygun bir SQL Scriptini string olarak veren metottur.
//var script = context.Database.GenerateCreateScript();

#endregion

#region ExecuteSql

//VT'a yapılacak insert, update ve delete sorgularını yazdığımız bir metottur. Bu metot işlevsel olarak alacağı parametreleri SQL Injection salddırılarına karşı korumaktadır.
//var result = context.Database.ExecuteSql($"INSERT/UPDATE/DELETE ........");

#endregion

#region ExecuteSqlRaw
//VT'a yapılacak insert, update ve delete sorgularını yazdığımız bir metottur. Bu metot işlevsel olarak alacağı parametreleri SQL Injection salddırılarına karşı koruma görevi geliştiricinin sorumluluğundadır.

//var result = context.Dtabase.ExecuteSql("INSERT/UPDATE/DELETE ........");


#endregion

#region SqlQuery

//SqlQuery fonksiyonu her ne kadar erişilebilir olsa da artık desteklenmemektedir. Bunun yerine DbSet propertysi üzerinden erişilebilen FromSql fonksiyonu gelmiştir.

#endregion

#region GetMigrations

//Uygulamada üretilmiş olan tüm migrationları runtimeda programatik olarak elde etmemizisağlar
//var migs = context.Database.GetMigrations();

#endregion

#region GetAppliedMigrations

// Uygulamada migrate edilmiş olan tüm migrationları elde etmemizi sağlayan fonksiyondur.
//var migs = context.Database.GetAppliedMigrations();


#endregion

#region GetPendingMigrations

// Uygulamada migrate edilmemiş olan tüm migrationları elde etmemizi sağlayan fonksiyondur.
//var migs = context.Database.GetPendingMigrations();


#endregion

#region Migrate

//Migrationları programatik olarak runtimeda migrate etmek için kullanılan bir fonksiyondur.

//context.Database.Migrate();
//EnsureCreated fonksiyonu migrationları kapsamamaktadır. O yüzden migration içinde yapılan çalışmalar yine migrate edilmelidir.
#endregion

#region OpenConnection

//context.Database.OpenConnection();

#endregion

#region CloseConnection
//context.Database.CloseConnection();


#endregion

#region GetConnectionString

//İlgili context nesnesinin o anda kullandığı connection string değeri ne ise onu elde eder.
// context.Database.GetConnectionString();

#endregion

#region GetDbConnection
//EF core'un kullanmış olduğu Ado.NET altyapısının kullandığı DbConnection nesnesini elde etmemizi sağlayan bir fonksiyondur. Yani bizleri Ado.NET kanadına götürür.
//SqlConnection connection = (SqlConnection)context.Database.GetDbConnection();

#endregion

#region SetDbConnection

//Özelleştirilmiş connection nesnelerini EF Core mimarisine dahil etmemizi sağlayan fonksiyondur.
//context.Database.SetDbConnection();

#endregion

#region ProviderName Propertysi

// EF Core'un kullanmış olduğu provider'ın ismini getiren fonksiyondur.
//Console.WriteLine(context.Database.ProviderName);

#endregion