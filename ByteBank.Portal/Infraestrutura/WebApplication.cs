using System.Net;
using System.Reflection;
using System.Text;
using ByteBank.Portal.Controller;
using ByteBank.Portal.Infraestrutura.Ioc;
using ByteBank.Service;
using ByteBank.Service.Cambio;

namespace ByteBank.Portal.Infraestrutura;

public class WebApplication
{
    private readonly string[] _prefixos;
    private readonly IContainer _container = new Container();

    public WebApplication(string[] prefixos)
    {
        if (prefixos == null)
            throw new ArgumentNullException(nameof(prefixos));

        _prefixos = prefixos;
        Configure();
    }

    private void Configure()
    {
        _container.Register(typeof(ICambioSerivce), typeof(CambioTeste));
        _container.Register(typeof(ICardService), typeof(CardService));

        _container.Register<ICambioSerivce, CambioTeste>();
        _container.Register<ICardService, CardService>();
    }

    public void Iniciar()
    {
        while (true)
            factorRequest();
    }

    private void factorRequest()
    {
        var httpListener = new HttpListener();
        foreach (var prefixo in _prefixos)
            httpListener.Prefixes.Add(prefixo);

        httpListener.Start();

        var contexto = httpListener.GetContext();
        var requisicao = contexto.Request;
        var resposta = contexto.Response;
        var path = requisicao.Url.PathAndQuery;
 
        if (Utilities.isArchive(path))
        {
            var handleArchive = new HandlerRequestArchives();
            handleArchive.Handler(resposta, path);
        }
        else 
        {
           var handleController = new HandlerRequestControllers(_container);
           handleController.Handler(resposta , path);
        }

        httpListener.Stop();

    }
}
