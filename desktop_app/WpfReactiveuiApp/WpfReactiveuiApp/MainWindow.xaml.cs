using System;
using System.Reactive;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ReactiveUI;
using Splat;
using System.Reactive.Linq;

namespace WpfReactiveuiApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
 public partial class MainWindow : Window
    {
        // ReactiveUI viewmodel implementation
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register(nameof(ViewModel), typeof(MainWindowViewModel), typeof(MainWindow));
        
        public MainWindowViewModel ViewModel
        {
            get => (MainWindowViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MainWindowViewModel)value;
        }
        
        public MainWindow()
        {
            // Create and set the view model
            ViewModel = new MainWindowViewModel();
            
            // Set data context for binding
            DataContext = ViewModel;
            
            InitializeComponents();
            BindCommands();
        }
        
        private void InitializeComponents()
        {
            // Configure the window
            Title = "Icon Clicker App";
            Width = 600;
            Height = 400;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            // Create the main grid layout
            var mainGrid = new Grid();
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            
            // Create the icon panel (top section)
            var iconPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            
            // Add the four icon buttons
            iconPanel.Children.Add(CreateIconButton("Home", "🏠"));
            iconPanel.Children.Add(CreateIconButton("Settings", "⚙️"));
            iconPanel.Children.Add(CreateIconButton("User", "👤"));
            iconPanel.Children.Add(CreateIconButton("Search", "🔍"));
            
            // Create the output panel (bottom section)
            var outputPanel = new Border
            {
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(10),
                Padding = new Thickness(10)
            };
            
            var outputTextBlock = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap
            };
            
            // Bind the output text
            outputTextBlock.SetBinding(TextBlock.TextProperty, 
                new System.Windows.Data.Binding("OutputText"));
            
            outputPanel.Child = outputTextBlock;
            
            // Add both panels to the main grid
            Grid.SetRow(iconPanel, 0);
            Grid.SetRow(outputPanel, 1);
            mainGrid.Children.Add(iconPanel);
            mainGrid.Children.Add(outputPanel);
            
            // Set the content of the window
            Content = mainGrid;
        }
        
        private UIElement CreateIconButton(string text, string iconText)
        {
            // Create a container for the icon and its label
            var container = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(15),
                Width = 80
            };
            
            // Create the clickable button with the icon
            var button = new Button
            {
                Content = new TextBlock
                {
                    Text = iconText,
                    FontSize = 36,
                    HorizontalAlignment = HorizontalAlignment.Center
                },
                Height = 70,
                Width = 70,
                Margin = new Thickness(5),
                Tag = text // Store the text as a tag for the command
            };
            
            // Create the label below the icon
            var label = new TextBlock
            {
                Text = text,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 5, 0, 0)
            };
            
            // Add the button and label to the container
            container.Children.Add(button);
            container.Children.Add(label);
            
            return container;
        }
        
        private void BindCommands()
        {
            // Find all buttons and bind them to the command
            var mainGrid = (Grid)Content;
            var iconPanel = (StackPanel)mainGrid.Children[0];
            
            foreach (StackPanel container in iconPanel.Children)
            {
                var button = (Button)container.Children[0];
                var text = (string)button.Tag;
                
                // Bind the button click to the command
                button.Events().Click
                    .Select(_ => text) // Extract the tag (text) from the button
                    .InvokeCommand(this, x => x.ViewModel.IconClickCommand);
            }
        }
    }
    