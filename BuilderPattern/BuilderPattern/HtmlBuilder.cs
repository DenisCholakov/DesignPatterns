using System.Runtime.InteropServices;
using System.Text;

var hello = "hello";
var sb = new StringBuilder();
sb.Append("<p>");
sb.Append(hello);
sb.Append("</p>");

Console.WriteLine(sb);

var words = new[] { "hello", "world" };
sb.Clear();
sb.Append("<ul>");
foreach (var word in words)
{
    sb.AppendFormat("<li>{0}</li>", word);
}
sb.Append("</ul>");

Console.WriteLine(sb);


var builder = new HtmlBuilder("ul");
builder.AddChild("li", "hello").AddChild("li", "world");

Console.WriteLine(builder);

// Fluent builder
public class HtmlBuilder
{
    private readonly string rootName;
    HtmlElement root = new HtmlElement();


    public HtmlBuilder(string rootName)
    {
        root.Name = rootName;
        this.rootName = rootName;
    }

    public HtmlBuilder AddChild(string childName, string childText)
    {
        var e = new HtmlElement(childName, childText);
        root.Elements.Add(e);

        return this;
    }

    public void Clear()
    {
        root = new HtmlElement { Name = rootName };
    }

    public override string ToString()
    {
        return root.ToString();
    }
}


public class HtmlElement
{
    private const int indentSize = 2;

    public string Name, Text;
    public List<HtmlElement> Elements = new();

    public HtmlElement() { }

    public HtmlElement(string name, string text)
    {
        Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
        Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
    }

    private string ToStringImpl(int indent)
    {
        var sb = new StringBuilder();
        var i = new string(' ', indentSize * indent);

        sb.AppendLine($"{i}<{Name}>");

        if (!string.IsNullOrWhiteSpace(Text))
        {
            sb.Append(new string(' ', indentSize * (indent + 1)));
            sb.AppendLine(Text);
        }

        foreach (var e in Elements)
        {
            sb.Append(e.ToStringImpl(indent + 1));
        }

        sb.AppendLine($"{i}</{Name}>");

        return sb.ToString();
    }

    public override string ToString()
    {
        return ToStringImpl(0);
    }
}