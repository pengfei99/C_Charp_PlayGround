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
        var mainPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoScroll = true,
            ColumnCount = 1,
            RowCount = 2,
            // global padding between all items
            Padding = new Padding(1)
        };

        // Configure rows - this is key for proper spacing
        mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Welcome label
        mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Logo
        mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

        // Logo
        var logoPictureBox = new PictureBox
        {
            SizeMode = PictureBoxSizeMode.Zoom,
            Dock = DockStyle.Fill,
            Size = new Size(600, 120),
            //Margin = new Padding(50, 10, 50, 10)
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
            Font = new Font("Arial", 16, FontStyle.Bold),
            ForeColor = Color.Gray,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill,
            AutoSize = false,
            // Height = 40,
            // Add space below the label
            //Margin = new Padding(0, 5, 0, 10) 
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
        var iconsPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Height = 150,
            AutoScroll = true,
            ColumnCount = 6,
            RowCount = 1,
            // global padding between all items
            Padding = new Padding(1)
        };
        // Clear any existing styles
        iconsPanel.ColumnStyles.Clear();

// Add spacer column at the beginning (left side)
        iconsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

// Add the 4 content columns with fixed width
        for (int i = 0; i < 4; i++)
        {
            iconsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        }

// Add spacer column at the end (right side)
        iconsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

// Table cell alignment
        iconsPanel.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;

        int column = 1;
        // Create clickable widgets
        foreach (var iconEntry in _icons)
        {
            var clickablePanel = new ClickablePanel(
                iconEntry.Value["path"],
                iconEntry.Value["text"],
                () => ShowServerText(iconEntry.Key)
            );
            // Set a consistent size for each panel
            clickablePanel.Size = new Size(120, 120);
    
            // Add with anchor in the center
            clickablePanel.Anchor = AnchorStyles.None;
            iconsPanel.Controls.Add(clickablePanel, column, 0);
            column++;
        }

        // Output Label
        _outputLabel = new Label
        {
            Text = "Select the server which you want to connect",
            Font = new Font("Arial", 12),
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill
        };

        // Assemble components
        mainPanel.Controls.Add(logoPictureBox, 0, 0);
        mainPanel.Controls.Add(welcomeLabel, 0, 1);
        mainPanel.Controls.Add(iconsPanel, 0, 2);
        mainPanel.Controls.Add(_outputLabel, 0, 3);

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
        return Path.Combine(
            "C:\\Users\\PLIU\\Documents\\git\\C_Charp_PlayGround\\desktop_app\\WinFormSimpleApp\\WinFormSimpleApp\\assets",
            filename);
    }
}