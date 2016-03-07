<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SoftScaleHead
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SoftScaleHead))
        Me.Movement = New System.Windows.Forms.GroupBox()
        Me.MotionLight = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ZeroButton = New System.Windows.Forms.Button()
        Me.ZeroControls = New System.Windows.Forms.GroupBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RememberLocation = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScaleWeight = New DisabledRichTextBox()
        Me.Movement.SuspendLayout()
        Me.ZeroControls.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Movement
        '
        Me.Movement.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Movement.Controls.Add(Me.MotionLight)
        Me.Movement.Location = New System.Drawing.Point(315, 0)
        Me.Movement.Name = "Movement"
        Me.Movement.Size = New System.Drawing.Size(89, 40)
        Me.Movement.TabIndex = 3
        Me.Movement.TabStop = False
        '
        'MotionLight
        '
        Me.MotionLight.BackColor = System.Drawing.Color.DimGray
        Me.MotionLight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray
        Me.MotionLight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray
        Me.MotionLight.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MotionLight.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!)
        Me.MotionLight.Location = New System.Drawing.Point(4, 12)
        Me.MotionLight.Name = "MotionLight"
        Me.MotionLight.Size = New System.Drawing.Size(80, 23)
        Me.MotionLight.TabIndex = 0
        Me.MotionLight.TabStop = False
        Me.MotionLight.Text = "MOTION"
        Me.MotionLight.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(0, -2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(309, 85)
        Me.Panel1.TabIndex = 5
        '
        'ZeroButton
        '
        Me.ZeroButton.BackColor = System.Drawing.Color.DimGray
        Me.ZeroButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ZeroButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!)
        Me.ZeroButton.Location = New System.Drawing.Point(4, 10)
        Me.ZeroButton.Margin = New System.Windows.Forms.Padding(0)
        Me.ZeroButton.Name = "ZeroButton"
        Me.ZeroButton.Size = New System.Drawing.Size(80, 24)
        Me.ZeroButton.TabIndex = 0
        Me.ZeroButton.Text = "ZERO"
        Me.ZeroButton.UseVisualStyleBackColor = False
        Me.ZeroButton.Visible = False
        '
        'ZeroControls
        '
        Me.ZeroControls.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ZeroControls.Controls.Add(Me.ZeroButton)
        Me.ZeroControls.Location = New System.Drawing.Point(315, 39)
        Me.ZeroControls.Name = "ZeroControls"
        Me.ZeroControls.Size = New System.Drawing.Size(89, 37)
        Me.ZeroControls.TabIndex = 7
        Me.ZeroControls.TabStop = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RememberLocation})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(219, 30)
        '
        'RememberLocation
        '
        Me.RememberLocation.Checked = True
        Me.RememberLocation.CheckOnClick = True
        Me.RememberLocation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.RememberLocation.Name = "RememberLocation"
        Me.RememberLocation.Size = New System.Drawing.Size(218, 26)
        Me.RememberLocation.Text = "Remember Location"
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
        Me.ScaleWeight.Font = New System.Drawing.Font("Lucida Console", 50.0!)
        Me.ScaleWeight.ForeColor = System.Drawing.Color.Chartreuse
        Me.ScaleWeight.Location = New System.Drawing.Point(0, 8)
        Me.ScaleWeight.Margin = New System.Windows.Forms.Padding(0, 30, 0, 0)
        Me.ScaleWeight.MaxLength = 12
        Me.ScaleWeight.Multiline = False
        Me.ScaleWeight.Name = "ScaleWeight"
        Me.ScaleWeight.ReadOnly = True
        Me.ScaleWeight.ShortcutsEnabled = False
        Me.ScaleWeight.Size = New System.Drawing.Size(309, 75)
        Me.ScaleWeight.TabIndex = 2
        Me.ScaleWeight.Text = "ERROR"
        Me.ScaleWeight.WordWrap = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(407, 82)
        Me.Controls.Add(Me.ZeroControls)
        Me.Controls.Add(Me.Movement)
        Me.Controls.Add(Me.ScaleWeight)
        Me.Controls.Add(Me.Panel1)
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(220, 80)
        Me.Name = "Form1"
        Me.Text = "SoftScaleHead"
        Me.Movement.ResumeLayout(False)
        Me.ZeroControls.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ScaleWeight As DisabledRichTextBox
    Friend WithEvents Movement As GroupBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ZeroButton As Button
    Friend WithEvents ZeroControls As GroupBox
    Friend WithEvents MotionLight As Button
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents RememberLocation As ToolStripMenuItem
End Class
