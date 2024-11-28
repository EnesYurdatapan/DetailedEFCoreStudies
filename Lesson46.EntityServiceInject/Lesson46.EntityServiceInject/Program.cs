using System.ComponentModel.DataAnnotations.Schema;

class PersonLogService : IPersonLogService
{
    public void LogPerson(string name)
    {
        throw new NotImplementedException();
    }
}
interface IPersonLogService
{
    void LogPerson(string name);
}

class IHasPersonService
{
   IPersonLogService PersonLogService { get; set; }
}
public class PersonServiceInjectionInterceptor /*: IMaterializationInterceptor*/
{
    //public object InitializedInstance(MaterializationInterceptionData materaliaztionData, object instance){
    // if (instance is IHasPersonService hasPersonService)
        // hasPersonService.PersonService = new PersonLogService();

    //return instance;
    //}
}

class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public override string ToString()
    {
        PersonService.LogPerson(Name);
        return base.ToString();
    }
    [NotMapped]
    public IPersonLogService PersonService { get; set; }
}

class Context
{
    //optionsBuilder.AddInterceptors(new PersonServiceInjectionInterceptor());
}