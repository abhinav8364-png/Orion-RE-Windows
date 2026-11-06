using System.IO;
using System.Text.Json;
using Orion.Windows.Models;

namespace Orion.Windows;

public interface ICatalogRepository
{
    IReadOnlyList<AppCatalogEntry> LoadCatalog();
    AppCatalogEntry ParseCatalogEntry(string json);
}

public sealed class CatalogRepository : ICatalogRepository
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public IReadOnlyList<AppCatalogEntry> LoadCatalog()
    {
        var catalog = new List<AppCatalogEntry>
        {
            new AppCatalogEntry
            {
                Name = "Orion Store",
                PackageId = "com.orion.store",
                Version = "2.0.0",
                Publisher = "RookieEnough",
                Category = "Utility",
                Rating = "4.9",
                FileSizeMb = 34,
                InstallType = "MSIX",
                AccentColor = "#5B5CEB",
                Description = "Native storefront client with a secure install flow and transparent metadata.",
                IsFeatured = true,
                Source = "Official" 
            },
            new AppCatalogEntry
            {
                Name = "Aurora Reader",
                PackageId = "com.aurora.reader",
                Version = "3.1.2",
                Publisher = "Northwind Labs",
                Category = "Productivity",
                Rating = "4.7",
                FileSizeMb = 58,
                InstallType = "EXE",
                AccentColor = "#27C7A3",
                Description = "Fast PDF and document reader designed for efficient note-taking and markup.",
                IsFeatured = true,
                Source = "Mirror A"
            },
            new AppCatalogEntry
            {
                Name = "Motion Studio",
                PackageId = "com.motion.studio",
                Version = "1.8.4",
                Publisher = "Luma Forge",
                Category = "Creative",
                Rating = "4.8",
                FileSizeMb = 92,
                InstallType = "MSIX",
                AccentColor = "#FF8C42",
                Description = "Edit videos and create cinematic overlays with device-friendly templates.",
                Source = "Mirror B"
            },
            new AppCatalogEntry
            {
                Name = "Terminus Pro",
                PackageId = "com.terminus.pro",
                Version = "2.4.0",
                Publisher = "Open Systems",
                Category = "Developer",
                Rating = "4.9",
                FileSizeMb = 28,
                InstallType = "ZIP",
                AccentColor = "#A3E635",
                Description = "Modern terminal, SSH, and local automation environment built for power users.",
                IsFeatured = true,
                Source = "Mirror C"
            },
            new AppCatalogEntry
            {
                Name = "Cryptid Vault",
                PackageId = "com.cryptid.vault",
                Version = "5.2.1",
                Publisher = "Cipher Works",
                Category = "Security",
                Rating = "4.6",
                FileSizeMb = 46,
                InstallType = "MSIX",
                AccentColor = "#4CC9F0",
                Description = "Encrypted storage and credential management designed for privacy-focused users.",
                Source = "Mirror D"
            },
            new AppCatalogEntry
            {
                Name = "Signal Peak",
                PackageId = "com.signal.peak",
                Version = "1.0.9",
                Publisher = "Drift Studio",
                Category = "Audio",
                Rating = "4.5",
                FileSizeMb = 65,
                InstallType = "EXE",
                AccentColor = "#FF5D8F",
                Description = "Compose, master, and produce clean audio mixes with a focused desktop workflow.",
                Source = "Mirror E"
            },
            new AppCatalogEntry
            {
                Name = "Atlas Maps",
                PackageId = "com.atlas.maps",
                Version = "8.0.3",
                Publisher = "Northbound",
                Category = "Travel",
                Rating = "4.7",
                FileSizeMb = 71,
                InstallType = "MSIX",
                AccentColor = "#5AA9E6",
                Description = "Interactive planning and route discovery for local and cross-country adventures.",
                Source = "Mirror F"
            },
            new AppCatalogEntry
            {
                Name = "Pixel Forge",
                PackageId = "com.pixel.forge",
                Version = "2.7.1",
                Publisher = "Studio Kilo",
                Category = "Creative",
                Rating = "4.8",
                FileSizeMb = 120,
                InstallType = "EXE",
                AccentColor = "#F5D76E",
                Description = "Graphics toolset for image design, icon generation, and publishing-ready output.",
                IsFeatured = true,
                Source = "Mirror G"
            }
        };

        catalog.AddRange(
        [
            CreateCatalogPreview("7-Zip", "7zip.7zip", "Igor Pavlov", "Utilities", "File archiver and file manager supporting a wide range of archive formats."),
            CreateCatalogPreview("Mozilla Firefox", "Mozilla.Firefox", "Mozilla", "Browsers", "Privacy-focused web browser with extensions, tab management, and cross-device sync.", true),
            CreateCatalogPreview("Google Chrome", "Google.Chrome", "Google", "Browsers", "Popular web browser with profile sync, extensions, and built-in security features."),
            CreateCatalogPreview("Brave Browser", "Brave.Brave", "Brave Software", "Browsers", "Chromium-based browser with privacy controls, tracking protection, and extension support."),
            CreateCatalogPreview("Vivaldi", "Vivaldi.Vivaldi", "Vivaldi Technologies", "Browsers", "Customizable browser with tab workspaces, built-in tools, and flexible layouts."),
            CreateCatalogPreview("Visual Studio Code", "Microsoft.VisualStudioCode", "Microsoft", "Developer", "Extensible code editor with debugging, source control, and a large extension ecosystem.", true),
            CreateCatalogPreview("Git", "Git.Git", "The Git Development Community", "Developer", "Distributed version control system for tracking changes and collaborating on code."),
            CreateCatalogPreview("GitHub Desktop", "GitHub.GitHubDesktop", "GitHub", "Developer", "Desktop client for reviewing changes and managing GitHub repositories."),
            CreateCatalogPreview("Windows Terminal", "Microsoft.WindowsTerminal", "Microsoft", "Developer", "Tabbed terminal host for Command Prompt, PowerShell, and other command-line tools."),
            CreateCatalogPreview("PowerShell", "Microsoft.PowerShell", "Microsoft", "Developer", "Cross-platform command-line shell and scripting language for automation."),
            CreateCatalogPreview("Python", "Python.Python.3.13", "Python Software Foundation", "Developer", "Python programming language runtime for scripting, data work, and application development."),
            CreateCatalogPreview("Node.js LTS", "OpenJS.NodeJS.LTS", "OpenJS Foundation", "Developer", "Long-term-support JavaScript runtime for building tools, services, and web applications."),
            CreateCatalogPreview("Docker Desktop", "Docker.DockerDesktop", "Docker", "Developer", "Desktop environment for building and running containerized applications."),
            CreateCatalogPreview("Notepad++", "Notepad++.Notepad++", "Don Ho", "Developer", "Lightweight source-code and text editor with syntax highlighting and plugins."),
            CreateCatalogPreview("JetBrains Toolbox", "JetBrains.Toolbox", "JetBrains", "Developer", "Install and manage JetBrains IDEs and their updates from one desktop application."),
            CreateCatalogPreview("Postman", "Postman.Postman", "Postman", "Developer", "API platform for creating requests, testing endpoints, and organizing API work."),
            CreateCatalogPreview("WinSCP", "WinSCP.WinSCP", "Martin Prikryl", "Developer", "Secure file transfer client supporting SFTP, SCP, FTP, and WebDAV."),
            CreateCatalogPreview("PuTTY", "PuTTY.PuTTY", "Simon Tatham", "Developer", "SSH and terminal client for connecting to remote systems."),
            CreateCatalogPreview("Microsoft PowerToys", "Microsoft.PowerToys", "Microsoft", "Utilities", "Windows utilities including FancyZones, PowerToys Run, and image resizing.", true),
            CreateCatalogPreview("Everything", "voidtools.Everything", "voidtools", "Utilities", "Fast desktop file-name search for locating files and folders on Windows."),
            CreateCatalogPreview("ShareX", "ShareX.ShareX", "ShareX Team", "Utilities", "Screen capture and workflow tool for screenshots, recording, and annotation."),
            CreateCatalogPreview("Rufus", "Rufus.Rufus", "Pete Batard", "Utilities", "Utility for creating bootable USB installation and recovery media."),
            CreateCatalogPreview("WizTree", "AntibodySoftware.WizTree", "Antibody Software", "Utilities", "Disk space analyzer that helps identify large files and folders."),
            CreateCatalogPreview("EarTrumpet", "File-New-Project.EarTrumpet", "File-New-Project", "Utilities", "Per-application audio volume controls and device switching for Windows."),
            CreateCatalogPreview("Greenshot", "Greenshot.Greenshot", "Greenshot", "Utilities", "Screenshot utility with capture, annotation, and export tools."),
            CreateCatalogPreview("VLC media player", "VideoLAN.VLC", "VideoLAN", "Media", "Open-source media player supporting many audio and video formats.", true),
            CreateCatalogPreview("Spotify", "Spotify.Spotify", "Spotify", "Media", "Music streaming desktop client with playlists, search, and playback controls."),
            CreateCatalogPreview("OBS Studio", "OBSProject.OBSStudio", "OBS Project", "Creative", "Open-source software for live streaming and video recording.", true),
            CreateCatalogPreview("Audacity", "Audacity.Audacity", "Audacity Team", "Creative", "Audio editor and recorder for multitrack editing and sound processing."),
            CreateCatalogPreview("Blender", "BlenderFoundation.Blender", "Blender Foundation", "Creative", "3D creation suite for modeling, animation, rendering, and compositing.", true),
            CreateCatalogPreview("GIMP", "GIMP.GIMP", "The GIMP Team", "Creative", "Image editor for photo retouching, illustration, and layered graphics."),
            CreateCatalogPreview("Krita", "KDE.Krita", "KDE", "Creative", "Digital painting application with brush engines, layers, and animation tools."),
            CreateCatalogPreview("Inkscape", "Inkscape.Inkscape", "Inkscape Project", "Creative", "Vector graphics editor for illustrations, diagrams, and scalable artwork."),
            CreateCatalogPreview("HandBrake", "HandBrake.HandBrake", "The HandBrake Team", "Creative", "Video transcoder for converting video files and preparing media for playback."),
            CreateCatalogPreview("Shotcut", "Meltytech.Shotcut", "Meltytech", "Creative", "Cross-platform video editor with timeline, filters, and format support."),
            CreateCatalogPreview("LibreOffice", "TheDocumentFoundation.LibreOffice", "The Document Foundation", "Productivity", "Office suite for documents, spreadsheets, presentations, and more.", true),
            CreateCatalogPreview("Obsidian", "Obsidian.Obsidian", "Obsidian", "Productivity", "Markdown note-taking app with local files, linking, and community plugins."),
            CreateCatalogPreview("Joplin", "Joplin.Joplin", "Joplin", "Productivity", "Note-taking and task app with notebooks, Markdown, and synchronization options."),
            CreateCatalogPreview("SumatraPDF", "SumatraPDF.SumatraPDF", "Krzysztof Kowalczyk", "Productivity", "Lightweight document reader for PDF, e-books, and other common formats."),
            CreateCatalogPreview("Calibre", "calibre.calibre", "Kovid Goyal", "Productivity", "E-book library manager with format conversion and device synchronization."),
            CreateCatalogPreview("Mozilla Thunderbird", "Mozilla.Thunderbird", "Mozilla", "Productivity", "Email and calendar client with accounts, contacts, and extension support."),
            CreateCatalogPreview("Notion", "Notion.Notion", "Notion Labs", "Productivity", "Workspace for notes, documents, project tracking, and shared knowledge."),
            CreateCatalogPreview("Bitwarden", "Bitwarden.Bitwarden", "Bitwarden", "Security", "Password manager for storing and organizing credentials across devices."),
            CreateCatalogPreview("KeePassXC", "KeePassXCTeam.KeePassXC", "KeePassXC Team", "Security", "Local password manager using encrypted database files."),
            CreateCatalogPreview("VeraCrypt", "IDRIX.VeraCrypt", "IDRIX", "Security", "Disk encryption utility for creating encrypted volumes and protecting data."),
            CreateCatalogPreview("Discord", "Discord.Discord", "Discord", "Communication", "Voice, video, and text chat client for communities and teams."),
            CreateCatalogPreview("Signal", "OpenWhisperSystems.Signal", "Signal Foundation", "Communication", "Private messaging and calling application from the Signal Foundation."),
            CreateCatalogPreview("Telegram Desktop", "Telegram.TelegramDesktop", "Telegram", "Communication", "Desktop messaging client with chats, groups, channels, and file sharing."),
            CreateCatalogPreview("Zoom Workplace", "Zoom.Zoom", "Zoom Communications", "Communication", "Video meeting client for joining calls, webinars, and team sessions."),
            CreateCatalogPreview("Slack", "SlackTechnologies.Slack", "Salesforce", "Communication", "Team messaging client with channels, direct messages, and integrations."),
            CreateCatalogPreview("Steam", "Valve.Steam", "Valve", "Games", "PC game library and client for purchasing, installing, and managing games.", true),
            CreateCatalogPreview("GOG GALAXY", "GOG.Galaxy", "GOG", "Games", "Game library client for managing games and connecting supported platforms."),
            CreateCatalogPreview("Epic Games Launcher", "EpicGames.EpicGamesLauncher", "Epic Games", "Games", "Game launcher and storefront for managing Epic Games titles."),
            CreateCatalogPreview("Heroic Games Launcher", "HeroicGamesLauncher.HeroicGamesLauncher", "Heroic Games", "Games", "Open-source game launcher for supported Epic Games and GOG libraries."),
            CreateCatalogPreview("itch", "ItchIo.Itch", "itch.io", "Games", "Desktop client for discovering and managing games from itch.io.")
        ]);

        catalog.AddRange(LoadOpenSourceCatalog());
        return catalog;
    }

    private static IReadOnlyList<AppCatalogEntry> LoadOpenSourceCatalog()
    {
        using var stream = typeof(CatalogRepository).Assembly.GetManifestResourceStream("Orion.Windows.Data.FossCatalog.tsv")
            ?? throw new InvalidOperationException("The verified open-source catalog resource is missing.");
        using var reader = new StreamReader(stream);
        var catalog = new List<AppCatalogEntry>();
        var lineNumber = 0;

        while (reader.ReadLine() is { } line)
        {
            lineNumber++;
            var fields = line.Split('\t');
            if (fields.Length != 6 || fields.Any(string.IsNullOrWhiteSpace))
            {
                throw new InvalidDataException($"Open-source catalog row {lineNumber} is malformed.");
            }

            catalog.Add(new AppCatalogEntry
            {
                Name = fields[0],
                PackageId = fields[1],
                Publisher = fields[2],
                Category = fields[3],
                License = fields[4],
                Description = fields[5],
                Version = "Latest from winget",
                Rating = string.Empty,
                FileSizeMb = 0,
                InstallType = "winget",
                Source = "winget",
                CanManageWithWinget = true,
                IsFreeOpenSource = true
            });
        }

        return catalog;
    }

    private static AppCatalogEntry CreateCatalogPreview(
        string name,
        string packageId,
        string publisher,
        string category,
        string description,
        bool isFeatured = false)
    {
        return new AppCatalogEntry
        {
            Name = name,
            PackageId = packageId,
            Version = "Not listed",
            Publisher = publisher,
            Description = description,
            Category = category,
            Rating = "—",
            FileSizeMb = 0,
            InstallType = "winget",
            IsFeatured = isFeatured,
            Source = "winget",
            CanManageWithWinget = true
        };
    }

    public AppCatalogEntry ParseCatalogEntry(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new InvalidOperationException("Catalog entry payload was empty.");
        }

        var entry = JsonSerializer.Deserialize<AppCatalogEntry>(json, JsonOptions);
        if (entry is null)
        {
            throw new JsonException("Catalog entry could not be deserialized.");
        }

        if (string.IsNullOrWhiteSpace(entry.Name) ||
            string.IsNullOrWhiteSpace(entry.PackageId) ||
            string.IsNullOrWhiteSpace(entry.Version))
        {
            throw new InvalidOperationException("Catalog entry is missing required metadata.");
        }

        return entry;
    }
}
