using System.Collections.Generic;

namespace CurrencyExchanger
{
    public class StockExchange
    {
        public Currency Currency;

        public Dictionary<Currency, CurrencyRates> ExchangeRates;
    }
}