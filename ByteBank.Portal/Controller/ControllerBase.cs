using System;
using System.Runtime.CompilerServices;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ByteBank.Portal.Controller
{

    public abstract class ControllerBase
    {
        protected string View([CallerMemberName] string nameArchive = null)
        {
            var type = GetType();
            var typeClassName = type.Name.Replace("Controller", "");
            var nameResource = $"ByteBank.Portal.View.{typeClassName}.{nameArchive}.html";
            var assembly = Assembly.GetExecutingAssembly();

            var streamResourceAssembly = assembly.GetManifestResourceStream(nameResource);
            var streamread = new StreamReader(streamResourceAssembly);

            var textPage = streamread.ReadToEnd();
            return textPage;
        }

        protected string View(object model, [CallerMemberName] string nameArchive = null)
        {
            var rawView = View(nameArchive);
            var allProperty = model.GetType().GetProperties();

            var regex = new Regex("\\{{(.*?)\\}}");

            var viewProcessed = regex.Replace(rawView , (match) => {
                var nameProperty = match.Groups[1].Value;
                var property = allProperty.Single(prop => 
                    prop.Name == nameProperty
                );
                var valueRaw = property.GetValue(model);
                return valueRaw?.ToString();
            });

            return viewProcessed;
        }

    }      
}