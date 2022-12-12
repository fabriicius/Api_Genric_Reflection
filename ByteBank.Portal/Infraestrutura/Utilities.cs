using System.Net;
using System.Reflection;
using System.Text;

namespace ByteBank.Portal.Infraestrutura;

    public static class Utilities
    {
        public static string ConvertPathNameAssembly(string path)
        {
            return "ByteBank.Portal" + (path.Replace('/', '.'));
        }

        public static string GetTypeContent(string path)
        {
            if (path.EndsWith(".css"))
                return "text/css; charset=utf-8";

            if (path.EndsWith(".js"))
                return "application/js; charset=utf-8";

            if (path.EndsWith(".html"))
                return "text/html; charset=utf-8";

            throw new NotFiniteNumberException("Type of content not valid");
        }

        public static bool isArchive(string path)
        {
            var cutPath = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var lastPath = cutPath.Last();

            return lastPath.Contains('.');
        }
    }
