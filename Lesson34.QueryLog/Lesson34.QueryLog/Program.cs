#region Query Log Nedir? 

//LINQ sorguları neticesinde generate edilen sorguları izleyebilmek ve olası teknik hataları ayıklayabilmek amacıyla query log mekanizmasından istifade etmekteyiz.

#endregion

#region Nasıl konfigüre edilir ?

//Microsoft.Extension.Logging.Console  kütüphanesi(Console harici alternatifleri de bulunmakta.)


class Context
{
// readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder=>builder.AddConsole());


// OnConfiguring
// {.....
//          optionsBuilder.UseLoggerFactory(loggerFactory);
// ...}
}

// await context.Persons.Where(...);  örneğin bu sorgu neticesinde oluşturulan SQL querysi console'a loglanır.
#endregion

#region Filtreleme nasıl yapılır ?

// readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder=>builder.AddFilter((category,level)=>{ return category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information;})).AddConsole());


#endregion