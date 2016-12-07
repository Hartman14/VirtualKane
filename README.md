Note for anyone using this code: I did this for a school course and do not plan on continuing with it. So, if you wish to use this as a 
starting point and continue building on what I have here feel free as this is for the public.

README & SUMMARY
----------------------------------------------------------------------------------------------------------------------------
Author: Jordan Hartman
----------------------------------------------------------------------------------------------------------------------------

Interface Requirements:
1)	Visual Studios 2013 IDE
2)	Arduino IDE
3)	Virtual Kane Hardware

---------------------------------------------------------------------------------------------------------------------------
How to Use:

Step 1: Plug Virtual Kane hardware’s’ USB connector into computers USB Port.

Step 2: (On your Arduino IDE) hit the upload icon to send your code for your hardware into its 
RAM

Step 3: Wait a few seconds for the code to begin running, and then start up the virtual interface 
in Visual studios. 

Step 4: Navigate into Diagnostics to confirm the hardware has calibrated. 

Step 5: Click the Run Sensor Read button and if the values aren’t updating repeat Step 3, 4 and 
5 as the hardware isn’t calibrated yet (unfortunate step, but this is a result of low level 
hardware), may take 1- 3 tries. If the values are updating, then Repeat Step 3 and skip to Step 6.

Step 6: Navigate to Quick Settings.

Step 7: Set enabled variables.

Step 8: Navigate to Diagnostics.

Step 9: Set Ranges and edit enabled variables if wanted.

Step 10: Press Run Sensor Read and view Real time values.

Notice: Unfortunately, if you wish to change settings, you must repeat 
Steps 3-9 or Steps 3, 8 and 9.


---------------------------------------------------------------------------------------------------------------------------
Detailed Table of Contents:

I. File List

II. Software Interface

A.	Start-up.cs
	1-2: Physical Functions
		-Code Functions
		
B.	Diagnostics.cs
1-5: Physical Functions
		-Code Functions
		
C.	Quick_Settings.cs
1-3: Physical Functions
		-Code Functions
		
D.	valueClass.cs
	1-4: Code Functions
	
E.	Echo1(Arduino File)
	1-5: Code Functions
	
III. Hardware Interface

IV. Limitations from Hardware

V. Highlight Reel
*. Enlarged Hardware Image

---------------------------------------------------------------------------------------------------------------------------
I. File List within Virtual Kane Interface

Diagnostics.cs
Settings.cs
Start-up.cs
valueClass.cs
Echo1(Arduino File)

----------------------------------------------------------------------------------------------------------------------------
II. Software Interface

A.	Start-up.cs
         
This file generates the initial form for the software interface.  On the form there are two
buttons that are generated to allow for navigation into the Quick Settings and Diagnostics options.

1.	Quick Settings button
		When pressed traverses between Homepage and Quick Settings Page
		
Code Functions:

			a. Quick_Setting_Click: creates an instance of Quick Setting Page
		
2.	Diagnostics button 
		When pressed traverses between Homepage and Diagnostics Page

Code Functions:

			a. Diagnositc_Click: creates an instance of Diagnostics Page

    
B.  Diagnostics.cs
 
This file generates the Diagnostics Page that displays all incoming data from the
hardware to the user.  It also allows the user to set the ranges of their sensor interface 
for a more personalized feel.

1.	Quick Settings Button 
		When pressed traverses between the Diagnostics page and Quick Settings Page
		
Code Functions:

	a. QS_Click: creates an instance of Quick Setting Page
		

2.	Run Sensor button 
		When pressed prompts the interface to begin realtime display of sensor distance 
readings and vibration setting achieved. After pressed user will notice the sensor dist. and the Vibration read values update in real time (4 times per second). Currently freezes form due to high Port traffic created between hardware and software, but although frozen it updates the display with real time values (Unfortunately without bypassing the Port entirely, which can’t happen due to hardware limitation, this is unavoidable) (Addressed again in Issues Section) 

Code Functions:

			a. RunReader_Click: starts the clock for the RefreshValues function
		

			b. RefreshValues: acts as an intermediary between the clock pings and 
RenewValues function

c. RenewValues: Checks port status and if open closes it. Then calls
theInit function to create a new port path (this prevents updating from 
opening multiple paths) and finally calls the kingParser function.

d. Init: creates the access point for the port and opens the connection.

e. kingParser: attempts to retrieve data by calling the retrieveValues function

f. retrieveValues: reads the outputs of the hardware (string value) from the port connection and sends data to ParseValues function

g. ParseValues: sends input(string) into a char array and begins to parse the data using the ValidInput and canBeInt functions. If values pass through these functions they are then sent to be stored. Once stored the checkStoredValue function, and if passed, it then updates the chart value assigned and calls the setVib function.

h. ValidInput: sends the parsed char to the function canBeInt and checks to see if sensor is enabled.

i. canBeInt: checks to see if char value can be converted into an int.

j. checkStoredValue: checks to see that the value stored is greater than 0 and less that 240(20ft) as the sensors range is upto 240.

k. setVib: takes the stored value and the sensor number and determines which values to populate in the isVibing function

l. isVibing: checks to see which vibrating setting is currently active.

3.	Back button (Pictured above: See Section II.B)
		When pressed navigates page back to Homepage

