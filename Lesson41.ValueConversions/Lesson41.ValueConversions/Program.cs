#region Value Conversion Nedir ?

// EF Core üzreinden veritabanı ile yapılan sorgulama süreçlerinde veriler üzerinde dönüşümler yapmamızı sağlayan bir özelliktir.
// SELECT sorguları sürecinde gelecek olan veriler üzerinde dönüşüm yapabiliriz.
// Ya da UPDATE, INSERT sorgularında da yazılım üzerinden veritabanına gönderdiğimiz veriler üzerinde de dönüşümler yapabilir ve böylece fiziksel olarak verileri manipüle edebiliriz.

#endregion

#region ValueConverter Kullanımı Nasıldır ?

//Value conversions özelliğini EF Core'daki Value Converter yapıları tarafından uygulayabilmekteyiz.

#region HasConversion

// En sade haliyle EF Core üzerinden value converter özelliği gören bir fonksiyondur.

class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public bool Married { get; set; }
    public List<string> Titles { get; set; }
}

// modelBuilder.Entity<Person>().Property(p=>p.Gender).HasConversion(// INSERT, UPDATE g=>g.ToUpper()  ,  //SELECT      g=> g=="M" ? "Male" : "Female");
// İlk parametre UPDATE ve INSERT komutlarındaki davranışı konfigüre eder, ikinci parametre ise SELECT.
#endregion

#endregion

#region Enum değerler ile Value Converter kullanımı

// Person classındaki Gender propertysini string yerine Gender enum veri yapısında olduğunu varsayalım.

// modelBuilder.Entity<Person>().Property(p=>p.Gender).HasConversion(// INSERT, UPDATE g=>g.ToString()  ,  //SELECT      g=> (Gender)Enum.Parse(typeof(Gender),g));
//Normalde enum int olarak kaydedilir fakat yukardaki konfigürasyonla string olarak kaydettirdik. SELECT ile çekerken de tekrar enum'a çevirerek bize getirir.
#endregion

#region Value Converter Sınıfı

//ValueConverter sınıfı, verisel dönüşümlerdeki çalışmaları üstlenebilecek bir sınıftır.
// Yani bu sınıfın instance'ı ile HasConversion fonksiyonunun yapılan çalışmaları üstlenebilir ve direkt bu instance'ı ilgili fonksiyona vererek dönüşümsel çalışmalarımızı gerçekleştirebiliriz.

// ValueConverter<Gender, string> converter = new(// INSERT, UPDATE g=>g.ToString()  ,  //SELECT      g=> (Gender)Enum.Parse(typeof(Gender),g));
// modelBuilder.Entity<Person>().Property(p=>p.Gender).HasConversion(converter);
#endregion

#region Custom ValueConverter Sınıfı

// EF Core'da verisel dönüşümler için custom olarak converter sıfınları üretebiliriz. Bunun için tek yapılması gereken custom sınıfının ValueConverter sınıfından miras almasını sağlamaktır.

//class GenderConverter :ValueConverter<Gender,string>
//{
//      public GenderConverter() : base ( //INSERT - UPDATE , // SELECT){ }
//
//
//  }

// modelBuilder.Entity<Person>().Property(p=>p.Gender).HasConversion<GenderConverter>();


#endregion

#region Built-in Converters Yapıları

//EF Core basit dönüşümler için kendi bünyesinde yerleşik converter sınıfları barındırmaktadır.

#region BoolToZeroOneConverter

// bool olan verinin int olarak tutulmasını sağlar.

// modelBuilder.Entity<Person>().Property(p=>p.Married).HasConversion<BoolToZeroOneConverter<int>>;
// modelBuilder.Entity<Person>().Property(p=>p.Gender).HasConversion<int>();

//üstteki ikisi aynı şeydir.


#endregion

#region BoolToStringConverter

//bool olan verinin string olarak tutulmasını sağlamaktadır.

// BoolToStringConverter converter = new("Bekar", "Evli");
// modelBuilder.Entity<Person>().Property(p=>p.Married).HasConversion(converter);

#endregion

#endregion

#region İlkel koleksiyonların serilizasyonu

// İçerisinde ilkel türlerden oluşturulmuş koleksiyonları barındıran modelleri migrate etmeye çalıştığımızda hata ile karşılaşmaktyız. Bu hatadan kurtulmak ve ilgili veriye koleksiyondaki verileri serilize ederek işleyebilmek için b u koleksiyonu normal metinsel değerlere dönüştürmemizi sağlayan bir conversion operasyonu gerçekleştirebiliriz.

// modelBuilder.Entity<Person>().Property(p=>p.Titles).HasConversion(t=> JsonSerializer.Serialize(t), t=> JsonSerializer.Deserialize<List<string>>(t,(JsonSerializerOptions)null);

#endregion

#region Value Converter For Nullable Fields

//.NET 6'dan önce value converterlar null değerlerin dönüşümünü desteklemiyordu. Artık etkiliyor.

#endregion