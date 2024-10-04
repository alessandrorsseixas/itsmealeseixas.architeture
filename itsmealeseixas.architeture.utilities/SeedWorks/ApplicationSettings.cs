using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.utilities.SeedWorks
{
    [ExcludeFromCodeCoverage]
    public class ApplicationSettings
    {
        public string CorsOrigins { get; set; }
        public string AppToken { get; set; }
        public string Tenant { get; set; }
        public string Project { get; set; }
        public string DatabaseType { get; set; }
        public string ProcessPath { get; set; }
        public string Api { get; set; }
    }
}
