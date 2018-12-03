using System;
using System.Linq;

namespace CurrencyExchanger
{
    public class Exchanger
    {
        private readonly StockExchange[] stockExchanges;

        public Exchanger(StockExchange[] exchangeRates)
        {
            this.stockExchanges = exchangeRates;
        }

        public Money Exchange(Money money, Currency newCurrency)
        {
            var stockExchange = this.stockExchanges.First(x => x.Currency == money.Currency);
            var varNewAmount = money.Amount *
                               (newCurrency == Currency.Bitcoin ? new Random(397).Next(0, 10000) : stockExchange.ExchangeRates[newCurrency].BuyRate);
            money.Currency = newCurrency;
            return new Money() { Amount = varNewAmount, Currency = newCurrency };
        }
    }
}