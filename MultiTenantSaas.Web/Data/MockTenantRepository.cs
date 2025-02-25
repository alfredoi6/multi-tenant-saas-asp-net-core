namespace MultiTenantSaas.Web.Data;

public static class MockTenantRepository
{
    public const string PeechtreeConsulting = nameof(PeechtreeConsulting);
    public const string GoldmanLaw = nameof(GoldmanLaw);
    public static IReadOnlyCollection<string> All = new[] {PeechtreeConsulting, GoldmanLaw};
    public static string Find(string? value)
    {
        return All.FirstOrDefault(t => t.Equals(value?.Trim(), StringComparison.OrdinalIgnoreCase)) ?? PeechtreeConsulting;
    }
}