#region EF Core Select Sorgularını Güçlendirme Teknikleri

#region IQueryable - IEnumerable Farkı

// IQueryable arayüzü üzerinde yapılan işlemler direkt generate edilecek olan sorguya yansıtılacaktır.
//IEnumerable arayüzü üzerinde yapılan işlemler temel sorgu neticesinde gelen ve in-memorye yüklenen instancelar üzerinde gerçekleştirilir. Yani sorguya yansıtılmaz.


// IQueryable ile yapılan sorgulama çalışmalarında sql sorguyu hedef verileri elde edecek şekilde generate edecekken, IEnumerable ilşe yapılan sorgulama çalışmalarında sql daha geniş verileri getirebilecek şekilde execute edilerek hedef veriler in-memory'de ayıklanır.

// Yani IQueryable hedef verileri getirirken, hedef verilerden daha fazlasını getirip in-memory'de ayıklar.
//IQueryable ve IEnumerable davranışsal olarak aralarında farklar barındırsalar da her ikisi de gecikmeli çalıştırma davranışı sergiler. Yani her iki arayüz üzerinden de oluşturulan işlemi execute edebilmek için .ToList() gibi tetikleyici fonksiyonları kullanmamız gerekmektedir.
#region AsQueryable

// IEnumerable üzerinde yapılan çalışmayı Queryable'a çevirir.

#endregion

#region AsEnumerable

// IQueryableüzerinde yapılan çalışmayı IEnumerable 'a çevirir.


#endregion


#endregion

#region Yalnızca ihtiyaç olan kolonları listeleme - Select

// var persons = await context.Persons.Select(p=> new{ p.Name}).ToListAsync();

#endregion

#region Result'ı limitleyin - Take

// await context.Persons.Take(50).ToListAsync();

#endregion

#region Join Sorgularında Eager Loading Sürecinde Verileri Filtreleyin

//await context.Persons.Include(p=>p.Orders.Where(o=>o.OrderId%2==0).OrderByDescending(o=>Order.Id)).ToListAsync();

#endregion

#region Şartlara Bağlı Join Yapılacaksa Explicit Loading Kullanın

//var person = await context.Persons.FirstOrDefaultAsync(p=>p.PersonId==1);

// if(person.Name=="Ayşe")
//      await context.Entry(person).Collection(p=>p.Orders).LoadAsync();

#endregion

#region Lazy Loading Kullanırken Dikkatli olun

#region İdeal Durum
// var persons = await context.Persons.Select(p=> new { p.Name, p.Orders }).ToListAsync();
//foreach(var person in persons)
//      foreach(var order in person.Orders)

#endregion
#region Riskli durum
// var persons = await context.Persons.ToListAsync();
//foreach(var person in persons)
//      foreach(var order in person.Orders)
#endregion

#endregion

#region İhtiyaç Noktalarında Ham SQL Kullanın - FromSql

//

#endregion

#region Asenkron Fonksiyonları Tercih Edin


#endregion

#endregion