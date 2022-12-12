using ByteBank.Service;
using ByteBank.Service.Cambio;

namespace ByteBank.Portal.Controller;

public class CardController : ControllerBase
{
    private ICardService _cardService; 
    public CardController(ICardService cardService)
    {
        _cardService = cardService;
    }

    public string Debito() => View( new {CARD_SALE = _cardService.GetCardDebitSale()});
    public string Credito() => View( new {CARD_SALE = _cardService.GetCardCreditSale()});

}
