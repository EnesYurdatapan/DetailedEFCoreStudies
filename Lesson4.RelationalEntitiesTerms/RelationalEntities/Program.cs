#region Relationships Terimleri

#region Principal Entity(Asıl Entity) && Dependent Entity(Bağımlı Entity)

//Kendi başına var olabilen tabloyu modelleyen entity.
//Örneğin Çalışanlar Ve Departmanlar tablosu, Çalışanların kesinlikle bir departmanı var, fakat Departmanlar bir yere bağlı kalmadan kendi başlarına da var olabilirler.
// Bu Örnekte departmanlar Principal Entity, Çalışanlar Dependent Entity

#endregion

#region Navigation Property

//İlişkisel tablolar arasındaki fiziksel erişimi entity classları üzerinden sağlayan propertyler.
//Bir property'nin navigation property olabilmesi için kesinlikle entity türünden olması gerekiyor.

class Employee
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}
class Department
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
//Navigation property'ler entitylerdeki tanımlarına göre n'e n, 1'e n veya 1'e 1 ilişki türlerini ifade ederler.

#endregion

#endregion



#region Entity Framework Core'da İlişki Yapılandırma Yöntemleri

#region Default Conventions

//Varsayılan entity kurallarını kullanarak yapılan ilişki yapılandırma yöntemleridir.
//Navigation propertyleri kullanarak ilişki şablonlarını çıkartır.

#endregion

#region Data Annotation Attributes

//Entity'nin niteliklerine göre ince ayarlar yapmamızı sağlayan attributelardır.
// [Key], [ForeignKey]

#endregion

#region Fluent API

//Entity modellerindeki ilişkileri yapılandırırken daha detaylı çalışmamızı sağlayan yöntemdir.

#region HasOne

// İlgili entity'nin ilişkisel entity'e bire bir veya bire çok olacak şekilde ilişkisini yapılandırmaya başlayan metottur.

#endregion

#region HasMany

// Çoka çok veya çoka bir ilişkileri yapılandırmaya başlayan metottur.

#endregion

#region WithOne

// HasOne veya HasMany'den sonra bire bir ya da çoka bir olacak şekilde ilişki yapılandırmasını tamamlayan metottur.

#endregion

#region WithMany

// HasOne veya HasMany'den sonra bire çok ya da çoka çok olacak şekilde ilişki yapılandırmasını tamamlayan metottur.


#endregion
#endregion

#endregion
