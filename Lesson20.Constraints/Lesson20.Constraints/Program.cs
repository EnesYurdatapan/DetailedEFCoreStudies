#region Alternate Keys - HasAlternateKey

//Bir entity içerisinde PK'e ek olarak her entity instance'ı için alternatif bir benzersiz tanımlayıcı işlevine sahip olan bir Key'dir.

// modelBuilder.Entity<Blog>()
//             .HasAlternateKey(b=>b.Url);

#endregion

#region ForeignKey Constraint

// Dependent entity üzerinde bir foreign key belirtmemiz gerekir.

// modelBuilder.Entity<Blog>()
//             .HasMany(b=>b.Posts)
//             .WithOne(b=>b.Blog)
//              .HasForeignKey(p=>p.BlogId);

// Veya Data Annotationsta [ForeignKey(nameof(Blog)] attribute'i ile yapabiliriz.

#endregion

#region Composite ForeignKey

// modelBuilder.Entity<Blog>()
//             .HasMany(b=>b.Posts)
//             .WithOne(b=>b.Blog)
//              .HasForeignKey(p=>new {p.BlodId, p.BlogUrl});

#endregion


#region Shadow Property üzerinden Foreign Key

//   modelBuilder.Entity<Blog>()
//               .Property<int>("BlogForeignKeyId");  >> Entity classımızda böyle bir property yok.


// modelBuilder.Entity<Blog>()
//             .HasMany(b=>b.Posts)
//             .WithOne(b=>b.Blog)
//              .HasForeignKey("BlogForeignKeyId");

#endregion


#region Unique Constraint

#region HasIndex - IsUnique Fonksiyonları

// Class seviyesinde uygulanabilen Index attribute'i ile propertyi Unique yapabiliyoruz.

//[Index(nameof(Blog.Url),IsUnique=true)]
class Blog
{
    public int Id { get; set; }
    public string BlogName { get; set; }
    public string Url { get; set; }
    public ICollection<Post> Posts { get; set; }

}


// ---------------------- FluentAPI -------------------------------------


// modelBuilder.Entity<Blog>()
//             .HasIndex(b=>b.Url)
//              .IsUnique();


#endregion

#region HasAlternateKey

//İlk regionda bahsettiğimiz HasAlternateKey ile de aslında UniqueConstraint uyguluyoruz.

#endregion

#endregion


#region Check Constraint

//Veri ekleme-güncelleme işlemleri için kontrol edilmesi gereken constraint varsa HasCheckConstraint fonksiyonu kullanılır.
// Mesela aşağıdaki örnekte A> B ise veri eklenebilir veya güncellenebilir.

class Post
{
    public int Id { get; set; }
    public int A { get; set; }
    public int B { get; set; }
}

//modelBuilder.Entity<Post>()
//             .HasCheckConstraint("a_b_check_const","[A]>[B]");


#endregion