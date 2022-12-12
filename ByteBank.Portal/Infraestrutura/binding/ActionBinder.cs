using System.Net;
using System.Reflection;
using System.Text;

namespace ByteBank.Portal.Infraestrutura.binding;

public class ActionBinder
{
    public ActionBindingInfo GetActionBindingInfo(object controller, string path)
    {
        var idxQuerryString = path.IndexOf('?');
        var isQuerryString = idxQuerryString >= 0;

        if (!isQuerryString) return new ActionBindingInfo(controller.GetType().GetMethod(path.Split('/', StringSplitOptions.RemoveEmptyEntries)[1]), 
        Enumerable.Empty<QueryStringNameValue>());

        var nameControllerAction = path.Substring(0 , idxQuerryString);
        var nameAction = nameControllerAction.Split('/', StringSplitOptions.RemoveEmptyEntries)[1];
        var queryString = path.Substring(idxQuerryString + 1);

        var tupleValue = getArgumentNameValue(queryString);
        var nameArgs = tupleValue.Select(t => t.Name).ToArray();
        return new ActionBindingInfo(GetMethodInfoWithArgs(nameAction, nameArgs, controller) , tupleValue);
    }

    private IEnumerable<QueryStringNameValue> getArgumentNameValue(string queryString)
    {
        var tuplesNameValue = queryString.Split('&', StringSplitOptions.RemoveEmptyEntries);
        foreach(var tuple in tuplesNameValue)
        {
            var nameValue = tuple.Split('=', StringSplitOptions.RemoveEmptyEntries);
            yield return new QueryStringNameValue(nameValue[0], nameValue[1]);
        }
    }

    private MethodInfo GetMethodInfoWithArgs(string nameAction , string[] args , object controller)
    {
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
                return overload;
        }

        throw new ArgumentException($"Overload {nameAction} of method is not found ");
    }
}