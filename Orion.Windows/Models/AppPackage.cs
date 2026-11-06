namespace Orion.Windows.Models;

public sealed class AppPackage
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public string Source { get; set; } = "Community mirror";
    public string InstallerType { get; set; } = "EXE";
    public long FileSizeBytes { get; set; } = 0L;
    public string? DownloadUrl { get; set; }
    public string? ChecksumSha256 { get; set; }
    public bool RequiresConfirmation { get; set; } = true;
}
