using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace CurrencyConvertor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        GetExchangeRateFileManager ExchangeRateManager = new GetExchangeRateFileManager();
        bool editLock = false;

        public MainWindow()
        {
            InitializeComponent();
            Title = "Konvertor měn";
        }

        /// <summary>
        /// stazeni kurzu z webu CNB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getExchangeRateButton_Click(object sender, RoutedEventArgs e)
        {
            ExchangeRateManager.downloadExchangeRatesFile();
            displayRates_dataGrid.ItemsSource = GetExchangeRateFileManager.exchangeRateFile.exchangeRateList;
            displayRates_dataGrid.IsReadOnly = true;

            convertFrom_comboBox.ItemsSource = GetExchangeRateFileManager.currenciesList.currenciesFrom;
            convertFrom_comboBox.SelectedValue = "CZK";
            convertTo_comboBox.ItemsSource = GetExchangeRateFileManager.currenciesList.currenciesTo;
            convertTo_comboBox.SelectedValue = "EUR";
        }

        /// <summary>
        /// export stazenych kurzovych hodnot 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Sešit Excelu (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            
            if (sfd.ShowDialog().Value)
            {
                try
                {
                    _Application excel = new Excel.Application();
                    Workbook workbook = excel.Workbooks.Add();
                    Worksheet worksheet = excel.ActiveSheet;
                    //Workbook workbook;
                    //Worksheet worksheet;
                    excel.Visible = false;
                    string path = sfd.FileName;

                    worksheet.Cells[1, 1] = GetExchangeRateFileManager.exchangeRateFile.date;
                    worksheet.Cells[2, 1] = "Země";
                    worksheet.Cells[2, 2] = "Měna";
                    worksheet.Cells[2, 3] = "Množství";
                    worksheet.Cells[2, 4] = "Kód";
                    worksheet.Cells[2, 5] = "Kurz";

                    var helper = GetExchangeRateFileManager.exchangeRateFile.exchangeRateList.ToArray();
                    foreach (var line in GetExchangeRateFileManager.exchangeRateFile.exchangeRateList)
                    {
                        var row = GetExchangeRateFileManager.exchangeRateFile.exchangeRateList.IndexOf(line);
                        worksheet.Cells[row + 3, 1] = line.Zeme;
                        worksheet.Cells[row + 3, 2] = line.Mena;
                        worksheet.Cells[row + 3, 3] = line.Mnozstvi;
                        worksheet.Cells[row + 3, 4] = line.Kod;
                        worksheet.Cells[row + 3, 5] = line.Kurz;
                    }

                    workbook.SaveAs(path);
                    workbook.Close();
                    
                    Marshal.ReleaseComObject(workbook);
                    Marshal.ReleaseComObject(excel);
                }
                catch (Exception ex)
                {
                    ex = ex.InnerException;
                }
            }

            
        }

        /// <summary>
        /// aktualizace vysledku v setAmount2_textBox po zadani hodnoty do setAmount1_textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setAmount1_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (editLock)
                return;
            editLock = true;

            string currencyFrom = convertFrom_comboBox.Text;
            string currencyTo = convertTo_comboBox.Text;
            double.TryParse(setAmount1_textBox.Text, out double amountFrom);

            if (!string.IsNullOrEmpty(currencyFrom) && !string.IsNullOrEmpty(currencyTo))
            {
                setAmount2_textBox.Text = GetExchangeRateFileManager.ConvertFromTo(currencyFrom, currencyTo, amountFrom).ToString("N3");
            }

            editLock = false;
        }

        /// <summary>
        /// aktualizace vysledku v setAmount1_textBox po zadani hodnoty do setAmount2_textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setAmount2_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (editLock)
                return;
            editLock = true;

            string currencyFrom = convertTo_comboBox.Text;
            string currencyTo = convertFrom_comboBox.Text;
            double.TryParse(setAmount2_textBox.Text, out double amountFrom);

            if (!string.IsNullOrEmpty(currencyFrom) && !string.IsNullOrEmpty(currencyTo))
            {
                setAmount1_textBox.Text = GetExchangeRateFileManager.ConvertToFrom(currencyFrom, currencyTo, amountFrom).ToString("N3");
            }

            editLock = false;
        }

        /// <summary>
        /// aktualizace vysledku v setAmount1_textBox pri vybrani polozky z comboboxTo ukazatelem mysi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void convertTo_comboBox_DropDownClosed(object sender, EventArgs e)
        {
            setAmount2_textBox_TextChanged(null, null);
        }

        /// <summary>
        /// aktualizace vysledku v setAmount2_textBox pri vybrani polozky z comboboxFrom ukazatelem mysi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void convertFrom_comboBox_DropDownClosed(object sender, EventArgs e)
        {
            setAmount1_textBox_TextChanged(null, null);
        }

        /// <summary>
        /// aktualizace vysledku v setAmount2_textBox pri vybrani polozky z comboboxFrom sipkou nahoru
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void convertFrom_comboBox_KeyUp(object sender, KeyEventArgs e)
        {
            setAmount1_textBox_TextChanged(null, null);
        }

        /// <summary>
        /// aktualizace vysledku v setAmount2_textBox pri vybrani polozky z comboboxFrom sipkou dolu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void convertFrom_comboBox_KeyDown(object sender, KeyEventArgs e)
        {
            setAmount1_textBox_TextChanged(null, null);
        }

        /// <summary>
        /// /// aktualizace vysledku v setAmount1_textBox pri vybrani polozky z comboboxTo sipkou nahoru
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void convertTo_comboBox_KeyUp(object sender, KeyEventArgs e)
        {
            setAmount2_textBox_TextChanged(null, null);
        }

        /// <summary>
        /// aktualizace vysledku v setAmount1_textBox pri vybrani polozky z comboboxTo sipkou dolu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void convertTo_comboBox_KeyDown(object sender, KeyEventArgs e)
        {
            setAmount2_textBox_TextChanged(null, null);
        }
    }
}
