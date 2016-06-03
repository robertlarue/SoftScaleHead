Imports System.Net.Sockets
Imports System.IO
Imports System.Configuration
Public Class SoftScaleHead
    Public FormIsClosing As Boolean = False
    Public ScaleIPAddress As String = ""
    Public ScalePort As String = "10001"
    Public ZeroEnabled As Boolean = False
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
                newConfigSection.Description = Me.Text
                newConfigSection.Port = ScalePort
                config.Sections.Add("_" & ScaleIPAddress & ScalePort, newConfigSection)
                config.Save(ConfigurationSaveMode.Modified)
                ConfigurationManager.RefreshSection("ScaleSettings")

            Else
                ScaleIPAddress = existingSection.IPAddress
                Me.Location = existingSection.Location
                Me.Size = existingSection.Size
                ZeroEnabled = existingSection.ZeroEnabled
                Me.Text = existingSection.Description
                ScalePort = existingSection.Port
            End If
            While Not FormIsClosing
                ConnectScale(ScaleIPAddress, ScalePort)
                Application.DoEvents()
            End While
        End If
    End Sub

    Private Sub SoftScaleHead_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        FormIsClosing = True

    End Sub
    Sub ConnectScale(IPscale As [String], portNumScale As [String])
        ' Create a TcpClient.
        ' Note, for this client to work you need to have a TcpServer 
        ' connected to the same address as specified by the server, port
        ' combination.
        Dim portScale As Int32 = Convert.ToInt32(portNumScale)
        Dim scaleClient As TcpClient = Nothing
        Try
            scaleClient = New TcpClient(IPscale, portScale)
            scaleClient.ReceiveTimeout = 1000
            ' Get a client stream for reading and writing.
            Dim scaleStream As NetworkStream = scaleClient.GetStream()
            Dim scaleReader As StreamReader = New StreamReader(scaleStream)

            ' Receive the TcpServer.response.
            ' Buffer to store the response bytes.
            ' Read the first batch of the TcpServer response bytes.
            Dim scaleChar As Char = Nothing
            Do
                scaleChar = Convert.ToChar(scaleReader.Read())
                If scaleChar = vbCr Then
                    Exit Do
                End If
            Loop
            Dim scaleLine As String = Nothing
            Do
                scaleChar = Convert.ToChar(scaleReader.Read())
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
            scaleLine = scaleLine.Replace("G", "")
            scaleLine = scaleLine.Replace("Z", "")
            scaleLine = scaleLine.Replace("M", "")
            scaleLine = scaleLine.Replace("O", "")
            scaleLine = scaleLine.Replace("C", "")
            If scaleLine.StartsWith("DIN") Then
                scaleLine = LastScaleReading
            End If
            If scaleLine.Length > 2 And scaleLine <> LastScaleReading Then
                ScaleWeight.Text = scaleLine
                Debug.WriteLine(scaleLine)
                LastScaleReading = scaleLine
            End If
            ' Close everything.
            scaleStream.Close()
            scaleClient.Close()
        Catch e As ArgumentNullException
            MsgBox("ArgumentNullException: " & e.Message)
            Application.Exit()
            End
        Catch e As SocketException
            ' Close everything.
            scaleClient.Close()
            ScaleWeight.Text = "ERROR"
            LastScaleReading = ""
            'MsgBox("Could not connect to scale" & vbNewLine & "SocketException: " & e.Message)
            'Application.Exit()
            'End
        Catch e As IOException
            scaleClient.Close()
            ScaleWeight.Text = "*"
            LastScaleReading = ""
        End Try
    End Sub 'ConnectScale

    Sub ZeroScale(server As [String], portNum As [String], messageFile As [String])
        Try
            ' Create a TcpClient.
            ' Note, for this client to work you need to have a TcpServer 
            ' connected to the same address as specified by the server, port
            ' combination.
            Dim port As Int32 = Convert.ToInt32(portNum)
            Dim client As New TcpClient(server, port)

            ' Get a client stream for reading and writing.
            '  Stream stream = client.GetStream();
            Dim stream As NetworkStream = client.GetStream()
            Dim TextLine As String
            TextLine = ""

            Dim messages() As String = File.ReadAllLines(messageFile)
            For Each message In messages
                Dim data As [Byte]() = System.Text.Encoding.ASCII.GetBytes(message + vbCrLf)
                stream.Write(data, 0, data.Length)
            Next

            ' Close everything.
            stream.Close()
            client.Close()
        Catch e As ArgumentNullException
            Console.WriteLine("ArgumentNullException: {0}", e)
        Catch e As SocketException
            Console.WriteLine("SocketException: {0}", e)
        End Try
    End Sub 'ZeroScale

    Private Sub SoftScaleHead_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Dim newHeight As Integer = 10
        If ScaleWeight.Height > 10 Then
            newHeight = ScaleWeight.Height
        End If
        Dim scaleFont As Font = New Font("Lucida Console", Convert.ToSingle(newHeight * 0.95), GraphicsUnit.Pixel)
        ScaleWeight.Font = scaleFont
        If Not Me.WindowState = FormWindowState.Minimized Then
            Try
                Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                Dim section As ScaleSettings = config.GetSection("_" & ScaleIPAddress & ScalePort)
                section.Size = Me.Size
                config.Save(ConfigurationSaveMode.Modified)
                ConfigurationManager.RefreshSection("ScaleSettings")
            Catch
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
                        ZeroButton.Visible = True
                        ZeroEnabled = True
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
                'Application.Exit()
                'End
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
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
    vbTab & "SoftScaleHead /a <IP Address> [/p <Port>] [/z]" & vbNewLine &
    vbTab & "/a    IP Address of Scale Moxa" & vbNewLine &
    vbTab & "/p    [Optional] Port number" & vbNewLine &
    vbTab & "/z    [Optional] Enable Zeroing" & vbNewLine &
    vbNewLine &
    vbTab & "Must include IP address" & vbNewLine &
    vbTab & "Port is 10001 unless otherwise specified" & vbNewLine &
    vbNewLine & "EXAMPLE:   " & vbNewLine &
    vbTab & "SoftScaleHead /a 172.16.4.201 /p 10001 /z" & vbNewLine &
    vbTab & "Connect to Scale with IP of 172.16.4.201" & vbNewLine &
    vbTab & "Use port 10001" & vbNewLine &
    vbTab & "Enable zeroing of the scale"
        MsgBox(HelpMessage, MsgBoxStyle.Information, "Invalid Command Line Arguments")
    End Sub
    Private Sub ZeroButton_Click(sender As Object, e As EventArgs) Handles ZeroButton.Click
        ZeroScale(ScaleIPAddress, ScalePort, My.Application.Info.DirectoryPath + "\zerocommands.txt")
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
        If Not Me.WindowState = FormWindowState.Minimized Then
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
    <ConfigurationProperty("Description", DefaultValue:="", IsRequired:=False)>
    Public Property Description() As String
        Get
            Return CType(Me("Description"), String)
        End Get
        Set(ByVal value As String)
            Me("Description") = value
        End Set
    End Property
End Class