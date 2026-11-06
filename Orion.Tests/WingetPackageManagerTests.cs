using Orion.Windows;
using Orion.Windows.Models;
using Xunit;

namespace Orion.Tests;

public sealed class WingetPackageManagerTests
{
    [Fact]
    public async Task InstallAsync_RejectsCatalogEntryNotEnabledForWinget()
    {
        var manager = new WingetPackageManager();

        await Assert.ThrowsAsync<InvalidOperationException>(() => manager.InstallAsync(new AppCatalogEntry
        {
            Name = "Catalog preview",
            PackageId = "com.example.preview"
        }));
    }

    [Fact]
    public async Task InstallAsync_RejectsPackageIdsThatCouldChangeTheCommand()
    {
        var manager = new WingetPackageManager();

        await Assert.ThrowsAsync<InvalidOperationException>(() => manager.InstallAsync(new AppCatalogEntry
        {
            Name = "Invalid listing",
            PackageId = "safe-id & unwanted-command",
            CanManageWithWinget = true
        }));
    }

    [Fact]
    public async Task UpgradeAsync_RejectsPackageIdsThatCouldChangeTheCommand()
    {
        var manager = new WingetPackageManager();

        await Assert.ThrowsAsync<InvalidOperationException>(() => manager.UpgradeAsync(new AppCatalogEntry
        {
            Name = "Invalid listing",
            PackageId = "safe-id | unwanted-command",
            CanManageWithWinget = true
        }));
    }
}
