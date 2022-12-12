using System.Collections.ObjectModel;
using System.Net;
using System.Reflection;
using System.Text;

namespace ByteBank.Portal.Infraestrutura.binding;

public class ActionBindingInfo
{
    public MethodInfo MethodInfo { get; private set; }
    public ReadOnlyCollection<QueryStringNameValue> TuppleArgsNameValue { get; private set; }

    public ActionBindingInfo(MethodInfo methodInfo, IEnumerable<QueryStringNameValue> tuppleArgsNameValue)
    {
        MethodInfo = methodInfo ?? throw new ArgumentNullException(nameof(MethodInfo));

        if (tuppleArgsNameValue == null)
            throw new ArgumentNullException(nameof(tuppleArgsNameValue));

        TuppleArgsNameValue =
            new ReadOnlyCollection<QueryStringNameValue>(tuppleArgsNameValue.ToList());
    }

    public object Invoke(object controller)
    {
        var auxContParam = TuppleArgsNameValue.Count;
        var haveArgs = auxContParam > 0;

        if(!haveArgs) return MethodInfo.Invoke(controller, new object[0]);

        var paramInvoke = new object[auxContParam];
        var paramMethodInfo = MethodInfo.GetParameters();

        for(int i = 0 ; i < auxContParam ;  i++)
        {
            var param = paramMethodInfo[i];
            var nameparam = param.Name;
            var args  = TuppleArgsNameValue.Single( t => t.Name == nameparam);

            paramInvoke[i] = Convert.ChangeType(args.Value , param.ParameterType);
        }

        return MethodInfo.Invoke(controller , paramInvoke);
    }

}
