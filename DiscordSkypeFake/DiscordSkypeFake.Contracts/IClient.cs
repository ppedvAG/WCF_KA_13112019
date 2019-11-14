using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace DiscordSkypeFake.Contracts
{
    [ServiceContract]
    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void ShowUsers(IEnumerable<string> users);

        [OperationContract(IsOneWay = true)]
        void LoginResponse(bool ok, string msg);

        [OperationContract(IsOneWay = true)]
        void LogoutResponse();

        [OperationContract(IsOneWay = true)]
        void ShowText(string text, DateTime time, string user);
        
        [OperationContract(IsOneWay = true)]
        void ShowImage(Stream img);
    }
}
