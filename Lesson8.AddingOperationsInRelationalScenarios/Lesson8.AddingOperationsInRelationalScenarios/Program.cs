using Entities;
using Lesson1;

ExampleDbContext exampleDbContext = new ExampleDbContext();


#region One to One İlişkisel Senaryolarda Veri Ekleme


#region 1. Yöntem >> Principal Entity Üzerinden Dependent Entity Verisi Ekleme

Person person = new Person();
person.Name = "Enes";
person.Address = new Address() { AddressDescription = "Osmangazi/BURSA" };

await exampleDbContext.AddAsync(person);
await exampleDbContext.SaveChangesAsync();

#endregion

// Eğer ki ekleme işlemi principal entity üzerinden gerçekleştiriliyorsa dependent entity nesnesi verilmek zorunda değildir !!
// Fakat eğer ki ekleme işlemi dependent entity üzerinden yapılıyorsa principal entity nesnesini zorunlu olarak oluşturmalıyız.

#region 2. Yöntem >>> Dependent Entity Üzerinden Principal Entity Verisi Ekleme

Address address = new Address();
address.AddressDescription = "Nilüfer/BURSA";
address.Person = new() { Name = "Ahmet" };

await exampleDbContext.AddAsync(address);
await exampleDbContext.SaveChangesAsync();

#endregion

#endregion


#region One to Many İlişkisel Senaryolarda Veri Ekleme



#region 1. Yöntem >> Principal Entity üzerinden dependent entity verisi ekleme

#region Nesne referansı üzerinden Ekleme
// Eğer bu yöntemi kullanmak istiyorsak :
// Blog entity'si içinde ICollection tipinde verdiğimiz Post classını object initializer ile ctor üzerinden newlememiz gerekir.
//Çünkü ekleme işlemlerinde Posts isimli collection'ın referansını oluşturmadığımız için null hatası alırız. Bu sebeple new'lemek zorundayız.

Blog blog = new Blog() { Name = "EnesY.com Blog" };
blog.Posts.Add(new() { Title = "Post1" });
blog.Posts.Add(new() { Title = "Post2" });
blog.Posts.Add(new() { Title = "Post3" });

await exampleDbContext.AddAsync(blog);
await exampleDbContext.SaveChangesAsync();



#endregion


#region Object Initializer Üzerinden Ekleme

// Bu yöntemde blog entity class'ının içinde Collection navigation propertysini newlememiz gerekmez.
Blog blog2 = new()
{
    Name = "A Blog",
    Posts = new HashSet<Post>() { new() { Title = "Post 4" }, new() { Title = "Post 5" } }
};

#endregion



#endregion


#region 2. Yöntem >>Dependent Entity üzerinden principal entity verisi ekleme
// Hiç kullanılmaz, mantığa aykırı bir yöntemdir.
// EF Core dependent entitylerden principal entity oluşturduğumuz durumlarda, öncelikle principal entity'i oluşturup sonra onun foreign keyini oluşturulan dependent entitye bağlar.
// Yani bir'e bir ekleme yapmış oluruz. Bir'e çok ilişkilerde bu yöntemle add işlemi genelde yapılmaz.
Post post = new()
{
    Title = "Post 6",
    Blog = new Blog() { Name = "B Blog" }
};


#endregion

#region 3. Yöntem >> Foreign Key Kolonu üzerinden veri ekleme

// 1. ve 2. Yöntem daha önceden olmayan verilerin ilişkisel olarak eklenmesini sağlarken, bu 3. yöntem önceden eklenmiş bir principal entity verisiyle yeni dependent entitiylerin ilişkisel olarak eşleştirilmesini sağlar.

Post post2 = new()
{
    BlogId = 1,
    Title = "Post 7"
};

#endregion
#endregion


#region Many to Many İlişkisel Senaryolarda Veri Ekleme

#region 1. Yöntem
// Many to Many ilişkisi eğer ki default convention üzerinden tasarlanmışsa kullanılan bir yöntemdir.
Book book = new()
{
    BookName = "Book A",
    Authors = new HashSet<Author>()
    {
        new(){AuthorName="Ali"},
        new(){AuthorName="Ahmet"},
        new(){AuthorName="Osman"},

    }
};

#endregion

#region 2. Yöntem
// Fluent API yöntemi ile
// Bu yöntemde var olan verilere de ekleyebiliyoruz.

//Author author = new()
//{
//    AuthorName = "Mustafa",
//    Books = new HashSet<BookAuthor>()
//    {
//        new(){BookId=1},
//        new(){Book = new () {BookName="B Book"}}
//    }
//};


#endregion


#endregion