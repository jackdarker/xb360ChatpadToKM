using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using LibUsbDotNet;
using LibUsbDotNet.Main;

namespace Xbox360WirelessChatpad
{
    class Controller
    {
        /// <summary>
        /// Tracks if the Wireless Controller is attached
        /// </summary>
        public bool controllerAttached = false;

        // Tracks the connected controller's number
        private int controllerNumber;

        // Tracks if the trigger will behave like a button or axis
        private bool triggerAsButton;

        // The Controllers associated endpoint writer in the receiver
        private UsbEndpointWriter epWriter;
        
        // Parent Window object necessary to communicate with form controls
        private Window_Main parentWindow;

        // Keep-Alive Thread, this will execute keep-alive commands periodically
        private System.Threading.Thread threadKeepAlive = null;
        private bool inhibitKeepAlive = false;
        private int inhibitCounter = 0;

        // Button Combo Thread, this will execute to monitor for special button
        // combinations like Mouse Mode and Shutdown
        private System.Threading.Thread threadButtonCombo = null;

        // Determines if the chatpad needs initialization/handshake command.
        private bool chatpadInitNeeded = true;

        // Mapping for various device commands
        private Dictionary<string, byte[]> controllerCommands = new Dictionary<string, byte[]>()
            {
                // General Device Commands
                { "RefreshConnection",  new byte[4] {0x08, 0x00, 0x00, 0x00} },
                { "KeepAlive1",         new byte[4] {0x00, 0x00, 0x0C, 0x1F} },
                { "KeepAlive2",         new byte[4] {0x00, 0x00, 0x0C, 0x1E} },
                { "ChatpadInit",        new byte[4] {0x00, 0x00, 0x0C, 0x1B} },
                { "SetControllerNum1",  new byte[4] {0x00, 0x00, 0x08, 0x42} },
                { "SetControllerNum2",  new byte[4] {0x00, 0x00, 0x08, 0x43} },
                { "SetControllerNum3",  new byte[4] {0x00, 0x00, 0x08, 0x44} },
                { "SetControllerNum4",  new byte[4] {0x00, 0x00, 0x08, 0x45} },
                { "DisableController",  new byte[4] {0x00, 0x00, 0x08, 0xC0} },

                // Chatpad LED Commands
                { "GreenOn",       new byte[4] {0x00, 0x00, 0x0C, 0x09} },
                { "GreenOff",      new byte[4] {0x00, 0x00, 0x0C, 0x01} },
                { "OrangeOn",      new byte[4] {0x00, 0x00, 0x0C, 0x0A} },
                { "OrangeOff",     new byte[4] {0x00, 0x00, 0x0C, 0x02} },
                { "MessengerOn",   new byte[4] {0x00, 0x00, 0x0C, 0x0B} },
                { "MessengerOff",  new byte[4] {0x00, 0x00, 0x0C, 0x03} },
                { "CapslockOn",    new byte[4] {0x00, 0x00, 0x0C, 0x08} },
                { "CapslockOff",   new byte[4] {0x00, 0x00, 0x0C, 0x00} }
            };

        // Contains the mapping of Chatpad Buttons, Green Modifiers, and
        // Orange Modifiers respectively.
        private Dictionary<int, WindowsInput.Native.VirtualKeyCode> keyMap = 
            new Dictionary<int, WindowsInput.Native.VirtualKeyCode>();
        private Dictionary<int, string> greenMap = new Dictionary<int, string>();
        private Dictionary<int, string> orangeMap = new Dictionary<int, string>();

       /*?? // Contains mappings of controller buttons, directional pad, and axes
        private Dictionary<String, uint> buttonMap = new Dictionary<String, uint>();
        private Dictionary<String, int> directionMap = new Dictionary<String, int>();
        private Dictionary<String, HID_USAGES> axisMap = new Dictionary<String, HID_USAGES>(); */

        // Tracks which Chatpad Modifiers are active
        private Dictionary<string, bool> chatpadMod = new Dictionary<string, bool>()
            {
                { "Green", false },
                { "Orange", false },
                { "Shift", false },
                { "Capslock", false },
                { "Messenger", false }
            };

        // Tracks which Chatpad LEDs are illuminated
        private Dictionary<string, bool> chatpadLED = new Dictionary<string, bool>()
            {
                { "Green", false },
                { "Orange", false },
                { "Capslock", false },
                { "Messenger", false }
            };

        // Tracks which keys are currently being held down, used to
        // determine if a keystroke should be sent or not
        private List<byte> chatpadKeysHeld = new List<byte>();

        // Tracks which keyboard keys are down, used to track if a
        // KeyUp command needs to be sent or not
        private List<WindowsInput.Native.VirtualKeyCode> keyboardKeysDown = 
            new List<WindowsInput.Native.VirtualKeyCode>();

        // Identifies if the sent key data should be upper case or lower case
        private bool flagUpperCase = false;

        // Identifies if Alt-Tab cycling has begun
        private bool altTabActive = false;

        // Used to determine if the data has changed since the last packet
        private byte[] dataPacketLast = new byte[3]; 

        // -----------------
        // Gamepad Variables
        // -----------------

        // The vJoy virtual joystick
        public JoyEmu vJoyInt;

        // Global Mouse Mode Flag for use by data packet processing
        public bool mouseModeFlag = false;

        // Special Command booleans used to detect when special button
        // combinations are pressed
        private bool cmdKillController = false;

        public bool debug = false;

        public Controller(Window_Main window)
        {
            // Stores the passed window as parentWindow for furtue use
            parentWindow = window;

            // Instantiate the vJoy interface
            vJoyInt = new JoyEmu(parentWindow);
        }

        public void registerEndpointWriter(UsbEndpointWriter writer)
        {
            // Store the Endpoint Writer for future use
            epWriter = writer;
        }

        public void registerJoystick(int ctrlNum)
        {
            // Stores the passed controller number for future use
            controllerNumber = ctrlNum;
        }

