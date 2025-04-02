using System.ComponentModel;

namespace WpfReactiveuiApp;

public partial class MainWindowViewModel : Component
{
    public MainWindowViewModel()
    {
        InitializeComponent();
    }

    public MainWindowViewModel(IContainer container)
    {
        container.Add(this);

        InitializeComponent();
    }
}