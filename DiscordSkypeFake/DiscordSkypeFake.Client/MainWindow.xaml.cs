using AdonisUI.Controls;
using DiscordSkypeFake.Contracts;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
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

namespace DiscordSkypeFake.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow, IClient
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUILogin(false);
            usernameTb.Text = $"Fred {Guid.NewGuid().ToString().Substring(0, 4)}";
        }


        public void LoginResponse(bool ok, string msg)
        {
            SetUILogin(ok);
            if (!ok)
                MessageBox.Show(msg);
        }

        public void LogoutResponse()
        {
            SetUILogin(false);
        }

        private void SetUILogin(bool ok)
        {
            usernameTb.IsEnabled = !ok;
            loginBtn.IsEnabled = !ok;
            logoutBtn.IsEnabled = ok;
            textTb.IsEnabled = ok;
            sendTextBtn.IsEnabled = ok;
            sendImageBtn.IsEnabled = ok;
        }


        public void ShowImage(Stream img)
        {
            var ms = new MemoryStream();
            img.CopyTo(ms);
            ms.Position = 0;

            //var vb = new Viewbox();

            var image = new Image();
            image.BeginInit();
            image.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            image.Stretch = Stretch.None;
            image.EndInit();
            //vb.Child = image;
            //chatLb.Items.Add(image);

            ms.Position = 0;
            var hash = SHA1.Create().ComputeHash(ms);

            var target = chatLb.Items.OfType<WrapPanel>().FirstOrDefault(x => x.Tag != null && x.Tag.Equals(Convert.ToBase64String(hash)));
            target.Children.Add(image);
            target.Tag = null;

        }

        public void ShowText(string text, DateTime time, string user)
        {
            if (text.StartsWith("IMG"))
            {
                var wrap = new WrapPanel();
                wrap.Children.Add(new TextBlock() { Text = $"[{time:t}] {user}: {text} wird geladen" });
                wrap.Tag = text.Substring(4);
                chatLb.Items.Add(wrap);
            }
            else
                chatLb.Items.Add($"[{time:t}] {user}: {text}");
        }

        public void ShowUsers(IEnumerable<string> users)
        {
            userLb.ItemsSource = users;
        }
        IServer server = null;
        DuplexChannelFactory<IServer> cf = null;
        private void Login(object sender, RoutedEventArgs e)
        {
            var http = new WSDualHttpBinding();
            http.Security.Mode = WSDualHttpSecurityMode.Message;
            http.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
            http.MaxReceivedMessageSize = int.MaxValue;


            var tcp = new NetTcpBinding();
            tcp.MaxReceivedMessageSize = int.MaxValue;
            tcp.Security.Mode = SecurityMode.None;
            cf = new DuplexChannelFactory<IServer>(this, tcp, new EndpointAddress("net.tcp://localhost:1"));
            //cf = new DuplexChannelFactory<IServer>(this, http, new EndpointAddress(new Uri("http://localhost.fiddler:2"), UpnEndpointIdentity.CreateDnsIdentity("SignedByRootCA")));
            //cf.Credentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "69ff17bd74b7e71da25dd06e2952276a4b956a36");
            //cf.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

            //            cf.Credentials.Windows.ClientCredential.UserName = "Fred";
            //            cf.Credentials.Windows.ClientCredential.Password = "123456";
            server = cf.CreateChannel();
            var myTask = Task.Run(() =>
             {
                 server.Login(usernameTb.Dispatcher.Invoke(() => usernameTb.Text));
             });

            myTask.ContinueWith(t => MessageBox.Show(t.Exception.InnerException.Message), TaskContinuationOptions.OnlyOnFaulted);
            //myTask.ContinueWith(t => MessageBox.Show("Erfolgreicher Login"), TaskContinuationOptions.OnlyOnRanToCompletion);

            ((ICommunicationObject)server).Faulted += MainWindow_Faulted;


        }

        private void MainWindow_Faulted(object sender, EventArgs e)
        {
            server = cf.CreateChannel();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            try
            {

                server?.Logout();
            }
            catch (Exception ex)
            {
                SetUILogin(false);
            }
        }

        private void SendText(object sender, RoutedEventArgs e)
        {
            server?.SendText(textTb.Text);
            textTb.Clear();
        }

        private void SendImage(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog() { Filter = "Bild|*.png;*.jpg|Alle Dateien|*.*" };
            if (dlg.ShowDialog().Value)
            {
                try
                {
                    server.SendImage(File.OpenRead(dlg.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($":-( {ex.Message}");
                }
            }
        }
    }
}
