

#region Backing Fields

// Tablo içerisindeki kolonları, entity class'ları içinde propertyler ile değil fieldlarla temsil etmemizi sağlayan bir özelliktir.
// Veritabanından bize gelen verileri kapsülleyerek dış dünyaya açabilir, dış dünyadan da veritabanına kapsülleyerek veri yollayabiliriz.
using Microsoft.EntityFrameworkCore;

class Person
{
    public int Id { get; set; }
    public string name;
    public string Name { get => name; set => name = value; }
 // public string Name { get => name.Substring(0,3); set => name = value; }  ***Kapsülleme örneği
    public string Department { get; set; }
}

#endregion


#region BackingField Attributes

// Attribute ile backingfield

class Person2
{
    public int Id { get; set; }
    public string name;
    [BackingField(nameof(name))]
    public string Name { get => name; set => name = value; }
    public string Department { get; set; }
}


#endregion


#region HasField FluentAPI

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<Person>()
//        .Property(p=>p.Name)
//        .HasField(nameof(Person.name));
//}


#endregion


#region Field and Property Access
//Ef Core sorgulama sürecinde entity içerisindeki property veya fieldları kullanıp kullanılmayacağının davranışını bizlere belirtmektedir.

//EF Core, hiçbir ayarlama yoksa varsayılan olarak propertyler üzerinden verileri işler. Eğer ki backing field bildiriliyorsa field üzerinden işler veya nasıl ayarlandıysa öyle işler.


//UsePropertyAccessMode üzerinden davranış modellemesi gerçekleştirilebilir.


//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<Person>()
//        .Property(p=>p.Name)
//        .HasField(nameof(Person.name))
//        .UsePropertyAccessMode(PropertyAccessMode.Field); // Veri erişim sürecinde saece fieldın kullanılmasını söyler. Eğer field'ın kullanılamayacağı durum mevcutsa exception fırlatır.
//}

#endregion


#region Field-Only Properties

//Entitylerde değerleri almak için properrtyler yerine metotların kullanıldığı veya belirli alanların hiç gösterilmemesi gerektiği durumlarda(örneğin primary key kolonu) kullanılabilir.

class Person3
{
    public int Id { get; set; }
    public string name;
    public string Department { get; set; }

    //public string GetName()
    //    => name;
    //public string SetName(string value)
    //    =>this.name = value;   >>> opsiyonel yapabildiğimiz işlemler
}

// modelBuilder.Entity<Person>()
// .Property(nameof(Person.name));  name field'ına bir property muamelesi yapması gerektiğini bildirdik.

#endregion