Imports System.Runtime.InteropServices

Public Class DWMColorAPI
    Public Structure DWM_COLORIZATION_PARAMS
        Public Color1 As Integer
        Public Color2 As Integer
        Public Intensity As Integer
        Public Saturation As Integer
        Public Brightness As Integer
        Public ReflectionIntensity As Integer
        Public Opaque As Boolean
    End Structure
    Private Declare Sub DWMGetColorizationParameters Lib "dwmapi.dll" Alias "#127" (ByRef parameters As DWM_COLORIZATION_PARAMS)
    Private Declare Sub DWMSetColorizationParameters Lib "dwmapi.dll" Alias "#131" (ByRef parameters As DWM_COLORIZATION_PARAMS)
    <DllImport("dwmapi.dll", PreserveSig:=False)> _
    Private Shared Function DwmIsCompositionEnabled() As Boolean
    End Function
    Public p As New DWM_COLORIZATION_PARAMS
    Private saved_p As New DWM_COLORIZATION_PARAMS
    Public ReadOnly Property SavedColor As DWM_COLORIZATION_PARAMS
        Get
            Return saved_p
        End Get
    End Property
    Public ReadOnly Property CurrentColor As DWM_COLORIZATION_PARAMS
        Get
            Return p
        End Get
    End Property
    Public Sub RestoreDWMColor()
        DWMSetColorizationParameters(saved_p)
    End Sub
    Public Sub InitDWM()
        Try
            DWMGetColorizationParameters(saved_p)
            DWMGetColorizationParameters(p)
        Catch ex As Exception
            'MessageBox.Show("Initialisierung fehlgeschlagen.")
        End Try
    End Sub
    Public Function DWMIsReady() As Boolean
        If DwmIsCompositionEnabled() = True Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Sub DWMSetColor(ByVal c As Color, ByVal saturation As Integer, ByVal brightness As Integer, ByVal reflectionintensity As Integer, ByVal intensity As Integer, ByVal opaque As Boolean)
        p.Color1 = c.ToArgb
        p.Color2 = p.Color1
        p.Saturation = saturation
        p.Brightness = brightness
        p.ReflectionIntensity = reflectionintensity
        p.Intensity = intensity
        p.Opaque = opaque
        DWMSetColorizationParameters(p)
    End Sub
    Public Sub DWMSetColor(ByVal c As Color, ByVal brightness As Integer)
        p.Color1 = c.ToArgb
        p.Color2 = p.Color1
        p.Saturation = saved_p.Saturation
        p.Brightness = brightness
        p.ReflectionIntensity = saved_p.ReflectionIntensity
        p.Intensity = saved_p.Intensity
        p.Opaque = saved_p.Opaque
        DWMSetColorizationParameters(p)
    End Sub
    Public Sub DWMSetColor(ByVal c As Color)
        p.Color1 = c.ToArgb
        p.Color2 = p.Color1
        p.Saturation = saved_p.Saturation
        p.Brightness = saved_p.Brightness
        p.ReflectionIntensity = saved_p.ReflectionIntensity
        p.Intensity = saved_p.Intensity
        p.Opaque = saved_p.Opaque
        DWMSetColorizationParameters(p)
    End Sub
    Public Sub DWMSetColor(ByVal p As DWM_COLORIZATION_PARAMS)
        DWMSetColorizationParameters(p)
    End Sub
End Class