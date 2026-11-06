using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Orion.Windows.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Orion.Windows;

public partial class MainViewModel : ObservableObject
{
    private readonly List<AppCatalogEntry> _allCatalogItems;

    [ObservableProperty]
    private string _title = "Orion-RE Windows";

    [ObservableProperty]
    private string _header = "Discover new software";

    [ObservableProperty]
    private string _statusMessage = "Storefront ready";

    [ObservableProperty]
    private string _appCatalogCount = "0 apps loaded";

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _selectedCategory = "All";

    [ObservableProperty]
    private AppCatalogEntry? _selectedApp;

    [ObservableProperty]
    private string _storeSummary = "0 apps";

    [ObservableProperty]
    private bool _hasCatalogResults;

    [ObservableProperty]
    private bool _hasSelectedApp;

    [ObservableProperty]
    private bool _canStartPackageAction;

    [ObservableProperty]
    private bool _isPackageOperationInProgress;

    [ObservableProperty]
    private string _packageOperationStatus = "Choose a winget listing to install or update.";

    public ObservableCollection<AppCatalogEntry> VisibleCatalog { get; } = new();
    public ObservableCollection<AppCatalogEntry> FeaturedApps { get; } = new();
    public ObservableCollection<string> Categories { get; } = new();

    private readonly IWingetPackageManager _packageManager;

    public MainViewModel(ICatalogRepository catalogRepository, IWingetPackageManager packageManager)
    {
        _packageManager = packageManager;
        _allCatalogItems = new List<AppCatalogEntry>(catalogRepository.LoadCatalog());

        foreach (var item in _allCatalogItems)
        {
            if (item.IsFeatured)
            {
                FeaturedApps.Add(item);
            }
        }

        Categories.Add("All");
        foreach (var category in _allCatalogItems.Select(i => i.Category).Distinct(StringComparer.OrdinalIgnoreCase))
        {
            Categories.Add(category);
        }

        ApplyFilters();
        SelectedApp = VisibleCatalog.FirstOrDefault();
        AppCatalogCount = _allCatalogItems.Count == 1
            ? "1 app available"
            : $"{_allCatalogItems.Count} apps available";
    }

    partial void OnSearchTextChanged(string value)
    {
        ApplyFilters();
    }

    partial void OnSelectedCategoryChanged(string value)
    {
        ApplyFilters();
    }

    partial void OnSelectedAppChanged(AppCatalogEntry? value)
    {
        HasSelectedApp = value is not null;
        UpdateCanStartPackageAction();
    }

    partial void OnIsPackageOperationInProgressChanged(bool value)
    {
        UpdateCanStartPackageAction();
    }

    [RelayCommand]
    private void ClearSearch()
    {
        SearchText = string.Empty;
    }

    public Task InstallSelectedAsync()
    {
        return RunPackageActionAsync(upgrade: false);
    }

    public Task UpgradeSelectedAsync()
    {
        return RunPackageActionAsync(upgrade: true);
    }

    private async Task RunPackageActionAsync(bool upgrade)
    {
        var app = SelectedApp;
        if (app is null || !app.CanManageWithWinget || IsPackageOperationInProgress)
        {
            return;
        }

        IsPackageOperationInProgress = true;
        var action = upgrade ? "Update" : "Install";
        PackageOperationStatus = $"{action} started for {app.Name}. Complete the prompts in the winget window.";

        try
        {
            var exitCode = upgrade
                ? await _packageManager.UpgradeAsync(app)
                : await _packageManager.InstallAsync(app);
            PackageOperationStatus = exitCode == 0
                ? $"{action} finished for {app.Name}."
                : $"winget could not {action.ToLowerInvariant()} {app.Name} (exit code {exitCode}). Review the winget window.";
        }
        catch (Win32Exception exception)
        {
            PackageOperationStatus = $"Could not start winget: {exception.Message}";
        }
        catch (InvalidOperationException exception)
        {
            PackageOperationStatus = exception.Message;
        }
        finally
        {
            IsPackageOperationInProgress = false;
        }
    }

    private void UpdateCanStartPackageAction()
    {
        CanStartPackageAction = !IsPackageOperationInProgress && SelectedApp?.CanManageWithWinget == true;
    }

    public void ApplyFilters()
    {
        var filtered = _allCatalogItems
            .Where(item =>
                (string.IsNullOrWhiteSpace(SearchText) ||
                 item.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                 item.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                 item.Publisher.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) &&
                (SelectedCategory == "All" || string.Equals(item.Category, SelectedCategory, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        VisibleCatalog.Clear();
        foreach (var item in filtered)
        {
            VisibleCatalog.Add(item);
        }

        StoreSummary = filtered.Count == 1
            ? "1 result"
            : $"{filtered.Count} results";
        HasCatalogResults = filtered.Count > 0;

        if (VisibleCatalog.Count > 0 && (SelectedApp is null || !VisibleCatalog.Contains(SelectedApp)))
        {
            SelectedApp = VisibleCatalog[0];
        }
        else if (VisibleCatalog.Count == 0)
        {
            SelectedApp = null;
        }
    }
}
