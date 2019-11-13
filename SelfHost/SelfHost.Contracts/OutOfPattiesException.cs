using System.Runtime.Serialization;

namespace SelfHost.Contracts
{
    [DataContract]
    public class OutOfPattiesException
    {
        [DataMember]
        public int PattieCount { get; set; }

        [DataMember]
        public Burger BurgerThatIsOutOfPatties { get; set; }
    }
}
