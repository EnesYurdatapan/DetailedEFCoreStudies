#region Default Convention

//Default Convention yönteminde bire çok ilişkiyi kurarken foreign key kolonuna karşılık gelen bir property tanımlamamıza gerek yoktur.
// Tanımlamadığımız takdirde EF Core kendisi otomatik olarak tanımlar. Eğer tanımlamak istiyorsak, tanımladığımızı alır.


class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Department Department { get; set; }
}

class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }
    public ICollection<Employee> Employees { get; set; }
}


#endregion

#region Data Annotations

// Dependent entityde foreign keyi kendimiz vermek istiyorsak ve isimlendirme geleneğinin dışında bir kullanım sergileyeceksek kullanırız.
// [ForeignKey] attribute'i ile

#endregion


#region Fluent API 

// OnModelCreating override'i üzerinden HasOne ve ardından WithMany fonksiyonları ile hallederiz.
//ForeignKey yine otomatik olarak oluşturulur, manuel verilmek isteniyorsa .HasForeignKey ile devamı getirilebilir.


//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{

//    modelBuilder.Entity<Employee>()
//        .HasOne(e=> e.Department)
//        .WithMany(d=> d.Employee)
//        
//}

#endregion