
namespace ByteBank.Portal.Infraestrutura.Ioc;

public class Container : IContainer
{
    private readonly Dictionary<Type, Type> _mapType = new();
    public object Recovery(Type Origin)
    {
        var typeOriginMap = _mapType.ContainsKey(Origin);
        if(typeOriginMap)
          return Recovery(_mapType[Origin]);
          
        var constructors = Origin.GetConstructors();
        var constructosNotParams = constructors.FirstOrDefault(a => a.GetParameters().Any() == false);

        if(constructosNotParams != null)  return constructosNotParams.Invoke(new object[0]);
        
        var constructor = constructors[0];
        var constructsParams = constructor.GetParameters();
        var valueParams = new object[constructsParams.Count()];

        for(int i = 0 ; i < constructsParams.Count() ; i ++)
        {
            var param = constructsParams[i];
            var typeParams = param.ParameterType;
            
            valueParams[i] = Recovery(typeParams);
        }

        return constructor.Invoke(valueParams);
    }

    public void Register(Type Origin, Type Destiny)
    {
        if (_mapType.ContainsKey(Origin))
            throw new InvalidOperationException("Type Exists");

        ValidImplements(Origin, Destiny);
        _mapType.Add(Origin , Destiny);
    }

    public void Register<TOrigin, TDestiny>() where TDestiny : TOrigin
    {
        if (_mapType.ContainsKey(typeof(TOrigin)))
            throw new InvalidOperationException("Type Exists");

        _mapType.Add(typeof(TOrigin) , typeof(TDestiny));
    }

    private void ValidImplements(Type Origin, Type Destiny)
    {
        if (Origin.IsInterface)
        {
            var implementsInterface = Destiny.GetInterfaces().Any(i => i == Origin);
            if (!implementsInterface)
                throw new InvalidOperationException("Type Destiny not implements interface");
        }
        else
        {
            var typeDestinyInheritsTypeOrigin = Destiny.IsSubclassOf(Origin);
            if (!typeDestinyInheritsTypeOrigin)
                throw new InvalidOperationException("Type Destiny not Inherits  origin");

        }
    }
}