        public void processDataPacket(object sender, EndpointDataEventArgs e)
        {
            //after pushing guide I get this: so what is what ?
            //08-00-00-F0-00-00-00-00
            //08-80-00-F0-00-00-00-00
            //08-C0-00-17-A0-00-00-00
            //08-80-00-00-00-00-00-00
            //make sure threads are started once only
            if (e.Buffer[0] == 0x08)
            {
                string s2 = BitConverter.ToString(e.Buffer, 0, 60);
                Console.WriteLine(s2);
                // This is a status packet, determine if the controller is connected
                bool controllerConnected = ((e.Buffer[1] & 0x80) > 0);

                if (!controllerConnected) {
                    // If the controller is not connected but used to be, report the error
                    if (controllerAttached){
                        parentWindow.Invoke(new logCallback(parentWindow.logMessage),
                            "Xbox 360 Wireless Controller " + controllerNumber + " Disconnected.");

                        // Clean up the Keep-Alive thread
                        killKeepAlive();
                        // Clean up the Button Combo thread
                        killButtonCombo();
                        // Refresh the form due to a disconnection
                        parentWindow.Invoke(new controllerDisconnectCallback(parentWindow.controllerDisconnected), controllerNumber);
                    }
                    controllerAttached = false;
                } else if (controllerConnected && !controllerAttached) {
                    // Flag that the controller has connected
                    controllerAttached = true;
                    // Set the LED for the controller number
                    switch (controllerNumber){
                        case 1:
                            sendData(controllerCommands["SetControllerNum1"]);
                            break;
                        case 2:
                            sendData(controllerCommands["SetControllerNum2"]);
                            break;
                        case 3:
                            sendData(controllerCommands["SetControllerNum3"]);
                            break;
                        case 4:
                            sendData(controllerCommands["SetControllerNum4"]);
                            break;
                        default:
                            parentWindow.Invoke(new logCallback(parentWindow.logMessage),
                                "ERROR: Unknown Controller Number.");
                            break;
                    }

                    // Create and start the Keep-Alive thread
                    threadKeepAlive = new System.Threading.Thread(new System.Threading.ThreadStart(tickKeepAlive));
                    threadKeepAlive.IsBackground = true;
                    threadKeepAlive.Name = "threadKeepAlive";
                    threadKeepAlive.Start();
                    Console.WriteLine("tickkeepalive started");

                    // Create and start the Special Button thread
                    threadButtonCombo = new System.Threading.Thread(new System.Threading.ThreadStart(tickButtonCombo));
                    threadButtonCombo.IsBackground = true;
                    threadButtonCombo.Name = "threadButtonCombo";
                    threadButtonCombo.Start();

                    // If Mouse Mode, create and start the Mouse Mode thread
          //??          if (mouseModeFlag) startMouseMode();

                    // Refresh the form due to a connection
                    parentWindow.Invoke(new controllerConnectCallback(parentWindow.controllerConnected), controllerNumber);
                    // Reports the Controller is Connected
                    parentWindow.Invoke(new logCallback(parentWindow.logMessage),
                        "Xbox 360 Wireless Controller " + controllerNumber + " Connected.");
                }
            }
            else if (e.Buffer[0] == 0x00 && e.Buffer[2] == 0x00 && e.Buffer[3] == 0xF0){
                if (controllerAttached) {                    
                    switch (e.Buffer[1]) {
                        case 0x01: // This is Gamepad data
                            ProcessGamepadData(e.Buffer);
                            break;
                        case 0x02: // This is Chatpad data
                            ProcessChatpadData(e.Buffer);
                            break;
                        default:  // Unknown Data, do nothing with it
                            break;
                    }
                }
            }
        }

