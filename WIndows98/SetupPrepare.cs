using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using Windows98;

namespace Windows98
{
    public class SetupPrepare : Form
    {
        private PrivateFontCollection _privateFonts;
        private int _currentStep = 1;
        private int _selectedIndex;
        private Color _selectedLabelColor = Colors.BiosGray;
        private List<Label> _labels = new List<Label>();

        public SetupPrepare()
        {
            Cursor.Hide();
            LoadFont();

            // Set the background color to blue
            this.BackColor = Colors.BiosBlue;

            // Set the form to run in full-screen mode
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            
            UpdateStep();
        }

        private void UpdateStep()
        {
            this.Controls.Clear();  // Clear existing controls
            _labels.Clear();

            switch (_currentStep)
            {
                case 1:
                    Controls.Add(Extensions.AddSetupLabelText("Microsoft Windows 98 Setup" +
                                                    "\n==========================", 32f, new Point(10, 10), _privateFonts));
            
                    Controls.Add(Extensions.AddSetupLabelText("Welcome to Setup.", 32f, new Point(100, 150), _privateFonts));
                    
                    Controls.Add(Extensions.AddSetupLabelText("The Setup program prepares Windows 98 to run on your" +
                                                              "\ncomputer.", 32f, new Point(100, 250), _privateFonts));
                    
                    Controls.Add(Extensions.AddSetupLabelText("- To set up Windows now, press ENTER.", 32f, new Point(150, 350), _privateFonts));
                    
                    Controls.Add(Extensions.AddSetupLabelText("- To learn more about Setup before continuing, press F1", 32f, new Point(150, 400), _privateFonts));
                    
                    Controls.Add(Extensions.AddSetupLabelText("- To quit Setup without installing Windows, press F3", 32f, new Point(150, 450), _privateFonts));
                    
                    Controls.Add(Extensions.AddSetupLabelText("Note: If you have not backed up your files recently, you" +
                                                              "\n      might want to do so before installing Windows. To back" +
                                                              "\n      up your files, press F3 to quit Setup now. Then, back" +
                                                              "\n      up your files by using Acronis.", 32f, new Point(100, 550), _privateFonts));
                    
                    Controls.Add(Extensions.AddSetupLabelText("To continue with Setup, press ENTER.", 32f, new Point(100, 800), _privateFonts));
                    
                    Controls.Add(Extensions.ShowControlsPanel(false, true, Colors.BiosGray, _privateFonts));
                    
                    break;
                case 2:
                    Controls.Add(Extensions.AddSetupLabelText("Microsoft Windows 98 Setup" +
                                                              "\n==========================", 32f, new Point(10, 10), _privateFonts));
                    
                    Controls.Add(Extensions.AddSetupLabelText("Setup needs to configure the unallocated space on your" +
                                                              "\nhard disk to prepare it for use with Windows. None of" +
                                                              "\nyour existing files will be affected.", 32f, new Point(100, 150), _privateFonts));
                    
                    Controls.Add(Extensions.AddSetupLabelText("To have Setup configure the space on your hard disk for you, " +
                                                              "\nchoose the recommended option.", 32f, new Point(100, 350), _privateFonts));
                    
                    Label configureLabel = Extensions.AddSetupLabelText("Configure unallocated disk space (recommended).",
                        32f, new Point(120, 550), _privateFonts);
                    _labels.Add(configureLabel);
                    Controls.Add(configureLabel);
                    
                    Label exitLabel = Extensions.AddSetupLabelText("Exit Setup.",
                        32f, new Point(120, 600), _privateFonts);
                    _labels.Add(exitLabel);
                    Controls.Add(exitLabel);
                    UpdateSelection();
                    
                    Controls.Add(Extensions.AddSetupLabelText("To accept the selection, press ENTER." +
                                                              "\nTo change the selection, press the UP or DOWN ARROW key," +
                                                              "\nand then press ENTER.", 32f, new Point(100, 800), _privateFonts));
                    
                    
                    Controls.Add(Extensions.ShowControlsPanel(false, false, Colors.BiosGray, _privateFonts));
                    break;
                case 3:
                    double availableSpace = Extensions.GetDiskSize();
                    bool isEnough = availableSpace >= 512;
                    if (!isEnough)  // Convert 512 MB to bytes
                    {
                        _currentStep++;
                        UpdateStep(); // proceed to the next step
                        break;
                    }
                    
                    Controls.Add(Extensions.AddSetupLabelText("Microsoft Windows 98 Setup" +
                                                              "\n==========================", 32f, new Point(10, 10), _privateFonts));
                    
                    Controls.Add(Extensions.AddSetupLabelText("You have a drive over 512MB in size. Would" +
                                                              "\nyou like to enable large disk support?", 32f, new Point(100, 150), _privateFonts));

                    Controls.Add(Extensions.AddSetupLabelText("This allows more efficient use of disk space" +
                                                              "\nand larger partitions to be defined.", 32f, new Point(100, 350), _privateFonts));
                    
                    Label noLargeDisk = Extensions.AddSetupLabelText("No, do not use large disk support",
                        32f, new Point(120, 450), _privateFonts);
                    _labels.Add(noLargeDisk);
                    Controls.Add(noLargeDisk);
                    
                    Label yesLargeDisk = Extensions.AddSetupLabelText("Yes, enable large disk support",
                        32f, new Point(120, 500), _privateFonts);
                    _labels.Add(yesLargeDisk);
                    Controls.Add(yesLargeDisk);
                    UpdateSelection();
                    
                    Controls.Add(Extensions.AddSetupLabelText("To accept the selection, press ENTER." +
                                                              "\nTo change the selection, press the UP or DOWN ARROW key," +
                                                              "\nand then press ENTER.", 32f, new Point(100, 800), _privateFonts));
                    
                    // TODO: ADD A TEXT FILE WITH largeDiskSupport = true/false
                    
                    Controls.Add(Extensions.ShowControlsPanel(false, false, Colors.BiosGray, _privateFonts));
                    
                    break;
                case 4:
                    Controls.Add(Extensions.AddSetupLabelText("Microsoft Windows 98 Setup" +
                                                              "\n==========================", 32f, new Point(10, 10), _privateFonts));

                    Panel cyanPanel = new Panel();
                    cyanPanel.Location = new Point(500, 400);
                    cyanPanel.Width = 1500;
                    cyanPanel.Height = 700;
                    cyanPanel.BackColor = Color.Teal;
                    
                    Controls.Add(cyanPanel);
                    break;
                default:
                    // Optionally handle completion or invalid step
                    break;
            }
        }

