using System.Net;
using System.Reflection;
using System.Text;
using System;
using ByteBank.Portal.Infraestrutura.binding;
using ByteBank.Portal.Infraestrutura.filters;
using ByteBank.Portal.Infraestrutura.Ioc;

namespace ByteBank.Portal.Infraestrutura;

public class HandlerRequestControllers
{
    private readonly ActionBinder _actionBinder = new ActionBinder();
    private readonly FilterResolver _filterResolver = new FilterResolver();
    private readonly ControllerResolver _controllerResolver;

    public HandlerRequestControllers(IContainer container)
    {
        _controllerResolver = new ControllerResolver(container);
    }

    public void Handler(HttpListenerResponse response, string path)
    {
        var pathSplit = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var controllerName = pathSplit[0];
        var actionName = pathSplit[1];

        var controllerFullName = $"ByteBank.Portal.Controller.{controllerName}Controller";

        // var controllerWrapper = Activator.CreateInstance("ByteBank.Portal", controllerFullName);
        // var controller = controllerWrapper.Unwrap();

        var controller = _controllerResolver.GetController(controllerFullName);

        var actionBindingInfo = _actionBinder.GetActionBindingInfo(controller, path);
        var filterResult = _filterResolver.VerifyFilter(actionBindingInfo);

        if (filterResult.GoContinue)
        {
            var resultadoAction = (string)actionBindingInfo.Invoke(controller);

            var bufferArchive = Encoding.UTF8.GetBytes(resultadoAction);
            response.ContentType = "text/html; charset=utf-8";
            response.StatusCode = 200;
            response.ContentLength64 = bufferArchive.Length;

            response.OutputStream.Write(bufferArchive, 0, bufferArchive.Length);
            response.OutputStream.Close();
        }
        else
        {
            response.StatusCode = 307;
            response.RedirectLocation = "/Error/Unexpected";
            response.OutputStream.Close();
        }
    }
}