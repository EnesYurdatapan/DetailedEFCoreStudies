#region Neden loglama yaparız ?

//Çalışan bir sistemin runtimeda nasıl davranış sergilediğini gözlemleyebilmek için log mekanizmaları kullanılır.


//Yapılan sorguların çalışma süreçlerindeki davranışları.
//Gerekirse hassas veriler üzerine de loglama işlemleri gerçekleştiriyoruz.

#endregion

#region Basit olarak loglama nasıl yapılır ?

//Minimun yapılandırma gerektirmesi.
//Herhangi bir nuget paketine ihtiyaç duymadan yapılabilmesi.

// optionsBuilder.LogTo(Console.WriteLine);
// optionsBuilder.LogTo(message=>Debug.WriteLine(message));

#endregion

#region Debug Penceresine Log Nasıl Atılır?

// optionsBuilder.LogTo(message=>Debug.WriteLine(message));

#endregion

#region Bir dosyaya log nasıl atılır ?

//Normalde console yahut debug pencereline atılan loglar pek takip edilebilir olmamaktadır.
//Logları kalıcı hale getirmek istediğimiz durumlarda en basit haliyle bu logları harici bir dosyaya atmak isteyebiliriz.

//StreamWriter _log=new("logs.txt", append:true);
//optionsBulder.LogTo(async message=> await _log.WriteLineAsync(message));

//StreamWriter'in dispose edilmesi gerekir :

//public override void Dispose()
//{
//base.Dispose();
//_log.Dispose();
//}

//public override async ValueTask  DisposeAsync()
//{
//await base.DisposeAsync();
//await _log.DisposeAsync();
//}

#endregion

#region Hassas Verilerin Loglanması - EnableSensitiveDataLogging

//Default olarak EF Core log mesajlarında herhangi bir verinin değerini içermemektedir. Bunun nedeni, gizlilik teşkil edebilecek verilerin loglama sürecinde yanlışlıkla da olsa açığa çıkmamasıdır.
//Bazen alınan hatalarda verinin değerini bilmek hatayı debug edebilmek için oldukça yardımcı olabilmektedir. bu durumda hassas verilerin de loglanmasını sağlayabiliriz.

//optionsBulder.LogTo(async message=> await _log.WriteLineAsync(message)).EnableSensitiveDataLogging();

#endregion

#region Exception Ayrıntısını loglama - EnableDetailedErrors

//optionsBulder.LogTo(async message=> await _log.WriteLineAsync(message)).EnableDetailedErrors();


#endregion

#region Log Levels

//EF Core default olarak debug seviyesinin üstündeki tüm davranışları loglar. Bunu konfigüre edebiliriz.
//optionsBulder.LogTo(async message=> await _log.WriteLineAsync(message), LogLevel.Information);


#endregion
