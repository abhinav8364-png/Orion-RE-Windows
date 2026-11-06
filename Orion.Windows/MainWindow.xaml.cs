// Copyright (C) 2026 Abhinav Krishnan and contributors
// SPDX-License-Identifier: GPL-3.0-only
using System.Windows;
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
