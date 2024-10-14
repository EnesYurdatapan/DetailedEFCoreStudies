#region Table Per Hierarchy(TPH) nedir ?

//Kalıtmsal ilişkiye sahip olan entitylerin olduğu senaryolarda her bir hiyerarşiye karşılık bir tablo oluşturan davranıştır.
//İçerisinde benzer alanlara sahip olan entityleri migrate ettiğimizde her entitye karşılık bir tablo oluşturmaktansa bu entityleri tek bir tabloda modellemek isteyebilir ve bu tablodaki kayıtları discriminator kolonu üzerinden birbirleniden ayırabiliriz. İşte bu tarz bir tablonun oluşturulması ve bu tarz bir tabloya göre sorgulama, veri ekleme, silme vs. gibi operasyonların şekillendirilmesi için TPH davranışını kullanabiliriz.
#endregion


#region TPH Nasıl Uygulanır ?

//EF Core'da entityler arasında temel bir kalıtımsal ilişki söz konusuysa, default olarak kabul edilen bir davranıştır.
// O yüzden herhangi bir konfigürasyon gerektirmez.

//Tüm entityler DbSet'e eklenmelidir.
class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

class Employee:Person
{
    public string? Department { get; set; }
}

class Techinician:Employee
{
    public string? Branch { get; set; }
}


#endregion

#region Discriminator kolonu nedir ?
//TPH yaklaşımı neticesinde kümülatif olarak inşa edilmiş tablonun hangi entitye karşılık veri tuttuğunu ayırt edebilmemizi sağlayan bir kolondur.
//EF Core tarafından otomatik olarak tabloya yerleştirilier.
// Default olarak içerisinde entity isimlerini tutar.
//Bu kolon özelleştirilebilir.


#endregion

#region Discriminator kolon adı nasıl değiştirilir ?

//Öncelikle hiyerarşinin başında hangi tablo varsa onun Fluent API'da konfigürasyonuna gidilir.
// Ardından HasDiscriminator fonksiyonu ile özelleştirilir.

//.HasDiscriminator<string>("ayırıcı");

#endregion

#region Discriminator Değerleri Nasıl Değiştirilir ?
//Yine hiyerarşi başındaki entity konfigürasyonlarına gelip, HasDiscriminator fonksiyonu ile özelleştirmede bulunarak ardından HasValue ile hangi entitye karşılık hangi değerin girileceğini belirtilen türde veririrz.
//Örneğin int yaparsak aşağıdaki gibi belirtilir; bunun yanında string veya başka türdeki değerleri de aynı şekilde  HasValue ile verebiliriz.

//.HasDiscriminator<int>("ayırıcı");
//.HasValue<Person>(1)
//.HasValue<Employee>(2)
//.HasValue<Customer>(3)
//.HasValue<Technician>(4)

#endregion


#region TPH'da veri ekleme

//Davranışların hiçbirinde veri eklerken,silerken,güncellerken vs. normal operasyonların dışında bir işlem yapılmaz!

//Hangi davranışı kullanıyorsak EF Core ona göre arka planda modellemeyi gerçekleştirir.

//Employee employee = new Employee(){ ...... } ; 
//Technician technician = new Technician(){ ...... } ; 

// await context.Employee.AddAsync(employee);
// await context.Technician.AddAsync(technician);

#endregion

#region TPH'de Veri silme

//Yine aynı şekilde kendi entitysi üzerinden silme operasyonu gerçekleşir.

//var employee = context.Employee.FindAsync(1);
// await context.Employees.Remove(employee);

#endregion

#region TPH'de veri güncelleme

//yine aynı şekilde Entity üzerinden yapılır.

#endregion

#region TPH'de veri sorgulama

//Veri sorgulama operasyonu bilinen DbSet propertysi üzerinden sorgulanır. Ancak burada dikkat edilmesi gereken husus ; kalıtımsal ilişkiye göre yapılan sorgulamada üst sınıf alt sınıftaki verileri de kapsamaktadır. Bu yüzden alt sınıfa ait veriler de gelecektir.
//Sorgulama süreçlerinde EF Core generate edilen sorguya bir Where davranışı eklemektedir.

#endregion

#region Farklı Entitylerde aynı isimde sütun olduğu durumlar
//Entitylerde aynı isimde kolonlar olabilir. Bu kolonları EF Core isimsel olarak özelleştirip ayıracaktır.

#endregion