#region Shadow Properties - Gölge Özellikler
//Entity sınıflarında fiziksel olarak tanımlanmayan/modellenmeyen fakat EF Core tarafından ilgili entity için var olan/var olduğu kabul edilen propertylerdir.

//Tabloda gösterilmesini istediğimiz, gerekli görmediğimiz, entity instance'ı üzerinde işlem yapmayacağımız kolonlar için shadow propertyler kullanılabilir.

//Shadow propetylerin değerleri ve stateleri Change Tracker tarafından kontrol edilir.
#endregion


#region Foreign Key - Shadow Properties
// Shadow Properties özelliğini aslında daha önce kullandık.
// Örneğin bire çok ilişkideki dependent entity'de foreign key tanımlamamamıza rağmen bir foreign key property'si oluşturulur. İşte bu shadow propertydir.



#endregion

#region Shadow Property oluşturma

//Foreign key dışında bir shadow property oluşturmak için Fluent API ile yapabiliriz.

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<Blog>()
//        .Property<DateTime>("CreatedDate); Artık bir CreatedDate kolonu oluşturmuş olduk. ShadowProperty
//}



#endregion

#region Shadow Property'e erişim sağlama

#region ChangeTracker ile Erişim


//var blog = await context.Blogs.FirstAsync();
//var createdDate= context.Entry(blog).Property("CreatedDate");
// Console.WriteLine(createdDate.CurrentValue);
// Console.WriteLine(createdDate.OriginalValue);

//createdDate.CurrentValue = DateTime.Now;
// await context.SaveChangesAsync();

#endregion

#region EF.Property ile Erişim
// Özellikle LINQ sorgularında Shadow Property'lerine erişim için EF.Property static yapılanmasını kullanabiliirz.

// context.Blogs.OrderBy(b=> EF.Property<DateTime>(b,"CreatedDate")).ToListAsync();

#endregion


#endregion


class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Post> Posts { get; set; }
}

class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool lastUpdated { get; set; }
    public Blog Blog { get; set; }
}