        public void ProcessChatpadData(byte[] dataPacket){
            // This function is called anytime received data is identified as chatpad data
            // It will parse the data, and depending on the value, send a keyboard command,
            // adjust a modifier for later use, flag initialization, or note the LED status.           
            if (dataPacket[24] == 0xF0)  {
                if (dataPacket[25] == 0x03) {
                    // This data represents handshake request, flag keep-alive to send
                    // chatpad initialization data.
                    chatpadInitNeeded = true;
                } else if (dataPacket[25] == 0x04) {
                    // This data represents the LED status. Not used because unsure of workings
                    //chatpadLED["Green"] = (dataPacket[26] & 0x08) > 0;
                    //chatpadLED["Orange"] = (dataPacket[26] & 0x10) > 0;
                    //chatpadLED["Messenger"] = (dataPacket[26] & 0x01) > 0;
                    //chatpadLED["Capslock"] = (dataPacket[26] & 0x20) > 0;
                    //Backlight = (dataPacket[26] & 0x80) > 0;
                } else {
                    parentWindow.Invoke(new logCallback(parentWindow.logMessage), "WARNING: Unknown Chatpad Status Data.");
                }
            } else if (dataPacket[24] == 0x00)  {
                // This data represents a key-press event
                // Check if anything has changed since the last dataPacket
                bool dataChanged = false;
                if (dataPacketLast != null) {
                    if (dataPacketLast[0] != dataPacket[25])
                        dataChanged = true;
                    else if (dataPacketLast[1] != dataPacket[26])
                        dataChanged = true;
                    else if (dataPacketLast[2] != dataPacket[27])
                        dataChanged = true;
                } else {
                    dataChanged = true;
                }

                // Store bits 25-27 of the data packet for later comparison
                dataPacketLast[0] = dataPacket[25];
                dataPacketLast[1] = dataPacket[26];
                dataPacketLast[2] = dataPacket[27];

                if (dataChanged) {
                    // Restart the keep alive inhibiter
                   //?? inhibitKeepAlive = true;  always send alive or chatpad stops working periodically ?
                   // inhibitCounter = 0;

                    // Record the Modifier Statuses
                    chatpadMod["Green"] = (dataPacket[25] & 0x02) > 0;
                    chatpadMod["Orange"] = (dataPacket[25] & 0x04) > 0;
                    chatpadMod["Shift"] = (dataPacket[25] & 0x01) > 0;
                    chatpadMod["Messenger"] = (dataPacket[25] & 0x08) > 0;

                    // Toggle Capslock Modifier based on Orange and Shift Modifiers
                    if (chatpadMod["Orange"] && chatpadMod["Shift"])
                        chatpadMod["Capslock"] = !chatpadMod["Capslock"];

                    // Set LEDs based on Modifiers
                    // Turning the LEDs on.
                    if (chatpadMod["Green"] && !chatpadLED["Green"])
                    {
                        sendData(controllerCommands["GreenOn"]);
                        chatpadLED["Green"] = true;
                    }
                    if (chatpadMod["Orange"] && !chatpadLED["Orange"])
                    {
                        sendData(controllerCommands["OrangeOn"]);
                        chatpadLED["Orange"] = true;
                    }
                    if (chatpadMod["Messenger"] && !chatpadLED["Messenger"])
                    {
                        sendData(controllerCommands["MessengerOn"]);
                        chatpadLED["Messenger"] = true;
                    }
                    if (chatpadMod["Capslock"] && !chatpadLED["Capslock"])
                    {
                        sendData(controllerCommands["CapslockOn"]);
                        chatpadLED["Capslock"] = true;
                    }

                    // Turning the LEDs off.
                    if (!chatpadMod["Green"] && chatpadLED["Green"])
                    {
                        sendData(controllerCommands["GreenOff"]);
                        chatpadLED["Green"] = false;
                    }
                    if (!chatpadMod["Orange"] && chatpadLED["Orange"])
                    {
                        sendData(controllerCommands["OrangeOff"]);
                        chatpadLED["Orange"] = false;
                    }
                    if (!chatpadMod["Messenger"] && chatpadLED["Messenger"])
                    {
                        sendData(controllerCommands["MessengerOff"]);
                        chatpadLED["Messenger"] = false;
                    }
                    if (!chatpadMod["Capslock"] && chatpadLED["Capslock"])
                    {
                        sendData(controllerCommands["CapslockOff"]);
                        chatpadLED["Capslock"] = false;
                    }
                    
                    // Set the Upper-Case flag and Shift Key status based on the
                    // XOR of Shift and Capslock Modifiers.
                    flagUpperCase = chatpadMod["Shift"] ^ chatpadMod["Capslock"];
                    if (flagUpperCase)
                        //Keyboard.KeyDown(Keys.LShiftKey);
                        vJoyInt.ButtonOn(true, WindowsInput.Native.VirtualKeyCode.LSHIFT);
                    else
                        vJoyInt.ButtonOn(false, WindowsInput.Native.VirtualKeyCode.LSHIFT);

                    // Set the Tab Key status based on the Messenger Modifier.
                    if (chatpadMod["Messenger"])
                       // Keyboard.KeyDown(Keys.Tab);
                        vJoyInt.ButtonOn(true, WindowsInput.Native.VirtualKeyCode.TAB);
                    else
                        vJoyInt.ButtonOn(false, WindowsInput.Native.VirtualKeyCode.TAB);

                    // Duplicates the Alt-Tab functionality with the Green and Orange
                    // Modifiers. Orange is Alt, Green is Tab
                    if (chatpadMod["Orange"])
                    {
                        if (chatpadMod["Green"])
                        {
                            if (altTabActive) {
                                // Keyboard.KeyPress(Keys.Tab);
                                vJoyInt.ButtonOn(true, WindowsInput.Native.VirtualKeyCode.TAB);
                            } else {
                                altTabActive = true;
                               // Keyboard.KeyDown(Keys.LMenu);
                               // Keyboard.KeyPress(Keys.Tab);
                                
                                vJoyInt.ButtonOn(true, WindowsInput.Native.VirtualKeyCode.LMENU);
                                vJoyInt.ButtonOn(true, WindowsInput.Native.VirtualKeyCode.TAB);
                                //?? wait
                                vJoyInt.ButtonOn(false, WindowsInput.Native.VirtualKeyCode.TAB);
                            }
                        }
                    }
                    else
                    {
                        if (altTabActive)
                        {
                            altTabActive = false;
                           // Keyboard.KeyUp(Keys.LMenu);
                            vJoyInt.ButtonOn(false, WindowsInput.Native.VirtualKeyCode.LMENU);
                        }
                    }

                    // Process the two different possible keys that could be held down
                    ProcessKeypress(dataPacket[26]);
                    ProcessKeypress(dataPacket[27]);

                    // Compile the list of keys that were once held but no longer being held
                    // For each one, send the KeyUp command if it was down and remove from the list.
                    List<byte> keysToRemove = new List<byte>();
                    foreach (var key in chatpadKeysHeld)
                        if (key != dataPacket[26] && key != dataPacket[27])
                            keysToRemove.Add(key);
                    foreach (var key in keysToRemove)
                    {
                        if (keyboardKeysDown.Contains(keyMap[key]))
                        {
                            keyboardKeysDown.Remove(keyMap[key]);
                            //Keyboard.KeyUp(keyMap[key]);
                            vJoyInt.ButtonOn(false, WindowsInput.Native.VirtualKeyCode.LMENU);
                        }
                        chatpadKeysHeld.Remove(key);
                    }
                }
            }
            else
                parentWindow.Invoke(new logCallback(parentWindow.logMessage), "WARNING: Unknown Chatpad Data.");
        }

