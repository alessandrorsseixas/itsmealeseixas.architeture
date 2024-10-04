using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.utilities.SeedWorks
{
    [ExcludeFromCodeCoverage]
    public class JwtBearerSettings
    {
        public string Secret { get; set; }
        public string Expires { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
