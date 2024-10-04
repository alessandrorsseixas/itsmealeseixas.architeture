using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.utilities.SeedWorks
{
    [ExcludeFromCodeCoverage]
    public class SMTPSettings
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Options { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string SSl { get; set; }

    }
}
