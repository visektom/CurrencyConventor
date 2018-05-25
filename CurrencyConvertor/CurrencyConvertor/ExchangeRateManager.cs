using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CurrencyConvertor
{
    public class GetExchangeRateFileManager
    {
        private static string remoteUri = "http://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.txt";
        public static ExchangeRateFileModel exchangeRateFile = new ExchangeRateFileModel();
        public static ConversionDataModel currenciesList = new ConversionDataModel();

        /// <summary>
        /// stahne soubor s kurzy z url a spusti parsovani souboru
        /// </summary>
        public void downloadExchangeRatesFile()
        {
            using (WebClient myWebClient = new WebClient())
            {
                try
                {
                    myWebClient.Encoding = Encoding.UTF8;
                    StreamReader stream = new StreamReader(myWebClient.OpenRead(remoteUri.ToString()));
                    parseExchangeRatesFile(stream);
                }
                catch (Exception ex)
                {
                    ex = ex.InnerException;
                }
            }
        }

        /// <summary>
        /// rozparsuje stazeny soubor s kurzy, naplni model stazenymi hodnotami
        /// </summary>
        /// <param name="inputStream"></param>
        public static void parseExchangeRatesFile(StreamReader inputStream)
        {
            string[] date = inputStream.ReadLine().Split('#');
            string line;

            exchangeRateFile.date = DateTime.ParseExact(date[0].Trim(), "dd.MM.yyyy", CultureInfo.CurrentCulture);
            exchangeRateFile.headers = inputStream.ReadLine();
            exchangeRateFile.exchangeRateList = new List<ExchangeRateItemModel>();
            currenciesList.currenciesFrom = new List<string>();
            currenciesList.currenciesTo = new List<string>();
            
            while ((line = inputStream.ReadLine()) != null)
            {
                string[] currencyLine = line.Split('|');
                exchangeRateFile.exchangeRateList.Add(new ExchangeRateItemModel { Zeme = currencyLine[0], Mena = currencyLine[1], Mnozstvi = int.Parse(currencyLine[2]), Kod = currencyLine[3].ToUpper(), Kurz = double.Parse(currencyLine[4]) });
                currenciesList.currenciesTo.Add(currencyLine[3].ToUpper());
                
            }
            exchangeRateFile.exchangeRateList.OrderBy(x => x.Kod);
            currenciesList.currenciesTo.Sort();
            currenciesList.currenciesFrom.Add("CZK");
        }

        /// <summary>
        /// provadi konverzi kurzu - zleva doprava
        /// </summary>
        /// <param name="currencyFrom"></param>
        /// <param name="currencyTo"></param>
        /// <param name="amountFrom"></param>
        /// <returns></returns>
        public static double ConvertFromTo(string currencyFrom, string currencyTo, double amountFrom)
        {
            double exchangeRate = exchangeRateFile.exchangeRateList.FirstOrDefault(x => x.Kod == currencyTo).Kurz;
            int amount = exchangeRateFile.exchangeRateList.FirstOrDefault(x => x.Kod == currencyTo).Mnozstvi;
            double amountTo = amountFrom * amount / exchangeRate;

            return amountTo;
        }

        /// <summary>
        /// provadi konverzi kurzu - zprava doleva
        /// </summary>
        /// <param name="currencyFrom"></param>
        /// <param name="currencyTo"></param>
        /// <param name="amountFrom"></param>
        /// <returns></returns>
        public static double ConvertToFrom(string currencyFrom, string currencyTo, double amountFrom)
        {
            double exchangeRate = exchangeRateFile.exchangeRateList.FirstOrDefault(x => x.Kod == currencyFrom).Kurz;
            int amount = exchangeRateFile.exchangeRateList.FirstOrDefault(x => x.Kod == currencyFrom).Mnozstvi;
            double amountTo = amountFrom / amount * exchangeRate;

            return amountTo;
        }
    }
}
