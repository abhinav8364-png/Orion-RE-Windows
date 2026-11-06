// Copyright (C) 2026 Abhinav Krishnan and contributors
// SPDX-License-Identifier: GPL-3.0-only
using Orion.Windows;
using Xunit;

namespace Orion.Tests;

public sealed class JsonSettingsStoreTests
{
    [Fact]
    public void SaveAndLoad_RoundTripsSettings()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), $"orion-settings-{Guid.NewGuid():N}.json");
        var store = new JsonSettingsStore(tempFile);
        var expected = new AppSettings
        {
            Theme = "Dusk",
            AccentColor = "#C7F14A",
            DownloadNotificationsEnabled = false,
            ShowUnsignedWarning = true
        };

        store.Save(expected);

        var actual = store.Load();

        Assert.Equal(expected.Theme, actual.Theme);
        Assert.Equal(expected.AccentColor, actual.AccentColor);
        Assert.Equal(expected.DownloadNotificationsEnabled, actual.DownloadNotificationsEnabled);
        Assert.Equal(expected.ShowUnsignedWarning, actual.ShowUnsignedWarning);

        File.Delete(tempFile);
    }

    [Fact]
    public void Load_ReturnsDefaults_WhenJsonIsCorrupted()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), $"orion-settings-{Guid.NewGuid():N}.json");
        File.WriteAllText(tempFile, "{ this is not json }");

        var store = new JsonSettingsStore(tempFile);
        var actual = store.Load();

        Assert.Equal("Dark", actual.Theme);
        Assert.Equal("#5B5CEB", actual.AccentColor);
        Assert.True(actual.DownloadNotificationsEnabled);

        File.Delete(tempFile);
    }
}
