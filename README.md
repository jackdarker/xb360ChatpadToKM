# xb360ChatpadToKM 
# = Xbox 360 Controller+Chatpad to Keyboard+Mouse


Small program to use wireless XBox360-Controller with Chatpad on windows 7 by emulating Mouse&amp;Keyboard

## Intention of this tool			

There is actually no Microsoft driver to support the chatpad on Windows-PC. The intention of this tool is to make it possible to use the chatpad just like a normal keyboard.
The inputs of the controller itself are also mapped to keyboard/mouse, making it possible to use the controller for games/applications that dont support the Microsoft driver directly.
The headphone and other controller-accessory is currently not supported.
Force feedback is not supported.

Please note: If you dont want to use the chatpad, I would suggest to use Freepie (http://andersmalmgren.github.io/FreePIE/) instead. It has better performance and more possibilitys (scripting).
If you want to use the chatpad but would like to map the controller inputs to a virtual vJoy device, I would suggest to use  KytechN24's xbox360wirelesschatpad (see Credits below).
There is also the famous ChatpadSuperDriver.

Cons:
- it is not possible to just map the chatpad to keyboard and leave the controller as it is (Xinput device). The driver has to replace the functionallity of all devices connected to the receiver, sadly. 
I would like to support a Xinput-Emulation mode but dont know how to do this. Also some games disable keyboard controls if they detect a working Xbox-Controller. 
- Only one controller at a time is supported. Because Windows/Games cannot differentiate between multiple mouse/keyboards, all controllers would need to be mapped to one keyboard/mouse. Okay, yes there are games that could be played by 2 players with one keyboard...
- The input, especially of the mouse, might be a little bit jumpy. The conversion of the controller input to keyboard/mouse is done in a background thread. The system might prepend the execution of the thread if it has other things to do.
- The program is not aware of the state of your game. F.e. It cannot see if you are in a barter-menu instead of the normal gamescreen. If the menu uses different Confirm/Cancle-keys, it is not possible to use the same buttons you would use in normal game modes (I saw this in Elder Scrolls Online). Also force fedback is not supported.  
- No makros & combos, f.e. Pressing button A resulting in sending „A“+wait 200ms +sending „A“; 

Pros:
- you can define modifier-buttons that switch between the mapping of the other buttons. This seems to be obsolete if you have a chatpad with all keys available. But I find it much easier to use such button-combinations instead of fumbling with the tiny chatpad keys.
- You can modify the mapping, deadzones and speedtranslation of the buttons/axis by default, the left stick emulates keypresses that can be mapped to Arrows or WASD-keys for movement commands. But you could also map it to some analog axis (f.e. Mouse-wheel) at the same time. The same applies to the trigger-buttons. 


## What you need				

- Windows 7 64bit; didnt test it on newer systems
- download the program from https://github.com/jackdarker/xb360ChatpadToKM/tree/master/Release
- (original) Xbox 360 wireless controller for PC 
- a Chatpad; You dont need the original microsoft version; ( I'm using this: Neuftech® 47 Button Wireless Chatpad Keyboard )
- libusb-Driver  v1.2.6 (https://sourceforge.net/projects/libusb-win32/files/)
- Microsoft Accessory Driver for Xbox 360 controller (for uninstalling)
- usbdeview (http://www.nirsoft.net/utils/usb_devices_view.html) (for uninstalling)

Warning: It is necessary to force the system to use the libusb-driver for the controller instead of the microsoft driver. This will of course make it impossible for games to use the controller in the „normal“ way. You will have to play all games with this tool unless you uninstall the libusb-driver and reinstall the Microsoft driver. I suggest to read the uninstall-section to get a feeling for this.


## Installation				


Plug-In and Install the Native Drivers for your XBOX 360 Wireless Receiver. Check if the controller is working in your game.

Installing the LibUSB Driver
- Extract the LibUSB -Archive to a Directory.
- Execute the following as an Admin: <libusbDirectory>/bin/Architecture/install-filter-win.exe
- Select "Install a Device Filter".
- Select the item with Description "Xbox 360 Wireless Receiver for Windows".
- Select "Install" then after the confirmation box select Cancel.
- Execute the following path: Directory/bin/inf-wizard.exe
- Select "Next".
- Select the item with Description "Xbox 360 Wireless Receiver for Windows".
- Select "Next" then save the new .inf somewhere.
- Select "Install Now" to install the driver.
- Select OK at the confirmation, the LibUSB driver should now be installed.

Register the controller to the receiver
- press and hold the connect-button on the receiver until it LED flashes
- press and hold the connect-button on the controller until the rotating-LED animation starts. 
- The LED 1 on the controller will blink one time after registering and will go off.

Install the program
- Extract the program -Archive to a Directory
- Run the .exe. 
- The program should show the message „Xbox 360 Wireless Receiver Connected“
- press the guide-button on the controller to activate it.  
- The program should show the message „Controller 1 connected“ and LED1 should be on 


## Uninstallation				

- stop the program
- Execute the following as an Admin: <libusbDirectory>/bin/Architecture/install-filter-win.exe
- Remove the device-filter
- run usbdeview and uninstall all devices with Xbox360 receiver in the description
- search in <windows>/system32 for libusb0* and delete those entrys
- uninstall and reinstall the Microsoft Accessory Driver for Xbox 360 controller
- check in the system/device-manager if the receiver was properly installed with the Microsoft driver


## Create a mapping-profile		

Please note: avoid Shift/Ctrl/Alt – keys since  they are not properly processed by the mapper

Default Profile (for Skyrim / Fallout)

Button       Mapped to ...   Recommed use	(Without Modifier pressed)
- X   R     Sheath/ reload	
- Y		Space		jump
- A		E		Activate
- B		Tab		inventory
- LBump		J		Modifier 1/Sprint	
- RBump		V		Shouts/VATS
- LTrigger	Mouse Right	Attack
- RTrigger	Mouse Left	Aim/Block
- LStick Press	J		Sneak
- RStick Press	F		View toggle
- Back		T		Wait-Menu
- Start		Escape		Pause menu
- Guide
- digipad		Q / 1..7 (clockwise)	Quick-Favorite
- LStick		WASD 			movement keys (Forward/Backward/strafing)
- RStick		Mouse coordinate	Aiming

Button	       Mapped to ...		Recommed use	(With Modifier 1 pressed)
- X		1)
- Y		C		BulletTime
- A		1)
- B		1)
- LBump		J		Modifier 1
- RBump		H
- LTrigger	1)
- RTrigger	1)
- LStick Press	1)
- RStick Press	1)
- Back		F9		quickload
- Start		F5		quicksave
- Guide
- digipad		8,9  (clockwise)	Quick-Favorite	
- LStick		1)
- RStick		X-MouseCoord. / y- Scroll	scrolling in menus / zooming

1) same mapping as with no modifier


## Version					

0.1.0	2016.02.25
- replaced InputManager dependency with InputSimulator
- removed vJoy dependency 
- only 1 controller supported
- profile switching not working


## Credits					

This program is based on the modified KytechN24's xbox360wirelesschatpad (https://github.com/KytechN24/xbox360wirelesschatpad).
The major difference to this program is that it maps to a virtual Joystick and supports multiple controllers.

This program uses InputSimulator-Library branch with mouse support http://??
