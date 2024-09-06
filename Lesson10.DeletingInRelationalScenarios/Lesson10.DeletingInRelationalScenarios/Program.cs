
using Entities;
using Lesson1;
using Microsoft.EntityFrameworkCore;

ExampleDbContext exampleDbContext = new ExampleDbContext();


#region One to One Veri Silme

Person? person = await exampleDbContext.Persons.Include(p=>p.Address).FirstOrDefaultAsync(p=>p.Id==1);

exampleDbContext.Addresses.Remove(person.Address);
await exampleDbContext.SaveChangesAsync();

#endregion



#region One to Many Veri Silme

Blog? blog = await exampleDbContext.Blogs.Include(b=>b.Posts).FirstOrDefaultAsync(b=>b.Id==1);

Post? post = blog.Posts.FirstOrDefault(p => p.Id == 2);
exampleDbContext.Posts.Remove(post);
await exampleDbContext.SaveChangesAsync();


#endregion



#region Many to Many Veri Silme

Book? book = await exampleDbContext.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.ID == 1);
Author? author = book.Authors.FirstOrDefault(a => a.Id == 2);
// context.Authors.Remove(author); YAZARI SİLMEYE KALKAR !!! YANLIŞ
book.Authors.Remove(author);
await exampleDbContext.SaveChangesAsync();

// cross table'dan ilgili ilişkili satırı siler.

#endregion



#region Cascade Delete

// Bu davranış modelleri Fluent API ile konfigüre edilir.
// Çoka çok ilişkide sadece Cascade kullanılır 
// One to One ilişkide dependent entity'de eğer foreign key primary key aynıysa SetNull kullanılamaz.

#region Cascade
// Esas tablodan silinen veriyle karşı/bağımlı tabloda bulunan ilişkili verilerin silinmesini sağlar.

Blog? blog2 = await  exampleDbContext.Blogs.FindAsync(1);
exampleDbContext.Blogs.Remove(blog2);
await exampleDbContext.SaveChangesAsync();

// ayarımız Cascade olduğundan, hem blog verisi hem de ona bağlı post verileri db'den silinir.




#endregion


#region SetNull
// Esas tablodan silinen veriyle karşı/bağımlı tabloda bulunan ilişkili verilere null değeri atanmasını sağlar.
// One to One senaryolarda eğer ki ForeignKey ve PrimaryKey kolonları aynı ise o zaman SetNull davranışını kullanamayız !

#endregion


#region Restrict

// Esas tablodan herhangi veri silinmeye çalışıldığında o veriye karşılık dependent table'da ilişkisel veriler varsa eğer bu sefer silme işlemini engeller.


#endregion

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<BookAuthor>().HasKey(ba => new {ba.BookId, ba.AuthorId}); // 

//    modelBuilder.Entity<Address>()
//        .HasOne(a=>a.Person)
//        .WithOne(p=>p.Address)
//          .HasForeignKey<Address>(a=>a.Id)
//          .OnDelete(DeleteBehavior.Cascade); *********************


//    modelBuilder.Entity<Post>()
//        .HasOne(p=> p.Blog)
//        .WithMany(b=> b.Posts)
//          .OnDelete(DeleteBehavior.SetNull) ***********************
//          .IsRequired(false); // ForeignKey alanını null yapabileceğimizden ötürü IsRequired değil demek istedik.
//}


#endregion