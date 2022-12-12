using System.Net;
using System.Reflection;
using System.Text;

namespace ByteBank.Portal.Infraestrutura;

public class HandlerRequestArchives
{
    public void Handler(HttpListenerResponse response, string path)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var nameResource = Utilities.ConvertPathNameAssembly(path);
        var resourceStream = assembly.GetManifestResourceStream(nameResource);

        if (!(resourceStream == null))
        {
            using (resourceStream)
            {
                var bytesResource = new byte[resourceStream.Length];
                resourceStream.Read(bytesResource, 0, (int)resourceStream.Length);

                response.ContentType = Utilities.GetTypeContent(path);
                response.StatusCode = 200;
                response.ContentLength64 = resourceStream.Length;

                response.OutputStream.Write(bytesResource, 0, bytesResource.Length);
                response.OutputStream.Close();
            }
        }
        else
        {
            response.StatusCode = 404;
            response.OutputStream.Close();
        }

    }
}
