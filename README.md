# SoftScaleHead
Remote scale head with zeroing capability for industrial weigh scales (Rice Lake, Cardinal)
![SoftScaleHead Screenshot](/SoftScaleHead.png?raw=true)

## Download
The latest version can be downloaded at the [Github Releases](https://github.com/robertlarue/SoftScaleHead/releases) page.

## Usage
The application is intended to be launched through customized shortcuts.
Right-click on SoftScaleHead.exe and create a shortcut. Edit the properties of the shortcut so that the Target includes the setup parameters.

    SoftScaleHead /a <IP_Address> /[/p Port] [/z Zero_Command_File] [/d Scale_Description] [/x X_pos] [/y Y_pos] [/w Width] [/h Height]
    /a    IP Address of the serial to Ethernet converter or internal network card of the scale head
    /p    [Optional] TCP port to use when connecting over Ethernet (Defaults to 10001)
    /z    [Optional] Enable zeroing of the scale by including this flag. Sends the commands file to the scale head when the zero button is clicked.
    /d    [Optional] Scale Description which is shown in the title bar of the main window
	/x    [Optional] X position of the window on first startup (left side screen is 0), subsequent startups will use the saved x position
	/y    [Optional] Y position of the window on first startup (top side screen is 0),  subsequent startups will use the saved y position
	/w    [Optional] Width of the window in pixels on first startup, subsequent startups will use the saved width
	/h    [Optional] Width of the window in pixels on first startup, subsequent startups will use the saved width

## Example
Connect to a scale called "Main Scale" with an Ethernet adapter at 192.168.1.101 on port 4001, and enable zeroing the scale.

    SoftScaleHead /a 192.168.1.101 /p 4001 /z CardinalZero.txt /d "Main Scale"

Type the above line into the Target field of the shortcut properties.

## GUI
Right-Click the scale display window to open the GUI menu. From here, you can enable zeroing and change the scale colors.
When choosing a zero command file, choose between the two that are included: RiceLakeZero.txt or CardinalZero.txt.

## Config File
All scale settings including IP Address, Port, Position, and Color will be saved in SoftScaleHead.exe.config
