var newPerson = Person.New.Called("Dimitrii").Works("quant").Build();

Console.WriteLine(newPerson);

public class Person
{
    public string Name;
    public string Position;

    public static Builder New => new Builder();

    public class Builder : PersonJobBuilder<Builder>
    {

    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
}

public abstract class PersomBuilder
{
    protected Person person = new Person();

    public Person Build()
    {
        return person;
    }
}

// class Foo : Bar<Foo>
public class PersonInfoBuilder<SELF> : PersomBuilder where SELF : PersonInfoBuilder<SELF>
{
    public SELF Called(string name)
    {
        person.Name = name;

        return (SELF)this;
    }
}

// When we need a new functionallity on the Person. We want to follo the O/C principle so we make a new class to build the Person position
public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>> where SELF : PersonJobBuilder<SELF>
{
    public SELF Works(string position)
    {
        person.Position = position;
        
        return (SELF)this;
    }
}
