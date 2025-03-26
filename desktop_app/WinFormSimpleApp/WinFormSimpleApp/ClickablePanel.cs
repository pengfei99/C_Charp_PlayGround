namespace WinFormSimpleApp;

public class ClickablePanel : Panel
    {
        private readonly string _iconPath;
        private readonly string _text;
        private readonly Action _callback;
        private readonly PictureBox _iconPictureBox;
        private readonly Label _textLabel;

        public ClickablePanel(string iconPath, string text, Action callback)
        {
            // Panel setup
            Size = new Size(120, 120);
            BorderStyle = BorderStyle.None;

            // Default styles
            BackColor = Color.White;

            // Create layout
            var layout = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            // Icon
            _iconPictureBox = new PictureBox
            {
                Image = Image.FromFile(iconPath),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(50, 50),
                Anchor = AnchorStyles.Top
            };

            // Text
            _textLabel = new Label
            {
                Text = text,
                Font = new Font("Arial", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom
            };

            // Add to layout
            layout.Controls.Add(_iconPictureBox);
            layout.Controls.Add(_textLabel);
            Controls.Add(layout);

            // Event handlers
            _iconPath = iconPath;
            _text = text;
            _callback = callback;

            MouseEnter += OnMouseEnter;
            MouseLeave += OnMouseLeave;
            Click += OnClick;
            _iconPictureBox.Click += OnClick;
            _textLabel.Click += OnClick;

            Cursor = Cursors.Hand;
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(224, 224, 224);
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.White;
        }

        private void OnClick(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(179, 229, 252);
            _callback?.Invoke();
        }
    }