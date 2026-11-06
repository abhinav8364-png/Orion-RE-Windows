// Copyright (C) 2026 Abhinav Krishnan and contributors
// SPDX-License-Identifier: GPL-3.0-only
namespace Orion.Windows;

public interface INotificationService
{
    void ShowInformation(string title, string message);
    void ShowWarning(string title, string message);
}

public sealed class NotificationService : INotificationService
{
    public void ShowInformation(string title, string message)
    {
        // Windows notification infrastructure will be wired in a later milestone.
    }

    public void ShowWarning(string title, string message)
    {
        // Windows notification infrastructure will be wired in a later milestone.
    }
}
