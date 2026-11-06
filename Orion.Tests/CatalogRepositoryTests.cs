using Orion.Windows;
using Xunit;

namespace Orion.Tests;

public sealed class CatalogRepositoryTests
{
    [Fact]
    public void LoadCatalog_ReturnsBroadCatalogWithUniquePackageIds()
    {
        var catalog = new CatalogRepository().LoadCatalog();

        Assert.True(catalog.Count >= 50);
        Assert.Equal(catalog.Count, catalog.Select(entry => entry.PackageId).Distinct(StringComparer.OrdinalIgnoreCase).Count());
        Assert.Contains(catalog, entry => entry.Category == "Browsers");
        Assert.Contains(catalog, entry => entry.Category == "Games");
        Assert.Contains(catalog, entry => entry.CanManageWithWinget && entry.InstallType == "winget");
        Assert.All(catalog.Where(entry => entry.PackageId.StartsWith("com.", StringComparison.Ordinal)), entry => Assert.False(entry.CanManageWithWinget));
    }

    [Fact]
    public void LoadCatalog_Includes100AdditionalLicensedOpenSourceWingetApps()
    {
        var catalog = new CatalogRepository().LoadCatalog();
        var openSourceApps = catalog.Where(entry => entry.IsFreeOpenSource).ToList();

        Assert.Equal(100, openSourceApps.Count);
        Assert.All(openSourceApps, app =>
        {
            Assert.False(string.IsNullOrWhiteSpace(app.PackageId));
            Assert.False(string.IsNullOrWhiteSpace(app.License));
            Assert.Equal("winget", app.Source);
            Assert.True(app.CanManageWithWinget);
        });
        Assert.Equal(catalog.Count, catalog.Select(entry => entry.PackageId).Distinct(StringComparer.OrdinalIgnoreCase).Count());
    }

    [Fact]
    public void ParseCatalogEntry_ValidJson_ReturnsEntry()
    {
        var repository = new CatalogRepository();
        var json = """
        {
          "name": "Aurora Reader",
          "packageId": "com.example.aurora",
          "version": "2.1.0",
          "publisher": "Example Labs"
        }
        """;

        var entry = repository.ParseCatalogEntry(json);

        Assert.Equal("Aurora Reader", entry.Name);
        Assert.Equal("com.example.aurora", entry.PackageId);
        Assert.Equal("2.1.0", entry.Version);
        Assert.Equal("Example Labs", entry.Publisher);
    }

    [Fact]
    public void ParseCatalogEntry_MissingRequiredFields_Throws()
    {
        var repository = new CatalogRepository();
        var json = """
        {
          "name": "Broken App",
          "packageId": "com.example.broken"
        }
        """;

        Assert.Throws<InvalidOperationException>(() => repository.ParseCatalogEntry(json));
    }
}
