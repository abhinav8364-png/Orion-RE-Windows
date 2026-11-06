namespace Orion.Windows.Models;

public sealed class AppCatalogEntry
{
    public string Name { get; set; } = string.Empty;
    public string PackageId { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = "General";
    public string Rating { get; set; } = "4.8";
    public long FileSizeMb { get; set; } = 42;
    public string InstallType { get; set; } = "MSIX";
    public string AccentColor { get; set; } = "#5B5CEB";
    public bool IsFeatured { get; set; }
    public string Source { get; set; } = "Orion-RE mirror";
    public bool CanManageWithWinget { get; set; }
    public bool IsFreeOpenSource { get; set; }
    public string License { get; set; } = "Not listed";
    public string RatingDisplay => string.IsNullOrWhiteSpace(Rating) || Rating == "—"
        ? "Not rated"
        : $"{Rating} / 5";
    public string FileSizeDisplay => FileSizeMb > 0 ? $"{FileSizeMb} MB" : "Not listed";
    public string Monogram => string.Concat(
        Name.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Take(2)
            .Select(part => char.ToUpperInvariant(part[0])));
}
