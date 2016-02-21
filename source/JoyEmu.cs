using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WindowsInput;


namespace Xbox360WirelessChatpad {
    public class JoyEmuProfile {
        public enum EnuButtons {
            A, B, X, Y, Back, Start, Guide, LStick, RStick, LBump, RBump, LTrig, RTrig, DUp, DDown, DLeft, DRight, DUpLeft, DUpRight, DDownLeft, DDownRight
        }
        public enum EnuAxis {
            LX, LY, RX, RY, LTrig, RTrig
        }

        public enum EnuMouse {
            None,X,Y,Z, LeftBt,MiddleBt,RightBt
        }
        
        //mapping with no modifier
        public Dictionary<EnuButtons, WindowsInput.Native.VirtualKeyCode> buttonToKey0 = 
            new Dictionary<EnuButtons, WindowsInput.Native.VirtualKeyCode>();
        public Dictionary<EnuAxis, EnuMouse> axisToMouse0 = new Dictionary<EnuAxis, EnuMouse>();
        //mapping with modifier 1
        public Dictionary<EnuButtons, WindowsInput.Native.VirtualKeyCode> buttonToKey1 = 
            new Dictionary<EnuButtons, WindowsInput.Native.VirtualKeyCode>();
        public Double DeadZoneAnalogL = 0.25;
        public Double DeadZoneDigitalL = 0.6;
        public Double DeadZoneAnalogR = 0.25;
        public Double DeadZoneTrigger = 0.2;

        //depends how often update is called from thread
        public Double maxMouseVelocityX = 10;
        public Double maxMouseVelocityY = 5;
        public int Mod1Delay = 0;
        public int Mod2Delay = 0;
        public int ScrollDelay = 0;
        public int ModActive = 0;

