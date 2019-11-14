using System.IO;
using System.ServiceModel;

namespace DiscordSkypeFake.Contracts
{
    [ServiceContract(CallbackContract = typeof(IClient),SessionMode = SessionMode.Required)]
    public interface IServer
    {
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void Login(string username);

        [OperationContract(IsOneWay = true, IsTerminating = true, IsInitiating = false)]
        void Logout();

        [OperationContract(IsOneWay = true)]
        void SendText(string text);

        [OperationContract(IsOneWay = true)]
        void SendImage(Stream img);
    }
}
