

using System.Runtime.InteropServices;
using CommunityToolkit.WinUI.Notifications;

[ClassInterface(ClassInterfaceType.None)]
[ComSourceInterfaces(typeof(INotificationActivationCallback))]
[Guid("77E9B49F-A2E6-42D5-A7D4-B99CDBFB15A3"), ComVisible(true)]


public class NotificationActivator : NotificationActivator
{
    public override void OnActivated(string arguments, NotificationUserInput userInput, string appUserModelId)
    {
        // Optional: handle activation (e.g., bring window to front)
    }
}
