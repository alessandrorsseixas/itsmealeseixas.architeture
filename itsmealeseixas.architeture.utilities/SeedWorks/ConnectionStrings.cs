using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.utilities.SeedWorks
{
    [ExcludeFromCodeCoverage]
    public class ConnectionStrings
    {
        public string SqlServerConnection { get; set; }
        public string PostgreslConnectionString { get; set; }
        public string DefaultConnection { get; set; }
    }
}