        public JoyEmuProfile() {
            SetDefault();
        }
        /// <summary>
        /// this detects if a modifier-key is active
        /// </summary>
        /// <param name="buttonStates"></param>
        public void DetectModifier (Dictionary<JoyEmuProfile.EnuButtons, Boolean> buttonStates) {
            ModActive = 0;
            if (buttonStates[JoyEmuProfile.EnuButtons.LBump]) {
                Mod1Delay++;  //??overflow
                if (Mod1Delay > 20) {
                    ModActive = 1;
                }
            } else {
                Mod1Delay = 0;
            }
        }
        public void ButtonOn(JoyEmuProfile.EnuButtons Bt, Boolean On) {
        /*    if (!On) {
                Keyboard.KeyUp(buttonToKey0[Bt]);
                Keyboard.KeyUp(buttonToKey1[Bt]);

            } else if (On && (ModActive ==0)) {
                inputSim.Keyboard.KeyDown(buttonToKey0[Bt]);
                Keyboard.KeyUp(buttonToKey1[Bt]);

            } else if (On && (ModActive==1)) {
                inputSim.Keyboard.KeyDown(buttonToKey1[Bt]);
                Keyboard.KeyUp(buttonToKey0[Bt]);
            }*/
        }
        public void SetDefault () {
            buttonToKey0 = new Dictionary<EnuButtons, WindowsInput.Native.VirtualKeyCode>();
            buttonToKey0[EnuButtons.A] = WindowsInput.Native.VirtualKeyCode.VK_E;
            buttonToKey0[EnuButtons.B] = WindowsInput.Native.VirtualKeyCode.TAB;
            buttonToKey0[EnuButtons.X] = WindowsInput.Native.VirtualKeyCode.VK_R;
            buttonToKey0[EnuButtons.Y] = WindowsInput.Native.VirtualKeyCode.SPACE;
            buttonToKey0[EnuButtons.Back] = WindowsInput.Native.VirtualKeyCode.VK_T;
            buttonToKey0[EnuButtons.Start] = WindowsInput.Native.VirtualKeyCode.ESCAPE;
            buttonToKey0[EnuButtons.Guide] = WindowsInput.Native.VirtualKeyCode.NONAME;
            buttonToKey0[EnuButtons.LBump] = WindowsInput.Native.VirtualKeyCode.VK_K;
            buttonToKey0[EnuButtons.RBump] = WindowsInput.Native.VirtualKeyCode.VK_V;
            buttonToKey0[EnuButtons.LStick] = WindowsInput.Native.VirtualKeyCode.VK_J;
            buttonToKey0[EnuButtons.RStick] = WindowsInput.Native.VirtualKeyCode.VK_F;
            buttonToKey0[EnuButtons.DUp] = WindowsInput.Native.VirtualKeyCode.VK_Q;
            buttonToKey0[EnuButtons.DUpRight] = WindowsInput.Native.VirtualKeyCode.VK_1;
            buttonToKey0[EnuButtons.DRight] = WindowsInput.Native.VirtualKeyCode.VK_2;
            buttonToKey0[EnuButtons.DDownRight] = WindowsInput.Native.VirtualKeyCode.VK_3;
            buttonToKey0[EnuButtons.DDown] = WindowsInput.Native.VirtualKeyCode.VK_4;
            buttonToKey0[EnuButtons.DDownLeft] = WindowsInput.Native.VirtualKeyCode.VK_5;
            buttonToKey0[EnuButtons.DLeft] = WindowsInput.Native.VirtualKeyCode.VK_6;
            buttonToKey0[EnuButtons.DUpLeft] = WindowsInput.Native.VirtualKeyCode.VK_7;
            buttonToKey0[EnuButtons.LTrig] = WindowsInput.Native.VirtualKeyCode.NONAME;
            buttonToKey0[EnuButtons.RTrig] = WindowsInput.Native.VirtualKeyCode.NONAME;

            buttonToKey1 = new Dictionary<EnuButtons, WindowsInput.Native.VirtualKeyCode>();
            buttonToKey1[EnuButtons.A] = WindowsInput.Native.VirtualKeyCode.VK_E;
            buttonToKey1[EnuButtons.B] = WindowsInput.Native.VirtualKeyCode.TAB;
            buttonToKey1[EnuButtons.X] = WindowsInput.Native.VirtualKeyCode.VK_R;
            buttonToKey1[EnuButtons.Y] = WindowsInput.Native.VirtualKeyCode.SPACE;
            buttonToKey1[EnuButtons.Back] = WindowsInput.Native.VirtualKeyCode.VK_T;
            buttonToKey1[EnuButtons.Start] = WindowsInput.Native.VirtualKeyCode.ESCAPE;
            buttonToKey1[EnuButtons.Guide] = WindowsInput.Native.VirtualKeyCode.NONAME;
            buttonToKey1[EnuButtons.LBump] = WindowsInput.Native.VirtualKeyCode.VK_K;
            buttonToKey1[EnuButtons.RBump] = WindowsInput.Native.VirtualKeyCode.VK_V;
            buttonToKey1[EnuButtons.LStick] = WindowsInput.Native.VirtualKeyCode.VK_J;
            buttonToKey1[EnuButtons.RStick] = WindowsInput.Native.VirtualKeyCode.VK_F;
            buttonToKey1[EnuButtons.DUp] = WindowsInput.Native.VirtualKeyCode.VK_Q;
            buttonToKey1[EnuButtons.DUpRight] = WindowsInput.Native.VirtualKeyCode.VK_1;
            buttonToKey1[EnuButtons.DRight] = WindowsInput.Native.VirtualKeyCode.VK_2;
            buttonToKey1[EnuButtons.DDownRight] = WindowsInput.Native.VirtualKeyCode.VK_3;
            buttonToKey1[EnuButtons.DDown] = WindowsInput.Native.VirtualKeyCode.VK_4;
            buttonToKey1[EnuButtons.DDownLeft] = WindowsInput.Native.VirtualKeyCode.VK_5;
            buttonToKey1[EnuButtons.DLeft] = WindowsInput.Native.VirtualKeyCode.VK_6;
            buttonToKey1[EnuButtons.DUpLeft] = WindowsInput.Native.VirtualKeyCode.VK_7;
            //??
            buttonToKey1[EnuButtons.LTrig] = WindowsInput.Native.VirtualKeyCode.NONAME;
            buttonToKey1[EnuButtons.RTrig] = WindowsInput.Native.VirtualKeyCode.NONAME;
            axisToMouse0 = new Dictionary<EnuAxis,EnuMouse>();
            axisToMouse0[EnuAxis.LX] = EnuMouse.None;
            axisToMouse0[EnuAxis.LY] = EnuMouse.None;
            axisToMouse0[EnuAxis.RX] = EnuMouse.X;
            axisToMouse0[EnuAxis.RY] = EnuMouse.Y;
            axisToMouse0[EnuAxis.LTrig] = EnuMouse.RightBt;
            axisToMouse0[EnuAxis.LTrig] = EnuMouse.LeftBt;
            
        }
    }
    class JoyEmu {

       
        private Window_Main parentWindow;
        public Dictionary<JoyEmuProfile.EnuButtons, Boolean> buttonStates = new Dictionary<JoyEmuProfile.EnuButtons, Boolean>();
        public Dictionary<JoyEmuProfile.EnuButtons, Boolean> buttonStatesBefore = new Dictionary<JoyEmuProfile.EnuButtons, Boolean>();
        private JoyEmuProfile m_Profile = new JoyEmuProfile();
        // -1.0 to +1.0
        public Dictionary<JoyEmuProfile.EnuAxis, Double> axisStates = new Dictionary<JoyEmuProfile.EnuAxis, Double>();
        WindowsInput.InputSimulator inputSim = new WindowsInput.InputSimulator();
        public JoyEmu(Window_Main window)
        {
            // Stores the passed window as parentWindow for furtue use
            parentWindow = window;
            Init();

        }
        public void SetProfile(JoyEmuProfile Profile) {
            m_Profile = Profile;
        }
        private void Init() {
            foreach (JoyEmuProfile.EnuButtons i in Enum.GetValues(typeof(JoyEmuProfile.EnuButtons))) {
                buttonStates[i] = false;
                buttonStatesBefore[i] = false;
            }
            foreach (JoyEmuProfile.EnuAxis i in Enum.GetValues(typeof(JoyEmuProfile.EnuAxis))) {
                axisStates[i] = 0;
            }
        }
        /// <summary>
        /// This function should be called at the start of each cycle
        /// </summary>
        public void ResetState() {
        }

