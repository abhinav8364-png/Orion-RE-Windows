using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Orion.Windows.Models;

namespace Orion.Windows;

public interface IWingetPackageManager
{
    Task<int> InstallAsync(AppCatalogEntry app);
    Task<int> UpgradeAsync(AppCatalogEntry app);
}

public sealed class WingetPackageManager : IWingetPackageManager
{
    private static readonly Regex PackageIdPattern = new("^[A-Za-z0-9][A-Za-z0-9._-]*$", RegexOptions.CultureInvariant);

    public Task<int> InstallAsync(AppCatalogEntry app)
    {
        return RunAsync(app, "install");
    }

    public Task<int> UpgradeAsync(AppCatalogEntry app)
    {
        return RunAsync(app, "upgrade");
    }

    private static async Task<int> RunAsync(AppCatalogEntry app, string operation)
    {
        ArgumentNullException.ThrowIfNull(app);

        if (!app.CanManageWithWinget || !PackageIdPattern.IsMatch(app.PackageId))
        {
            throw new InvalidOperationException("This catalog entry is not enabled for winget installation.");
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = "winget.exe",
            UseShellExecute = true,
            WindowStyle = ProcessWindowStyle.Normal
        };
        startInfo.Arguments = $"{operation} --id {app.PackageId} --exact --source winget --interactive";

        using var process = Process.Start(startInfo)
            ?? throw new Win32Exception("Windows Package Manager could not be started.");
        await process.WaitForExitAsync();
        return process.ExitCode;
    }
}