Code Functions:

			a. Back_Click: Navigates back to the Home Page

4.	6 Range setting comboboxes
	Give user the option to change the vibration range values for user to personalize 
their activation space.

Code Functions:

			a. CR1_SelectedIndexChanged & CR2_SelectedIndexChange: changes 
the values of the labels for the minimum range variable of the Middle Range portion.
		
b. MR1_SelectedIndexChanged & MR2_SelectedIndexChange: changes the values of the labels for the minimum range variable of the Long Range portion.
		
5.	Sensor/Vibration Enabling checkboxes
		Allows user to disable and enable sensors 
		
			Code Functions:

a. EnableSens1_CheckChanged & EnableSens2_CheckChanged: checks if sensors are changed to enable/disabled. If changed to disabled, changed associated sensor and Vibration motor to disabled as well if it was previously enabled. If changed to enabled, calls RenewValues function (reference section II.B.2.c) in Diagnostics.cs and enables both associated sensor and motor. Then calls the setSensorActive and setVibActive functions from the valueClass.cs(See Section II.D.3&4). 

b. EnableVib1_CheckChanged & EnableVib2_CheckChanged: checks if motors are changed to enable/disabled. If changed to disabled, changed associated Vibration motor to disabled. If changed to enabled, enables associated motor and calls RenewValues function (See section II.B.2.c) as well as the setVibActive function from the valueClass.cs(See Section II.D.3).
		
     C.  Quick_Settings.cs
 
(Diagnostics.cs form view)
Allows user to quickly enable and disable sensors and vibration motors.

1. Allow user check and uncheck enabled sensors and motors 


a. EnableSens1_CheckChanged & EnableSens2_CheckChanged: checks if sensors are changed to enable/disabled. If changed to disabled, changed associated Vibration motor to disabled as well if it was previously enabled. If changed to enabled, calls setSensorActive and setVibActive functions from the valueClass.cs(See Section II.D.3&4). 

B. EnableVib1_CheckChanged & EnableVib2_CheckChanged: checks if motors are changed to enable/disabled. If changed to disabled, changed associated Vibration motor to disabled. If changed to enabled, enables associated motor and calls the setVibActive function from the valueClass.cs(See Section II.D.3).

2. Back button (Pictured above: See Section II.C)
		When pressed navigates page back to Homepage

Code Functions:

			a. Back_Click: Navigates back to the Home Page

		3. Quick Settings button
		When pressed traverses between the Diagnostics page and Quick Settings Page
		
Code Functions:

			a. QS_Click: creates an instance of Quick Setting Page

D. valueClass.cs
	Acts as an intermediary for Diagnostics.cs and Quick_Settings.cs

	      	Code Functions:

1. setSensorActive: sets the active sensor based on current data in case of form change.

2. getSensorActive: retrieves active sensor from stored data (whether 
enabled/disabled)

3. setVibActive: sets the active vibration motor based on current data.

4. getSensorActive: retrieves the active sensor from stored data (whether 
enabled/disabled).

	E. Echo1(Arduino file (variation of Java code))
This file runs the hardware algorithm that activates the ultrasonic sensors and vibration 
motors.  It also sends the retrieved data to a port where it is then picked up by the virtual interface.

	Code Functions:

	1. setup: creates port access code

2. loop: tells hardware what it is it needs to be doing, calls sendSignal, printOutputs and incoming functions.

	3. sendSignal: sends an ultrasonic wave from a sensor to retrieve data.

4. printOutputs: prints outputs calculate from the wave of distance from obstruction in inches and writes it to the Port for the virtual interface to read.

5. incoming: determines which vibration setting to initiate based on distance
values gained from the ultrasonic wave.


----------------------------------------------------------------------------------------------------------------------------
III. Hardware Interface
 
A. Arduino Mega 2560 Motherboard
Stored and ran Echo1(See Section II.E) after receiving it through a USB cable. Then by sending signals between Ultrasonic sensors and vibrations motors operated the code. 


----------------------------------------------------------------------------------------------------------------------------
IV. Limitations of Hardware that were apparent

	A. Lack of permanent internal storage 
		
1. Caused the hardware to require a new download of the Echo1 file (See Section II.E) upon receiving power.

2. Requires settling time for code to calibrate Ultrasonic Sensors

	B. Port communication 
		
		1. Causes Port reference errors when more than one instance tries to connect 
with the hardware.

	Attempted Fixes:

	
a. Use aysnc function: failed as the background thread would trip a Port 
access error.  This was unfixable as it was later discovered to be a flaw in the hardwares Port to where once it registers its communication point(thread), it would only allow access from this new point(thread).

b.Use clock iterations and update on pings(Current Version Uses This): This solved the Port error but still caused the form to freeze everything but the updating values. This was caused by the traffic between the port and virtual interface to flood the interface and freeze the form.


----------------------------------------------------------------------------------------------------------------------------
V. Highlight Reel

	A. kingParser function (See Section II.B.2.e) as it successfully housed and ran the 
communication variant that interpreted ‘Arduino Java’ into ‘C#’.

	B. loop function (See Section II.E.2) as it successfully ran the hardware in regards to 
communicating with the Port and activating components.

----------------------------------------------------------------------------------------------------------------------------

 
