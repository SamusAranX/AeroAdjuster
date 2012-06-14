<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AdjustAeroMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InvertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BrightnessToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IgnoreBlack = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeDColor = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.EnableTransitions = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimingFunctionComboBox = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.UrsprünglicheAeroFarbeWiederherstellenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeIconColor = New System.Windows.Forms.ToolStripMenuItem()
        Me.SchließenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.Filter = "*.jpg"
        Me.FileSystemWatcher1.NotifyFilter = System.IO.NotifyFilters.Size
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Aero Adjuster"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AdjustAeroMenuItem, Me.InvertToolStripMenuItem, Me.BrightnessToolStripMenuItem, Me.IgnoreBlack, Me.ChangeDColor, Me.ToolStripSeparator1, Me.EnableTransitions, Me.TimingFunctionComboBox, Me.ToolStripSeparator2, Me.ToolStripMenuItem2, Me.ToolStripComboBox1, Me.ToolStripSeparator3, Me.UrsprünglicheAeroFarbeWiederherstellenToolStripMenuItem, Me.ChangeIconColor, Me.SchließenToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(273, 296)
        '
        'AdjustAeroMenuItem
        '
        Me.AdjustAeroMenuItem.Image = Global.Aero_Adjuster.My.Resources.Resources.colorpencil
        Me.AdjustAeroMenuItem.Name = "AdjustAeroMenuItem"
        Me.AdjustAeroMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.AdjustAeroMenuItem.Text = "Fit Aero color to wallpaper color"
        '
        'InvertToolStripMenuItem
        '
        Me.InvertToolStripMenuItem.CheckOnClick = True
        Me.InvertToolStripMenuItem.Name = "InvertToolStripMenuItem"
        Me.InvertToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.InvertToolStripMenuItem.Text = "Invert color"
        '
        'BrightnessToolStripMenuItem
        '
        Me.BrightnessToolStripMenuItem.CheckOnClick = True
        Me.BrightnessToolStripMenuItem.Name = "BrightnessToolStripMenuItem"
        Me.BrightnessToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.BrightnessToolStripMenuItem.Text = "Adjust brightness as well"
        '
        'IgnoreBlack
        '
        Me.IgnoreBlack.CheckOnClick = True
        Me.IgnoreBlack.Name = "IgnoreBlack"
        Me.IgnoreBlack.Size = New System.Drawing.Size(272, 22)
        Me.IgnoreBlack.Text = "Ignore non-colors (black, grey, white)"
        '
        'ChangeDColor
        '
        Me.ChangeDColor.CheckOnClick = True
        Me.ChangeDColor.Name = "ChangeDColor"
        Me.ChangeDColor.Size = New System.Drawing.Size(272, 22)
        Me.ChangeDColor.Text = "Change Desktop Background Color"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(269, 6)
        '
        'EnableTransitions
        '
        Me.EnableTransitions.CheckOnClick = True
        Me.EnableTransitions.Name = "EnableTransitions"
        Me.EnableTransitions.Size = New System.Drawing.Size(272, 22)
        Me.EnableTransitions.Text = "Enable color transitions"
        '
        'TimingFunctionComboBox
        '
        Me.TimingFunctionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TimingFunctionComboBox.Enabled = False
        Me.TimingFunctionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.TimingFunctionComboBox.Items.AddRange(New Object() {"None", "Ease", "Linear", "Ease-in", "Ease-out", "Ease-in-out"})
        Me.TimingFunctionComboBox.Name = "TimingFunctionComboBox"
        Me.TimingFunctionComboBox.Size = New System.Drawing.Size(121, 23)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(269, 6)
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Enabled = False
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(272, 22)
        Me.ToolStripMenuItem2.Text = "Color is taken from this image part:"
        '
        'ToolStripComboBox1
        '
        Me.ToolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ToolStripComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ToolStripComboBox1.Items.AddRange(New Object() {"Entire image", "Top left", "Top center", "Top right", "Middle left", "Middle center", "Middle right", "Bottom left", "Bottom center", "Bottom right"})
        Me.ToolStripComboBox1.Name = "ToolStripComboBox1"
        Me.ToolStripComboBox1.Size = New System.Drawing.Size(121, 23)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(269, 6)
        '
        'UrsprünglicheAeroFarbeWiederherstellenToolStripMenuItem
        '
        Me.UrsprünglicheAeroFarbeWiederherstellenToolStripMenuItem.Image = Global.Aero_Adjuster.My.Resources.Resources.colorswatch
        Me.UrsprünglicheAeroFarbeWiederherstellenToolStripMenuItem.Name = "UrsprünglicheAeroFarbeWiederherstellenToolStripMenuItem"
        Me.UrsprünglicheAeroFarbeWiederherstellenToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.UrsprünglicheAeroFarbeWiederherstellenToolStripMenuItem.Text = "Restore original Aero color"
        '
        'ChangeIconColor
        '
        Me.ChangeIconColor.Name = "ChangeIconColor"
        Me.ChangeIconColor.Size = New System.Drawing.Size(272, 22)
        Me.ChangeIconColor.Text = "ToolStripMenuItem1"
        Me.ChangeIconColor.Visible = False
        '
        'SchließenToolStripMenuItem
        '
        Me.SchließenToolStripMenuItem.Image = Global.Aero_Adjuster.My.Resources.Resources.cross
        Me.SchließenToolStripMenuItem.Name = "SchließenToolStripMenuItem"
        Me.SchließenToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.SchließenToolStripMenuItem.Text = "Close"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(0, 0)
        Me.ControlBox = False
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(16, 16)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(16, 16)
        Me.Name = "Form1"
        Me.Opacity = 0.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FileSystemWatcher1 As System.IO.FileSystemWatcher
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents UrsprünglicheAeroFarbeWiederherstellenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SchließenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AdjustAeroMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InvertToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripComboBox1 As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BrightnessToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IgnoreBlack As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EnableTransitions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimingFunctionComboBox As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ChangeDColor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeIconColor As System.Windows.Forms.ToolStripMenuItem

End Class
