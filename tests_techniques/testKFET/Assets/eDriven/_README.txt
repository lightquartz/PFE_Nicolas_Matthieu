-------------------------------------------------------
eDriven.Gui - Unity3d RIA Solution
Copyright © Danko Kozar 2010-2013. All rights reserved.
http://edrivengui.com/
-------------------------------------------------------

Thank you for trying eDriven.Gui! :)

For any questions, suggestions or community help, please visit the eDriven Q&A forum thread: 
http://forum.unity3d.com/threads/148424-eDriven-Q-amp-A


-------------------------------------------------------
Terms and Conditions
-------------------------------------------------------

Use of this version of eDriven.Gui must be limited to evaluation, educational purposes or component development only, and it is not to be used for commercial purposes. 

The content of "Assets/eDriven/Libs" folder (the assemblies) may not be changed in any way.

All watermarks in the application will be removed after purchasing and installing the commercial version.

You may purchase the commercial version in the Unity Asset Store: http://u3d.as/content/adjungo/e-driven-gui/36Q


-------------------------------------------------------
Disclaimer of Warranties
-------------------------------------------------------

1. YOU EXPRESSLY UNDERSTAND AND AGREE THAT YOUR USE OF THIS PACKAGE IS PROVIDED “AS IS” AND “AS AVAILABLE” WITHOUT WARRANTY OF ANY KIND, TO THE MAXIMUM EXTENT PERMITTED BY APPLICABLE LAW. 
IN PARTICULAR, ADJUNGO, ITS SUBSIDIARIES, HOLDING COMPANIES AND AFFILIATES, AND ITS LICENSORS DO NOT REPRESENT OR WARRANT TO YOU THAT:

	(A) YOUR USE OF THE PACKAGE WILL MEET YOUR REQUIREMENTS,
	(B) YOUR USE OF THE PACKAGE WILL BE UNINTERRUPTED, TIMELY, SECURE OR FREE FROM ERROR, 
	(C) ANY INFORMATION OBTAINED BY YOU AS A RESULT OF YOUR USE OF THE PACKAGE WILL BE ACCURATE OR RELIABLE, AND
	(D) THAT DEFECTS IN THE OPERATION OR FUNCTIONALITY OF ANY SOFTWARE PROVIDED TO YOU AS PART OF THE PACKAGE WILL BE CORRECTED

2. YOUR USE OF THE PACKAGE IS AT YOUR OWN RISK AND YOU ARE SOLELY RESPONSIBLE FOR ANY DAMAGE TO YOUR COMPUTER SYSTEM, OR OTHER DEVICE, OR LOSS OF DATA THAT RESULTS FROM SUCH USE.


-------------------------------------------------------
Getting started
-------------------------------------------------------

1. DLL-s
--------

eDriven (core, free) package, normally contains 3 DLL-s: eDriven.Core, eDriven.Audio and eDriven.Networking.
These libraries are already contained inside the eDriven.Gui package, so no need to install eDriven (core) separatelly. But, if you have eDriven (core) already installed, just delete eDriven (core) DLL-s (if you don't do that, you could have clashes).

In general, DLL-s that has to be present inside your project are:
- eDriven.Animation
- eDriven.Audio
- eDriven.Core
- eDriven.Core.Designer
- eDriven.Core.Editor
- eDriven.Gui
- eDriven.Networking


2. Reducing the build size
--------------------------

If not using features of some DLLs, you are free to delete them from the project (to reduce the build size).
For instance, if not using designer, you could delete eDriven.Core.Designer.dll and eDriven.Core.Editor.dll from the project. 
Or, if not using networking, remove eDriven.Networking.


3. Editor folder
----------------

If using eDriven.Core.Editor.dll, it has to be inside the folder named Editor, or else you get errors.


4. Compromised display quality
------------------------------

When exporting to web or other platforms and noticing problems with graphics (dark, blurry or missing GUI elements), go to Edit -> Project Settings -> Quality.
If the quality level for your platform is set to Fastest, set it to Fast or any other setting.


5. Documentation
----------------

A number of starting demo scenes with corresponding scripts are located inside the eDriven.Gui/Demo folder.

Use "Help -> eDriven" menu to get more information.

You could use the folowing video material to get up and running quickly: http://www.youtube.com/watch?v=5-lrbAV9brk

eDriven.Gui homepage: http://edrivengui.com/
eDriven API: http://edriven.dankokozar.com/api/1-0/
eDriven.Core source: https://github.com/dkozar/eDriven
eDriven WebPlayer demos: http://edrivenunity.com
eDriven Q&A forum thread: http://forum.unity3d.com/threads/148424-eDriven-Q-amp-A

Have fun! ^_^


6. Licensing
----------------

eDriven.Gui is (as all the Unity Asset Store assets are) licenced PER-USER, not per-project, per-game or per-team.
Please be fair and purchase a separate license for each team member.
This way I could spend more time with eDriven (you are actually speeding up its further development).

Thank you!

Danko Kozar