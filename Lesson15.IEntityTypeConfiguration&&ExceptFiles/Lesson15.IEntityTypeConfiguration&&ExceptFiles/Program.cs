#region OnModelCreating
//Genel anlamda veritabanı ile ilgili konfigürasyonel operasyonların dışında entityler üzerinde konfigürasyonel çalışmalar yapmamızı sağlayan bir fonksiyondur.
#endregion


#region IEntityTypeConfiguration<T> arayüzü
//Entity bazlı yapılacak olan konfigürasyonları o entitye özel harici bir dosya üzerinde yapmamızı sağlayan bir arayüzdür.

//Harici bir dosyada konfigürasyonların yürütülmesi merkezi bir yapılandırma noktası oluşturmamızı sağlamaktadır.

//Harici bir dosyada konfigürasyonların yürütülmesi entity sayısının fazla olduğu senaryolarda yönetilebilirliği arttırır ve yapılandırma ile ilgili geliştiricinin yükünü azaltır.

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

class Order
{
    public int OrderId { get; set; }
    public string Description { get; set; }
    public DateTime OrderDate { get; set; }
}

class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.OrderId);
        builder.Property(p => p.Description)
            .HasMaxLength(50);
    }
}

// ApplyConfiguration Metodu, harici konfigürasyonel sınıflarımızı EF Core'a bildirebilmek için kullandığımız bir metottur.
//ApplyConfigurationFromAssembly Metodu, uygulama bazında oluşturulan harici konfigürasyonel sınıfların her birini OnModelCreating metodunda ApplyConfiguration ile tek tek bildirmek yerine bu sınıfların bulunduğu Assembly'i bildirerek
// IEntityTypeConfiguration arayüzünden türeyen tüm sınıfları ilgili Entity'e karşılık konfigürasyonel değer olarak baz almasını tek kalemde gerçekleştirmemizi sağlar.

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.ApplyConfiguration(new OrderConfiguration());
//    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    
//}