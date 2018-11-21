using System;
using System.Collections.Generic;

namespace CurrencyExchanger
{
    public class Program
    {
        static void Main(string[] args)
        {
            var exchanger = GetExchanger();
            var newMoney = exchanger.Exchange(new Money() {Amount = 100500, Currency = Currency.RUB}, Currency.USD);
            Console.WriteLine($"{newMoney.Currency} : {newMoney.Amount}");
            Console.ReadKey();
        }

        private static Exchanger GetExchanger()
        {
            return new Exchanger(
                new StockExchange[]
                {
                    new StockExchange()
                    {
                        Currency = Currency.RUB,
                        ExchangeRates = new Dictionary<Currency, CurrencyRates>()
                        {
                            {
                                Currency.USD,
                                new CurrencyRates() {BuyRate = new decimal(0.01524), SellRate = new decimal(0.01424)}
                            },
                            {
                                Currency.EUR,
                                new CurrencyRates() {BuyRate = new decimal(0.012), SellRate = new decimal(0.0112)}
                            }
                        }
                    },
                    new StockExchange()
                    {
                        Currency = Currency.EUR,
                        ExchangeRates = new Dictionary<Currency, CurrencyRates>()
                        {
                            {
                                Currency.USD,
                                new CurrencyRates() {BuyRate = new decimal(1.11), SellRate = new decimal(1.15)}
                            },
                            {
                                Currency.GBP,
                                new CurrencyRates() {BuyRate = new decimal(0.91), SellRate = new decimal(0.92)}
                            }
                        }
                    },
                    new StockExchange()
                    {
                        Currency = Currency.USD,
                        ExchangeRates = new Dictionary<Currency, CurrencyRates>()
                        {
                            {
                                Currency.EUR,
                                new CurrencyRates() {BuyRate = new decimal(0.873465), SellRate = new decimal(0.883465)}
                            },
                            {
                                Currency.GBP,
                                new CurrencyRates() {BuyRate = new decimal(0.777771), SellRate = new decimal(0.80)}
                            },
                            {
                                Currency.RUB,
                                new CurrencyRates() {BuyRate = new decimal(34.5), SellRate = new decimal(35.3)}
                            }
                        }
                    }
                }
            );
        }
    }
}