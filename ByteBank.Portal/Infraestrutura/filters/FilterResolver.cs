using ByteBank.Portal.Filters;
using ByteBank.Portal.Infraestrutura.binding;

namespace ByteBank.Portal.Infraestrutura.filters
{
    public class FilterResolver
    {
        public FiltersResult VerifyFilter(ActionBindingInfo actionBindingInfo)
        {
            var methodInfo = actionBindingInfo.MethodInfo;
            var attribute = methodInfo.GetCustomAttributes(typeof(FiltersAttribute), false);

            foreach (FiltersAttribute filter in attribute)
                if (!filter.goContinue())
                    return new FiltersResult(false);

            return new FiltersResult(true);        
        }
    }
}
