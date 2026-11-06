// Copyright (C) 2026 Abhinav Krishnan and contributors
// SPDX-License-Identifier: GPL-3.0-only
using Orion.Windows;
using Orion.Windows.Models;
using Xunit;

namespace Orion.Tests;

public sealed class DownloadServiceTests
{
    [Fact]
    public async Task DownloadAsync_Throws_WhenPackageHasNoDownloadUrl()
    {
        var service = new DownloadService();
        var package = new AppPackage { Name = "Test App", DownloadUrl = null };

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.DownloadAsync(package, Path.Combine(Path.GetTempPath(), "test-app.zip")));
    }

    [Fact]
    public void AppTracker_ReportsUpdateWhenNewerVersionAvailable()
    {
        var tracker = new AppTracker();

        Assert.True(tracker.IsUpdateAvailable("com.example.app", "1.0.0", "1.1.0"));
        Assert.False(tracker.IsUpdateAvailable("com.example.app", "2.0.0", "1.9.0"));
    }
}
