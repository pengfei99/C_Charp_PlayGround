using System.Windows;
using CommunityToolkit.WinUI.Notifications; 

namespace WpfToastApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        new ToastContentBuilder()
            .AddText("WPF .NET 9 Notification")
            .AddText("Hello from a toast on Windows Server 2019!")
            .Show();
    }
}