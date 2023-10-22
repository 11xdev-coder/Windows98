using System.Drawing.Text;

namespace Windows98;

public class SetupPrepareHelp : Form
{
    private PrivateFontCollection _privateFonts;
    private int _currentStep;

    public SetupPrepareHelp(int step)
    {
        _currentStep = step;
        LoadFont();

        // Customize the appearance
        this.BackColor = Colors.BiosGray;
        this.WindowState = FormWindowState.Maximized;
        this.FormBorderStyle = FormBorderStyle.None;
        this.TopMost = true;

        UpdateStep();
    }

    private void UpdateStep()
    {
        Controls.Clear();
        Controls.Add(Extensions.ShowControlsPanel(true, false, Colors.BiosBlue, _privateFonts));
        Controls.Add(Extensions.AddCustomLabelText("Setup Help" + 
                                                   "\n==========", 32f, new Point(10, 50), Colors.BiosBlue, _privateFonts));
        
        switch (_currentStep)
        {
            case 1:
                Controls.Add(Extensions.AddCustomLabelText("Using Setup", 32f, new Point(50, 150), Color.Black, _privateFonts));
                Controls.Add(Extensions.AddCustomLabelText("Setup installs Windows 98 on your computer's hard disk." +
                                                           "\nThe Setup program:" +
                                                           "\n" + 
                                                           "\n  - Identifies your computer's components (display," +
                                                           "\n  keyboard, mouse, network, and so on)." +
                                                           "\n" +
                                                           "\n  - Configures your hard disk, if necessary, so that" +
                                                           "\n  Windows can use it." +
                                                           "\n" +
                                                           "\nCDR100: Unknown error" +
                                                           "\nCDR101: Not ready" +
                                                           "\nCDR102: EMS memory no longer valid" +
                                                           "\nCDR103: CDROM not High Sierra or ISO-9660 format" +
                                                           "\nCDR104: Door open" +
                                                           "\nSometimes Setup displays options in a box. To choose the" +
                                                           "\nhighlighted option, press ENTER. To choose a different" +
                                                           "\noption, use the ARROW keys to select the option you want," +
                                                           "\nand then press ENTER.", 32f, new Point(50, 250), Color.Black, _privateFonts));
                break;
            case 2:
                Controls.Add(Extensions.AddCustomLabelText("Configuring the Space on Your Hard Disk" +
                                                           "\n" +
                                                           "\nSetup cannot continue unless a portion of your hard disk is" +
                                                           "\nconfigured so that Windows can use it." +
                                                           "\n" +
                                                           "\nTo have Setup configure this space, choose the recommended" +
                                                           "\noption, 'Configure unallocated disk space.'" +
                                                           "\n" +
                                                           "\nIf you choose the recommended option, Setup will prepare the" +
                                                           "\nlargest unallocated area of your disk for use with Windows." +
                                                           "\nNone of your existing files will be affected." +
                                                           "\n" +
                                                           "\nIf you have other hard disks with unallocated space, Setup" +
                                                           "\nconfigures the largest unallocated space on each one." +
                                                           "\n" +
                                                           "\nNote: Setup configures only the largest space on each disk," +
                                                           "\nand leaves any smaller spaces unallocated.", 32f, new Point(50, 150), Color.Black, _privateFonts));
                break;
            case 3:
                break;
        }
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

        switch (e.KeyCode)
        {
            case Keys.F3:
                ShowConfirmationWindow();
                break;
            case Keys.Escape:
                Close();
                break;
        }
    }
    
    private void ShowConfirmationWindow()
    {
        SetupPrepareConfirmExit confirmationWindow = new SetupPrepareConfirmExit(true);
        confirmationWindow.ShowDialog(this);
        confirmationWindow.BringToFront(); 

        if (confirmationWindow.IsConfirmed)
        {
            Application.Exit();
        }
    }
}