        public void UpdateStateNew() {
            m_Profile.DetectModifier(buttonStates);
            foreach (JoyEmuProfile.EnuButtons i in Enum.GetValues(typeof(JoyEmuProfile.EnuButtons))) {
                m_Profile.ButtonOn(i, buttonStates[i]);
            }

        }
        /// <summary>
        /// This function should be called after aquiring all inputs in each cycle.
        /// </summary>
        public void UpdateState() {
            m_Profile.ModActive = 0;
            if (buttonStates[JoyEmuProfile.EnuButtons.LBump]) {
                m_Profile.Mod1Delay++;  //??overflow
                if (m_Profile.Mod1Delay > 20) {
                    m_Profile.ModActive = 1;
                }
            } else {
                m_Profile.Mod1Delay = 0;
            }
            
            ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.LStick],WindowsInput.Native.VirtualKeyCode.VK_J);
            ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.RStick] ,WindowsInput.Native.VirtualKeyCode.VK_F);
            ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.LBump], WindowsInput.Native.VirtualKeyCode.VK_K);

            if (m_Profile.ModActive == 0) {
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.A] ,WindowsInput.Native.VirtualKeyCode.VK_E);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.B] ,WindowsInput.Native.VirtualKeyCode.TAB);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.X] ,WindowsInput.Native.VirtualKeyCode.VK_R);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.Y] ,WindowsInput.Native.VirtualKeyCode.SPACE);
                ButtonOn(false, WindowsInput.Native.VirtualKeyCode.VK_C);

                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.RBump], WindowsInput.Native.VirtualKeyCode.VK_V);
                ButtonOn(false, WindowsInput.Native.VirtualKeyCode.VK_H);

                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.Back], WindowsInput.Native.VirtualKeyCode.VK_T);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.Start], WindowsInput.Native.VirtualKeyCode.ESCAPE);
                ButtonOn(false, WindowsInput.Native.VirtualKeyCode.F9);
                ButtonOn(false, WindowsInput.Native.VirtualKeyCode.F5);

                ButtonOn(false, WindowsInput.Native.VirtualKeyCode.VK_8);
                ButtonOn(false, WindowsInput.Native.VirtualKeyCode.VK_9);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DUp], WindowsInput.Native.VirtualKeyCode.VK_Q);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DUpRight], WindowsInput.Native.VirtualKeyCode.VK_1);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DRight], WindowsInput.Native.VirtualKeyCode.VK_2);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DDownRight], WindowsInput.Native.VirtualKeyCode.VK_3);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DDown], WindowsInput.Native.VirtualKeyCode.VK_4);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DDownLeft], WindowsInput.Native.VirtualKeyCode.VK_5);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DLeft], WindowsInput.Native.VirtualKeyCode.VK_6);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DUpLeft], WindowsInput.Native.VirtualKeyCode.VK_7);
                MoveMouse(JoyEmuProfile.EnuAxis.RX, JoyEmuProfile.EnuAxis.RY);
            } else if (m_Profile.ModActive == 1) {
                //Todo should we call Keyup of the other modfiers ?
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.A], WindowsInput.Native.VirtualKeyCode.VK_E);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.B], WindowsInput.Native.VirtualKeyCode.TAB);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.X], WindowsInput.Native.VirtualKeyCode.VK_R);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.Y], WindowsInput.Native.VirtualKeyCode.VK_C);
                ButtonOn(false, WindowsInput.Native.VirtualKeyCode.SPACE);

                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.Back], WindowsInput.Native.VirtualKeyCode.F9);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.Start], WindowsInput.Native.VirtualKeyCode.F5);
                ButtonOn(false, WindowsInput.Native.VirtualKeyCode.VK_T);
                ButtonOn(false, WindowsInput.Native.VirtualKeyCode.ESCAPE);

                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.RBump], WindowsInput.Native.VirtualKeyCode.VK_H);
                ButtonOn(false, WindowsInput.Native.VirtualKeyCode.VK_V);

                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DUp], WindowsInput.Native.VirtualKeyCode.VK_8);
                ButtonOn(false, WindowsInput.Native.VirtualKeyCode.VK_Q);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DUpRight], WindowsInput.Native.VirtualKeyCode.VK_9);
                ButtonOn(false, WindowsInput.Native.VirtualKeyCode.VK_1);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DRight], WindowsInput.Native.VirtualKeyCode.VK_3);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DDownRight], WindowsInput.Native.VirtualKeyCode.VK_4);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DDown], WindowsInput.Native.VirtualKeyCode.VK_5);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DDownLeft], WindowsInput.Native.VirtualKeyCode.VK_6);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DLeft], WindowsInput.Native.VirtualKeyCode.VK_7);
                ButtonOn(buttonStates[JoyEmuProfile.EnuButtons.DUpLeft], WindowsInput.Native.VirtualKeyCode.VK_8);
                MouseScrollY(JoyEmuProfile.EnuAxis.RY);
                MoveMouse(JoyEmuProfile.EnuAxis.RX);
            }
            MouseButton(JoyEmuProfile.EnuButtons.LTrig, WindowsInput.MouseButton.RightButton);
            MouseButton(JoyEmuProfile.EnuButtons.RTrig, WindowsInput.MouseButton.LeftButton);

            MoveButton(JoyEmuProfile.EnuAxis.LX, JoyEmuProfile.EnuAxis.LY);

        }
        private void MouseButton(JoyEmuProfile.EnuButtons Bt, WindowsInput.MouseButton key) {
            //MouseButtons should not be send repeatedly ?
            if (buttonStates[Bt] && ! buttonStatesBefore[Bt] ) {
               // Console.WriteLine("On {0}", key);
               switch (key) {
                    case WindowsInput.MouseButton.LeftButton:
                        inputSim.Mouse.LeftButtonDown();
                        break;
                    case WindowsInput.MouseButton.RightButton:
                        inputSim.Mouse.RightButtonDown();
                        break;
                    //no middlebutton Down?
                    default:
                        break;
                }
            } else if (!buttonStates[Bt] && buttonStatesBefore[Bt] ) {
                switch (key) {
                    case WindowsInput.MouseButton.LeftButton:
                        inputSim.Mouse.LeftButtonUp();
                        break;
                    case WindowsInput.MouseButton.RightButton:
                        inputSim.Mouse.RightButtonUp();
                        break;
                    default:
                        break;
                }
 
              //  Console.WriteLine("Off {0}", key);
            }
            buttonStatesBefore[Bt] = buttonStates[Bt];
        }
        private void MouseScrollY(JoyEmuProfile.EnuAxis y) {
            if ((axisStates[y] * axisStates[y]) > (m_Profile.DeadZoneAnalogR * m_Profile.DeadZoneAnalogR)) {
                m_Profile.ScrollDelay++; //??overflow
            }
            //Todo: as parameter
            if (m_Profile.ScrollDelay > 8) {
                m_Profile.ScrollDelay = 0;
                if (axisStates[y] < 0) {
                   //?? Mouse.Scroll(Mouse.ScrollDirection.Down);
                    inputSim.Mouse.VerticalScroll(-1);
                } else {
                    inputSim.Mouse.VerticalScroll(1);
                }
            }
        }
        
        public void ButtonOn(Boolean On, WindowsInput.Native.VirtualKeyCode Char) {
            if (On) {
                inputSim.Keyboard.KeyDown(Char);
            } else {
                //??Keyboard.KeyUp(Char);
                inputSim.Keyboard.KeyUp(Char);
            }
        }
        private Double RescaleAnalog(Double x, Double DeadZone) {
            Double ret = 0;
            if ((x * x) > (DeadZone * DeadZone)) {
                if (x >= 0) {
                    ret = (1 / (1 - DeadZone)) * (x - DeadZone);
                } else {
                    ret = (1 / (1 - DeadZone)) * (x + DeadZone);
                }
            }
            return ret;
        }
        private void MoveMouse(JoyEmuProfile.EnuAxis x) {
            if ((axisStates[x] * axisStates[x]) > (m_Profile.DeadZoneAnalogL * m_Profile.DeadZoneAnalogL)) {
                int mouseVelX = (int)(m_Profile.maxMouseVelocityX * RescaleAnalog(axisStates[x], m_Profile.DeadZoneAnalogL));
                //Mouse.MoveRelative(mouseVelX, 0);
                inputSim.Mouse.MoveMouseBy(mouseVelX, 0);
                
            }

        }
        private void MoveMouse(JoyEmuProfile.EnuAxis x, JoyEmuProfile.EnuAxis y) {
            if ((axisStates[x] * axisStates[x] + axisStates[y] * axisStates[y]) > (m_Profile.DeadZoneAnalogL * m_Profile.DeadZoneAnalogL)) {
                int mouseVelX = (int)(m_Profile.maxMouseVelocityX * RescaleAnalog(axisStates[x], m_Profile.DeadZoneAnalogL));
                int mouseVelY = (int)(m_Profile.maxMouseVelocityY * RescaleAnalog(axisStates[y], m_Profile.DeadZoneAnalogL));
                //Mouse.MoveRelative(mouseVelX, -mouseVelY);
                inputSim.Mouse.MoveMouseBy(mouseVelX, -mouseVelY);
            }
        }
        private void MoveButton(JoyEmuProfile.EnuAxis x, JoyEmuProfile.EnuAxis y) {
            Boolean leftStickOnX = StickOn(axisStates[x], m_Profile.DeadZoneDigitalL);
            Boolean leftStickOnY = StickOn(axisStates[y], m_Profile.DeadZoneDigitalL);
	        double leftStickDir	= StickDirection(	axisStates[x], axisStates[y] );
            ButtonOn((leftStickOnY && (leftStickDir > Math.PI * (1.0 / 8.0)) && 
                (leftStickDir < Math.PI * (7.0 / 8.0))), WindowsInput.Native.VirtualKeyCode.VK_W);
            ButtonOn((leftStickOnX && (leftStickDir > Math.PI * (5.0 / 8.0)) && 
                (leftStickDir < Math.PI * (11.0 / 8.0))), WindowsInput.Native.VirtualKeyCode.VK_A);
            ButtonOn((leftStickOnY && (leftStickDir > Math.PI * (9.0 / 8.0)) && 
                (leftStickDir < Math.PI * (15.0 / 8.0))), WindowsInput.Native.VirtualKeyCode.VK_S);
            ButtonOn((leftStickOnX && ((leftStickDir > Math.PI * (13.0 / 8.0)) || 
                (leftStickDir < Math.PI * (3.0 / 8.0)))), WindowsInput.Native.VirtualKeyCode.VK_D);
            
        }

        private Boolean StickOn (Double x, Double y, Double DeadZone) {
            return ((x*x) + (y*y))>(DeadZone*DeadZone);
        }
        private Boolean StickOn(Double x, Double DeadZone) {
            return ((x * x) ) > (DeadZone * DeadZone);
        }
        // in rad 0.. 2 PI
        private double StickDirection (double x, double y) {
            double dir = 0;          
            if (x > 0 ) {
                dir = Math.Atan(y / x);
                if ( y >= 0) {
                    //1.quadrant
                } else if (y < 0) {
                    // 4.quadrant
                    dir = Math.PI + Math.PI + dir;
                }
            } else if (x < 0) {
                dir = Math.Atan(y / x);
                if (y >= 0) {
                    //2.quadrant
                    dir = Math.PI + dir;
                } else if (y < 0) {
                    // 3.quadrant
                    dir = Math.PI + dir;
                }
            } else { //x==0
                if (y >= 0) {
                    dir = Math.PI /2;
                } else if (y < 0) {
                    // tan is -
                    dir = Math.PI *3/2;
                }
            }
	        return dir;
        }
    }
}
