using ByteBank.Service;
using ByteBank.Service.Cambio;

namespace ByteBank.Portal.Infraestrutura.Ioc;

public interface IContainer
{
    void Register (Type typeOrigin , Type typeDestiny);
    void Register<TOrigin, TDestiny>() where TDestiny : TOrigin;
    object Recovery(Type typeOrigin);
}
