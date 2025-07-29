using ExRate.Domain.Entities;

namespace ExRate.Domain.Interfaces;

public interface IExchangeRateProvider
{
    string ProviderName { get; }
    
    Task<ExchangeRateResponse?> GetExchangeRateAsync(string fromCurrency, string toCurrency, decimal amount);
}
