#region Transaction nedir ?

// Transaction, veritabanındaki kümülatif işlemleri atomik bir şekilde gerçekleştirmemizi sağlayan bir özelliktir.
// Bir transaction içerisindeki tüm işlemler commit edildiği taktirde veritabanına fiziksel olarak yansıtılacaktır. Rollback yapılıyorsa tüm işlemler geri alınacak ve fiziksel olarak veritabanında herhangi bir verisel değişiklik durumu söz konusu olmayacaktır.
//Transaction'ın genel amacı veritabanındaki tutarlılık durumunu korumaktır. Tutarsızlıklara önlem almaktır.

#endregion

#region Default Transaction Davranışı

//EF Core'da varsayılan olarak, yapılan tüm işlemler SaveChanges fonksiyonuyla veritabanına fiziksel olarak uygulanır.
//SaveChanges default olarak bir transaction yapısına sahiptir.
//Eğer ki bu süreçte bir problem/hata durumu söz konusu olursa tüm işlemler geri alını(rollback) ve işlemlerin hiçbiri veritabanına uygulanmaz.
// Böylece SaveChanges tüm işlemlerin ya tamamen başarılı olacağını ya da bir hata oluşursa veritabanını değiştirmeden işlemleri sonlandıracağını ifade etmektedir.
#endregion


#region Transaction Kontrolünü Manuel Sağlama

// IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
//EF Core'da transaction kontrolü iradeli bir şekilde manuel sağlamak yani elde etmek istiyorsak BeginTransactionAsync fonksiyonu çağırılmalıdır.
// Transaction manuel olarak başlatıldığında SaveChanges fonksiyonu bu transaction'ı otomatik olarak commit etmez. Default süreç başlattığımız için yine CommitAsync fonksiyonunu çağırarak default olarak sonuçlandırmalıyız.

//await context.Persons.AddAsync(person);
//await context.SaveChangesAsync();

//await transaction.CommitAsync();
#endregion

#region Savepoints

// Savepoints, veritabanı işlemleri sürecinde bir hata oluşursa veya başka bir nedenle yapılan işlemlerin geri alınması gerekiyorsa transaction içerisinde dönüş yapılabilecek noktaları ifade eden bir özelliktir.

#region CreateSavepoint

//Transaction içerisinde geri dönüş noktası oluşturmamızı sağlayan bir fonksiyondur.

#endregion

#region RollbackToSavePoint

//Transaction içerisinde herhangi bir geri dönüş noktasına(Savepoint'e) rollback yapmamızı sağlayan fonksiyondur.

#endregion

// IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
// Person p1 = await context.Persons.FindAsync(1);
// Person p2 = await context.Persons.FindAsync(2);
// context.Persons.RemoveRange(p1,p2);
// await context.SaveChangesAsync();

// transaction.CreateSavepointAsync("t1");
// Person p3 = await context.Persons.FindAsync(3);
// context.Persons.Remove(p3);
// await context.SaveChangesAsync();

//transaction.RollbackToSavepointAsync("t1);
//transaction.CommitAsync();

//Yukarıdaki örnekte p1 ve p2 silinecek, p3 savepoint noktasına rollback edildiği için silinmeyecektir.
//Savepoints özelliği bir transaction içerisinde istedildiği kadar kullanılabilir.


#endregion

#region TransactionScope

//Veritabanı işlemlerini bir grup olarak yapmamızı sağlayan bir sınıftır.
//ADO.NET ile de kullanılabilir.

// using TransactionScope transactionScope=new();
// Veritabanı işlemleri...
//..
//..

//transaction.Complete();  >> Complete fonksiyonu yapılan veritabanı işlemlerinin commit edilmesini sağlar. 
//Eğer ki rollback yapacaksanız complete fonksiyonunun tetiklenmesi yeterlidir.

#endregion