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
        var offers = new List<ExchangeRateResponse>();

        foreach (var provider in _providers)
        {
            try
            {
                var offer = await provider.GetExchangeRateAsync(request);
                if (offer != null)
                    offers.Add(offer);
            }
            catch
            {
                continue;
            }
        }

        var best = offers.OrderByDescending(o => o.Amount).FirstOrDefault();

        return best ?? throw new Exception("No valid exchange rates available.");
    }
}