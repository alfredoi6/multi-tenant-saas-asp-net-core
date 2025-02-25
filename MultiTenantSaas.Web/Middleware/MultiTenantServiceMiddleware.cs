using MultiTenantSaas.Web.Data;
using MultiTenantSaas.Web.Services;

namespace MultiTenantSaas.Web.Middleware;

public class MultiTenantServiceMiddleware : IMiddleware
{
    private readonly ITenantService setter;
    public MultiTenantServiceMiddleware(ITenantService setter)
    {
        this.setter = setter;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Query.TryGetValue("tenant", out var values))
        {
            var tenant = MockTenantRepository.Find(values.FirstOrDefault());
            setter.SetTenant(tenant);
        }
        else
        {
            // set default tenant
            setter.SetTenant(MockTenantRepository.PeechtreeConsulting);
        }
        await next(context);
    }
}