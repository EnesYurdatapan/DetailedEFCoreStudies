#region Entity Splitting

//Birden fazla fiziksel tabloyu EF Core kısmında tek bir entity ile temsil etmemizi sağlayan özelliktir.


#endregion

class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int Number { get; set; }

    public string City { get; set; }
    public string Street { get; set; }


}


class Context
{
    // public DbSet<Person> Persons;

    // modelBuilder.Entity<Person>(entityBuilder => {
    // entityBuilder.ToTable("Persons")
    // .SplitToTable("PhoneNumbers, tableBuilder =>
    // { tableBuilder.Property(person=>person.Id).HasColumnName("PersonId");
    //   tableBuilder.Property(person=>person.PhoneNumber);
    //   })
    //   .SplitToTable("Addresses, tableBuilder =>
    // { tableBuilder.Property(person=>person.Id).HasColumnName("PersonId");
    //   tableBuilder.Property(person=>person.Street); })
}