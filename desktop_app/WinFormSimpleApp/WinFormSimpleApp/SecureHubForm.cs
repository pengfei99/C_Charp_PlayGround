namespace WinFormSimpleApp;

public class SecureHubForm : Form
{
    private Dictionary<string, Dictionary<string, string>> _icons;
    private Label _outputLabel;

    public SecureHubForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        // Form setup
        Text = "CASD Secure Hub";
        Size = new Size(800, 600);
        StartPosition = FormStartPosition.CenterScreen;

        // Main layout panel
        var mainPanel = new Panel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true
        };

        // Logo
        var logoPictureBox = new PictureBox
        {
            SizeMode = PictureBoxSizeMode.Zoom,
            Dock = DockStyle.Top,
            Size = new Size(600, 120),
        };

        try
        {
            logoPictureBox.Image = Image.FromFile(GetIconPath("casd.png"));
        }
        catch (Exception ex)
        { 
            MessageBox.Show($"Error loading logo: {ex.Message}");
            logoPictureBox.BackColor = Color.LightGray;
            logoPictureBox.Image = null;
        }

        // Welcome Label
        var welcomeLabel = new Label
        {
            Text = "Welcome to the CASD Secure HUB",
            Font = new Font("Arial",  16, FontStyle.Bold),
            ForeColor = Color.Gray,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top
        };

        // Icons configuration
        _icons = new Dictionary<string, Dictionary<string, string>>
        {
            {
                "icon1",
                new Dictionary<string, string> { { "path", GetIconPath("server.png") }, { "text", "Server_1" } }
            },
            {
                "icon2",
                new Dictionary<string, string> { { "path", GetIconPath("server.png") }, { "text", "Server_2" } }
            },
            {
                "icon3",
                new Dictionary<string, string> { { "path", GetIconPath("server.png") }, { "text", "Server_3" } }
            },
            {
                "icon4",
                new Dictionary<string, string> { { "path", GetIconPath("server.png") }, { "text", "Server_4" } }
            }
        };

        // Icons panel
        var iconsPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.LeftToRight,
            Dock = DockStyle.Top,
            WrapContents = true
        };

        // Create clickable widgets
        foreach (var iconEntry in _icons)
        {
            var clickablePanel = new ClickablePanel(
                iconEntry.Value["path"],
                iconEntry.Value["text"],
                () => ShowServerText(iconEntry.Key)
            );
            iconsPanel.Controls.Add(clickablePanel);
        }

        // Output Label
        _outputLabel = new Label
        {
            Text = "Select the server which you want to connect",
            Font = new Font("Arial", 12),
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top
        };

        // Assemble components
       // mainPanel.Controls.Add(_outputLabel);
       //  mainPanel.Controls.Add(iconsPanel);
        mainPanel.Controls.Add(welcomeLabel);
        mainPanel.Controls.Add(logoPictureBox);

        Controls.Add(mainPanel);
    }

    private void ShowServerText(string iconKey)
    {
        var serverId = _icons[iconKey]["text"];
        _outputLabel.Text = $"Connect to server: {serverId}";
    }

    private string GetIconPath(string filename)
    {
        // In a real application, replace this with your actual path resolution logic
        return Path.Combine("C:\\Users\\PLIU\\Documents\\git\\C_Charp_PlayGround\\desktop_app\\WinFormSimpleApp\\WinFormSimpleApp\\assets", filename);
    }
}