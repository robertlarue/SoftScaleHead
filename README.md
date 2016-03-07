# SoftScaleHead
Remote scale head with zeroing capability for industrial weigh scales (Rice Lake, Cardinal)

##Download
The latest version can be downloaded at the [Github Releases](https://github.com/robertlarue/SoftScaleHead/releases) page.

##Usage
The application is intended to be launched through customized shortcuts.
Right-click on SoftScaleHead.exe and create a shortcut. Edit the properties of the shortcut so that the Target includes the setup parameters.

    SoftScaleHead /a <IP_Address> /[/p Port] [/z Enable_Zero] [/d Scale_Description]
    /a    IP Address of the serial to Ethernet converter or internal network card of the scale head
    /p    TCP port to use when connecting over Ethernet (Optional, defaults to 10001)
    /z    Enable zeroing of the scale. Sends the commands listed in zerocommands.txt to the scale head
    /d    Scale Description which is shown in the title bar of the main window

##Example
Connect to a scale called "Main Scale" with an Ethernet adapter at 192.168.1.101 on port 4001, and enable zeroing the scale.

    SoftScaleHead /a 192.168.1.101 /p 4001 /z /d "Main Scale"
