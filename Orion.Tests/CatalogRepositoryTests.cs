// Copyright (C) 2026 Abhinav Krishnan and contributors
// SPDX-License-Identifier: GPL-3.0-only
using Orion.Windows;
using Orion.Windows.Models;
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
        Assert.All(catalog, entry =>
        {
            Assert.True(entry.CanManageWithWinget);
            Assert.Equal("winget", entry.Source);
        });
    }

    [Fact]
    public void LoadCatalog_Includes500AdditionalLicensedOpenSourceWingetApps()
    {
        var catalog = new CatalogRepository().LoadCatalog();
        var openSourceApps = catalog.Where(entry => entry.IsFreeOpenSource).ToList();

        Assert.Equal(606, openSourceApps.Count);
        Assert.Equal(500, openSourceApps.Count(app => app.HasLicenseUrl));
        Assert.All(openSourceApps, app =>
        {
            Assert.False(string.IsNullOrWhiteSpace(app.PackageId));
            Assert.False(string.IsNullOrWhiteSpace(app.License));
            Assert.Equal("winget", app.Source);
            Assert.True(app.CanManageWithWinget);
        });
        Assert.Equal(catalog.Count, catalog.Select(entry => entry.PackageId).Distinct(StringComparer.OrdinalIgnoreCase).Count());
    }

    [Theory]
    [InlineData("https://example.org/license", true)]
    [InlineData("http://example.org/license", true)]
    [InlineData("javascript:alert(1)", false)]
    [InlineData("file:///C:/license.txt", false)]
    [InlineData("", false)]
    public void AppCatalogEntry_HasLicenseUrlOnlyForWebAddresses(string licenseUrl, bool expected)
    {
        var entry = new AppCatalogEntry { LicenseUrl = licenseUrl };

        Assert.Equal(expected, entry.HasLicenseUrl);
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
