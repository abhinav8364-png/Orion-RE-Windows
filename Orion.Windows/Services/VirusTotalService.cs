namespace Orion.Windows;

public interface IVirusTotalService
{
    Task<string> SubmitHashAsync(string sha256Hash, CancellationToken cancellationToken = default);
}

public sealed class VirusTotalService : IVirusTotalService
{
    public Task<string> SubmitHashAsync(string sha256Hash, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(sha256Hash))
        {
            throw new ArgumentException("A SHA-256 hash is required.", nameof(sha256Hash));
        }

        return Task.FromResult($"hash {sha256Hash} queued for user-approved submission");
    }
}
