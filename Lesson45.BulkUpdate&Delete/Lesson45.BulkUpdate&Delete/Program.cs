#region ExecuteUpdate

//await context.Persons.where(p.PersonId>3).ExecuteUpdateAsync(p=>p.SetProperty(p=>p.Name, v=>v.Name + "yeni"))

#endregion

#region ExecuteDelete

//await context.Persons.where(p.PersonId>3).ExecuteDeleteAsync();


#endregion

//İki fonksiyonda da SaveChanges fonksiyonunu çağırmamız gerekmemektedir. Çünkü bunlar Execute fonksiyonlarıdır.
//İhtiyaç halinde transaction kontrolünü ele alarak bu fonksiyonların işlevleri süreç içinde kontrol edilebilir.