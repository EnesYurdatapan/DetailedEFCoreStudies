
using System.Globalization;

class Photo
{
    public int PersonId { get; set; }
    public string Url { get; set; }
    public Person Person { get; set; }
}
public enum Gender { Man, Woman }
class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public Photo Photo { get; set; }
    public ICollection<Order> Order { get; set; }
}
class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }
    public Person Person { get; set; }
}


#region Join

#region Query Syntax

// var query = from photo in context.Photos
// join person in context.Persons
// on photo.PersonId equals person.PersonId
// select new
// {
//      person.Name,
//       photo.Url
// };
// query.ToListAsync();
#endregion

#region Method Syntax

// var query = context.Photos.Join(context.Persons, photo=>photo.PersonId,person=>person.PersonId,(photo,person) => new { person.Name, photo.Url });
//var datas = await query.ToListAsync();

#endregion

#region Multiple Columns Join

#region  Query Syntax

// var query = from photo in context.Photos
// join person in context.Persons
// on new{ photo.PersonId, photo.Url }  equals new{ person.PersonId, Url = person.Name }
// select new
// {
//      person.Name,
//       photo.Url
// };
//var datas = await query.ToListAsync();

#endregion

#region Method Syntax

// var query = context.Photos.Join(context.Persons, photo=>new{ photo.PersonId, photo.Url },person=>new{ person.PersonId, Url = person.Name },(photo,person) => new { person.Name, photo.Url });
//var datas = await query.ToListAsync();

#endregion

#endregion

#region 2'den Fazla Tabloyla Join

#region Query Syntax
//var query = from photo in context.Photos
//  join person in context.Persons
//  on photo.PersonId
//  equals person.PersonId
//  join order in Context.Orders
//  on person.PersonId
//  equals order.PersonId
//  select new
//  {
//      person.Name,
//      photo.Url,
//      order.Description
//  }; 
// var datas = await query.ToListAsync();

#endregion

#region Method Syntax

// var query = context.Photos.Join(context.Persons, photo=>photo.PersonId,person=>person.PersonId,(photo,person) => new { person.PersonId, person.Name, photo.Url }).Join(context.Orders, oncekiSorgu=> oncekiSorgu.PersonId, order=> order.PersonId, (oncekiSorgu, order) => new { oncekiSorgu.Name, oncekiSorgu.Url, order.Description });
// var data = await query.ToListAsync();

#endregion

#endregion

#region Grup Join

// var query = from photo in context.Photos
// join order in context.Orders
// on photo.PersonId equals order.PersonId into personOrders
// select new
// {
//      person.Name,
//       Count = personOrders.Count(),
//       personOrders
// };
// query.ToListAsync();

//Orderları tekil olarak değil de person ile grup olarak elde ettik
#endregion

#endregion

#region Left Join

//DefaultIfEmpty : Sorgulama sürecinde ilişkisel olarak karşılığı olmayan verilere default değerini yazdıran yani LEFT JOIN sorgusunu oluşturtan bir fonksiyondur

// var query = from person in context.Persons
// join order in context.Orders
// on person.PersonId equals order.PersonId into personOrders
// from order in persoOrders.DefaultIfEmpty()
// select new
// {
//      person.Name,
//      order.Description
// };
// query.ToListAsync();

#endregion

#region Right Join

// Left joinin tam tersi olarak sorgudaki tabloların yerlerinin değiştirilmesi yeterlidir


// var query = from order in context.Orders
// join person in context.Persons
// on order.PersonId equals person.PersonId into orderPersons
// from person in orderPersons.DefaultIfEmpty()
// select new
// {
//      person.Name,
//       order.Description
// };
// query.ToListAsync();

#endregion

#region Full Join

// var leftQuery = from person in context.Persons
// join order in context.Orders
// on person.PersonId equals order.PersonId into personOrders
// from order in persoOrders.DefaultIfEmpty()
// select new
// {
//      person.Name,
//      order.Description
// };


// var rightQuery = from order in context.Orders
// join person in context.Persons
// on order.PersonId equals person.PersonId into orderPersons
// from person in orderPersons.DefaultIfEmpty()
// select new
// {
//      person.Name,
//       order.Description
// };

// var fullJoin = leftQuery.Union(rightQuery);

//var datas = await fullJoin.ToListAsync();

#endregion

#region Cross Join

//SQL'de CROSS JOIN, iki tablo arasında her satırın diğer tabloya ait her satır ile eşleşmesini sağlar
// CROSS JOIN, genellikle diğer JOIN türleri gibi ilişkili veri elde etmekten ziyade tüm olası kombinasyonları görmek için kullanılır.
//Örneğin A tablosunda 3 satır ve B tablosunda 2 satır varsa toplam 3x2 = 6 satırlık veri oluşur.

// EF Core'da from üzerine from yazdığımızda bunun bir Cross Join olduğunu anlar.

// var query = from order in context.Orders
// from person in context.Persons
// select new
// {
//      order,
//      person
// };
// query.ToListAsync();

#endregion

#region Collection Selector'da Where Kullanma Durumu

// EF bu durumda bir inner join sorgusu oluşturur.

// var query = from order in context.Orders
// from person in context.Persons.Where(p=>p.PersonId==order.PersonId)
// select new
// {
//      order,
//      person
// };

#endregion

#region Cross Apply

// var query = from order in context.Orders
// from person in context.Persons.Select(p=>order.Description)
// select new
// {
//      order,
//      person
// };

#endregion

#region Outer Apply

// var query = from order in context.Orders
// from person in context.Persons.Select(p=>order.Description).DefaultIfEmpty();
// select new
// {
//      order,
//      person
// };

#endregion