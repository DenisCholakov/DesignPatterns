using System.Text;

var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
Console.WriteLine(cb);

public class CodeBuilder
{
    private readonly int identSize = 2;
    private List<Property> properties = new List<Property>();
    private string className;

    public CodeBuilder(string className)
    {
        this.className = className;
    }

    public CodeBuilder AddField(string name, string type)
    {
        this.properties.Add(new Property { Name = name, Type = type });

        return this;
    }

    public override string ToString()
    {
        string identation = new string(' ', identSize);

        var sb = new StringBuilder();
        sb.AppendLine($"public class {className}");
        sb.AppendLine("{");
        foreach (var prop in properties)
        {
            sb.AppendLine(identation + $"public {prop.Type} {prop.Name};");
        }
        sb.AppendLine("}");

        return sb.ToString();
    }


    private class Property
    {
        public string Name { get; set; }

        public string Type { get; set; }
    }
}
