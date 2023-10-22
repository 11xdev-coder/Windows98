using System.Drawing.Text;

namespace Windows98;

public class SetupPrepareConfirmExit : Form
{
    public bool IsConfirmed { get; private set; }
    private PrivateFontCollection _privateFonts;
    private Color _borderColor;

    public SetupPrepareConfirmExit(bool invertColors)
    {
        LoadFont();

        // Customize the appearance
        this.FormBorderStyle = FormBorderStyle.None;
        this.Size = new Size(1500, 600);  // Set your desired size
        this.StartPosition = FormStartPosition.CenterScreen;
    
        Color backgroundColor, textColor;

        if (invertColors)
        {
            backgroundColor = Color.Black;
            textColor = Colors.BiosGray;
            _borderColor = Colors.BiosGray; // Set border color for inverted colors
        }
        else
        {
            backgroundColor = Colors.BiosGray;
            textColor = Color.Black;
            _borderColor = Color.Black; // Set border color for regular colors
        }

        this.BackColor = backgroundColor;
        this.TopMost = true;

        Controls.Add(Extensions.AddCustomLabelText("Exiting Setup", 32f, new Point(580, 5), textColor, _privateFonts));
    
        // Add the confirmation message
        Controls.Add(Extensions.AddCustomLabelText("Windows 98 is not completely installed. If you quit" +
                                                   "\nSetup now, you will need to run the Setup program" +
                                                   "\nagain.", 32f, new Point(50, 100), textColor, _privateFonts));

        Controls.Add(Extensions.AddCustomLabelText("- To quit Setup, press F3.", 32f, new Point(50, 300), textColor, _privateFonts));

        Controls.Add(Extensions.AddCustomLabelText("- To return to the previous screen, press ESC.", 32f, new Point(50, 350), textColor, _privateFonts));
    }

    
    private void LoadFont()
    {
        _privateFonts = new PrivateFontCollection();
        var fontPath = Path.Combine(Application.StartupPath, "Fonts", "Perfect DOS VGA 437 Win.ttf");
        _privateFonts.AddFontFile(fontPath);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.KeyCode == Keys.F3)
        {
            IsConfirmed = true;
            this.Close();
        }
        else if (e.KeyCode == Keys.Escape)
        {
            IsConfirmed = false;
            this.Close();
        }
    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        int borderWidth = 5;
        Color borderColor = _borderColor;

        int margin = 20;

        // Calculate the gap for the "Exiting Setup" text
        SizeF textSize = e.Graphics.MeasureString("Exiting Setup", new Font(_privateFonts.Families[0], 24f));
        int textWidth = (int)textSize.Width;
        int textGap = textWidth + 80;  // Additional 40 pixels to ensure space around the text

        // Draw the two segments of the top border
        e.Graphics.FillRectangle(new SolidBrush(borderColor), margin, margin, (this.Width - 2 * margin - textGap) / 2, borderWidth);
        e.Graphics.FillRectangle(new SolidBrush(borderColor), this.Width / 2 + textGap / 2, margin, (this.Width - 2 * margin - textGap) / 2, borderWidth);

        // Draw left border
        e.Graphics.FillRectangle(new SolidBrush(borderColor), margin, margin, borderWidth, this.Height - 2 * margin);
        // Draw right border
        e.Graphics.FillRectangle(new SolidBrush(borderColor), this.Width - borderWidth - margin, margin, borderWidth, this.Height - 2 * margin);
        // Draw bottom border
        e.Graphics.FillRectangle(new SolidBrush(borderColor), margin, this.Height - borderWidth - margin, this.Width - 2 * margin, borderWidth);

        // Draw the corners (you can customize the size/shape of these as needed)
        int cornerSize = 15;
        e.Graphics.FillRectangle(new SolidBrush(borderColor), margin, margin, cornerSize, borderWidth);
        e.Graphics.FillRectangle(new SolidBrush(borderColor), margin, margin, borderWidth, cornerSize);
        e.Graphics.FillRectangle(new SolidBrush(borderColor), this.Width - cornerSize - margin, margin, cornerSize, borderWidth);
        e.Graphics.FillRectangle(new SolidBrush(borderColor), this.Width - borderWidth - margin, margin, borderWidth, cornerSize);
        e.Graphics.FillRectangle(new SolidBrush(borderColor), margin, this.Height - cornerSize - margin, borderWidth, cornerSize);
        e.Graphics.FillRectangle(new SolidBrush(borderColor), margin, this.Height - borderWidth - margin, cornerSize, borderWidth);
        e.Graphics.FillRectangle(new SolidBrush(borderColor), this.Width - borderWidth - margin, this.Height - cornerSize - margin, borderWidth, cornerSize);
        e.Graphics.FillRectangle(new SolidBrush(borderColor), this.Width - cornerSize - margin, this.Height - borderWidth - margin, cornerSize, borderWidth);
    }
}