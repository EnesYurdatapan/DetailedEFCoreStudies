
using Entities;
using Lesson1;
using Microsoft.EntityFrameworkCore;

ExampleDbContext exampleDbContext = new ExampleDbContext();

#region One to One İlişkisel Senaryolarda Veri Güncelleme
// Normal update işlemini zaten biliyoruz.
// Burada nesneler arasındaki ilişkini update edilmesinden bahsedeceğiz.


#region 1. Durum >> Principal Tablodaki veriye bağımlı olan veriyi değiştirme

// nesneyi include fonksiyonu yardımıyla içindeki address bilgisiyle beraber çekip, adres bilgisini sildik.
//Daha sonra yeni adres bilgisini ekleyerek güncelleme işlemini yaptık.

Person? person = await exampleDbContext.Persons.Include(p => p.Address).FirstOrDefaultAsync(p => p.Id == 1);
exampleDbContext.Addresses.Remove(person.Address);
person.Address = new Address()
{
    AddressDescription ="Yeni Adres"
};

await exampleDbContext.SaveChangesAsync();
#endregion


#region 2. Durum >> Bağımlı verinin ilişkisel olduğu ana veriyi güncelleme

// Önce bağımlı verimizi çekeriz ve sileriz, daha sonra person bilgisini değiştirip yeniden ekleriz.
Address? address = await exampleDbContext.Addresses.FindAsync(1);
exampleDbContext.Addresses.Remove(address);
await exampleDbContext.SaveChangesAsync();

Person? person2 = await exampleDbContext.Persons.FindAsync(2);
address.Person = person;
await exampleDbContext.Addresses.AddAsync(address);
await exampleDbContext.SaveChangesAsync();


#endregion



#endregion



#region One to Many İlişkisel Senaryolarda Veri Güncelleme

#region 1. Durum >> Principal Tablodaki veriye bağımlı olan veriyi değiştirme

// Ana veriyi getirip, bağlı entitylerden silmek istediğimizi sildiririz, sonra yenisini ekleriz.

Blog? blog = await exampleDbContext.Blogs.Include(b=>b.Posts).FirstOrDefaultAsync(b=>b.Id==1);
var postToDelete = blog.Posts.FirstOrDefault(p => p.Id == 2);
blog.Posts.Remove(postToDelete);

blog.Posts.Add(new() { Title = "Post 4" });

#endregion


#region 2. Durum >> Bağımlı verinin ilişkisel olduğu ana veriyi güncelleme

Post? post3 = await exampleDbContext.Posts.FindAsync(4);
post3.Blog = new()
{
    Name = "Blog 2"
};
await exampleDbContext.SaveChangesAsync();

// VEYA var olan bir bloga ilişkilendirmek için


Post? post4 = await exampleDbContext.Posts.FindAsync(5);
Blog? blog2 = await exampleDbContext.Blogs.FindAsync(2);
post4.Blog = blog2;
await exampleDbContext.SaveChangesAsync();


#endregion


#endregion



#region Many To Many İlişkisel Senaryolarda Veri Güncelleme

#region 1. örnek

Book? book = await exampleDbContext.Books.FindAsync(1);
Author? author = await exampleDbContext.Authors.FindAsync(3);
book.Authors.Add(author);

await exampleDbContext.SaveChangesAsync();
#endregion

#region 2. örnek

Author? author2 = await exampleDbContext.Authors.Include(a=>a.Books).FirstOrDefaultAsync(a=>a.Id==3);
foreach (var item in author2.Books)
{
    if (item.ID!=1)
    {
        author2.Books.Remove(item);
    }

    await exampleDbContext.SaveChangesAsync();
}


#endregion


#endregion