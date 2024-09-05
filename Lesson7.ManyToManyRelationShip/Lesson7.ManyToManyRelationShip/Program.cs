// Çoka Çok ilişkide bir "Cross Table" oluşturulur.
// Örneğin book ile author arasında çoka çok bir ilişki vardır, BookAuthor adında bir cross table oluşturulur.
// Tekil tablolarımız bu cross table ile arasında 1-M ilişki özelliği gösterir, böylece 2 tekil tablo çoka çok ilişki oluşturmuş olur.
// Cross Table içinde Composite Primary Key dediğimiz; 2 tablonun verilerinin id'lerinin birleşiminin oluşturduğu bir key şekli vardır.


#region Default Convention

//EF Core Otomatik olarak many to many olduğunu ICollection navigation propertyleri ile anlar.
//Primary key'i bütünsel olarak kabul etmeliyiz.


class Book
{
    public int Id { get; set; }
    public string BookName { get; set; }
    public ICollection<Author> Authors { get; set; }
}


class Author
{
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public ICollection<Book> Books { get; set; }
}


#endregion



#region Data Annotations

//Cross Table Manuel Olarak oluşturulmalıdır.
// Navigation propertyler cross table içinde tekil, tekil tablolar içinde "ICollection<BookAuthor>" şeklinde verilir
// Composite primary key vermek için burda da fluent api kullanmak zorundayız !!
// Cross Table'a karşılık bir entity oluşturuyorsak eğer DbSet olarak Context classında vermemize gerek yok.
class BookAuthor
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }
    public Book Book { get; set; }
    public Author Author { get; set; }
}


//    modelBuilder.Entity<BookAuthor>().HasKey(ba => new {ba.BookId, ba.AuthorId}); // 




#endregion


#region Fluent API

//Cross Table manuel oluşturulmalı, DbSet'e yine gerek yok.
//Composite PK HasKey metodu ile kurulur.

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<BookAuthor>().HasKey(ba => new {ba.BookId, ba.AuthorId}); // 

//    modelBuilder.Entity<BookAuthor>()
//        .HasOne(ba=> ba.Book)
//        .WithMany(b=> b.Authors)
//          .HasForeignKey(ba=>ba.BookId)


//    modelBuilder.Entity<BookAuthor>()
//        .HasOne(ba=> ba.Author)
//        .WithMany(a=> a.Books)
//          .HasForeignKey(ba=>ba.AuthorId)
//}


#endregion