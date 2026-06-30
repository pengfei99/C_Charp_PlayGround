using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp1;

public enum NotificationType
{
    Information,
    Success,
    Warning,
    Error
}
public class NotificationManager
{
    private readonly Queue<ToastNotification> _notificationQueue = new Queue<ToastNotification>();
    private bool _isDisplayingNotification = false;
    private readonly object _lockObject = new object();

    public void Show(string title, string message, NotificationType type, TimeSpan duration)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            var notification = new ToastNotification(title, message, type, duration, CloseNotification);
                
            lock (_lockObject)
            {
                _notificationQueue.Enqueue(notification);
                    
                if (!_isDisplayingNotification)
                {
                    DisplayNextNotification();
                }
            }
        });
    }

    private void DisplayNextNotification()
    {
        lock (_lockObject)
        {
            if (_notificationQueue.Count > 0)
            {
                _isDisplayingNotification = true;
                var notification = _notificationQueue.Dequeue();
                notification.Show();
            }
            else
            {
                _isDisplayingNotification = false;
            }
        }
    }

    private void CloseNotification(ToastNotification notification)
    {
        notification.Close();
            
        lock (_lockObject)
        {
            DisplayNextNotification();
        }
    }
}
}