using MultiTenantSaas.Web.Data;

namespace MultiTenantSaas.Web.Services;

public class TenantService : ITenantService
{
    public string Tenant { get; private set; } = MockTenantRepository.PeechtreeConsulting;
    public void SetTenant(string tenant)
    {
        Tenant = tenant;
    }
}