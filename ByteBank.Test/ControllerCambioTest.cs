using System.Reflection;
using System.Text.RegularExpressions;

namespace ByteBank.Test;

[TestClass]
public class ControllerCambioTest
{
    [TestMethod]
    public void testMain()
    {
        var controller = new CambioControllerFake();
        var nameAction = "Calculo";
        var args = new string[] { "coinDestiny", "value" };
        // var args = new string[] { "coinOrigin", "coinDestiny", "value" };
        var argsCount = args.Length;

        var bindingFlags = BindingFlags.Instance |
        BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly;

        var methods = controller.GetType().GetMethods(bindingFlags);
        var methodsOverload = methods.Where(meth => meth.Name == nameAction);

        foreach (var overload in methodsOverload)
        {
            var param = overload.GetParameters();

            if (argsCount != param.Length)
                continue;

            var math =
            param.All(p =>
                args.Contains(p.Name)
            );

            if (math)
            {
                Console.WriteLine(overload);
                break;
            }
        }
    }

    [TestMethod]
    public void testViewOverload()
    {
        var viewRaw = "teste1 {{Prop1}} maisTeste {{Prop2}}";
        var regex = new Regex("\\{{(.*?)\\}}");
        var result = regex.Matches(viewRaw);
        foreach(Match match in result){
            var values = match.Groups[1].Value;
        }
    }

     [TestMethod]
    public void testAnonymousTypeAny()
    {
        var anonymousType = new {
            Prop1 = 1,
            Prop2 = "prop2"
        };

        var result = anonymousType.GetType().GetProperties();
        
    }


    public class CambioControllerFake
    {
        public string Calculo(string coinOrigin, string coinDestiny, decimal value)
        => null;

        public string Calculo(string coinDestiny, decimal value)
        => Calculo("BRL", coinDestiny, value);
    }
}