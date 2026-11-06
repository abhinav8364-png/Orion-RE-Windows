// Copyright (C) 2026 Abhinav Krishnan and contributors
// SPDX-License-Identifier: GPL-3.0-only
namespace Orion.Windows;

public interface IAppTracker
{
    IReadOnlyList<string> DiscoverInstalledApplications();
    bool IsUpdateAvailable(string packageId, string installedVersion, string latestVersion);
}

public sealed class AppTracker : IAppTracker
{
    public IReadOnlyList<string> DiscoverInstalledApplications()
    {
        return [
            "Orion-RE Windows",
            "Microsoft Edge",
            "Windows Terminal"
        ];
    }

    public bool IsUpdateAvailable(string packageId, string installedVersion, string latestVersion)
    {
        if (string.IsNullOrWhiteSpace(installedVersion) || string.IsNullOrWhiteSpace(latestVersion))
        {
            return false;
        }

        return string.CompareOrdinal(installedVersion, latestVersion) < 0;
    }
}
