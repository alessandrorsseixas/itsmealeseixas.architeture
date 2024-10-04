using itsmealeseixas.architeture.utilities.Seedworks.Abrastracts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.utilities.SeedWorks
{
    [ExcludeFromCodeCoverage]
    public class Email : EntityExternal
    {
        public int Code { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string TO { get; set; }
        public string CC { get; set; }
        public string CCO { get; set; }
        public Email(int code, string title, string body)
        {
            Code = code;
            Title = title;
            Body = body;
        }
    }
}
