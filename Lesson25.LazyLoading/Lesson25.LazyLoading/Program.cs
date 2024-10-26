#region Lazy Loading Nedir ?

// Navigation properyler üzerinde bir işlem yapılmaya çalışıldığı taktirde ilgili propertynin temsil ettiği tabloya özel bir sorgu oluşturulup execute edilmesini ve verilerin yüklenmesini sağlayan bir yaklaşımdır.

// var employee = await context.Employees.FindAsync(2);
// Console.WriteLine(employee.Region.Name);

//senaryo budur.

#endregion

#region Proxy'lerle Lazy Loading

//Microsoft.EntityFrameworkCore.Proxies kütüphanesi

// protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//{
// optionsBuilder.UseLazyLoadingProxies();

//}


#endregion


#region Propertylerin Virtual olması

// Eğer ki proxyler üzerinden lazy loading operasyonu gerçekleştiriyorsanız navigation propertylerin virtual ile işaretlenmesi gerekir.

#endregion

#region Proxy olmaksızın Lazy Loading

//Proxyler tüm platformlarda desteklenmeyebilir. Böyle bir durumda manuel bir şekilde lazy loadingi uygulamak mecburiyetinde kalabiliriz.


#region ILazyLoader Interface'i ile Lazy Loading

//Microsoft.EntityFrameworkCore.Abstraction

//Manuel yapılan Lazy Loading operasyonlarında Navigation Propertylerin virtual ile işaretlenmesine gerek yoktur

using System.Runtime.CompilerServices;

class Employee
{
  //  ILazyLoader _lazyLoader;
    Region _region;
    //public Employee()
    //{
        
    //}
    public Employee(/*ILazyLoader lazyLoader*/)
    {
        //_lazyLoader = lazyLoader;
    }

   // public  Region Region { get => _lazyLoader.Load(this, ref _region); set => _region =value; }
}
class Region
{
    //  ILazyLoader _lazyLoader;
    ICollection<Employee> _employees;

    //public Region()
    //{

    //}
    public Region(/*ILazyLoader lazyLoader*/)
    {
        //_lazyLoader = lazyLoader;
    }
    //public ICollection<Employee> Employees { get => _lazyLoader.Load(this, ref _employees); set => _employees = value; }
}


#endregion

#region Delegate ile Lazy Loading



class Employee2
{
    Action<object, string> _lazyLoader;
    Region _region;
    //public Employee()
    //{

    //}
    public Employee2(Action<object, string> lazyLoader)
    {
        _lazyLoader = lazyLoader;
    }

    // public  Region Region { get => _lazyLoader.Load(this, ref _region); set => _region =value; }
}
class Region2
{
    Action<object, string> _lazyLoader;
    ICollection<Employee> _employees;

    //public Region()
    //{

    //}
    public Region2(Action<object, string> lazyLoader)
    {
        _lazyLoader = lazyLoader;
    }
    //public ICollection<Employee> Employees { get => _lazyLoader.Load(this, ref _employees); set => _employees = value; }
}

static class LazyLoadingExtension
{
    public static TRelated Load<TRelated>(this Action<object,string> loader, object entity, ref TRelated navigation, [CallerMemberName]string navigationName=null)
    {
        loader.Invoke(entity,navigationName);
        return navigation;
    }
}


#endregion

#endregion

#region N+1 Problemi

//var region = await context.Regions.FindAsync(1);
//foreach (var employee in region.Employees)
//{
//    Console.WriteLine(order.OrderDate);
//}

// Lazy Loading kullanım açısından oldukça maliyetli ve performans düşürücü bir etkiye  sahip yöntemdir. O yüzden kullanırken mümkün oldukça dikkatli olmalı ve özellikle navigation propertylerin döngüsel tetikleme durumlarında lazy loadingi tercih etmemeye odaklanmalıyız. Aksi taktirde her bir tetiklemeye karşılık aynı sorguları üretip execute edecektir. Bu durumu N+1 problemi olarak adlandırırız.
#endregion