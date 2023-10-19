using System.Drawing.Text;

namespace Windows98;

public class SetupPrepareConfirmExit : Form
{
    public bool IsConfirmed { get; private set; }
    private PrivateFontCollection _privateFonts;

    public SetupPrepareConfirmExit()
    {
        LoadFont();
        // Customize the appearance
        this.FormBorderStyle = FormBorderStyle.None;
        this.Size = new Size(300, 200);  // Set your desired size
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.Gray;
        this.TopMost = true; 

        Label label = new Label
        {
            Text = "Press F3 again to exit, Esc to cancel.",
            Location = new Point(10, 10),
            AutoSize = true
        };
        this.Controls.Add(label);
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
}