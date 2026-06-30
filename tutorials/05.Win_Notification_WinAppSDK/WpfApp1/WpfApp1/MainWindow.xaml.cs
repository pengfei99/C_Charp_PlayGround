using System;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

        private NotificationManager _notificationManager;

        public MainWindow()
        {
            InitializeComponent();
            _notificationManager = new NotificationManager();
        }

        private void ShowNotification_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            string message = MessageTextBox.Text;
            int duration = (int)DurationSlider.Value;
            
            NotificationType type = NotificationType.Information;
            
            switch (((ComboBoxItem)NotificationTypeComboBox.SelectedItem).Content.ToString())
            {
                case "Success":
                    type = NotificationType.Success;
                    break;
                case "Warning":
                    type = NotificationType.Warning;
                    break;
                case "Error":
                    type = NotificationType.Error;
                    break;
                default:
                    type = NotificationType.Information;
                    break;
            }
            
            _notificationManager.Show(title, message, type, TimeSpan.FromSeconds(duration));
        }
    }
