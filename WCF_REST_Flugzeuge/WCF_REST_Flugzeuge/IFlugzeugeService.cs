using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WCF_REST_Flugzeuge
{
    [ServiceContract]
    public interface IFlugzeugeService
    {
        [OperationContract]
        [WebGet(UriTemplate = "Flug")]
        IEnumerable<Flugzeug> GetAll();

        [OperationContract]
        [WebGet(UriTemplate = "Flug?id={id}")]
        Flugzeug GetbyId(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Flug")]
        void Add(Flugzeug f);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Flug?id={id}")]
        void Delete(int id);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Flug")]
        void Update(Flugzeug f);
    }
}
