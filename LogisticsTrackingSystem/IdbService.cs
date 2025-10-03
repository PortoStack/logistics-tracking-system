using LogisticsTrackingSystem;
using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace LogisticsTrackingSystem
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IdbService" in both code and config file together.
    [ServiceContract]
    public interface IdbService
    {
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "customers")]
        CustomerObj[] customers();

    }
}
