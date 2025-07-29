using ExchangeRate.Models;

namespace ExchangeRate.Interfaces;

public interface IExchangeRateProvider
{
    Task<ExchangeRateResponse?> GetExchangeRateAsync(ExchangeRateRequest request);
}