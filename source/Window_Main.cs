using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Xbox360WirelessChatpad
{
    // Necessary to update the form items in a thread-safe manner
    delegate void controllerDisconnectCallback(int ctrl);
    delegate void controllerConnectCallback(int ctrl);
    delegate void mouseModeLabelCallback(int ctrl, bool modeStatus);
    delegate void logCallback(string message);
    delegate void debugCallback(Double RX, Double RY, Double LX, Double LY);

    public partial class Window_Main : Form
    {
        // The Xbox Wireless Receiver connected to the computer via USB
        private Receiver xboxReceiver;

        // The Xbox Wireless Controllers. Each controller is comprised of a
        // Gamepad (Joystick and Buttons) and a Chatpad (Attached Keyboard)
        private Controller[] xboxControllers = new Controller[4];

        public Window_Main()
        {
            // Instantiate the Controllers
            xboxControllers[0] = new Controller(this);
            // Initialize the Form Components
            InitializeComponent();
        }

        private void Window_Main_Load(object sender, EventArgs e)
        {
            // Load the Configuration Settings
            Properties.Settings.Default.Reload();

            // Load Controller 1 Configuration
            // Keyboard Type
            xboxControllers[0].configureChatpad(Properties.Settings.Default.ctrl1KeyboardType);
            switch (Properties.Settings.Default.ctrl1KeyboardType)
            {
                case "Q W E RT Y":
                    ctrl1QwertyButton.Checked = true;
                    break;
                case "Q W E R T Z":
                    ctrl1QwertzButton.Checked = true;
                    break;
                case "A Z E R T Y":
                    ctrl1AzertyButton.Checked = true;
                    break;
                default:
                    // Use QWERTY if the configuration file has junk data
                    ctrl1QwertyButton.Checked = true;
                    break;
            }

            // Register each Controller to a vJoy Joystick
            xboxControllers[0].registerJoystick(1);

            // Instantiate and Connect to the Receiver
            xboxReceiver = new Receiver(xboxControllers, this);
        }

        private void Window_Main_Resize(object sender, EventArgs e)
        {
            // Hides the window and minimizes to the System Tray upon Resize
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }

        private void Window_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Cleanup the Wireless Receiver
                xboxReceiver.killReceiver();

                // Save the configuraiton file variables
                Properties.Settings.Default.Save();
            }
            catch
            {
                // If we have an exception, force the process to close
                System.Environment.Exit(0);
            }
        }

        private void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            // Moves the window to the Taskbar when clicking the System Tray icon
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            // Close the Window
            this.Close();
        }

        private void appLogTextbox_TextChanged(object sender, EventArgs e)
        {
            // Automatically scrolls to the end of the textbox whenever the text changes
            appLogTextbox.SelectionStart = appLogTextbox.Text.Length;
            appLogTextbox.ScrollToCaret();
            appLogTextbox.Refresh();
        }

        private void chatpadTextBox_Enter(object sender, EventArgs e)
        {
            // Removes out pre-populated message in the chatpadTextBox
            chatpadTextBox.TextAlign = HorizontalAlignment.Left;
            chatpadTextBox.Text = "";

            // Removes the event handler so we don't continually remove the text
            chatpadTextBox.Enter -= chatpadTextBox_Enter;
        }

        private void triggerType_CheckChanged(object sender, EventArgs e)
        {
            // Set corresponding controller trigger type based on the check box
            string checkBoxName = ((CheckBox)sender).Name;

            if (checkBoxName.Contains("1"))
            {
                if (((CheckBox)sender).Checked)
                    Properties.Settings.Default.ctrl1TriggerAsButton = true;
               else
                    Properties.Settings.Default.ctrl1TriggerAsButton = false;

                xboxControllers[0].configureGamepad(Properties.Settings.Default.ctrl1TriggerAsButton);
            }

        }

        private void keyboardType_Selected(object sender, EventArgs e)
        {
            // Set the corresponding controller keyboard type based on the radio buttons
            string radioButtonName = ((RadioButton)sender).Name;

            if (radioButtonName.Contains("1"))
            {
                if (((RadioButton)sender).Checked)
                {
                    Properties.Settings.Default.ctrl1KeyboardType = ((RadioButton)sender).Text;
                    xboxControllers[0].configureChatpad(((RadioButton)sender).Text);
                }
            }

        }

        private void deadzoneL_ValueChanged(object sender, EventArgs e)
        {
            // Set the corresponding controllers right deadzone based on the slider
            string trackBarName = ((TrackBar)sender).Name;

            if (trackBarName.Contains("1"))
            {
                // Controller 1
                Properties.Settings.Default.ctrl1DeadzoneL = ctrl1LeftDeadzone.Value;
              //??  xboxControllers[0].deadzoneL = (int)Math.Round(ctrl1LeftDeadzone.Value * 327.67);
                ctrl1LeftDeadzonePercentLabel.Text = ctrl1LeftDeadzone.Value.ToString() + "%";
            }

        }

        private void deadzoneR_ValueChanged(object sender, EventArgs e)
        {
            // Set the corresponding controllers right deadzone based on the slider
            string trackBarName = ((TrackBar)sender).Name;

            if (trackBarName.Contains("1"))
            {
                // Controller 1
                Properties.Settings.Default.ctrl1DeadzoneR = ctrl1RightDeadzone.Value;
                //??    xboxControllers[0].deadzoneR = (int)Math.Round(ctrl1RightDeadzone.Value * 327.67);
                ctrl1RightDeadzonePercentLabel.Text = ctrl1RightDeadzone.Value.ToString() + "%";
            }
       
        }

        public void controllerConnected(int ctrl)
        {
            switch (ctrl)
            {
                case 1:
                 //  ctrl1Group.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        public void controllerDisconnected(int ctrl)
        {
            switch (ctrl)
            {
                case 1:
                //   ctrl1Group.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        public void mouseModeUpdate(int ctrl, bool modeStatus)
        {
            switch (ctrl)
            {
                case 1:
         /*           ctrl1MouseModeBox.Checked = modeStatus;
                    Properties.Settings.Default.ctrl1MouseMode = modeStatus;
                    break;
        */
                default:
                    break;
            }            
        }

        public void logDebug(Double RX, Double RY, Double LX, Double LY) {
            numericUpDown1.Value = (Decimal)RX;
            numericUpDown2.Value = (Decimal)RY;
            numericUpDown3.Value = (Decimal)LX;
            numericUpDown4.Value = (Decimal)LY;

        }
        public void logMessage(string message)
        {
            string currentTime;
            currentTime = DateTime.Now.ToString("G");

            // Pre-pend the Text Box text with timestamp and new message
            appLogTextbox.Text = "[" + currentTime + "] - " + message + "\r\n" + appLogTextbox.Text;

            // Scroll to Top of Textbox
            appLogTextbox.Select(0, 0);
            appLogTextbox.ScrollToCaret();
        }

        private void customBox_CheckChanged(object sender, EventArgs e)
        {
            // Enable corresponding controller custom mappings option based on the check box name
            string checkBoxName = ((CheckBox)sender).Name;

            if (checkBoxName.Contains("1"))
            {

            }
      
        }
        //Todo Remove
        private void ctrl1ConfigButton_Click(object sender, EventArgs e)
        {
            showCtrlConfigDialog(1);
        }
        //Todo Remove
        private void showCtrlConfigDialog(int ctrlnumber)
        {
            Custom_Settings_Window ctrldlg = new Custom_Settings_Window(ctrlnumber, this);
            ctrldlg.ShowDialog();
            if (ctrldlg.DialogResult == DialogResult.OK)
            {
                switch (ctrldlg.controllerNumber) {
                    case 1:
                        Properties.Settings.Default.ctrl1Profile = ctrldlg.profilePath;
                        break;
                }
                if (ctrldlg.profilePath != "")
                {
                    switch (ctrldlg.controllerNumber)
                    {
                        case 1:
                            //xboxControllers[0].configureGamepad(Properties.Settings.Default.ctrl1Profile);
              //              ctrl1TriggerTypeBox.Enabled = false;
                            break;
            
                    }
                }
                else
                {
                    switch (ctrldlg.controllerNumber)
                    {
                        case 1:
                            //xboxControllers[0].configureGamepad(Properties.Settings.Default.ctrl1TriggerAsButton);
                            break;
            
                    }
                }
            }
            ctrldlg.Dispose();
        }
        private void Start() {
            xboxControllers[0].vJoyInt.SetProfile(new JoyEmuProfile());
            xboxControllers[0].debug = cbDebug.CheckState==CheckState.Checked;
            xboxReceiver.connectReceiver();
            button4.Text = "Stop";
        }
        private void Stop() {
                xboxReceiver.killReceiver();
                button4.Text = "Run";
        }
        private void button4_Click(object sender, EventArgs e) {
            if (xboxReceiver.receiverStarted) {
                Stop();
            } else {
                Start();
            }
            
        }

    }
}
