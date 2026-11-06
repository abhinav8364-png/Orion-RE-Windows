// Copyright (C) 2026 Abhinav Krishnan and contributors
// SPDX-License-Identifier: GPL-3.0-only
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Orion.Windows.Models;

namespace Orion.Windows;

public interface IDownloadService
{
    Task DownloadAsync(AppPackage package, string destination, IProgress<double>? progress = null, CancellationToken cancellationToken = default);
}

public sealed class DownloadService : IDownloadService
{
    private readonly HttpClient _httpClient;

    public DownloadService(HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? new HttpClient();
    }

    public async Task DownloadAsync(AppPackage package, string destination, IProgress<double>? progress = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(package);
        ArgumentException.ThrowIfNullOrWhiteSpace(destination);

        if (string.IsNullOrWhiteSpace(package.DownloadUrl))
        {
            throw new InvalidOperationException("The selected package has no download URL.");
        }

        var directory = Path.GetDirectoryName(destination);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using var response = await _httpClient.GetAsync(package.DownloadUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        await using var fileStream = new FileStream(destination, FileMode.Create, FileAccess.Write, FileShare.None);
        await using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

        var totalBytes = response.Content.Headers.ContentLength ?? 0;
        var bytesRead = 0L;
        var buffer = new byte[81920];

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var bytes = await contentStream.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken).ConfigureAwait(false);
            if (bytes == 0)
            {
                break;
            }

            await fileStream.WriteAsync(buffer.AsMemory(0, bytes), cancellationToken).ConfigureAwait(false);
            bytesRead += bytes;

            if (totalBytes > 0)
            {
                var percent = bytesRead / (double)totalBytes;
                progress?.Report(percent);
            }
        }

        progress?.Report(1.0);
    }
}
