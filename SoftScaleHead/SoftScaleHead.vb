Imports System.Net.Sockets
Imports System.IO
Imports System.Configuration
Public Class SoftScaleHead
    Public FormIsClosing As Boolean = False
    Public ScaleIPAddress As String = ""
    Public ScalePort As String = "10001"
    Public ScaleOnline As Boolean = True
    Public ScaleClient As TcpClient = Nothing
    Public ScaleStream As NetworkStream = Nothing
    Public ScaleStreamReader As StreamReader = Nothing
    Public ZeroEnabled As Boolean = False
    Public ZeroCommandFile As String = My.Application.Info.DirectoryPath & "\" & "RiceLakeZero.txt"
    Public ArgsFound As Boolean = False
    Public LastScaleReading As String = ""
    'Public ArgsFound As Boolean = False
    Private Sub SoftScaleHead_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If ArgsFound Then
            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            Dim existingSection As ScaleSettings = config.GetSection("_" & ScaleIPAddress & ScalePort)
            If IsNothing(existingSection) Then
                Dim newConfigSection As ScaleSettings = New ScaleSettings
                newConfigSection.IPAddress = ScaleIPAddress
                newConfigSection.Location = Me.Location
                newConfigSection.Size = Me.Size
                newConfigSection.ZeroEnabled = ZeroEnabled
                newConfigSection.ZeroCommandFile = ZeroCommandFile
                newConfigSection.Description = Me.Text
                newConfigSection.Port = ScalePort
                newConfigSection.BackGroundColor = Color.Black
                newConfigSection.TextColor = Color.Chartreuse
                config.Sections.Add("_" & ScaleIPAddress & ScalePort, newConfigSection)
                config.Save(ConfigurationSaveMode.Modified)
                ConfigurationManager.RefreshSection("ScaleSettings")
            Else
                ScaleIPAddress = existingSection.IPAddress
                Me.Location = existingSection.Location
                Me.Size = existingSection.Size
                ZeroEnabled = existingSection.ZeroEnabled
                EnableZeroingToolStripMenuItem.Checked = ZeroEnabled
                ZeroButton.Visible = ZeroEnabled
                ZeroCommandFile = existingSection.ZeroCommandFile
                CommandFileNameToolStripMenuItem.Text = Path.GetFileName(existingSection.ZeroCommandFile)
                Me.Text = existingSection.Description
                ScalePort = existingSection.Port
                Panel1.BackColor = existingSection.BackGroundColor
                ScaleWeight.BackColor = existingSection.BackGroundColor
                Dim ControlColor As Color
                If existingSection.BackGroundColor.GetBrightness < 0.1 Then
                    ControlColor = Lighten(existingSection.BackGroundColor, 0.1)
                Else
                    ControlColor = Darken(existingSection.BackGroundColor, 0.1)
                End If
                Me.BackColor = ControlColor
                ScaleWeight.ForeColor = existingSection.TextColor
            End If

            Application.DoEvents()
            ReadFromScale()
        End If
    End Sub

    Private Sub ReadFromScale()
        While Not FormIsClosing
            If ScaleOnline Then
                If ScaleStreamReader Is Nothing Then
                    ConnectScale(ScaleIPAddress, ScalePort)
                Else
                    ReadScaleStream()
                End If
            Else
                If ConnectionTimer.Enabled = False Then
                    ConnectionTimer.Start()
                End If
            End If
            Application.DoEvents()
        End While
    End Sub

    Private Sub SoftScaleHead_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        FormIsClosing = True

    End Sub
    Private Sub ReadScaleStream()
        Try
            ' Get a client stream for reading and writing.


            ' Receive the TcpServer.response.
            ' Buffer to store the response bytes.
            ' Read the first batch of the TcpServer response bytes.
            Dim scaleChar As Char = ""
            Do
                scaleChar = Convert.ToChar(ScaleStreamReader.Read())
                If scaleChar = vbCr Then
                    Exit Do
                End If
            Loop
            Dim scaleLine As String = ""
            Do
                scaleChar = Convert.ToChar(ScaleStreamReader.Read())
                If scaleChar = vbCr Then
                    Exit Do
                Else
                    scaleLine += scaleChar
                End If
            Loop
            scaleLine = scaleLine.Replace(vbLf, "")
            scaleLine = scaleLine.Replace(Convert.ToChar(2) & " ", "")
            scaleLine = scaleLine.Replace(Convert.ToChar(2) & "- ", "-")
            If scaleLine.Contains("M") Then
                MotionLight.BackColor = Color.Red
                MotionLight.ForeColor = Color.White
            Else
                MotionLight.BackColor = Color.FromKnownColor(KnownColor.DimGray)
                MotionLight.ForeColor = Color.FromKnownColor(KnownColor.ControlText)
            End If
            If scaleLine.Contains("Z") Then
                ZeroButton.BackColor = Color.Chartreuse
            Else
                ZeroButton.BackColor = Color.FromKnownColor(KnownColor.DimGray)
            End If

            scaleLine = scaleLine.Replace("L", "")
            scaleLine = scaleLine.Replace("B", "")
            scaleLine = scaleLine.Replace("K", "")
            scaleLine = scaleLine.Replace("G", "")
            scaleLine = scaleLine.Replace("Z", "")
            scaleLine = scaleLine.Replace("M", "")
            scaleLine = scaleLine.Replace("O", "")
            scaleLine = scaleLine.Replace("C", "")
            If scaleLine.StartsWith("DIN") Then
                scaleLine = LastScaleReading
            End If
            If scaleLine.Length > 2 And scaleLine <> LastScaleReading Then
                HideErrorDropdown()
                ScaleWeight.Text = scaleLine
                Debug.WriteLine(scaleLine)
                LastScaleReading = scaleLine
            End If
        Catch e As ArgumentNullException
            Debug.WriteLine("ReadScaleStream ArgumentNullException: " & e.Message)
            Application.Exit()
            End
        Catch e As IOException
            Try
                Debug.WriteLine("Pinging " & ScaleIPAddress)
                ScaleOnline = My.Computer.Network.Ping(ScaleIPAddress, 1000)
            Catch
                ScaleOnline = False
            End Try
            If ScaleOnline Then
                ScaleWeight.Text = "SERIAL"
                ShowErrorDropdown("No serial data received. Check connection.")
            Else
                ScaleWeight.Text = "DISCONN"
                ShowErrorDropdown("Network disconnected. Check connection.")
                ScaleStream.Close()
                ScaleStream = Nothing
                ScaleClient.Close()
                ScaleClient = Nothing
                ScaleStreamReader = Nothing
            End If
            LastScaleReading = ""
            Debug.WriteLine("ReadScaleStream IOException: " & e.Message)
        Catch e As SocketException
            ScaleWeight.Text = "ERROR"
            LastScaleReading = ""
            ScaleOnline = False
            ScaleStream.Close()
            ScaleStream = Nothing
            ScaleClient.Close()
            ScaleClient = Nothing
            ScaleStreamReader = Nothing
            Debug.WriteLine("ReadScaleStream SocketException: " & e.Message)
        Catch e As NullReferenceException
            Debug.WriteLine("ReadScaleStream NullReferenceException: " & e.Message)
        End Try
    End Sub
    Sub ConnectScale(IPscale As [String], portNumScale As [String])
        ' Create a TcpClient.
        ' Note, for this client to work you need to have a TcpServer 
        ' connected to the same address as specified by the server, port
        ' combination.
        Dim portScale As Int32 = Convert.ToInt32(portNumScale)
        Try
            ScaleClient = New TcpClient()
            ScaleClient.ReceiveTimeout = 100
            ScaleClient.SendTimeout = 100
            Dim connectResult = ScaleClient.BeginConnect(Net.IPAddress.Parse(IPscale), portScale, Nothing, Nothing)
            Dim connectSuccess = connectResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1))
            If Not connectSuccess Then
                Throw New SocketException()
            End If
            ScaleClient.EndConnect(connectResult)
            ScaleStream = ScaleClient.GetStream()
            ScaleStreamReader = New StreamReader(ScaleStream)
        Catch e As ArgumentNullException
            Debug.WriteLine("ConnectScale ArgumentNullException: " & e.Message)
            Application.Exit()
            End
        Catch e As IOException
            ScaleWeight.Text = "ERROR"
            LastScaleReading = ""
            ScaleOnline = False
            If ScaleStream IsNot Nothing Then
                ScaleStream.Close()
            End If
            ScaleStream = Nothing
            If ScaleClient IsNot Nothing Then
                ScaleClient.Close()
            End If
            ScaleClient = Nothing
            ScaleStreamReader = Nothing
            Debug.WriteLine("ConnectScale IOException: " & e.Message)
        Catch e As SocketException
            ' Close everything.
            If ScaleClient IsNot Nothing Then
                ScaleClient.Close()
            End If
            ScaleWeight.Text = "DISCON"
            ShowErrorDropdown("Network disconnected. Check connection.")
            LastScaleReading = ""
            ScaleOnline = False
            If ScaleStream IsNot Nothing Then
                ScaleStream.Close()
            End If
            ScaleStream = Nothing
            If ScaleClient IsNot Nothing Then
                ScaleClient.Close()
            End If
            ScaleClient = Nothing
            ScaleStreamReader = Nothing
            Debug.WriteLine("ConnectScale SocketException: " & e.Message)
        Catch e As NullReferenceException
            Debug.WriteLine("ConnectScale NullReferenceException: " & e.Message)
        End Try
    End Sub 'ConnectScale

    Sub ZeroScale(messageFile As [String])
        Try
            Dim TextLine As String
            TextLine = ""
            If File.Exists(messageFile) Then
                Dim messages() As String = File.ReadAllLines(messageFile)
                For Each message In messages
                    Dim data As [Byte]()
                    If message = "^R" Then
                        data = {18}
                    Else
                        data = System.Text.Encoding.ASCII.GetBytes(message + vbCrLf)
                    End If
                    ScaleStream.Write(data, 0, data.Length)
                Next
            End If

        Catch e As ArgumentNullException
            Debug.WriteLine("ZeroScale ArgumentNullException: {0}", e)
        Catch e As SocketException
            Debug.WriteLine("ZeroScale SocketException: {0}", e)
        Catch e As NullReferenceException
            Debug.WriteLine("ZeroScale NullReferenceException: {0}", e)
        End Try
    End Sub 'ZeroScale

    Private Sub SoftScaleHead_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If (Not Me.WindowState = FormWindowState.Minimized) Then
            Dim newHeight As Integer = 10
            If ScaleWeight.Height > 10 Then
                newHeight = ScaleWeight.Height
            End If
            Dim minWidth = (0.00004 * (Me.Size.Height - 20) ^ 2) + ((Me.Size.Height - 20) * 4.3) - 9.5376
            Dim newMinSize As Size = New Size(minWidth, Me.MinimumSize.Height)
            Dim newSize As Size = New Size(minWidth, Me.Size.Height)
            Me.MinimumSize = newMinSize
            Me.Size = newSize
            Dim scaleFont As Font = New Font("Lucida Console", Convert.ToSingle(newHeight), GraphicsUnit.Pixel)
            ScaleWeight.Font = scaleFont
            Try
                Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                Dim section As ScaleSettings = config.GetSection("_" & ScaleIPAddress & ScalePort)
                section.Size = Me.Size
                config.Save(ConfigurationSaveMode.Modified)
                ConfigurationManager.RefreshSection("ScaleSettings")
            Catch ex As Exception
                Debug.WriteLine("Resize Exception: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub SoftScaleHead_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim args As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Application.CommandLineArgs
            Dim argnum As Integer = 0
            If args.Count > 0 Then
                ArgsFound = True
                For Each arg As String In args
                    Dim switch As String = Strings.Left(arg, 3)
                    If switch = "/a" Then
                        Dim IPAddressArg As String = args(argnum + 1)
                        If CheckIpAddress(IPAddressArg) Then
                            ScaleIPAddress = IPAddressArg
                        Else
                            DisplayHelpMessage()
                            Application.Exit()
                            End
                        End If
                    ElseIf switch = "/p"
                        Dim PortArg As String = args(argnum + 1)
                        If CheckPort(PortArg) Then
                            ScalePort = PortArg
                        Else
                            DisplayHelpMessage()
                            Application.Exit()
                            End
                        End If
                    ElseIf switch = "/z"
                        Dim ZeroCommandArg As String = My.Application.Info.DirectoryPath & "\" & args(argnum + 1)
                        ZeroButton.Visible = True
                        EnableZeroingToolStripMenuItem.Checked = True
                        ZeroEnabled = True
                        If File.Exists(ZeroCommandArg) Then
                            ZeroCommandFile = ZeroCommandArg
                            CommandFileNameToolStripMenuItem.Text = Path.GetFileName(ZeroCommandArg)
                        End If
                    ElseIf switch = "/x"
                        Dim CurrentLocation As Point = Me.Location
                        Try
                            CurrentLocation.X = Convert.ToInt32(args(argnum + 1))
                            Me.Location = CurrentLocation
                        Catch
                            DisplayHelpMessage()
                            Application.Exit()
                            End
                        End Try
                    ElseIf switch = "/y"
                        Dim CurrentLocation As Point = Me.Location
                        Try
                            CurrentLocation.Y = Convert.ToInt32(args(argnum + 1))
                            Me.Location = CurrentLocation
                        Catch
                            DisplayHelpMessage()
                            Application.Exit()
                            End
                        End Try
                    ElseIf switch = "/w"
                        Dim CurrentSize As Size = Me.Size
                        Try
                            CurrentSize.Width = Convert.ToInt32(args(argnum + 1))
                            Me.Size = CurrentSize
                        Catch
                            DisplayHelpMessage()
                            Application.Exit()
                            End
                        End Try
                    ElseIf switch = "/h"
                        Dim CurrentSize As Size = Me.Size
                        Try
                            CurrentSize.Height = Convert.ToInt32(args(argnum + 1))
                            Me.Size = CurrentSize
                        Catch
                            DisplayHelpMessage()
                            Application.Exit()
                            End
                        End Try
                    ElseIf switch = "/d"
                        Try
                            Me.Text = args(argnum + 1)
                        Catch
                            DisplayHelpMessage()
                            Application.Exit()
                            End
                        End Try
                    End If
                    argnum += 1
                Next
            Else
                DisplayHelpMessage()
                Application.Exit()
                End
            End If

        Catch ex As Exception
            Debug.WriteLine("Load Exception: " & ex.Message)
            Application.Exit()
            End
        End Try
    End Sub

    Public Function CheckPort(port As String) As Boolean
        Dim portInt As Integer = 0
        If Integer.TryParse(port, portInt) Then
            If portInt > 0 And portInt < 65536 Then
                Return True
            End If
        End If
        Return False
    End Function
    Public Function CheckIpAddress(ipAddr As String) As Boolean
        Dim parts() As String = ipAddr.Split(New Char() {"."c}, StringSplitOptions.RemoveEmptyEntries)
        Dim rv As Boolean
        If parts.Length <> 4 Then
            rv = False
        Else
            Dim anIP As Net.IPAddress = Net.IPAddress.Loopback
            If Net.IPAddress.TryParse(ipAddr, anIP) Then
                rv = True
            Else
                rv = False
            End If
        End If
        Return rv
    End Function
    Sub DisplayHelpMessage()
        Dim HelpMessage As String =
    "DESCRIPTION:" & vbNewLine &
    vbTab & "SoftScaleHead is a program to view the weight" & vbNewLine &
    vbTab & "on a scale. It also can zero a scale if the RX" & vbNewLine &
    vbTab & "pin of the scale is connected" & vbNewLine &
    "USAGE:  " & vbNewLine &
    vbTab & "SoftScaleHead /a <IP Address> [/p <Port>] [/z <zerocommands.txt>]" & vbNewLine &
    vbTab & "/a    IP Address of Scale Moxa" & vbNewLine &
    vbTab & "/p    [Optional] Port number" & vbNewLine &
    vbTab & "/z    [Optional] Enable Zeroing Command" & vbNewLine &
    vbTab & "/d    [Optional] Scale Description" & vbNewLine &
    vbNewLine &
    vbTab & "Must include IP address" & vbNewLine &
    vbTab & "Port is 10001 unless otherwise specified" & vbNewLine &
    vbNewLine & "EXAMPLE:   " & vbNewLine &
    vbTab & "SoftScaleHead /a 172.16.4.201 /p 10001 /z RiceLakeZero.txt /d ""LANE 1""" & vbNewLine &
    vbTab & "Connect to Rice Lake Scale LANE 1 with IP of 172.16.4.201" & vbNewLine &
    vbTab & "Use port 10001" & vbNewLine &
    vbTab & "Enable zeroing of the scale"
        MsgBox(HelpMessage, MsgBoxStyle.Information, "Invalid Command Line Arguments")
    End Sub
    Private Sub ZeroButton_Click(sender As Object, e As EventArgs) Handles ZeroButton.Click
        If Not ZeroButtonTimeout.Enabled Then
            ZeroButtonTimeout.Start()
            ZeroScale(ZeroCommandFile)
        End If
    End Sub

    Private Sub SoftScaleHead_MouseEnter(sender As Object, e As EventArgs) Handles MyBase.MouseEnter
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub SoftScaleHead_MouseHover(sender As Object, e As EventArgs) Handles MyBase.MouseHover
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub ScaleWeight_MouseHover(sender As Object, e As EventArgs) Handles ScaleWeight.MouseHover
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub ScaleWeight_MouseEnter(sender As Object, e As EventArgs) Handles ScaleWeight.MouseEnter
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub SoftScaleHead_LocationChanged(sender As Object, e As EventArgs) Handles MyBase.LocationChanged
        If (Not Me.WindowState = FormWindowState.Minimized) Then
            Try
                Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                Dim section As ScaleSettings = config.GetSection("_" & ScaleIPAddress & ScalePort)
                section.Location = Me.Location
                config.Save(ConfigurationSaveMode.Modified)
                ConfigurationManager.RefreshSection("ScaleSettings")
            Catch
            End Try
        End If
    End Sub

    Private Sub ConnectionTimer_Tick(sender As Object, e As EventArgs) Handles ConnectionTimer.Tick
        Try
            Debug.WriteLine("Pinging " & ScaleIPAddress)
            ScaleOnline = My.Computer.Network.Ping(ScaleIPAddress, 1000)
        Catch ex As Exception
            Debug.WriteLine("ConnectionTimer Exception: " & ex.Message)
        End Try
        If ScaleOnline Then
            ConnectionTimer.Stop()
        End If
    End Sub

    Private Sub UncheckOtherColors(MenuItem As ToolStripMenuItem)
        If MenuItem.Checked Then
            For Each Item As ToolStripMenuItem In ColorMenu.DropDownItems
                If Item IsNot MenuItem Then
                    Item.Checked = False
                End If
            Next
        End If
    End Sub
    Private Sub SaveColorSettings()
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim section As ScaleSettings = config.GetSection("_" & ScaleIPAddress & ScalePort)
        section.BackGroundColor = ScaleWeight.BackColor
        section.TextColor = ScaleWeight.ForeColor
        config.Save(ConfigurationSaveMode.Modified)
        ConfigurationManager.RefreshSection("ScaleSettings")
    End Sub

    Private Sub SaveZeroSettings()
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim section As ScaleSettings = config.GetSection("_" & ScaleIPAddress & ScalePort)
        section.ZeroEnabled = ZeroEnabled
        section.ZeroCommandFile = ZeroCommandFile
        config.Save(ConfigurationSaveMode.Modified)
        ConfigurationManager.RefreshSection("ScaleSettings")
    End Sub

    Private Sub SaveDescriptionSettings()
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim section As ScaleSettings = config.GetSection("_" & ScaleIPAddress & ScalePort)
        section.Description = Me.Text
        config.Save(ConfigurationSaveMode.Modified)
        ConfigurationManager.RefreshSection("ScaleSettings")
    End Sub

    Private Sub ColorMenu_Click(sender As Object, e As EventArgs) Handles ColorMenu.Click
        Dim bgColorDialog As ColorDialog = New ColorDialog()
        bgColorDialog.Color = ScaleWeight.BackColor
        If (bgColorDialog.ShowDialog() = DialogResult.OK) Then
            If bgColorDialog.Color = ScaleWeight.ForeColor Then
                MsgBox("Text color must be different than background color. Please choose another color.")
            Else
                ScaleWeight.BackColor = bgColorDialog.Color
                Panel1.BackColor = bgColorDialog.Color
                Dim ControlColor As Color
                If bgColorDialog.Color.GetBrightness < 0.1 Then
                    ControlColor = Lighten(bgColorDialog.Color, 0.1)
                Else
                    ControlColor = Darken(bgColorDialog.Color, 0.1)
                End If
                Me.BackColor = ControlColor
                SaveColorSettings()
            End If
        End If
    End Sub

    Public Shared Function Lighten(inColor As Color, inAmount As Double) As Color
        Return Color.FromArgb(inColor.A, CInt(Math.Min(255, inColor.R + 255 * inAmount)), CInt(Math.Min(255, inColor.G + 255 * inAmount)), CInt(Math.Min(255, inColor.B + 255 * inAmount)))
    End Function

    Public Shared Function Darken(inColor As Color, inAmount As Double) As Color
        Return Color.FromArgb(inColor.A, CInt(Math.Max(0, inColor.R - 255 * inAmount)), CInt(Math.Max(0, inColor.G - 255 * inAmount)), CInt(Math.Max(0, inColor.B - 255 * inAmount)))
    End Function

    Private Sub TextColorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TextColorToolStripMenuItem.Click
        Dim textColorDialog As ColorDialog = New ColorDialog()
        textColorDialog.Color = ScaleWeight.BackColor
        If (textColorDialog.ShowDialog() = DialogResult.OK) Then
            If textColorDialog.Color = ScaleWeight.BackColor Then
                MsgBox("Text color must be different than background color. Please choose another color.")
            Else
                ScaleWeight.ForeColor = textColorDialog.Color
                SaveColorSettings()
            End If
        End If
    End Sub

    Private Sub EnableZeroingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnableZeroingToolStripMenuItem.Click
        ZeroEnabled = EnableZeroingToolStripMenuItem.Checked
        ZeroButton.Enabled = ZeroEnabled
        ZeroButton.Visible = ZeroEnabled
        SaveZeroSettings()
    End Sub

    Private Sub RenameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenameToolStripMenuItem.Click
        Dim NewScaleName As String = InputBox("Enter New Scale Name", "Rename Scale", "", Me.Location.X, Me.Location.Y)
        If NewScaleName <> "" Then
            Me.Text = NewScaleName
            SaveDescriptionSettings()
        End If
    End Sub

    Private Sub OpenCommandFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenCommandFileToolStripMenuItem.Click
        Dim ZeroCommandFileDialog As New OpenFileDialog()
        ZeroCommandFileDialog.InitialDirectory = My.Application.Info.DirectoryPath
        ZeroCommandFileDialog.CheckFileExists = True
        If ZeroCommandFileDialog.ShowDialog() = DialogResult.OK Then
            ZeroCommandFile = ZeroCommandFileDialog.FileName
            CommandFileNameToolStripMenuItem.Text = Path.GetFileName(ZeroCommandFileDialog.FileName)
            SaveZeroSettings()
        End If
    End Sub

    Private Sub ZeroButtonTimeout_Tick(sender As Object, e As EventArgs) Handles ZeroButtonTimeout.Tick
        ZeroButtonTimeout.Stop()
    End Sub

    Private Sub ShowErrorDropdown(msg As String)
        ErrorMessage.Visible = True
        ErrorMessage.Text = " " & msg
        If msg.ToLower().Contains("serial") Then
            ErrorImage.Visible = True
            ErrorImage.BringToFront()
            ErrorImage.Image = My.Resources.SerialError
        ElseIf msg.ToLower().Contains("network")
            ErrorImage.Visible = True
            ErrorImage.BringToFront()
            ErrorImage.Image = My.Resources.NetworkError
        End If
    End Sub

    Private Sub HideErrorDropdown()
        ErrorMessage.Visible = False
        ErrorImage.Visible = False
        ErrorImage.BringToFront()
    End Sub
End Class
Public Class DisabledRichTextBox
    Inherits System.Windows.Forms.RichTextBox
    ' See: http://wiki.winehq.org/List_Of_Windows_Messages

    Private Const WM_SETFOCUS As Integer = &H07
    Private Const WM_ENABLE As Integer = &H0A
    Private Const WM_SETCURSOR As Integer = &H20

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = 516 Then
            SoftScaleHead.ContextMenuStrip1.Show(SoftScaleHead, New Point(0, 0))
        End If
        If Not (m.Msg = WM_SETFOCUS OrElse m.Msg = WM_ENABLE OrElse m.Msg = WM_SETCURSOR) Then
            MyBase.WndProc(m)
        End If
    End Sub
End Class
Public Class ScaleSettings
    Inherits ConfigurationSection
    <ConfigurationProperty("IPAddress", DefaultValue:="", IsRequired:=True)>
    Public Property IPAddress() As String
        Get
            Return CType(Me("IPAddress"), String)
        End Get
        Set(ByVal value As String)
            Me("IPAddress") = value
        End Set
    End Property
    <ConfigurationProperty("Port", DefaultValue:="", IsRequired:=False)>
    Public Property Port() As String
        Get
            Return CType(Me("Port"), String)
        End Get
        Set(ByVal value As String)
            Me("Port") = value
        End Set
    End Property
    <ConfigurationProperty("Size", DefaultValue:="", IsRequired:=False)>
    Public Property Size() As Size
        Get
            Return CType(Me("Size"), Size)
        End Get
        Set(ByVal value As Size)
            Me("Size") = value
        End Set
    End Property
    <ConfigurationProperty("Location", DefaultValue:="", IsRequired:=False)>
    Public Property Location() As Point
        Get
            Return CType(Me("Location"), Point)
        End Get
        Set(ByVal value As Point)
            Me("Location") = value
        End Set
    End Property
    <ConfigurationProperty("ZeroEnabled", DefaultValue:="False", IsRequired:=False)>
    Public Property ZeroEnabled() As Boolean
        Get
            Return CType(Me("ZeroEnabled"), Boolean)
        End Get
        Set(ByVal value As Boolean)
            Me("ZeroEnabled") = value
        End Set
    End Property
    <ConfigurationProperty("ZeroCommandFile", DefaultValue:="", IsRequired:=False)>
    Public Property ZeroCommandFile() As String
        Get
            Return CType(Me("ZeroCommandFile"), String)
        End Get
        Set(ByVal value As String)
            Me("ZeroCommandFile") = value
        End Set
    End Property
    <ConfigurationProperty("Description", DefaultValue:="", IsRequired:=False)>
    Public Property Description() As String
        Get
            Return CType(Me("Description"), String)
        End Get
        Set(ByVal value As String)
            Me("Description") = value
        End Set
    End Property
    <ConfigurationProperty("BackGroundColor", DefaultValue:="Black", IsRequired:=False)>
    Public Property BackGroundColor() As Color
        Get
            Return CType(Me("BackGroundColor"), Color)
        End Get
        Set(ByVal value As Color)
            Me("BackGroundColor") = value
        End Set
    End Property
    <ConfigurationProperty("TextColor", DefaultValue:="Chartreuse", IsRequired:=False)>
    Public Property TextColor() As Color
        Get
            Return CType(Me("TextColor"), Color)
        End Get
        Set(ByVal value As Color)
            Me("TextColor") = value
        End Set
    End Property
End Class