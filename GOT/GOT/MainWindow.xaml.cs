using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GOT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<GOTApi> quotes = new List<GOTApi>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void JokeBttn_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                string url = "https://got-quotes.herokuapp.com/quotes";
                string jsonData = client.GetStringAsync(url).Result;
                GOTApi api = JsonConvert.DeserializeObject<GOTApi>(jsonData);

                CharacterLbl.Content = api.character;
                JokeTxt.Text = api.quote;
                quotes.Add(api);

            }
        }

        private void ExportBttn_Click(object sender, RoutedEventArgs e)
        {
            string path = "GOT_Quotes.json";
            string json = JsonConvert.SerializeObject(quotes);
            File.WriteAllText(path, json);
        }
    }
}
