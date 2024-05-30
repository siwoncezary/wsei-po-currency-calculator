using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyCalculator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        TableName.SelectedIndex = 0;
    }

    private async void DownloadButton_OnClick(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        if (button is not null)
        {
            button.IsEnabled = false;
        }
        else
        {
            return;
        }

        var item = TableName.SelectedItem;
        var table = ((ComboBoxItem)TableName.SelectedItem).Content.ToString().Split(" ")[1];    
        string currencies = await DownloadData(table);
        var tables = Deserialize(currencies);
        Rates.ItemsSource = tables[0].rates;
        button.IsEnabled = true;
    }

    private async Task<string> DownloadData(string table)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Accepted", "application/json");
        var response = await client.GetAsync($"https://api.nbp.pl/api/exchangerates/tables/{table}/");
        return await response.Content.ReadAsStringAsync();
    }

    private List<Model.TableRate>? Deserialize(string json)
    {
        return JsonSerializer.Deserialize<List<Model.TableRate>>(json);
    }
}