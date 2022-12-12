
namespace ByteBank.Test;

[TestClass]
public class UnitTest1
{

    [TestMethod]
    public void TestMethod1()
    {
        string path = "/Cambio/Calculo?valor=10&moedaDestino=USD&moedaOrigem=BRL";
        var idxQuerryString = path.IndexOf('?');
        var isQuerryString = idxQuerryString >= 0;

        if (!isQuerryString)
        {
            var namerAction = path.Split('/', StringSplitOptions.RemoveEmptyEntries)[1];
        }
        // return controller.GetType().GetMethod();

        var nameControllerAction = path.Substring(0, idxQuerryString);
        var nameAction = nameControllerAction.Split('/', StringSplitOptions.RemoveEmptyEntries)[1];
        var queryString = path.Substring(idxQuerryString + 1);

        var tupleValue = getArgumentNameValue(queryString);
    }

    [TestMethod]
    public void TestMethod2()
    {
        string queryString= "valor=10&moedaDestino=USD&moedaOrigem=BRL";
        var tuplesNameValue = queryString.Split('&', StringSplitOptions.RemoveEmptyEntries);
        foreach (var tuple in tuplesNameValue)
        {
            var nameValue = tuple.Split('=', StringSplitOptions.RemoveEmptyEntries);
            var retorno = new QueryStringNameValue(nameValue[0], nameValue[1]);
        }
        
    }

    public IEnumerable<QueryStringNameValue> getArgumentNameValue(string queryString)
    {
        var tuplesNameValue = queryString.Split('&', StringSplitOptions.RemoveEmptyEntries);
        foreach (var tuple in tuplesNameValue)
        {
            var nameValue = tuple.Split('=', StringSplitOptions.RemoveEmptyEntries);
            yield return new QueryStringNameValue(nameValue[0], nameValue[1]);
        }
    }

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
}