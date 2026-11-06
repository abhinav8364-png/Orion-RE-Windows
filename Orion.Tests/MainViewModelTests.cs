// Copyright (C) 2026 Abhinav Krishnan and contributors
// SPDX-License-Identifier: GPL-3.0-only
using Orion.Windows;
using Orion.Windows.Models;
using Xunit;

namespace Orion.Tests;

public sealed class MainViewModelTests
{
    [Fact]
    public void Search_FiltersCatalogAndSelectsFirstResult()
    {
        var viewModel = CreateViewModel();

        viewModel.SearchText = "reader";

        var result = Assert.Single(viewModel.VisibleCatalog);
        Assert.Equal("Aurora Reader", result.Name);
        Assert.Same(result, viewModel.SelectedApp);
        Assert.True(viewModel.HasCatalogResults);
        Assert.True(viewModel.HasSelectedApp);
    }

    [Fact]
    public async Task InstallSelected_UsesWingetForEnabledListing()
    {
        var packageManager = new TestPackageManager();
        var viewModel = CreateViewModel(packageManager);
        var selectedApp = Assert.IsType<AppCatalogEntry>(viewModel.SelectedApp);
        selectedApp.CanManageWithWinget = true;

        await viewModel.InstallSelectedAsync();

        Assert.Same(selectedApp, packageManager.InstalledApp);
        Assert.Null(packageManager.UpgradedApp);
        Assert.Contains("finished", viewModel.PackageOperationStatus);
        Assert.False(viewModel.IsPackageOperationInProgress);
    }

    [Fact]
    public async Task UpgradeSelected_UsesWingetForEnabledListing()
    {
        var packageManager = new TestPackageManager();
        var viewModel = CreateViewModel(packageManager);
        var selectedApp = Assert.IsType<AppCatalogEntry>(viewModel.SelectedApp);
        selectedApp.CanManageWithWinget = true;

        await viewModel.UpgradeSelectedAsync();

        Assert.Same(selectedApp, packageManager.UpgradedApp);
        Assert.Null(packageManager.InstalledApp);
    }

    [Fact]
    public async Task InstallSelected_DoesNotRunForCatalogOnlyListing()
    {
        var packageManager = new TestPackageManager();
        var viewModel = CreateViewModel(packageManager);

        Assert.False(viewModel.CanStartPackageAction);
        await viewModel.InstallSelectedAsync();

        Assert.Null(packageManager.InstalledApp);
        Assert.Null(packageManager.UpgradedApp);
    }

    [Fact]
    public async Task OpenSourceCatalogEntry_CanBeInstalledAndUpdated()
    {
        var packageManager = new TestPackageManager();
        var viewModel = new MainViewModel(new CatalogRepository(), packageManager);
        var app = Assert.Single(viewModel.VisibleCatalog, entry => entry.PackageId == "Apache.OpenOffice");

        viewModel.SelectedApp = app;

        Assert.True(app.IsFreeOpenSource);
        Assert.True(viewModel.CanStartPackageAction);

        await viewModel.InstallSelectedAsync();
        await viewModel.UpgradeSelectedAsync();

        Assert.Same(app, packageManager.InstalledApp);
        Assert.Same(app, packageManager.UpgradedApp);
    }

    [Fact]
    public void EmptySearchResult_ClearsSelectionAndCanBeRecovered()
    {
        var viewModel = CreateViewModel();

        viewModel.SearchText = "not in the catalog";

        Assert.Empty(viewModel.VisibleCatalog);
        Assert.Null(viewModel.SelectedApp);
        Assert.False(viewModel.HasCatalogResults);
        Assert.False(viewModel.HasSelectedApp);

        viewModel.ClearSearchCommand.Execute(null);

        Assert.Equal(2, viewModel.VisibleCatalog.Count);
        Assert.True(viewModel.HasCatalogResults);
        Assert.NotNull(viewModel.SelectedApp);
    }

    [Fact]
    public void SelectingCategory_FiltersCatalog()
    {
        var viewModel = CreateViewModel();

        viewModel.SelectedCategory = "Developer";

        var result = Assert.Single(viewModel.VisibleCatalog);
        Assert.Equal("Terminus Pro", result.Name);
    }

    private static MainViewModel CreateViewModel(TestPackageManager? packageManager = null)
    {
        return new MainViewModel(new TestCatalogRepository(
        [
            new AppCatalogEntry
            {
                Name = "Aurora Reader",
                PackageId = "com.example.reader",
                Publisher = "Northwind Labs",
                Category = "Productivity",
                Description = "A document reader."
            },
            new AppCatalogEntry
            {
                Name = "Terminus Pro",
                PackageId = "com.example.terminal",
                Publisher = "Open Systems",
                Category = "Developer",
                Description = "A modern terminal."
            }
        ]), packageManager ?? new TestPackageManager());
    }

    private sealed class TestCatalogRepository(IReadOnlyList<AppCatalogEntry> entries) : ICatalogRepository
    {
        public IReadOnlyList<AppCatalogEntry> LoadCatalog() => entries;

        public AppCatalogEntry ParseCatalogEntry(string json)
        {
            throw new NotSupportedException();
        }
    }

    private sealed class TestPackageManager : IWingetPackageManager
    {
        public AppCatalogEntry? InstalledApp { get; private set; }
        public AppCatalogEntry? UpgradedApp { get; private set; }

        public Task<int> InstallAsync(AppCatalogEntry app)
        {
            InstalledApp = app;
            return Task.FromResult(0);
        }

        public Task<int> UpgradeAsync(AppCatalogEntry app)
        {
            UpgradedApp = app;
            return Task.FromResult(0);
        }
    }
}
