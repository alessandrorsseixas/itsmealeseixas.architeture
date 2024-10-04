using itsmealeseixas.architeture.utilities.SeedWorks.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.utilities.SeedWorks
{
    [ExcludeFromCodeCoverage]
    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _tenantId;

      

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentTenantId()
        {
            _tenantId = _tenantId==null? _httpContextAccessor.HttpContext == null? _tenantId:_httpContextAccessor.HttpContext.Request.Headers["X-Tenant-Id"]:_tenantId;
            // Obtenha o TenantId a partir do cabeçalho 'X-Tenant-Id' da solicitação
            return _tenantId;
        }

        public void SetCurrentTenantId(string tenantId)
        {
            _tenantId = tenantId;
        }
    }
}