        private void LoadFont()
        {
            _privateFonts = new PrivateFontCollection();
            var fontPath = Path.Combine(Application.StartupPath, "Fonts", "Perfect DOS VGA 437 Win.ttf");
            _privateFonts.AddFontFile(fontPath);
        }
        
        private void SelectionLogic(KeyEventArgs e)
        {
            // Check if _labels is null or empty, and if so, simply return to avoid processing further
            if (_labels == null || _labels.Count == 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Up)
            {
                _selectedIndex--;
            }
            else if (e.KeyCode == Keys.Down)
            {
                _selectedIndex++;
            }

            // Clamp the _selectedIndex to be within the bounds of the _labels list
            _selectedIndex = Math.Max(0, Math.Min(_selectedIndex, _labels.Count - 1));
            
            // Set the BackColor property of the label at the specified index to the SelectedLabelColor variable
            _labels[_selectedIndex].BackColor = _selectedLabelColor;
            _labels[_selectedIndex].ForeColor = Color.Black;

            // Set the BackColor property of all other labels in the array to Color.White
            for (int i = 0; i < _labels.Count; i++)
            {
                if (i != _selectedIndex)
                {
                    _labels[i].BackColor = Color.Transparent;
                    _labels[i].ForeColor = Colors.BiosGray;
                }
            }
        }

        private void UpdateSelection()
        {
            if (_labels.Count > 0 && _selectedIndex >= 0 && _selectedIndex < _labels.Count)
            {
                _labels[_selectedIndex].BackColor = _selectedLabelColor;
                _labels[_selectedIndex].ForeColor = Color.Black;
            }
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.F3:
                    ShowConfirmationWindow();
                    break;
                case Keys.F1:
                    ShowHelpWindow();
                    break;
                case Keys.Enter:
                {
                    if (_labels.Count > 0 && _selectedIndex >= 0 && _selectedIndex < _labels.Count)
                    {
                        Label selectedLabel = _labels[_selectedIndex];
                        if(selectedLabel.Text == "Exit Setup.") Application.Exit();
                    }

                    _currentStep++;  // Always proceed to the next step when "Enter" is pressed
                    UpdateStep();
                    break;
                }
                case Keys.Up:
                case Keys.Down:
                    SelectionLogic(e);
                    break;
                case Keys.F5:
                    this.BackColor = Color.Black;
                    break;
            }
        }

        private void ShowConfirmationWindow()
        {
            SetupPrepareConfirmExit confirmationWindow = new SetupPrepareConfirmExit(false);
            confirmationWindow.ShowDialog(this);
            confirmationWindow.BringToFront(); 

            if (confirmationWindow.IsConfirmed)
            {
                Application.Exit();
            }
        }

        private void ShowHelpWindow()
        {
            SetupPrepareHelp helpWindow = new SetupPrepareHelp(_currentStep);
            helpWindow.ShowDialog(this);
            helpWindow.BringToFront();
        }
    }
}