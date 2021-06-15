using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WSCUIL
{
    [DataContract]
    public class ExceptionFaultContract
    {
        [DataMember]
        public string StatusCode { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}