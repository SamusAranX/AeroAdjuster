Option Strict On
Imports Microsoft.Win32
Imports System.Drawing.Imaging
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Threading
Imports System.Text.RegularExpressions

Public Class Form1

    Dim dwm As New DWMColorAPI
    Public settings As New Settings

    'Lerp-Zeugs
    Dim amount As Single
    WithEvents LerpTimer As New Windows.Forms.Timer With {.Interval = 25}
    Dim doLerp As Boolean = True

    'Für ColorHelper
    Dim colorDiff As Integer = 20

    'Timingfunktionen
    Dim ease As PointF() = {New PointF(0.25, 0.1), New PointF(0.25, 1.0)}
    Dim linear As PointF() = {New PointF(0, 0), New PointF(1, 1)}
    Dim easein As PointF() = {New PointF(0.42, 0), New PointF(1, 1)}
    Dim easeout As PointF() = {New PointF(0, 0), New PointF(0.58, 1)}
    Dim easeinout As PointF() = {New PointF(0.42, 0), New PointF(0.58, 1)}
    Public _timingfunctions As PointF()() = {ease, linear, easein, easeout, easeinout}
    Dim currentIndex As Integer

    Public Shared Function readXML() As Settings
        Try
            If File.Exists(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Aero Adjuster"), "settings.xml")) Then
                Dim sett As New Settings
                Dim x As New Xml.Serialization.XmlSerializer(sett.GetType)
                Dim enc As New System.Text.UTF8Encoding

                Using fs As New FileStream(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Aero Adjuster"), "settings.xml"), FileMode.Open)
                    Try
                        Return DirectCast(x.Deserialize(fs), Settings)
                    Catch ex As Exception
                        Dim s As New Settings
                        With s
                            .adjustBrightness = True
                            .ignoreNonColors = True
                            .invertColor = False
                            .picPort = Aero_Adjuster.Settings.PicturePortion.EntirePicture
                            .timingFunction = Aero_Adjuster.Settings.TimingFunctions.Ease
                            .transitions = True
                            Return s
                        End With
                    End Try
                End Using
            Else
                Return New Settings
            End If
        Catch ex As Exception
            Return New Settings
        End Try
    End Function

    Public Shared Function writeXML(ByVal sett As Settings) As Boolean
        Try
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            Dim x As New Xml.Serialization.XmlSerializer(sett.GetType())
            Dim enc As New System.Text.UTF8Encoding
            Dim xml As New Xml.XmlTextWriter(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Aero Adjuster"), "settings.xml"), enc)

            x.Serialize(xml, sett)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function Clamp(ByVal max As Single, ByVal val As Single) As Single
        Return Math.Max(0.0F, Math.Min(max, val))
    End Function

    Private Sub ChangeColor(ByVal from As ColorAdjustmentHelper, ByVal [to] As ColorAdjustmentHelper, ByVal [step] As Single)
        'dwm.DWMSetColor(CubicBezier(from.col, [to].col, [step]), Lerp(from.brightness, [to].brightness, [step]))
        If settings.adjustBrightness Then
            Dim r As Integer = Convert.ToInt32(Clamp(255.0F, Lerp(from.col.R, [to].col.R, Clamp(1, [step]))))
            Dim g As Integer = Convert.ToInt32(Clamp(255.0F, Lerp(from.col.G, [to].col.G, Clamp(1, [step]))))
            Dim b As Integer = Convert.ToInt32(Clamp(255.0F, Lerp(from.col.B, [to].col.B, Clamp(1, [step]))))
            dwm.DWMSetColor(Color.FromArgb(r, g, b), Convert.ToInt32(Clamp(120.0F, Lerp(from.brightness, [to].brightness, [step]))))
        Else
            Dim r As Integer = Convert.ToInt32(Clamp(255.0F, Lerp(from.col.R, [to].col.R, Clamp(1, [step]))))
            Dim g As Integer = Convert.ToInt32(Clamp(255.0F, Lerp(from.col.G, [to].col.G, Clamp(1, [step]))))
            Dim b As Integer = Convert.ToInt32(Clamp(255.0F, Lerp(from.col.B, [to].col.B, Clamp(1, [step]))))
            dwm.DWMSetColor(Color.FromArgb(r, g, b))
        End If
    End Sub

    Private Function Lerp(ByVal start As Single, ByVal [end] As Single, ByVal amount As Single) As Single
        Dim difference As Single = [end] - start
        Dim ci As Single

        'Trace.WriteLine(settings.timingFunction)
        Dim tf As PointF() = _timingfunctions(settings.timingFunction)
        ci = CubicInterpolate(New PointF(tf(0).X, tf(0).Y), New PointF(tf(1).X, tf(1).Y), amount).Y
        'Trace.WriteLine(ci)

        Dim adjusted As Single = difference * ci
        Return start + adjusted
    End Function

    Private Function CubicBezier(ByVal P1 As Single, ByVal P2 As Single, ByVal t As Single) As Single
        'Dim a As Single = Convert.ToSingle((1 - t) ^ 2 * 0)
        Dim b As Single = 2 * (1 - t) * t * P1
        Dim c As Single = Convert.ToSingle(t ^ 2 * P2)

        'Return a + b + c
        Return b + c
    End Function

    'Aus Mangel an Vector2 in VB.NET hab ich einfach PointF genommen, da ich eh nur X und Y davon brauche
    Private Function CubicInterpolate(ByVal P1 As PointF, ByVal P2 As PointF, ByVal t As Single) As PointF
        Return New PointF(CubicBezier(P1.X, P2.X, t), CubicBezier(P1.Y, P2.Y, t))
        'Y ist der Output für den Zeitwert t
    End Function

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        writeXML(settings)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Trace.WriteLine(String.Format("Desktopfarbe: rgb({0},{1},{2})", SystemColors.Desktop.R, SystemColors.Desktop.G, SystemColors.Desktop.B))
        dwm.InitDWM()

        If Not Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Aero Adjuster")) Then
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Aero Adjuster"))
        End If
        Dim wp As New IO.FileInfo(GetCurrentWallpaper)
        FileSystemWatcher1.Path = wp.DirectoryName
        settings = readXML()

        RemoveHandler InvertToolStripMenuItem.CheckedChanged, AddressOf InvertToolStripMenuItem_CheckedChanged
        InvertToolStripMenuItem.Checked = settings.invertColor
        If InvertToolStripMenuItem.Checked Then
            InvertToolStripMenuItem.Image = My.Resources.colorinvert
        Else
            InvertToolStripMenuItem.Image = My.Resources.color
        End If
        AddHandler InvertToolStripMenuItem.CheckedChanged, AddressOf InvertToolStripMenuItem_CheckedChanged

        RemoveHandler BrightnessToolStripMenuItem.CheckedChanged, AddressOf BrightnessToolStripMenuItem_CheckedChanged
        BrightnessToolStripMenuItem.Checked = settings.adjustBrightness
        If BrightnessToolStripMenuItem.Checked Then
            BrightnessToolStripMenuItem.Image = My.Resources.brightness
        Else
            BrightnessToolStripMenuItem.Image = My.Resources.brightness_off
        End If
        AddHandler BrightnessToolStripMenuItem.CheckedChanged, AddressOf BrightnessToolStripMenuItem_CheckedChanged

        RemoveHandler IgnoreBlack.CheckedChanged, AddressOf IgnoreBlack_CheckedChanged
        IgnoreBlack.Checked = settings.ignoreNonColors
        If IgnoreBlack.Checked Then
            IgnoreBlack.Image = My.Resources.ignorenoncolors
        Else
            IgnoreBlack.Image = My.Resources.dontignorenoncolors
        End If
        AddHandler IgnoreBlack.CheckedChanged, AddressOf IgnoreBlack_CheckedChanged

        RemoveHandler ChangeDColor.CheckedChanged, AddressOf ChangeDColor_CheckedChanged
        ChangeDColor.Checked = settings.changeDesktopColors
        If ChangeDColor.Checked Then
            ChangeDColor.Image = My.Resources.paintcan
        Else
            ChangeDColor.Image = My.Resources.nopaint
        End If
        AddHandler ChangeDColor.CheckedChanged, AddressOf ChangeDColor_CheckedChanged

        RemoveHandler EnableTransitions.CheckedChanged, AddressOf EnableTransitions_CheckedChanged
        EnableTransitions.Checked = settings.transitions
        doLerp = EnableTransitions.Checked
        TimingFunctionComboBox.Enabled = EnableTransitions.Checked
        If EnableTransitions.Checked Then
            EnableTransitions.Image = My.Resources.transitions
        Else
            EnableTransitions.Image = My.Resources.notransitions
        End If
        AddHandler EnableTransitions.CheckedChanged, AddressOf EnableTransitions_CheckedChanged

        RemoveHandler TimingFunctionComboBox.SelectedIndexChanged, AddressOf TimingFunctionComboBox_SelectedIndexChanged
        TimingFunctionComboBox.SelectedIndex = settings.timingFunction
        If TimingFunctionComboBox.SelectedIndex = 0 Then
            TimingFunctionComboBox.SelectedIndex = 2
        End If
        currentIndex = settings.timingFunction
        AddHandler TimingFunctionComboBox.SelectedIndexChanged, AddressOf TimingFunctionComboBox_SelectedIndexChanged

        RemoveHandler ToolStripComboBox1.SelectedIndexChanged, AddressOf ToolStripComboBox1_SelectedIndexChanged
        ToolStripComboBox1.SelectedIndex = settings.picPort
        AddHandler ToolStripComboBox1.SelectedIndexChanged, AddressOf ToolStripComboBox1_SelectedIndexChanged
        'AdjustColor()
    End Sub

    Friend Function GetCurrentWallpaper() As String
        Dim wp As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", False)
        Dim wpp As String = wp.GetValue("WallPaper").ToString()
        wp.Close()
        Return wpp
    End Function

    Private Sub AdjustColor(ByVal lerp As Boolean)
        If lerp Then
            LerpTimerStart()
        Else
            AdjustAeroColor()
        End If
    End Sub

    Private Sub AdjustAeroColor()
        Dim av As ColorAdjustmentHelper = getAverageColor()
        'Trace.WriteLine("ping")
        While av.col = Color.Transparent
            'Trace.WriteLine("transparent")
            av = getAverageColor()
        End While
        'Trace.WriteLine("pong")
        If settings.adjustBrightness Then
            dwm.DWMSetColor(av.col, av.brightness)
        Else
            dwm.DWMSetColor(av.col)
        End If
        Dim rk As RegistryKey
        rk = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\DWM", True)
        Dim hex As Integer = av.col.ToArgb 'Hexadezimalform der Farbe
        Dim brightness As Integer = av.brightness
        rk.SetValue("ColorizationAfterglow", hex)
        rk.SetValue("ColorizationColor", hex)
        rk.SetValue("ColorizationBlurBalance", brightness)
        rk.Close()

        DesktopColors.SetColor(av.col)
    End Sub

    Private Sub FileSystemWatcher1_Changed(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles FileSystemWatcher1.Changed
        AdjustColor(doLerp)
    End Sub

    Private Function getAverageColor() As ColorAdjustmentHelper
        Static safetynet As Integer
        Try
            Dim cah As New ColorAdjustmentHelper
            Dim col As Color
            Using fs As New IO.FileStream(GetCurrentWallpaper, IO.FileMode.Open, IO.FileAccess.Read, FileShare.ReadWrite)
                Using orig As Bitmap = CType(Bitmap.FromStream(fs, True, False), Bitmap)
                    'Dim width As Integer = If((CInt(orig.Width / 3) - 1) < 1, CInt(orig.Width / 3), CInt(orig.Width / 3) - 1)
                    'Dim height As Integer = If((CInt(orig.Height / 3) - 1) < 1, CInt(orig.Height / 3), CInt(orig.Height / 3) - 1)
                    Dim width As Integer = CInt(orig.Width / 3)
                    Dim height As Integer = CInt(orig.Height / 3)
#If DEBUG Then
                    Trace.WriteLine("Original: " & orig.Width.ToString & " " & orig.Height.ToString & " - Abschnitt: " & width.ToString & " " & height.ToString)
#End If
                    Dim rect As New Rectangle(0, 0, width, height)
                    Using bmp As New Bitmap(width, height)
                        Using bmpg As Graphics = Graphics.FromImage(bmp)
                            Select Case settings.picPort
                                'TODO: Auf Teilungsergebnisse kleiner als 1 prüfen
                                '^Done. Jdfs. sollten hier keine Fehler mehr auftreten
                                Case Aero_Adjuster.Settings.PicturePortion.EntirePicture
                                    rect = New Rectangle(0, 0, orig.Width, orig.Height)
                                    bmpg.DrawImage(orig, rect, 0, 0, orig.Width, orig.Height, GraphicsUnit.Pixel)
                                    col = ColorHelper.ColorHelper.CalculateAverageColor(bmp, IgnoreBlack.Checked, colorDiff)

                                Case Aero_Adjuster.Settings.PicturePortion.TopLeft
                                    bmpg.DrawImage(orig, rect, 0, 0, width, height, GraphicsUnit.Pixel)
                                    col = ColorHelper.ColorHelper.CalculateAverageColor(bmp, IgnoreBlack.Checked, colorDiff)

                                Case Aero_Adjuster.Settings.PicturePortion.TopCenter
                                    bmpg.DrawImage(orig, rect, width, 0, width, height, GraphicsUnit.Pixel)
                                    col = ColorHelper.ColorHelper.CalculateAverageColor(bmp, IgnoreBlack.Checked, colorDiff)

                                Case Aero_Adjuster.Settings.PicturePortion.TopRight
                                    bmpg.DrawImage(orig, rect, CInt(width * 2), 0, width, height, GraphicsUnit.Pixel)
                                    col = ColorHelper.ColorHelper.CalculateAverageColor(bmp, IgnoreBlack.Checked, colorDiff)

                                Case Aero_Adjuster.Settings.PicturePortion.MiddleLeft
                                    bmpg.DrawImage(orig, rect, 0, height, width, height, GraphicsUnit.Pixel)
                                    col = ColorHelper.ColorHelper.CalculateAverageColor(bmp, IgnoreBlack.Checked, colorDiff)

                                Case Aero_Adjuster.Settings.PicturePortion.MiddleCenter
                                    bmpg.DrawImage(orig, rect, width, height, width, height, GraphicsUnit.Pixel)
                                    col = ColorHelper.ColorHelper.CalculateAverageColor(bmp, IgnoreBlack.Checked, colorDiff)

                                Case Aero_Adjuster.Settings.PicturePortion.MiddleRight
                                    bmpg.DrawImage(orig, rect, CInt(width * 2), height, width, height, GraphicsUnit.Pixel)
                                    col = ColorHelper.ColorHelper.CalculateAverageColor(bmp, IgnoreBlack.Checked, colorDiff)

                                Case Aero_Adjuster.Settings.PicturePortion.BottomLeft
                                    bmpg.DrawImage(orig, rect, 0, CInt(height * 2), width, height, GraphicsUnit.Pixel)
                                    col = ColorHelper.ColorHelper.CalculateAverageColor(bmp, IgnoreBlack.Checked, colorDiff)

                                Case Aero_Adjuster.Settings.PicturePortion.BottomCenter
                                    bmpg.DrawImage(orig, rect, width, CInt(height * 2), width, height, GraphicsUnit.Pixel)
                                    col = ColorHelper.ColorHelper.CalculateAverageColor(bmp, IgnoreBlack.Checked, colorDiff)

                                Case Aero_Adjuster.Settings.PicturePortion.BottomRight
                                    bmpg.DrawImage(orig, rect, CInt(width * 2), CInt(height * 2), width, height, GraphicsUnit.Pixel)
                                    col = ColorHelper.ColorHelper.CalculateAverageColor(bmp, IgnoreBlack.Checked, colorDiff)

                            End Select

                            If settings.invertColor Then
                                cah.col = Color.FromArgb(col.R Xor 255, col.G Xor 255, col.B Xor 255)
                            Else
                                cah.col = col
                            End If

                            'Dim hslol As New HSL With {.H  col.GetHue, .S = 0, .L = (col.GetBrightness - col.GetSaturation) / 2}
                            'col = _HSL.HSL_to_RGB(hslol)
                            Try
                                cah.brightness = Convert.ToInt32(((Convert.ToInt32(col.R) + Convert.ToInt32(col.G) + Convert.ToInt32(col.B)) / 3) * 0.47)
                            Catch ex As OverflowException 'Evtl. seit 1.6 obsolet, da alle Bytes in Integer umgerechnet werden
                                Try
                                    cah.brightness = Convert.ToInt32(((Convert.ToInt32(col.R) + Convert.ToInt32(col.G) + Convert.ToInt32(col.B) + 10) / 3) * 0.47)
                                Catch ex2 As OverflowException
                                    cah.brightness = 128 'Hoffentlich wird das NIE ausgeführt
                                End Try
                            End Try
                        End Using
                    End Using
                End Using
            End Using

            safetynet = 0
            Return cah
#If DEBUG Then
        Catch ex As FormatException
            Trace.WriteLine(ex.Message)
            Application.Exit()
#End If
        Catch ex As Exception
            safetynet += 1
            If safetynet = 3 Then
                safetynet = 0
                Return New ColorAdjustmentHelper With {.brightness = 42, .col = Color.DarkGray}
            End If
            Return New ColorAdjustmentHelper With {.brightness = 0, .col = Color.Transparent}
        End Try
    End Function

    Private Sub SchließenToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SchließenToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub UrsprünglicheAeroFarbeWiederherstellenToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UrsprünglicheAeroFarbeWiederherstellenToolStripMenuItem.Click
        If doLerp Then
            LerpTimerStart(New ColorAdjustmentHelper With {.brightness = dwm.SavedColor.Brightness, .col = Color.FromArgb(dwm.SavedColor.Color1)})
        Else
            dwm.DWMSetColor(dwm.SavedColor)
            DesktopColors.RestoreColor()
        End If
    End Sub

    Private Sub AdjustAeroMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdjustAeroMenuItem.Click
        AdjustColor(doLerp)
    End Sub

    Private Sub InvertToolStripMenuItem_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles InvertToolStripMenuItem.CheckedChanged
        settings.invertColor = InvertToolStripMenuItem.Checked
        If InvertToolStripMenuItem.Checked Then
            InvertToolStripMenuItem.Image = My.Resources.colorinvert
        Else
            InvertToolStripMenuItem.Image = My.Resources.color
        End If
        AdjustColor(doLerp)
    End Sub

    Private Sub BrightnessToolStripMenuItem_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BrightnessToolStripMenuItem.CheckedChanged
        settings.adjustBrightness = BrightnessToolStripMenuItem.Checked
        If BrightnessToolStripMenuItem.Checked Then
            BrightnessToolStripMenuItem.Image = My.Resources.brightness
        Else
            BrightnessToolStripMenuItem.Image = My.Resources.brightness_off
        End If
        AdjustColor(doLerp)
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        settings.picPort = CType(ToolStripComboBox1.SelectedIndex, Aero_Adjuster.Settings.PicturePortion)
        AdjustColor(doLerp)
    End Sub

    Private Sub IgnoreBlack_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles IgnoreBlack.CheckedChanged
        settings.ignoreNonColors = IgnoreBlack.Checked
        If IgnoreBlack.Checked Then
            IgnoreBlack.Image = My.Resources.ignorenoncolors
        Else
            IgnoreBlack.Image = My.Resources.dontignorenoncolors
        End If
        AdjustColor(doLerp)
    End Sub

    Private Sub ChangeDColor_CheckedChanged(sender As Object, e As System.EventArgs) Handles ChangeDColor.CheckedChanged
        settings.changeDesktopColors = ChangeDColor.Checked
        If ChangeDColor.Checked Then
            ChangeDColor.Image = My.Resources.paintcan
        Else
            ChangeDColor.Image = My.Resources.nopaint
        End If
        AdjustColor(doLerp)
    End Sub

    Private Sub EnableTransitions_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles EnableTransitions.CheckedChanged
        settings.transitions = EnableTransitions.Checked
        doLerp = EnableTransitions.Checked
        TimingFunctionComboBox.Enabled = EnableTransitions.Checked
        If EnableTransitions.Checked Then
            If TimingFunctionComboBox.SelectedIndex = 0 Then
                settings.timingFunction = DirectCast(2, Aero_Adjuster.Settings.TimingFunctions)
                TimingFunctionComboBox.SelectedIndex = 2
            End If
            EnableTransitions.Image = My.Resources.transitions
        Else
            EnableTransitions.Image = My.Resources.notransitions
        End If
    End Sub

    Private Sub TimingFunctionComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimingFunctionComboBox.SelectedIndexChanged
        settings.timingFunction = DirectCast(TimingFunctionComboBox.SelectedIndex, Settings.TimingFunctions)
        Select Case TimingFunctionComboBox.SelectedIndex
            Case 0
                EnableTransitions.Checked = False
            Case 1 To 5
                EnableTransitions.Checked = True
        End Select
        currentIndex = TimingFunctionComboBox.SelectedIndex
    End Sub

    Private Sub LerpTimerStart()
        Dim av As ColorAdjustmentHelper = getAverageColor()
        While av.col = Color.Transparent
            av = getAverageColor()
        End While
        Dim lh As New LerpHelper With {.fromColor = New ColorAdjustmentHelper With {.brightness = dwm.CurrentColor.Brightness, .col = Color.FromArgb(dwm.CurrentColor.Color1)}, .toColor = av}
        LerpTimer.Tag = lh
        LerpTimer.Start()
    End Sub

    Private Sub LerpTimerStart(ByVal cah As ColorAdjustmentHelper)
        Dim lh As New LerpHelper With {.fromColor = New ColorAdjustmentHelper With {.brightness = dwm.CurrentColor.Brightness, .col = Color.FromArgb(dwm.CurrentColor.Color1)}, .toColor = cah}
        LerpTimer.Tag = lh
        LerpTimer.Start()
    End Sub

    Private Sub LerpTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LerpTimer.Tick
        'Trace.WriteLine(amount)
        Dim col As Color = DirectCast(LerpTimer.Tag, LerpHelper).toColor.col
        If amount <= 0 AndAlso settings.changeDesktopColors Then
            DesktopColors.SetColor(col)
        End If

        amount += LerpTimer.Interval / 1000.0F
        ChangeColor(DirectCast(LerpTimer.Tag, LerpHelper).fromColor, DirectCast(LerpTimer.Tag, LerpHelper).toColor, amount)
        If amount > 1 Then
            amount = 0
            Dim rk As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\DWM", True)
            Dim hex As Integer = col.ToArgb 'Hexadezimalform der Farbe
            Dim brightness As Integer = DirectCast(LerpTimer.Tag, LerpHelper).toColor.brightness
            rk.SetValue("ColorizationAfterglow", hex)
            rk.SetValue("ColorizationColor", hex)
            rk.SetValue("ColorizationBlurBalance", brightness)
            rk.Close()

            LerpTimer.Stop()
        End If
    End Sub
End Class