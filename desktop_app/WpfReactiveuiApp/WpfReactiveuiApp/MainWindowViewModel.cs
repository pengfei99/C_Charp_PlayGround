using System.ComponentModel;
using System.Reactive;
using ReactiveUI;

namespace WpfReactiveuiApp;

// Main window view model
public class MainWindowViewModel : ReactiveObject
{
    private string _outputText;
        
    // Property for the output text panel
    public string OutputText
    {
        get => _outputText;
        set => this.RaiseAndSetIfChanged(ref _outputText, value);
    }
        
    // Commands for each icon button
    public ReactiveCommand<string, Unit> IconClickCommand { get; }
        
    public MainWindowViewModel()
    {
        // Initialize with empty output
        _outputText = "Output will appear here...";
            
        // Create command that takes a string parameter (the text to display)
        IconClickCommand = ReactiveCommand.Create<string>(text => 
        {
            OutputText = $"You clicked: {text}";
        });
    }
}