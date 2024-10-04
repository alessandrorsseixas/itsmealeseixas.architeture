using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.utilities.SeedWorks.Interfaces
{
    public interface ITenantProvider
    {
        string GetCurrentTenantId();
        void SetCurrentTenantId(string tenantId);
    }
}
