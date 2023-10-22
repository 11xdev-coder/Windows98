using System.Drawing.Text;

namespace Windows98
{
    public static class Extensions
    {
        
        public static Label AddSetupLabelText(string text, float size, Point location, PrivateFontCollection privateFonts)
        {
            return AddCustomLabelText(text, size, location, Colors.BiosGray, privateFonts);
        }
        
        public static Label AddCustomLabelText(string text, float size, Point location, Color color, PrivateFontCollection privateFonts)
        {
            // Create a new Label control
            Label textLabel = new Label();
            textLabel.Text = text;
            textLabel.Font = new Font(privateFonts.Families[0], size, FontStyle.Regular);
            textLabel.ForeColor = color; 
            textLabel.BackColor = Color.Transparent;
            textLabel.Location = location;
            textLabel.AutoSize = true;  // Ensure the label size adjusts to fit the text

            return textLabel;
        }

        public static Panel ShowControlsPanel(bool isHelp, bool showRemoveColor, Color color, PrivateFontCollection _privateFonts)
        {
            // Create a grey panel.
            Panel bottomPanel = new Panel();
            bottomPanel.Height /= 2;
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.BackColor = color;

            if (!isHelp)
            {
                if (showRemoveColor)
                {
                    bottomPanel.Controls.Add(AddCustomLabelText("ENTER=Continue   F1=Help   F3=Exit   " +
                                                                           "F5=Remove Color            |", 32f, new Point(5, 5), Color.Black, _privateFonts));
                }
                else
                {
                    bottomPanel.Controls.Add(AddCustomLabelText("ENTER=Continue   F1=Help   F3=Exit   " +
                                                                           "                           |", 32f, new Point(5, 5), Color.Black, _privateFonts));
                }
            }
            else
            {
                bottomPanel.Controls.Add(AddCustomLabelText("F3=Exit   ESC=Previous Screen                                   |", 32f,
                    new Point(5, 5), Colors.BiosGray, _privateFonts));
            }
            
            return bottomPanel;
        }
        
        public static double GetDiskSize()
        {
            DriveInfo driveInfo = new DriveInfo("C");
            long totalSizeInBytes = driveInfo.TotalSize;
            double totalSizeInMB = totalSizeInBytes / (1024.0 * 1024.0);  // Convert bytes to megabytes

            return totalSizeInMB;
        }
    }
}

