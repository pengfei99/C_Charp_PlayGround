using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormSimpleApp
{
    public class ClickablePanel : Panel
    {
        private readonly string _iconPath;
        private readonly string _text;
        private readonly Action _callback;
        private readonly PictureBox _iconPictureBox;
        private readonly Label _textLabel;
        private readonly TableLayoutPanel _tableLayout;

        // Add customization properties
        public Color HoverColor { get; } = Color.FromArgb(224, 224, 224);
        public Color ClickColor { get;} = Color.FromArgb(179, 229, 252);
        public Color DefaultColor { get;} = Color.White;
        public Font TextFont { get;}

        public ClickablePanel(string iconPath, string text, Action callback)
        {
            // Store parameters
            _iconPath = iconPath;
            _text = text;
            _callback = callback;

            // Panel setup
            Size = new Size(120, 120);
            BorderStyle = BorderStyle.None;
            BackColor = DefaultColor;
            Padding = new Padding(5);

            // Use TableLayoutPanel for better centering
            _tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            // Set row and column styles for proper centering
            _tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F)); // Icon takes 70% of height
            _tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F)); // Text takes 30% of height

            // Set default font
            TextFont = new Font("Segoe UI", 9F, FontStyle.Regular);

            // Create and configure the icon
            _iconPictureBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                Margin = new Padding(10, 5, 10, 0)
            };
            
            // Safely load the image
            try
            {
                _iconPictureBox.Image = Image.FromFile(iconPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading image: {ex.Message}");
                // You could set a default/placeholder image here
            }

            // Create and configure the text label
            _textLabel = new Label
            {
                Text = text,
                Font = TextFont,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                AutoEllipsis = true,
                Margin = new Padding(3, 0, 3, 5)
            };

            // Add controls to the table layout
            _tableLayout.Controls.Add(_iconPictureBox, 0, 0);
            _tableLayout.Controls.Add(_textLabel, 0, 1);
            Controls.Add(_tableLayout);

            // Add event handlers
            MouseEnter += OnMouseEnter;
            MouseLeave += OnMouseLeave;
            Click += OnClick;
            _iconPictureBox.Click += OnClick;
            _textLabel.Click += OnClick;

            // Set cursor
            Cursor = Cursors.Hand;
        }

        // Property to update text
        public string PanelText
        {
            get => _textLabel.Text;
        }

        // Method to update the icon
        public void UpdateIcon(string newIconPath)
        {
            try
            {
                _iconPictureBox.Image?.Dispose(); // Dispose old image
                _iconPictureBox.Image = Image.FromFile(newIconPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating icon: {ex.Message}");
            }
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            BackColor = HoverColor;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            BackColor = DefaultColor;
        }

        private void OnClick(object sender, EventArgs e)
        {
            BackColor = ClickColor;
            _callback?.Invoke();
            
        }

        // Clean up resources
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _iconPictureBox.Image?.Dispose();
                TextFont?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}