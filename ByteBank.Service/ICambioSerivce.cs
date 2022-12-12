namespace ByteBank.Service;

public interface ICambioSerivce
{
    decimal Calcular(string moedaOrigem, string moedaDestino, decimal valor);
}

