using System.Net;
using System.Reflection;
using System.Text;
using System;

namespace ByteBank.Portal.Infraestrutura;

public class QueryStringNameValue
{
    public QueryStringNameValue(string name, string value)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public string Name { get; set; }
    public string Value { get; set; }
}