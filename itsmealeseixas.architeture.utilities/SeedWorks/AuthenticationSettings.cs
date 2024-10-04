using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.utilities.SeedWorks
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationSettings
    {
        public string RequireConfirmedAccount { get; set; }
        public string RequireConfirmedEmail { get; set; }
        public string RequireConfirmedPhoneNumber { get; set; }
        public string RequireUniqueEmail { get; set; }
        public string RequireDigit { get; set; }
        public string RequiredLength { get; set; }
        public string RequireUppercase { get; set; }
        public string RequireLowercase { get; set; }
        public string RequireNonAlphanumeric { get; set; }
        public string MaxFailedAccessAttempts { get; set; }
        public string DefaultLockoutTimeSpan { get; set; }
        public string DefaultMafExpiredTimeSpan { get; set; }
        public string DefaultWebAppUrl { get; set; }
    }
}
