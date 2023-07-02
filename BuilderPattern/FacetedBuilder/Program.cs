var pb = new PersonBuilder();
Person person = pb
    .Lives
        .At("Marko Bochar str")
        .In("Sofia")
        .WithPostalCode("1000")
    .Works
        .At("CoBuilder")
        .AsA("Software Engineer")
        .Earning(10000);

Console.WriteLine(person);

public class Person
{
    // address
    public string StreetAdress { get; set; }

    public string Postcode { get; set; }

    public string City { get; set; }

    // emplyment

    public string CompanyName { get; set; }

    public string Position { get; set; }

    public int AnnualIncome { get; set; }

    public override string ToString()
        => $"{nameof(StreetAdress)}: {StreetAdress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
}

public class PersonBuilder // facade
{
    // reference!
    protected Person person = new ();

    public PersonJobBuilder Works => new(person);
    public PersonAddressBuilder Lives => new(person);

    public static implicit operator Person(PersonBuilder pb) => pb.person;
}

public class PersonAddressBuilder : PersonBuilder
{
    public PersonAddressBuilder(Person person) => this.person = person;

    public PersonAddressBuilder At(string streetAddress)
    {
        person.StreetAdress = streetAddress;

        return this;
    }

    public PersonAddressBuilder WithPostalCode(string postCode)
    {
        person.Postcode = postCode;

        return this;
    }

    public PersonAddressBuilder In(string City)
    {
        person.City = City;

        return this;
    }
}

public class PersonJobBuilder : PersonBuilder
{
    public PersonJobBuilder(Person person) => this.person = person;

    public PersonJobBuilder At(string companyName)
    {
        person.CompanyName = companyName;

        return this;
    }

    public PersonJobBuilder AsA(string position)
    {
        person.Position = position;

        return this;
    }

    public PersonJobBuilder Earning(int amount)
    {
        person.AnnualIncome = amount;

        return this;
    }
}