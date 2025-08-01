using ExchangeRate.Interfaces;
using ExchangeRate.Models;

public class ExchangeRateService : IExchangeRateService
{
    private readonly IEnumerable<IExchangeRateProvider> _providers;

    public ExchangeRateService(IEnumerable<IExchangeRateProvider> providers)
    {
        _providers = providers;
    }

    public async Task<ExchangeRateResponse> GetBestDealAsync(ExchangeRateRequest request)
    {
        var offers = await Task.WhenAll(_providers.Select(p => p.GetExchangeRateAsync(request)));

        var best = offers.OrderByDescending(o => o.Amount).FirstOrDefault();

        return best ?? throw new Exception("No valid exchange rates available.");
    }
}