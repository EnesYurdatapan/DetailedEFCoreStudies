#region Explicit Loading nedir ?

//Oluşturulan sorguya eklenecek verilerin şartlara bağlı bir şekilde/ihtiyaçlara istinaden yüklenmesini sağlayan bir yaklaşımdır.

// ÖRNEK : Id'si 2 olan employee'i getirip ismi "Enes" ise Order verilerini görmek istiyoruz ;

// var employee = await context.Employees.FirstOrDefaultAsync(e=>e.Id==2);
// if(employee.Name=="Enes")
//{
// var orders = await context.Orders.Where(o=>o.EmployeeId==employee.Id).ToListAsync();
//}


// Mantığı bu şekildedir fakat biz bu işlemi daha kolay yöntemlerle yaparız.


#endregion

#region Reference

//Explicit Loading sürecinde ilişkisel olarak sorguya eklenmek istenen tablonun navigation propertysi tekil bir tüse bu tabloyu reference ile sorguya ekleyebiliriz.

// var employee = await context.Employees.FirstOrDefaultAsync(e=>e.Id==2);
//context.Entry(employee).Reference(e=>e.Region).LoadAsync(); >> Bu satırda navigation property verileri eklenmiş oldu.
#endregion


#region Collection

//Explicit Loading sürecinde ilişkisel olarak sorguya eklenmek istenen tablonun navigation propertysi çoğul bir türse bu tabloyu Collection ile sorguya ekleyebiliriz.

// var employee = await context.Employees.FirstOrDefaultAsync(e=>e.Id==2);
// ..
// ..

//context.Entry(employee).Collection(e=>e.Orders).LoadAsync();

#endregion

#region Collectionlarda Aggregate Operatör Uygulamak


// var employee = await context.Employees.FirstOrDefaultAsync(e=>e.Id==2);
// ..
// ..

//await context.Entry(employee).Collection(e=>e.Orders).Query().CountAsync();


#endregion


#region Collectionlarda Filtreleme

// var employee = await context.Employees.FirstOrDefaultAsync(e=>e.Id==2);
// ..
// ..

//await context.Entry(employee).Collection(e=>e.Orders).Query().Where(q=>q.OrderDate.Day==DateTime.Now.Day).ToListAsync();

#endregion