# Multi-Tenant Apps with EF Core and ASP.NET Core

This project is based on the article **["Multi-Tenant Apps with EF Core and ASP.NET Core"](https://blog.jetbrains.com/dotnet/2022/06/22/multi-tenant-apps-with-ef-core-and-asp-net-core/)** by **Khalid Abuhakmeh**, which provides a comprehensive guide on implementing multitenancy within a shared infrastructure.

## **Multiple Tenants, Same Infrastructure**
The article discusses the approach of supporting multiple tenants within a **single database** while ensuring strict data isolation. This is achieved using Entity Framework Core’s **global query filters**, which automatically filter data based on the `TenantId`. By injecting a tenant resolution service into `DbContext`, we can dynamically apply filtering to enforce data segregation.

## **How We Applied This Approach**
Inspired by this methodology, our project:
- Implements **tenant-based filtering** in EF Core using `.HasQueryFilter(x => x.TenantId == tenantId)`.
- Introduces a **Tenant Service** that dynamically resolves and sets the `TenantId` per request.
- Uses **ASP.NET Core Middleware** to extract the tenant identifier from authentication claims and inject it into the application context.
- Ensures best practices for **data isolation and security**, reducing the risk of cross-tenant data leaks.

This foundation enables early-stage SaaS startups to **build secure, scalable multi-tenant applications** while laying the groundwork for future **SOC2 compliance**.

> **Credit:** This project is built upon concepts from Khalid Abuhakmeh's JetBrains article, and we extend its ideas with additional security measures and middleware-based tenant resolution.
