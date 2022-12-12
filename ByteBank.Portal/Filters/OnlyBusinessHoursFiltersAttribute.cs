using ByteBank.Portal.Infraestrutura.filters;

namespace ByteBank.Portal.Filters
{
    public class OnlyBusinessHoursFiltersAttribute : FiltersAttribute
    {
        public override bool goContinue()
        {
            var hours = DateTime.Now.Hour;
            // return hours >= 9 && hours < 16;
            return true;
        }
    }
}