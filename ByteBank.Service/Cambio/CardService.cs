using ByteBank.Service;

namespace ByteBank.Service.Cambio;

public class CardService : ICardService
{
    public string GetCardCreditSale()
    => "Byte Bank Gold Platinum";

    public string GetCardDebitSale()
    => "Byte Bank tax off";
}

