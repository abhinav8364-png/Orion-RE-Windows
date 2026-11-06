// Copyright (C) 2026 Abhinav Krishnan and contributors
// SPDX-License-Identifier: GPL-3.0-only
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Orion.Windows;

public partial class App : Application
{
    public static IHost AppHost { get; private set; } = null!;

    public static IServiceProvider Services => AppHost.Services;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        AppHost = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<ISettingsStore, JsonSettingsStore>();
                services.AddSingleton<ICatalogRepository, CatalogRepository>();
                services.AddSingleton<IWingetPackageManager, WingetPackageManager>();
                services.AddSingleton<IDownloadService, DownloadService>();
                services.AddSingleton<IAppTracker, AppTracker>();
                services.AddSingleton<INotificationService, NotificationService>();
                services.AddSingleton<IVirusTotalService, VirusTotalService>();
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();
            })
            .Build();

        var mainWindow = Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
