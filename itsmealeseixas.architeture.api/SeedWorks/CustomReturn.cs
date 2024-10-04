using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.api.SeedWorks
{

    [DataContract]
    public class CustomReturn
    {
        public bool transactionExecute { get; set; }
        public int statusCode { get; set; }
        public int messagecode { get; set; }
        public string messageTitle { get; set; }
        public object message { get; set; }

        public object data { get; set; }

    }
}