        public void ProcessGamepadData(byte[] dataPacket)
        {
            // This function is called anytime received data is identified as gamepad data
            // It will parse the data and feed it to the vJoy device as necessary

            // --------------------------
            // Directional Pad Processing
            // --------------------------
            //string s2 = BitConverter.ToString(dataPacket, 0, 60);
             //Console.WriteLine(s2);
            // Set the POV hat based on the currently held direction
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.DUp] = (dataPacket[6]==0x1);
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.DDown] = (dataPacket[6] == 0x2);
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.DLeft] = (dataPacket[6] == 0x4);
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.DRight] = (dataPacket[6] == 0x8);
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.DUpLeft] = (dataPacket[6] == 0x5);
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.DDownLeft] = (dataPacket[6] == 0x6);
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.DUpRight] = (dataPacket[6] == 0x9);
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.DDownRight] = (dataPacket[6] == 0xA);
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.A]= (dataPacket[7] & 0x10) > 0;
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.B] = (dataPacket[7] & 0x20) > 0; 
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.X] = (dataPacket[7] & 0x40) > 0;
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.Y] = (dataPacket[7] & 0x80) > 0;
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.Start] = (dataPacket[6] & 0x10) > 0;
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.Back] = (dataPacket[6] & 0x20) > 0;
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.LStick] = (dataPacket[6] & 0x40) > 0;
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.RStick] = (dataPacket[6] & 0x80) > 0;
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.Guide] = (dataPacket[7] & 0x04) > 0;
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.LBump] = (dataPacket[7] & 0x01) > 0; 
            vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.RBump] = (dataPacket[7] & 0x02) > 0; 

            // ---------------
            // Axis Processing
            // ---------------
            // Record the left stick and right stick X and Y values and left and right trigger values
            short leftX = (short)(dataPacket[10] | (dataPacket[11] << 8));
            short leftY = (short)(dataPacket[12] | (dataPacket[13] << 8));
            short rightX = (short)(dataPacket[14] | (dataPacket[15] << 8));
            short rightY = (short)(dataPacket[16] | (dataPacket[17] << 8));
            int leftTrig = dataPacket[8];
            int rightTrig = dataPacket[9];
                // Set the left stick X and Y values
                // Note: For some reason, the left stick Y axis is inverted, multiplied by -1 to fix
                vJoyInt.axisStates[JoyEmuProfile.EnuAxis.LX] = ((Double)leftX / 32767);
                vJoyInt.axisStates[JoyEmuProfile.EnuAxis.LY] = ((Double)leftY / 32767);
                // Set the right stick X and Y values
                vJoyInt.axisStates[JoyEmuProfile.EnuAxis.RX] = ((Double)rightX / 32767);
                vJoyInt.axisStates[JoyEmuProfile.EnuAxis.RY] = ((Double)rightY / 32767);
                vJoyInt.axisStates[JoyEmuProfile.EnuAxis.LTrig] = ((Double)leftTrig / 255);
                vJoyInt.axisStates[JoyEmuProfile.EnuAxis.RTrig] = ((Double)rightTrig / 255);
                vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.LTrig] = leftTrig > 50;
                vJoyInt.buttonStates[JoyEmuProfile.EnuButtons.RTrig] = rightTrig > 50;
                if (debug) {
                    parentWindow.Invoke(new debugCallback(parentWindow.logDebug),
                            vJoyInt.axisStates[JoyEmuProfile.EnuAxis.RX], vJoyInt.axisStates[JoyEmuProfile.EnuAxis.RY],
                            vJoyInt.axisStates[JoyEmuProfile.EnuAxis.LX], vJoyInt.axisStates[JoyEmuProfile.EnuAxis.LY]);
                }

            // ------------------
            // Special Processing
            // ------------------
            // The special button combinations will be stored as booleans when they are
            // detected. A higher level timer function will use these booleans to determine
            // if the associated actions should be executed.
            // Indicate to toggle mouse mode based on the command sequence:
            // LBump + RBump + Back
           /*?? if (((dataPacket[7] & 0x01) > 0) && ((dataPacket[7] & 0x02) > 0) && ((dataPacket[6] & 0x20) > 0))
                cmdMouseModeToggle = true;
            else
                cmdMouseModeToggle = false;
            */
            // Indicate to shutdown controller based on the command sequence:
            // LTrig + RTrig + Back
                if ((leftTrig >= 50) && (rightTrig >= 50) && ((dataPacket[6] & 0x20) > 0)) {
                    cmdKillController = true;
                } else {
                    cmdKillController = false;
                }
        }

        private void sendData(byte[] dataToSend)
        {
            // This function sends the supplied data via the Endpoint Writer
            // to the Wireless Receiver as long as it is attached.
            int bytesWritten;

            ErrorCode ec = epWriter.Write(dataToSend, 2000, out bytesWritten);

            if (ec != ErrorCode.None) {
                parentWindow.Invoke(new logCallback(parentWindow.logMessage),
                    "ERROR: Problem Sending Controller Data.");
            }
        }

        private void ProcessKeypress(byte key)
        {
            if (key != 0 && !chatpadKeysHeld.Contains(key))
            {
                // If key is non-zero and was not previously being held down,
                // record that is now being held
                chatpadKeysHeld.Add(key);

                // Process the keystroke for the character associated with the key
                // depending on the status of Orange and Green modifiers.
                if (chatpadMod["Orange"])
                {
                    if (flagUpperCase)
                        SendKeys.SendWait(orangeMap[key].ToUpper());
                    else
                        SendKeys.SendWait(orangeMap[key]);
                }
                else if (chatpadMod["Green"])
                {
                    if (flagUpperCase)
                        SendKeys.SendWait(greenMap[key].ToUpper());
                    else
                        SendKeys.SendWait(greenMap[key]);
                }
                else
                {
                    keyboardKeysDown.Add(keyMap[key]);
                    //Keyboard.KeyDown(keyMap[key]);
                    vJoyInt.ButtonOn(true, keyMap[key]);
                }
            }
        }

        public void configureChatpad(string keyboardType)
        {
            // Updates the controller dictionaries with keystrokes based on the
            // specified keyboard type.
            switch (keyboardType)
            {
                case "Q W E R T Y":
                    keyMap[23] = WindowsInput.Native.VirtualKeyCode.VK_1; greenMap[23] = ""; orangeMap[23] = "";
                    keyMap[22] = WindowsInput.Native.VirtualKeyCode.VK_2; greenMap[22] = ""; orangeMap[22] = "";
                    keyMap[21] = WindowsInput.Native.VirtualKeyCode.VK_3; greenMap[21] = ""; orangeMap[21] = "";
                    keyMap[20] = WindowsInput.Native.VirtualKeyCode.VK_4; greenMap[20] = ""; orangeMap[20] = "";
                    keyMap[19] = WindowsInput.Native.VirtualKeyCode.VK_5; greenMap[19] = ""; orangeMap[19] = "";
                    keyMap[18] = WindowsInput.Native.VirtualKeyCode.VK_6; greenMap[18] = ""; orangeMap[18] = "";
                    keyMap[17] = WindowsInput.Native.VirtualKeyCode.VK_7; greenMap[17] = ""; orangeMap[17] = "";
                    keyMap[103] = WindowsInput.Native.VirtualKeyCode.VK_8; greenMap[103] = ""; orangeMap[103] = "";
                    keyMap[102] = WindowsInput.Native.VirtualKeyCode.VK_9; greenMap[102] = ""; orangeMap[102] = "";
                    keyMap[101] = WindowsInput.Native.VirtualKeyCode.VK_0; greenMap[101] = ""; orangeMap[101] = "";

                    keyMap[39] = WindowsInput.Native.VirtualKeyCode.VK_Q; greenMap[39] = "!"; orangeMap[39] = "¡";
                    keyMap[38] = WindowsInput.Native.VirtualKeyCode.VK_W; greenMap[38] = "@"; orangeMap[38] = "å";
                    keyMap[37] = WindowsInput.Native.VirtualKeyCode.VK_E; greenMap[37] = "€"; orangeMap[37] = "é";
                    keyMap[36] = WindowsInput.Native.VirtualKeyCode.VK_R; greenMap[36] = "#"; orangeMap[36] = "$";
                    keyMap[35] = WindowsInput.Native.VirtualKeyCode.VK_T; greenMap[35] = "{%}"; orangeMap[35] = "Þ";
                    keyMap[34] = WindowsInput.Native.VirtualKeyCode.VK_Y; greenMap[34] = "{^}"; orangeMap[34] = "ý";
                    keyMap[33] = WindowsInput.Native.VirtualKeyCode.VK_U; greenMap[33] = "&"; orangeMap[33] = "ú";
                    keyMap[118] = WindowsInput.Native.VirtualKeyCode.VK_I; greenMap[118] = "*"; orangeMap[118] = "í";
                    keyMap[117] = WindowsInput.Native.VirtualKeyCode.VK_O; greenMap[117] = "{(}"; orangeMap[117] = "ó";
                    keyMap[100] = WindowsInput.Native.VirtualKeyCode.VK_P; greenMap[100] = "{)}"; orangeMap[100] = "=";

                    keyMap[55] = WindowsInput.Native.VirtualKeyCode.VK_A; greenMap[55] = "{~}"; orangeMap[55] = "á";
                    keyMap[54] = WindowsInput.Native.VirtualKeyCode.VK_S; greenMap[54] = "š"; orangeMap[54] = "ß";
                    keyMap[53] = WindowsInput.Native.VirtualKeyCode.VK_D; greenMap[53] = "{{}"; orangeMap[53] = "ð";
                    keyMap[52] = WindowsInput.Native.VirtualKeyCode.VK_F; greenMap[52] = "{}}"; orangeMap[52] = "£";
                    keyMap[51] = WindowsInput.Native.VirtualKeyCode.VK_G; greenMap[51] = "¨"; orangeMap[51] = "¥";
                    keyMap[50] = WindowsInput.Native.VirtualKeyCode.VK_H; greenMap[50] = "/"; orangeMap[50] = "\\";
                    keyMap[49] = WindowsInput.Native.VirtualKeyCode.VK_J; greenMap[49] = "'"; orangeMap[49] = "\"";
                    keyMap[119] = WindowsInput.Native.VirtualKeyCode.VK_K; greenMap[119] = "{[}"; orangeMap[119] = "☺";
                    keyMap[114] = WindowsInput.Native.VirtualKeyCode.VK_L; greenMap[114] = "{]}"; orangeMap[114] = "ø";
                    keyMap[98] = WindowsInput.Native.VirtualKeyCode.OEM_COMMA; greenMap[98] = ":"; orangeMap[98] = ";";

                    keyMap[70] = WindowsInput.Native.VirtualKeyCode.VK_Z; greenMap[70] = "`"; orangeMap[70] = "æ";
                    keyMap[69] = WindowsInput.Native.VirtualKeyCode.VK_X; greenMap[69] = "«"; orangeMap[69] = "œ";
                    keyMap[68] = WindowsInput.Native.VirtualKeyCode.VK_C; greenMap[68] = "»"; orangeMap[68] = "ç";
                    keyMap[67] = WindowsInput.Native.VirtualKeyCode.VK_V; greenMap[67] = "-"; orangeMap[67] = "_";
                    keyMap[66] = WindowsInput.Native.VirtualKeyCode.VK_B; greenMap[66] = "|"; orangeMap[66] = "{+}";
                    keyMap[65] = WindowsInput.Native.VirtualKeyCode.VK_N; greenMap[65] = "<"; orangeMap[65] = "ñ";
                    keyMap[82] = WindowsInput.Native.VirtualKeyCode.VK_M; greenMap[82] = ">"; orangeMap[82] = "µ";
                    keyMap[83] = WindowsInput.Native.VirtualKeyCode.OEM_PERIOD; greenMap[83] = "?"; orangeMap[83] = "¿";
                    keyMap[99] = WindowsInput.Native.VirtualKeyCode.RETURN; greenMap[99] = ""; orangeMap[99] = "";

                    keyMap[85] = WindowsInput.Native.VirtualKeyCode.LEFT; greenMap[85] = ""; orangeMap[85] = "";
                    keyMap[84] = WindowsInput.Native.VirtualKeyCode.SPACE; greenMap[84] = ""; orangeMap[84] = "";
                    keyMap[81] = WindowsInput.Native.VirtualKeyCode.RIGHT; greenMap[81] = ""; orangeMap[81] = "";
                    keyMap[113] = WindowsInput.Native.VirtualKeyCode.BACK; greenMap[113] = ""; orangeMap[113] = "";
                    break;

                case "Q W E R T Z":
                    keyMap[23] = WindowsInput.Native.VirtualKeyCode.VK_1; greenMap[23] = ""; orangeMap[23] = "";
                    keyMap[22] = WindowsInput.Native.VirtualKeyCode.VK_2; greenMap[22] = ""; orangeMap[22] = "";
                    keyMap[21] = WindowsInput.Native.VirtualKeyCode.VK_3; greenMap[21] = ""; orangeMap[21] = "";
                    keyMap[20] = WindowsInput.Native.VirtualKeyCode.VK_4; greenMap[20] = ""; orangeMap[20] = "";
                    keyMap[19] = WindowsInput.Native.VirtualKeyCode.VK_5; greenMap[19] = ""; orangeMap[19] = "";
                    keyMap[18] = WindowsInput.Native.VirtualKeyCode.VK_6; greenMap[18] = ""; orangeMap[18] = "";
                    keyMap[17] = WindowsInput.Native.VirtualKeyCode.VK_7; greenMap[17] = ""; orangeMap[17] = "";
                    keyMap[103] = WindowsInput.Native.VirtualKeyCode.VK_8; greenMap[103] = ""; orangeMap[103] = "";
                    keyMap[102] = WindowsInput.Native.VirtualKeyCode.VK_9; greenMap[102] = ""; orangeMap[102] = "";
                    keyMap[101] = WindowsInput.Native.VirtualKeyCode.VK_0; greenMap[101] = ""; orangeMap[101] = "";

                    keyMap[39] = WindowsInput.Native.VirtualKeyCode.VK_Q; greenMap[39] = "!"; orangeMap[39] = "@";
                    keyMap[38] = WindowsInput.Native.VirtualKeyCode.VK_W; greenMap[38] = "\""; orangeMap[38] = "¡";
                    keyMap[37] = WindowsInput.Native.VirtualKeyCode.VK_E; greenMap[37] = "€"; orangeMap[37] = "é";
                    keyMap[36] = WindowsInput.Native.VirtualKeyCode.VK_R; greenMap[36] = "$"; orangeMap[36] = "¥";
                    keyMap[35] = WindowsInput.Native.VirtualKeyCode.VK_T; greenMap[35] = "{%}"; orangeMap[35] = "Þ";
                    keyMap[34] = WindowsInput.Native.VirtualKeyCode.VK_Z; greenMap[34] = "&"; orangeMap[34] = "{^}";
                    keyMap[33] = WindowsInput.Native.VirtualKeyCode.VK_U; greenMap[33] = "/"; orangeMap[33] = "ü";
                    keyMap[118] = WindowsInput.Native.VirtualKeyCode.VK_I; greenMap[118] = "{(}"; orangeMap[118] = "í";
                    keyMap[117] = WindowsInput.Native.VirtualKeyCode.VK_O; greenMap[117] = "{)}"; orangeMap[117] = "ö";
                    keyMap[100] = WindowsInput.Native.VirtualKeyCode.VK_P; greenMap[100] = "="; orangeMap[100] = "\\";

                    keyMap[55] = WindowsInput.Native.VirtualKeyCode.VK_A; greenMap[55] = "å"; orangeMap[55] = "ä";
                    keyMap[54] = WindowsInput.Native.VirtualKeyCode.VK_S; greenMap[54] = "ß"; orangeMap[54] = "š";
                    keyMap[53] = WindowsInput.Native.VirtualKeyCode.VK_D; greenMap[53] = "«"; orangeMap[53] = "ð";
                    keyMap[52] = WindowsInput.Native.VirtualKeyCode.VK_F; greenMap[52] = "»"; orangeMap[52] = "£";
                    keyMap[51] = WindowsInput.Native.VirtualKeyCode.VK_G; greenMap[51] = "¨"; orangeMap[51] = "☺";
                    keyMap[50] = WindowsInput.Native.VirtualKeyCode.VK_H; greenMap[50] = "{{}"; orangeMap[50] = "`";
                    keyMap[49] = WindowsInput.Native.VirtualKeyCode.VK_J; greenMap[49] = "{}}"; orangeMap[49] = "ø";
                    keyMap[119] = WindowsInput.Native.VirtualKeyCode.VK_K; greenMap[119] = "{[}"; orangeMap[119] = "æ";
                    keyMap[114] = WindowsInput.Native.VirtualKeyCode.VK_L; greenMap[114] = "{]}"; orangeMap[114] = "œ";
                    keyMap[98] = WindowsInput.Native.VirtualKeyCode.OEM_COMMA; greenMap[98] = "':"; orangeMap[98] = "#;";

                    keyMap[70] = WindowsInput.Native.VirtualKeyCode.VK_Y; greenMap[70] = "<"; orangeMap[70] = "°";
                    keyMap[69] = WindowsInput.Native.VirtualKeyCode.VK_X; greenMap[69] = ">"; orangeMap[69] = "|";
                    keyMap[68] = WindowsInput.Native.VirtualKeyCode.VK_C; greenMap[68] = "{~}"; orangeMap[68] = "ç";
                    keyMap[67] = WindowsInput.Native.VirtualKeyCode.VK_V; greenMap[67] = "-"; orangeMap[67] = "_";
                    keyMap[66] = WindowsInput.Native.VirtualKeyCode.VK_B; greenMap[66] = "*"; orangeMap[66] = "{+}";
                    keyMap[65] = WindowsInput.Native.VirtualKeyCode.VK_N; greenMap[65] = ";"; orangeMap[65] = "ñ";
                    keyMap[82] = WindowsInput.Native.VirtualKeyCode.VK_M; greenMap[82] = ":"; orangeMap[82] = "µ";
                    keyMap[83] = WindowsInput.Native.VirtualKeyCode.OEM_PERIOD; greenMap[83] = "?"; orangeMap[83] = "¿";
                    keyMap[99] = WindowsInput.Native.VirtualKeyCode.RETURN; greenMap[99] = ""; orangeMap[99] = "";

                    keyMap[85] = WindowsInput.Native.VirtualKeyCode.LEFT; greenMap[85] = ""; orangeMap[85] = "";
                    keyMap[84] = WindowsInput.Native.VirtualKeyCode.SPACE; greenMap[84] = ""; orangeMap[84] = "";
                    keyMap[81] = WindowsInput.Native.VirtualKeyCode.RIGHT; greenMap[81] = ""; orangeMap[81] = "";
                    keyMap[113] = WindowsInput.Native.VirtualKeyCode.BACK; greenMap[113] = ""; orangeMap[113] = "";
                    break;

                case "A Z E R T Y":
                    keyMap[23] = WindowsInput.Native.VirtualKeyCode.VK_1; greenMap[23] = ""; orangeMap[23] = "";
                    keyMap[22] = WindowsInput.Native.VirtualKeyCode.VK_2; greenMap[22] = ""; orangeMap[22] = "";
                    keyMap[21] = WindowsInput.Native.VirtualKeyCode.VK_3; greenMap[21] = ""; orangeMap[21] = "";
                    keyMap[20] = WindowsInput.Native.VirtualKeyCode.VK_4; greenMap[20] = ""; orangeMap[20] = "";
                    keyMap[19] = WindowsInput.Native.VirtualKeyCode.VK_5; greenMap[19] = ""; orangeMap[19] = "";
                    keyMap[18] = WindowsInput.Native.VirtualKeyCode.VK_6; greenMap[18] = ""; orangeMap[18] = "";
                    keyMap[17] = WindowsInput.Native.VirtualKeyCode.VK_7; greenMap[17] = ""; orangeMap[17] = "";
                    keyMap[103] = WindowsInput.Native.VirtualKeyCode.VK_8; greenMap[103] = ""; orangeMap[103] = "";
                    keyMap[102] = WindowsInput.Native.VirtualKeyCode.VK_9; greenMap[102] = ""; orangeMap[102] = "";
                    keyMap[101] = WindowsInput.Native.VirtualKeyCode.VK_0; greenMap[101] = ""; orangeMap[101] = "";

                    keyMap[39] = WindowsInput.Native.VirtualKeyCode.VK_A; greenMap[39] = "à"; orangeMap[39] = "&";
                    keyMap[38] = WindowsInput.Native.VirtualKeyCode.VK_Z; greenMap[38] = "æ"; orangeMap[38] = "{~}";
                    keyMap[37] = WindowsInput.Native.VirtualKeyCode.VK_E; greenMap[37] = "€"; orangeMap[37] = "\"";
                    keyMap[36] = WindowsInput.Native.VirtualKeyCode.VK_R; greenMap[36] = "é"; orangeMap[36] = "$";
                    keyMap[35] = WindowsInput.Native.VirtualKeyCode.VK_T; greenMap[35] = "#"; orangeMap[35] = "{(}";
                    keyMap[34] = WindowsInput.Native.VirtualKeyCode.VK_Y; greenMap[34] = "ý"; orangeMap[34] = "-";
                    keyMap[33] = WindowsInput.Native.VirtualKeyCode.VK_U; greenMap[33] = "ù"; orangeMap[33] = "`";
                    keyMap[118] = WindowsInput.Native.VirtualKeyCode.VK_I; greenMap[118] = "ì"; orangeMap[118] = "_";
                    keyMap[117] = WindowsInput.Native.VirtualKeyCode.VK_O; greenMap[117] = "œ"; orangeMap[117] = "@";
                    keyMap[100] = WindowsInput.Native.VirtualKeyCode.VK_P; greenMap[100] = "ó"; orangeMap[100] = "{)}";

                    keyMap[55] = WindowsInput.Native.VirtualKeyCode.VK_Q; greenMap[55] = "å"; orangeMap[55] = "☺";
                    keyMap[54] = WindowsInput.Native.VirtualKeyCode.VK_S; greenMap[54] = "š"; orangeMap[54] = "«";
                    keyMap[53] = WindowsInput.Native.VirtualKeyCode.VK_D; greenMap[53] = "ð"; orangeMap[53] = "»";
                    keyMap[52] = WindowsInput.Native.VirtualKeyCode.VK_F; greenMap[52] = "Þ"; orangeMap[52] = "{{}";
                    keyMap[51] = WindowsInput.Native.VirtualKeyCode.VK_G; greenMap[51] = "¨"; orangeMap[51] = "¥";
                    keyMap[50] = WindowsInput.Native.VirtualKeyCode.VK_H; greenMap[50] = "|"; orangeMap[50] = "ø";
                    keyMap[49] = WindowsInput.Native.VirtualKeyCode.VK_J; greenMap[49] = "µ"; orangeMap[49] = "¨";
                    keyMap[119] = WindowsInput.Native.VirtualKeyCode.VK_K; greenMap[119] = "/"; orangeMap[119] = "\\";
                    keyMap[114] = WindowsInput.Native.VirtualKeyCode.VK_L; greenMap[114] = "$"; orangeMap[114] = "£";
                    keyMap[98] = WindowsInput.Native.VirtualKeyCode.VK_M; greenMap[98] = "*"; orangeMap[98] = "{^}";

                    keyMap[70] = WindowsInput.Native.VirtualKeyCode.VK_W; greenMap[70] = "¡"; orangeMap[70] = "<";
                    keyMap[69] = WindowsInput.Native.VirtualKeyCode.VK_X; greenMap[69] = "¿"; orangeMap[69] = ">";
                    keyMap[68] = WindowsInput.Native.VirtualKeyCode.VK_C; greenMap[68] = "ç"; orangeMap[68] = "{[}";
                    keyMap[67] = WindowsInput.Native.VirtualKeyCode.VK_V; greenMap[67] = "="; orangeMap[67] = "{]}";
                    keyMap[66] = WindowsInput.Native.VirtualKeyCode.VK_B; greenMap[66] = "{+}"; orangeMap[66] = "{%}";
                    keyMap[65] = WindowsInput.Native.VirtualKeyCode.VK_N; greenMap[65] = "?"; orangeMap[65] = "ñ";
                    keyMap[82] = WindowsInput.Native.VirtualKeyCode.OEM_COMMA; greenMap[82] = "!"; orangeMap[82] = "'";
                    keyMap[83] = WindowsInput.Native.VirtualKeyCode.OEM_PERIOD; greenMap[83] = ":"; orangeMap[83] = ";";
                    keyMap[99] = WindowsInput.Native.VirtualKeyCode.RETURN; greenMap[99] = ""; orangeMap[99] = "";

                    keyMap[85] = WindowsInput.Native.VirtualKeyCode.LEFT; greenMap[85] = ""; orangeMap[85] = "";
                    keyMap[84] = WindowsInput.Native.VirtualKeyCode.SPACE; greenMap[84] = ""; orangeMap[84] = "";
                    keyMap[81] = WindowsInput.Native.VirtualKeyCode.RIGHT; greenMap[81] = ""; orangeMap[81] = "";
                    keyMap[113] = WindowsInput.Native.VirtualKeyCode.BACK; greenMap[113] = ""; orangeMap[113] = "";
                    break;

                default:
                    parentWindow.Invoke(new logCallback(parentWindow.logMessage),
                        "ERROR: Unknown Keyboard Type.");
                    break;
            }
        }
        public void configureGamepad(bool triggerAsBtn)
        {
            // Updates the controller dictionaries and record the specified
            // trigger type of either Button or Axis.
            triggerAsButton = triggerAsBtn;

          
        }
        public void configureGamepad(string customCfgFile)
        {
            //var profileINI = new FileIniDataParser();
            //IniData profileData = profileINI.LoadFile(customCfgFile);

           /* buttonMap["A"] = Convert.ToUInt32(profileData["ButtonMapping"]["buttonA"]);
            buttonMap["B"] = Convert.ToUInt32(profileData["ButtonMapping"]["buttonB"]);
            buttonMap["X"] = Convert.ToUInt32(profileData["ButtonMapping"]["buttonX"]);
            buttonMap["Y"] = Convert.ToUInt32(profileData["ButtonMapping"]["buttonY"]);
            buttonMap["LStick"] = Convert.ToUInt32(profileData["ButtonMapping"]["Lstick"]);
            buttonMap["RStick"] = Convert.ToUInt32(profileData["ButtonMapping"]["Rstick"]);
            buttonMap["LBump"] = Convert.ToUInt32(profileData["ButtonMapping"]["Lbump"]);
            buttonMap["RBump"] = Convert.ToUInt32(profileData["ButtonMapping"]["Rbump"]);
            buttonMap["Back"] = Convert.ToUInt32(profileData["ButtonMapping"]["back"]);
            buttonMap["Start"] = Convert.ToUInt32(profileData["ButtonMapping"]["start"]);
            buttonMap["Guide"] = Convert.ToUInt32(profileData["ButtonMapping"]["guide"]);

            directionMap["Neutral"] = -1;
            directionMap["Up"] = 0;
            directionMap["UpRight"] = 4500;
            directionMap["Right"] = 9000;
            directionMap["DownRight"] = 13500;
            directionMap["Down"] = 18000;
            directionMap["DownLeft"] = 22500;
            directionMap["Left"] = 27000;
            directionMap["UpLeft"] = 31500;

            if (Convert.ToBoolean(profileData["TriggerSettings"]["trigAsBttn"]))
            {
                buttonMap["LTrig"]  = Convert.ToUInt32(profileData["TriggerSettings"]["Ltrig"]);
                buttonMap["RTrig"]  = Convert.ToUInt32(profileData["TriggerSettings"]["Rtrig"]);

                axisMap["LX"] = HID_USAGES.HID_USAGE_X;
                axisMap["LY"] = HID_USAGES.HID_USAGE_Y;
                axisMap["RX"] = HID_USAGES.HID_USAGE_Z;
                axisMap["RY"] = HID_USAGES.HID_USAGE_RZ;
            }
            else
            {
                axisMap["LX"] = HID_USAGES.HID_USAGE_X;
                axisMap["LY"] = HID_USAGES.HID_USAGE_Y;
                axisMap["RX"] = HID_USAGES.HID_USAGE_RX;
                axisMap["RY"] = HID_USAGES.HID_USAGE_RY;
                axisMap["LTrig"] = HID_USAGES.HID_USAGE_Z;
                axisMap["RTrig"] = HID_USAGES.HID_USAGE_RZ;
            }*/
        }
        public void startController()
        {
            // Sends command to begin polling for controller data
            // Note: This is intentionally redundant, things seem to work better as a result
            sendData(controllerCommands["RefreshConnection"]);
            sendData(controllerCommands["RefreshConnection"]);
        }
        public void killController()
        {
            // Sends command to disable the controller
            sendData(controllerCommands["DisableController"]);
            parentWindow.Invoke(new logCallback(parentWindow.logMessage),
                "Disconnecting Xbox 360 Wireless Controller " + controllerNumber + ".");
        }
        /// <summary>
        /// thread-called function to update emulator-output
        /// </summary>
        private void tickButtonCombo()
        {
            // This function is executed periodically to process special button combinations when they
            // are held down for a specific amount of time.

            int killControllerTick = 0;
            int lastCycle=(DateTime.Now).Millisecond;
            int thisCycle = lastCycle;
            try {
                while (true) {
                    // Increment kill controller counter if key combination for killing the
                    // controller is detected, otherwise reset the counter.
                    if (cmdKillController)
                        killControllerTick++;
                    else
                        killControllerTick = 0;

                    // Kills the controller if the kill controller counter hits 6 (3 seconds)
                    if (killControllerTick == 6000 / 20)
                        sendData(controllerCommands["DisableController"]);
                    //now refresh Joystick-output
                    vJoyInt.UpdateState();
                    thisCycle = (DateTime.Now).Millisecond;
                    System.Threading.Thread.Sleep(20);
                    //System.Threading.Thread.Sleep(Math.Max(1, (20 - (thisCycle - lastCycle))));
                    lastCycle = thisCycle;
                }
            } catch (ThreadAbortException abortException) {
                ;
            }
        }
        public void killButtonCombo()
        {
            // Cleans up the Button Combo thread
            if (threadButtonCombo != null) {
                threadButtonCombo.Abort();
                threadButtonCombo.Join();
                threadButtonCombo = null;
            }
        }
        /// <summary>
        /// thread-called function to send keep alive message
        /// </summary>
        private void tickKeepAlive()
        {
            // This function loops every second on a separate background thread
            // as long as the controller is connected. It will send unique alternating
            // device commands in order to keep the device alive, and if necessary send
            // chatpad initialization commands.

            // Keep-Alive Toggle, this will determine which keep-alive command will be sent
            // during each execution cycle, True = Command 1, False = Command 2
            bool keepAliveToggle = false;
            try {
                while (true) {
                    if (epWriter != null) {
                        if (inhibitKeepAlive) {
                            if (inhibitCounter >= 2) {
                                inhibitCounter = 0;
                                inhibitKeepAlive = false;
                            } else
                                inhibitCounter++;
                        } else {
                            if (keepAliveToggle)
                                sendData(controllerCommands["KeepAlive1"]);
                            else
                                sendData(controllerCommands["KeepAlive2"]);

                            keepAliveToggle = !keepAliveToggle;
                        }

                        if (chatpadInitNeeded) {
                            // Initialize Chatpad Communication
                            sendData(controllerCommands["ChatpadInit"]);

                            // Set Initialization flag to False, no need to do it again
                            chatpadInitNeeded = false;
                        }

                        System.Threading.Thread.Sleep(1000);
                    }
                }
            } 
            catch (ThreadAbortException abortException) {
                Console.WriteLine("tickkeepalive aborted");
            }
        }
        public void killKeepAlive()
        {
            // Cleans up the Keep-Alive thread
            if (threadKeepAlive != null) {
                threadKeepAlive.Abort();
                threadKeepAlive.Join(); 
                threadKeepAlive = null;
            }
        }
        
    }
}
