using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;

namespace RESTClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tb1.Text = "WCF";
        }

        private void Such(object sender, RoutedEventArgs e)
        {
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;
                var json = web.DownloadString($"https://www.googleapis.com/books/v1/volumes?q={tb1.Text}");
                //MessageBox.Show(json);
                tbjson.Text = json;

                //Newtonsoft
                BooksResult result = JsonConvert.DeserializeObject<BooksResult>(json);
                myGrid.ItemsSource = result.items.Select(x => x.volumeInfo);

                //BooksResult result = System.Text.jsonserializer

            }

        }
    }
}
