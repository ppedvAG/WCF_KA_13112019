using DiscordSkypeFake.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DiscordSkypeFake.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class DSFServer : IServer
    {
        Dictionary<IClient, string> users = new Dictionary<IClient, string>();


        public void Login(string username)
        {
            Console.WriteLine($"Login: {username}");
            var client = OperationContext.Current.GetCallbackChannel<IClient>();
            if (users.ContainsValue(username))
                client.LoginResponse(false, $"Der Name {username} ist bereits in verwendung");
            else
            {
                client.ShowText($"Hi {username}", DateTime.Now, "Admin");
                users.Add(client, username);
                client.LoginResponse(true, "");
                SendUserlist();
            }
        }

        public void Logout()
        {
            var client = OperationContext.Current.GetCallbackChannel<IClient>();
            if (users.TryGetValue(client, out string usr))
            {
                Console.WriteLine($"Logout: {usr}");
                client.LogoutResponse();
                users.Remove(client);
                SendUserlist();
            }
        }

        public void SendImage(Stream img)
        {
            Console.WriteLine($"SendImage: ...");
            var ms = new MemoryStream();
            img.CopyTo(ms);

            var dict = new Dictionary<IClient, MemoryStream>();
            users.Keys.ToList().ForEach(x =>
            {
                var msC = new MemoryStream();
                ms.Position = 0;
                ms.CopyTo(msC);
                msC.Position = 0;
                dict.Add(x, msC);
            });

            SendToAllClient(x =>
            {
                x.ShowImage(dict[x]);
            });

        }

        public void SendText(string text)
        {
            var client = OperationContext.Current.GetCallbackChannel<IClient>();
            if (users.TryGetValue(client, out string usr))
            {
                Console.WriteLine($"SendText: {text} von  {usr}");

                SendToAllClient(x => x.ShowText(text, DateTime.Now, usr));

            }
        }

        private void SendUserlist()
        {
            SendToAllClient(c => c.ShowUsers(users.Values));
        }

        private void SendToAllClient(Action<IClient> client)
        {
            Parallel.ForEach(users, u =>
            {
                try
                {
                    client.Invoke(u.Key);
                }
                catch (Exception)
                {
                    users.Remove(u.Key);
                    SendUserlist();
                }
            });
        }
    }
}
