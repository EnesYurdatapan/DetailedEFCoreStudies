#region Table Per Type (TPT) Nedir ?

//Entitylerin arasında kalıtımsal ilişkiye sahip olduğu durumlarda her bir entitye karşılık bir tablo generate eden davranıştır.
// Her generate edilen tablo hiyerarşik düzlemde kendi aralarında birebir ilişkiye sahiptir.

#endregion

class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

class Employee : Person
{
    public string? Department { get; set; }
}

class Techinician : Employee
{
    public string? Branch { get; set; }
}

#region TPT nasıl uygulanır ?

//TPT'yi uygulayabilme için öncelikle entityler kendi aralarında olması gereken mantıkta inşaa edilir.
// Entityler DbSet olarak bildirilmelidir.
//Hiyerarşik olarak aralarında kalıtımsal ilişki olan tüm entityler OnModelCreating fonksiyonunda ToTable metodu ile konfigüre edilmelidir.


// modelBuilder.Entity<Person>().ToTable("Persons");
// modelBuilder.Entity<Employee>().ToTable("Employees");
// modelBuilder.Entity<Technician>().ToTable("Technician");

#endregion


#region TPT'de veri ekleme
namespace example
{
Techinician techinician = new() { Name ="Enes", Surname="Yurdatapan", Department="Yazılım",Branch="Kodlama" }
//await context.Technicians.AddAsync(technician);
//await context.SaveChangesAsync();
}

#endregion


#region TPT'de veri silme

//Employee silinecek = await context.Employees.FindAsync(3);
//await context.Employees.Remove(silinecek);
//await context.SaveChangesAsync();
#endregion

#region TPT'de veri güncelleme

// Veriyi dağıttığı için herhangi bir yerden yakalayıp güncelleme yapılabilir.

//Technician technician = await context.Technicians.FindAsync(2);
// technician.Name="Ahmet";
//await context.Technicians.UpdateAsync(technician);
// await context.SaveChangesAsync();


#endregion


