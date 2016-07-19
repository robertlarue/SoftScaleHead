<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SoftScaleHead
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SoftScaleHead))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ConnectionTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ZeroButton = New System.Windows.Forms.Button()
        Me.MotionLight = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ZeroButtonTimeout = New System.Windows.Forms.Timer(Me.components)
        Me.ErrorImage = New System.Windows.Forms.PictureBox()
        Me.ScaleZeroingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EnableZeroingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenCommandFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CommandFileNameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ColorMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RenameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ErrorMessage = New DisabledRichTextBox()
        Me.ScaleWeight = New DisabledRichTextBox()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.ErrorImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(0, -2)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(353, 89)
        Me.Panel1.TabIndex = 5
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ScaleZeroingToolStripMenuItem, Me.ColorMenu, Me.TextColorToolStripMenuItem, Me.RenameToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(190, 92)
        '
        'ConnectionTimer
        '
        Me.ConnectionTimer.Interval = 5000
        '
        'ZeroButton
        '
        Me.ZeroButton.BackColor = System.Drawing.Color.DimGray
        Me.ZeroButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ZeroButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ZeroButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.ZeroButton.Location = New System.Drawing.Point(2, 45)
        Me.ZeroButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 4)
        Me.ZeroButton.MaximumSize = New System.Drawing.Size(65, 0)
        Me.ZeroButton.MinimumSize = New System.Drawing.Size(60, 20)
        Me.ZeroButton.Name = "ZeroButton"
        Me.ZeroButton.Size = New System.Drawing.Size(65, 38)
        Me.ZeroButton.TabIndex = 0
        Me.ZeroButton.Text = "ZERO"
        Me.ZeroButton.UseVisualStyleBackColor = False
        Me.ZeroButton.Visible = False
        '
        'MotionLight
        '
        Me.MotionLight.BackColor = System.Drawing.Color.DimGray
        Me.MotionLight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MotionLight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray
        Me.MotionLight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray
        Me.MotionLight.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MotionLight.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!)
        Me.MotionLight.Location = New System.Drawing.Point(2, 4)
        Me.MotionLight.Margin = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.MotionLight.MaximumSize = New System.Drawing.Size(65, 0)
        Me.MotionLight.MinimumSize = New System.Drawing.Size(60, 20)
        Me.MotionLight.Name = "MotionLight"
        Me.MotionLight.Size = New System.Drawing.Size(65, 37)
        Me.MotionLight.TabIndex = 0
        Me.MotionLight.TabStop = False
        Me.MotionLight.Text = "MOTION"
        Me.MotionLight.UseVisualStyleBackColor = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.MotionLight, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ZeroButton, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(355, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(69, 87)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'ZeroButtonTimeout
        '
        Me.ZeroButtonTimeout.Interval = 1000
        '
        'ErrorImage
        '
        Me.ErrorImage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ErrorImage.BackColor = System.Drawing.Color.White
        Me.ErrorImage.Image = CType(resources.GetObject("ErrorImage.Image"), System.Drawing.Image)
        Me.ErrorImage.Location = New System.Drawing.Point(0, 19)
        Me.ErrorImage.Name = "ErrorImage"
        Me.ErrorImage.Size = New System.Drawing.Size(354, 68)
        Me.ErrorImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ErrorImage.TabIndex = 8
        Me.ErrorImage.TabStop = False
        Me.ErrorImage.Visible = False
        '
        'ScaleZeroingToolStripMenuItem
        '
        Me.ScaleZeroingToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EnableZeroingToolStripMenuItem, Me.OpenCommandFileToolStripMenuItem, Me.CommandFileNameToolStripMenuItem})
        Me.ScaleZeroingToolStripMenuItem.Image = Global.SoftScaleHead.My.Resources.Resources.ExpanderElement_10708
        Me.ScaleZeroingToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ScaleZeroingToolStripMenuItem.Name = "ScaleZeroingToolStripMenuItem"
        Me.ScaleZeroingToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.ScaleZeroingToolStripMenuItem.Text = "Scale Zeroing"
        '
        'EnableZeroingToolStripMenuItem
        '
        Me.EnableZeroingToolStripMenuItem.CheckOnClick = True
        Me.EnableZeroingToolStripMenuItem.Name = "EnableZeroingToolStripMenuItem"
        Me.EnableZeroingToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.EnableZeroingToolStripMenuItem.Text = "Enable Zeroing"
        '
        'OpenCommandFileToolStripMenuItem
        '
        Me.OpenCommandFileToolStripMenuItem.Image = Global.SoftScaleHead.My.Resources.Resources.Open_6529
        Me.OpenCommandFileToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.OpenCommandFileToolStripMenuItem.Name = "OpenCommandFileToolStripMenuItem"
        Me.OpenCommandFileToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.OpenCommandFileToolStripMenuItem.Text = "Open Command File"
        '
        'CommandFileNameToolStripMenuItem
        '
        Me.CommandFileNameToolStripMenuItem.Enabled = False
        Me.CommandFileNameToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic)
        Me.CommandFileNameToolStripMenuItem.Image = Global.SoftScaleHead.My.Resources.Resources.ViewCode_Markup__6279
        Me.CommandFileNameToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.CommandFileNameToolStripMenuItem.Name = "CommandFileNameToolStripMenuItem"
        Me.CommandFileNameToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.CommandFileNameToolStripMenuItem.Text = "RiceLakeZero.txt"
        '
        'ColorMenu
        '
        Me.ColorMenu.Image = Global.SoftScaleHead.My.Resources.Resources.pane_appearance_16x16_on
        Me.ColorMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ColorMenu.Name = "ColorMenu"
        Me.ColorMenu.Size = New System.Drawing.Size(189, 22)
        Me.ColorMenu.Text = "Set Background Color"
        '
        'TextColorToolStripMenuItem
        '
        Me.TextColorToolStripMenuItem.Image = Global.SoftScaleHead.My.Resources.Resources.FontColor_11146
        Me.TextColorToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TextColorToolStripMenuItem.Name = "TextColorToolStripMenuItem"
        Me.TextColorToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.TextColorToolStripMenuItem.Text = "Set Text Color"
        '
        'RenameToolStripMenuItem
        '
        Me.RenameToolStripMenuItem.Image = Global.SoftScaleHead.My.Resources.Resources.Rename_6779
        Me.RenameToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.RenameToolStripMenuItem.Name = "RenameToolStripMenuItem"
        Me.RenameToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.RenameToolStripMenuItem.Text = "Rename"
        '
        'ErrorMessage
        '
        Me.ErrorMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ErrorMessage.BackColor = System.Drawing.Color.Red
        Me.ErrorMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ErrorMessage.CausesValidation = False
        Me.ErrorMessage.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.ErrorMessage.DetectUrls = False
        Me.ErrorMessage.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ErrorMessage.ForeColor = System.Drawing.Color.White
        Me.ErrorMessage.Location = New System.Drawing.Point(1, 0)
        Me.ErrorMessage.Margin = New System.Windows.Forms.Padding(0, 24, 0, 0)
        Me.ErrorMessage.MaxLength = 0
        Me.ErrorMessage.MinimumSize = New System.Drawing.Size(0, 20)
        Me.ErrorMessage.Multiline = False
        Me.ErrorMessage.Name = "ErrorMessage"
        Me.ErrorMessage.ReadOnly = True
        Me.ErrorMessage.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.ErrorMessage.ShortcutsEnabled = False
        Me.ErrorMessage.Size = New System.Drawing.Size(353, 20)
        Me.ErrorMessage.TabIndex = 7
        Me.ErrorMessage.Text = "ERROR"
        Me.ErrorMessage.Visible = False
        Me.ErrorMessage.WordWrap = False
        '
        'ScaleWeight
        '
        Me.ScaleWeight.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ScaleWeight.BackColor = System.Drawing.Color.Black
        Me.ScaleWeight.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ScaleWeight.CausesValidation = False
        Me.ScaleWeight.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.ScaleWeight.DetectUrls = False
        Me.ScaleWeight.Font = New System.Drawing.Font("Lucida Console", 60.0!)
        Me.ScaleWeight.ForeColor = System.Drawing.Color.Chartreuse
        Me.ScaleWeight.Location = New System.Drawing.Point(0, 6)
        Me.ScaleWeight.Margin = New System.Windows.Forms.Padding(0, 24, 0, 0)
        Me.ScaleWeight.MaxLength = 12
        Me.ScaleWeight.Multiline = False
        Me.ScaleWeight.Name = "ScaleWeight"
        Me.ScaleWeight.ReadOnly = True
        Me.ScaleWeight.ShortcutsEnabled = False
        Me.ScaleWeight.Size = New System.Drawing.Size(353, 81)
        Me.ScaleWeight.TabIndex = 2
        Me.ScaleWeight.Text = "ERROR"
        Me.ScaleWeight.WordWrap = False
        '
        'SoftScaleHead
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(26, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(426, 87)
        Me.Controls.Add(Me.ErrorImage)
        Me.Controls.Add(Me.ErrorMessage)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.ScaleWeight)
        Me.Controls.Add(Me.Panel1)
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(300, 92)
        Me.Name = "SoftScaleHead"
        Me.Text = "SoftScaleHead"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.ErrorImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ScaleWeight As DisabledRichTextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ConnectionTimer As Timer
    Friend WithEvents ColorMenu As ToolStripMenuItem
    Friend WithEvents TextColorToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ZeroButton As Button
    Friend WithEvents MotionLight As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents RenameToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScaleZeroingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EnableZeroingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenCommandFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CommandFileNameToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ZeroButtonTimeout As Timer
    Friend WithEvents ErrorMessage As DisabledRichTextBox
    Friend WithEvents ErrorImage As PictureBox
End Class
