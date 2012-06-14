Imports System.Runtime.InteropServices

Public Class DesktopColors
    <DllImport("user32.dll")> _
    Private Shared Function SetSysColors(cElements As Integer, lpaElements As Integer(), lpaRgbValues As UInteger()) As Boolean
    End Function

    Public Shared dcolor As Color = SystemColors.Desktop
    Public Shared elements As Integer() = {1}
    Public Shared dcolors As UInteger() = {CUInt(System.Drawing.ColorTranslator.ToWin32(dcolor))}

    Public Shared Sub SetColor(col As Color)
        Dim colors As UInteger() = {CUInt(ColorTranslator.ToWin32(col))}
        SetSysColors(elements.Length, elements, colors)
        Dim dc As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\Colors", True)
        dc.SetValue("Background", String.Format("{0} {1} {2}", col.R, col.G, col.B))
        dc.Close()
    End Sub

    Public Shared Sub RestoreColor()
        SetSysColors(elements.Length, elements, dcolors)
        Dim dc As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\Colors", True)
        dc.SetValue("Background", String.Format("{0} {1} {2}", dcolor.R, dcolor.G, dcolor.B))
        dc.Close()
    End Sub
End Class
