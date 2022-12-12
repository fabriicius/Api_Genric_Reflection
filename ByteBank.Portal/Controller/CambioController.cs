using ByteBank.Portal.Filters;
using ByteBank.Service;
using ByteBank.Service.Cambio;

namespace ByteBank.Portal.Controller;

public class CambioController : ControllerBase
{
    private ICambioSerivce _cambioService;
    private ICardService _cardService;

    public CambioController(ICardService cardService , ICambioSerivce cambioService)
    {
        _cambioService = cambioService;
        _cardService = cardService;
    }

    [OnlyBusinessHoursFiltersAttribute]
    public string MXN()
    {
        var valorFinal = _cambioService.Calcular("MXN", "BRL", 1);
        return View(new
        {
            VALOR_EM_REAIS = valorFinal
        });
    }

    [OnlyBusinessHoursFiltersAttribute]
    public string USD()
    {
        var valorFinal = _cambioService.Calcular("USD", "BRL", 1);
        return View(new
        {
            VALOR_EM_REAIS = valorFinal
        });
    }

    [OnlyBusinessHoursFiltersAttribute]
    public string Calculo(string coinDestiny) =>
        Calculo("BRL", coinDestiny, 1);

    [OnlyBusinessHoursFiltersAttribute]
    public string Calculo(string coinDestiny, decimal value) =>
        Calculo("BRL", coinDestiny, value);


    [OnlyBusinessHoursFiltersAttribute]
    public string Calculo(string coinOrigin, string coinDestiny, decimal value)
    {
        var valueTotal = _cambioService.Calcular(coinOrigin, coinDestiny, value);
        var cardSale = _cardService.GetCardCreditSale();
        return View(new
        {
            COIN_DESTINY = coinDestiny,
            VALUE_COIN_DESTINY = valueTotal,
            COIN_ORIGIN = coinOrigin,
            VALUE_COIN_ORIGIN = value,
            CARD_SALE = cardSale
        });
    }


}

