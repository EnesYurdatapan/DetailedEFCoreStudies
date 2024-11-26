#region Connection Resiliency Nedir ?

//EF Core üzerinde yapılan veritabanı çalışmaları sürecinde ister istemez veritabanı bağlantısında kopuşlar/ kesintiler vs. meydana gelebilir.
// Connectiob Resiliency ile kopan bağlantıyı tekrar kurmak için gerekli bağlantı taleplerinde bulunabilir  ve bir yandan da execution strategy dediğimiz davranış modellerini belirleyerek bağlantıların kopması durumunda tekrar edecek olan sorguları baştan sona yeniden tetikleyebiliriz.

#endregion

#region EnableRetryOnFailure

//Uygulama sürecinde veritabanı bağlantısı koptuğu taktirde bu yapılandırma sayesinde bağlantıyı tekrardan kurmaya çalışabiliriz.

// OnConfiguring
// { 
//      optionsBuilder.UseSqlServer("...ConnectionString..., builder=> builder.EnableRetryOnFailure());
//
//
//  }

#region MaxRetryCount

//Yeniden bağlantı sağlanmasını deneme durumunun kaç kere gerçekleştirileceğini bildirmektedir.
//Default değeri 6'dır
#endregion

#region MaxRetryDelay

// Yeniden bağlantı sağlanmasını deneme periyodunu bildirmektedir.
//Default Değeri 30.

#endregion

//      optionsBuilder.UseSqlServer("...ConnectionString..., builder=> builder.EnableRetryOnFailure( maxRetryCount : 5, maxRetryDelay : TimeSpan.FromSeconds(15), errorNumbersToAdd : new[] {4060}));



#endregion

#region Execution Strategies

// EF Core ile yapılan bir işlem vt bağlantısı sebebiyle başarısız olduğu taktirde yeniden bağlantı denenirken yapılan davranışa ExecutionStrategy denmektedir. Az Önce yaptığımız bir Execution Strategy idi.
// Bu stratejiyi default değerlerle kullanabileceğmiz gibi custom olarak de kendimize göre özelleştirebilir ve bağlantı koptuğu durumlarda istediğimiz aksiyonları alabiliriz.

#region Default Execution Strategy
// Eğer ki EnableRetryOnFailure metodunu kullanıyorsak  bu default execution strategy'e karşılık gelecektir.


#endregion

#region Custom Execution Strategy


class CustomExecutionStrategy /*: ExecutionStrategy*/
{
    //public CustomExecutionStrategy(..........)
    //{
    //       ....... 
    //}

    //public CustomExecutionStrategy(.......)
    //{
    //      .........  
    //}

    //protected override bool ShouldRetryOn(Exception exception)
    //{
                // Yeniden bağlantı durumunun söz konusu olduğu anlarda yapılacak işlemler...
                //Console.Writeline("Bağlantı tekrar kuruluyor...");
    //}
}

class Context
{
    // OnConfiguring
    // { 
    //      optionsBuilder.UseSqlServer("...ConnectionString..., builder=> builder.ExecutionStrategy(dependencies => new CustomExecutionStrategy(dependencies, 3 ,TimeSpan.FromSeconds(15)))));
    //
    //
    //  }
}

#endregion

#region Bağlantı koptuğu anda execute edilmesi gereken tüm çalışmaları tekrar işlemek

//EF Core ile yapılan çalışma sürecinde veritabanı bağlantısının kesilmesi durumlarında, bazen bağlantının tekrardan kurulması tek başına yetmemekte, kesintinin olduğu çalışmanın da baştan tekrardan işlenmesi gerekebilmektedir. İşte bu tarz durumlara karşılık EF Core Execute - ExecuteAsync fonksiyonunu bizlere sumaktadır.

//Execute fonksiyonu, içerisine vermiş olduğumuz kodları commit edilene kadar işleyecektir. Eğer ki bağlantı kesilmesi meydana gelirse, bağlantının tekraran kurulması durumunda Execute içerisindeki çalışmalar tekrar baştan işlenecek ve böylece yapılan işlemin tutarlılığı için gerekli çalışma sağlanmış olunacaktır.

// var strategy = context.Database.CreateExecutionStrategy();
//await strategy.ExecuteAsync(async () => 
// {
//      using var transaction = await context.Database.BeginTransactionAsync();
//      await context.Persons.AddAsync(new() { Name="Hilmi" });
//      await context.SaveChangesAsync();

//await transaction.CommitAsync();

#endregion

#region Execution Strategy hangi durumlarda kullanılır ?

// Veritabanının şifresi belirli periyotlarda otomatik olarak değişen uygulamalarda güncel şifreyle connection string'i sağlayacak bir operasyonu custom execution strategy belirleyerek gerçekleştirebilirsiniz.

#endregion

#endregion