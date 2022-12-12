
namespace ByteBank.Portal.Infraestrutura.filters
{
    public class FiltersResult
    {
        public bool GoContinue { get; private set; }
        public FiltersResult(bool goContinue)
        {
            GoContinue = goContinue;
        }
    }
}
