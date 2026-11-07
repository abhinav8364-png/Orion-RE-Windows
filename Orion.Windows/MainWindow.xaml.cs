// Copyright (C) 2026 Abhinav Krishnan and contributors
// SPDX-License-Identifier: GPL-3.0-only
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Orion.Windows.Models;

namespace Orion.Windows;

public partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; }

    public MainWindow()
    {
        InitializeComponent();
        ViewModel = App.Services.GetRequiredService<MainViewModel>();
        DataContext = ViewModel;
    }

    private async void InstallSelected_Click(object sender, RoutedEventArgs e)
    {
        await ConfirmAndRunPackageActionAsync(upgrade: false);
    }

    private async void UpgradeSelected_Click(object sender, RoutedEventArgs e)
    {
        await ConfirmAndRunPackageActionAsync(upgrade: true);
    }

    private void LicenseLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        if (!e.Uri.IsAbsoluteUri || (e.Uri.Scheme != Uri.UriSchemeHttp && e.Uri.Scheme != Uri.UriSchemeHttps))
        {
            return;
        }

        e.Handled = true;
        try
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
        }
        catch (Exception exception) when (exception is Win32Exception or InvalidOperationException)
        {
            MessageBox.Show(
                this,
                $"Could not open the license source: {exception.Message}",
                "Unable to open license source",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private async Task ConfirmAndRunPackageActionAsync(bool upgrade)
    {
        AppCatalogEntry? app = ViewModel.SelectedApp;
        if (app is null || !app.CanManageWithWinget || ViewModel.IsPackageOperationInProgress)
        {
            return;
        }

        var action = upgrade ? "update" : "install";
        var result = MessageBox.Show(
            this,
            $"Orion will open Windows Package Manager to {action} {app.Name}.\n\n" +
            $"Catalog publisher: {app.Publisher}\nExact package ID: {app.PackageId}\nSource: winget\n\n" +
            "The installer will not be run silently. Verify the publisher shown by winget, then approve any installer prompts in its window.",
            $"{char.ToUpperInvariant(action[0])}{action[1..]} {app.Name}?",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning,
            MessageBoxResult.No);

        if (result != MessageBoxResult.Yes)
        {
            return;
        }

        if (upgrade)
        {
            await ViewModel.UpgradeSelectedAsync();
        }
        else
        {
            await ViewModel.InstallSelectedAsync();
        }
    }
}
