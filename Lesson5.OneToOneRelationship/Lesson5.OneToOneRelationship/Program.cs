#region DefaultConvention

//Her iki entity classında da navigation propertyler tekil olarak tanımlanır.
// One to One ilişki türünde dependent entitynin hangisi olduğunu default olarak belirleyemeyiz.
//Bu durumda fiziksel olarak bir foreign key propertysi tanımlayarak çözüm getiriyoruz.
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

class Employee
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public EmployeeAddress EmployeeAddress { get; set; }
}

class EmployeeAddress
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string Address { get; set; }
    public Employee Employee { get; set; }
}



#endregion



#region DataAnnotations

//Navigation propertyler tanımlanmalıdır.
//Foreign key propertysi default name convention dışında bir kolon olacaksa [ForeignKey] attrbiute ile bildirebiliriz.
//Foreign Key kolonu oluşturulmak zorunda da değildir :
//Dependent entitynin Primary key'ini aynı zamanda foreign key olarak kullanabiliyoruz ve daha az maliyet harcıyoruz. 

class EmployeeForDataAnotation
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public EmployeeAddressForDataAnotation EmployeeAddress { get; set; }
}

class EmployeeAddressForDataAnotation
{
    [Key,ForeignKey(nameof(EmployeeForDataAnotation))]
    public int Id { get; set; }
    //[ForeignKey(nameof(EmployeeForDataAnotation))]
    //public int Example { get; set; } //attribute sayesinde bunu foreign key yaptı
    public string Address { get; set; }
    public EmployeeForDataAnotation EmployeeForDataAnotation { get; set; }
}



#endregion


#region Fluent API

//Navigation propertyler tanımlanır.
// Konfigürasyonları OnModelCreating metodunu aşağıdaki gibi ezerek belirtiriz.

class EmployeeForFluent
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public EmployeeAddressForFluent EmployeeAddressForFluent { get; set; }
}

class EmployeeAddressForFluent
{
    public int Id { get; set; }
    public string Address { get; set; }
    public EmployeeForFluent EmployeeForFluent { get; set; }
}

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<EmployeeAddressForFluent>().HasKey(ea => ea.Id); // Id'nin primary key olduğunu bildirdik

//    modelBuilder.Entity<EmployeeForFluent>()
//        .HasOne(e=> e.EmployeeAddressForFluent)
//        .WithOne(ea=> ea.EmployeeForFluent)
//        .HasForeignKey<EmployeeAddressForFluent>(Ea=>Ea.Id);  //aynı zamanda sen bir Foreign keysin artık
//}

#endregion
