using SelfHost.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfHost.ClientWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cf = new ChannelFactory<IBurgerService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:1"));

            ShowBurger("NET TCP", cf.CreateChannel());

        }

        private void ShowBurger(string info, IBurgerService burgerService)
        {
            listBox1.Items.Add("---------------------------------------");
            listBox1.Items.Add(info);
            var bla = burgerService.GetAllBurgers().ToList();

            bla.ForEach(x => listBox1.Items.Add($"{x.Name} {x.Patties} Patties    Ei: {x.Ei } "));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var cf = new ChannelFactory<IBurgerService>(new BasicHttpBinding(), new EndpointAddress("http://localhost.fiddler:2"));

            ShowBurger("Basic HTTP", cf.CreateChannel());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var cf = new ChannelFactory<IBurgerService>(new WSHttpBinding(), new EndpointAddress("http://localhost:3"));

            ShowBurger("WS HTTP", cf.CreateChannel());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var ipcBind = new NetNamedPipeBinding();
            ipcBind.Security.Mode = NetNamedPipeSecurityMode.None;
            var cf = new ChannelFactory<IBurgerService>(ipcBind, new EndpointAddress("net.pipe://burger/"));

            ShowBurger("Named Pipe", cf.CreateChannel());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var cf = new ChannelFactory<IBurgerService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:1"));

            var neu = new Burger() { Name = "NEU", Patties = 4, Ei = true };
            cf.CreateChannel().Add(neu);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var cf = new ChannelFactory<IBurgerService>(new BasicHttpBinding(), new EndpointAddress("http://localhost.fiddler:2"));

            var neu = new Burger() { Name = "NEU", Patties = -74, Ei = true };
            try
            {
                cf.CreateChannel().Add(neu);
            }
            catch (FaultException<OutOfPattiesException> fex)
            {
                MessageBox.Show("Zu wenig Fleisch!");
            }
            catch (FaultException fex)
            { MessageBox.Show(fex.Message); }
        }
    }
}
