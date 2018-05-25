using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CurrencyConvertor
{
    public partial class ExchangeRateFileModel
    {
        public DateTime date { get; set; }
        public string headers { get; set; }
        public List<ExchangeRateItemModel> exchangeRateList { get; set; }
    }

    public partial class ExchangeRateItemModel
    {
        [DisplayName("Země")]
        public string Zeme { get; set; }
        [DisplayName("Měna")]
        public string Mena { get; set; }
        [DisplayName("Množství")]
        public int Mnozstvi { get; set; }
        [DisplayName("Kód")]
        public string Kod { get; set; }
        [DisplayName("Kurz")]
        public double Kurz { get; set; }
    }

    public partial class ConversionDataModel
    {
        public List<string> currenciesFrom;
        public List<string> currenciesTo;
    }
}
