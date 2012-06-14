Public Class HSL
    Private _h As Double
    Private _s As Double
    Private _l As Double

    Public Sub New()
        _h = 0
        _s = 0
        _l = 0
    End Sub

    Public Property H As Double
        Get
            Return _h
        End Get
        Set(ByVal value As Double)
            _h = value
            _h = If(_h > 1, 1, If(_h < 0, 0, _h))
        End Set
    End Property

    Public Property S As Double
        Get
            Return _s
        End Get
        Set(ByVal value As Double)
            _s = value
            _s = If(_s > 1, 1, If(_s < 0, 0, _s))
        End Set
    End Property

    Public Property L As Double
        Get
            Return _l
        End Get
        Set(ByVal value As Double)
            _l = value
            _l = If(_l > 1, 1, If(_l < 0, 0, _l))
        End Set
    End Property
End Class

Public Class _HSL
    Public Shared Function HSL_to_RGB(ByVal hsl As HSL) As Color
        Dim r As Double = 0
        Dim g As Double = 0
        Dim b As Double = 0
        Dim temp1, temp2 As Double
        If hsl.L = 0 Then
            r = 0
            g = 0
            b = 0
        Else
            If hsl.S = 0 Then
                r = hsl.L
                g = hsl.L
                b = hsl.L
            Else
                temp2 = If(hsl.L <= 0.5, hsl.L * (1.0 + hsl.S), hsl.L + hsl.S - hsl.L * hsl.S)
                temp1 = 2.0 * hsl.L - temp2
                Dim t3() As Double = {hsl.H + 1.0 / 3.0, hsl.H, hsl.H - 1.0 / 3.0}
                Dim clr() As Double = {0, 0, 0}
                Dim i As Integer
                For i = 0 To 2
                    If t3(i) < 0 Then
                        t3(i) += 1.0
                    End If
                    If t3(i) > 1 Then
                        t3(i) -= 1.0
                    End If
                    If 6.0 * t3(i) < 1.0 Then
                        clr(i) = temp1 + (temp2 - temp1) * t3(i) * 6.0
                    ElseIf 2.0 * t3(i) < 1.0 Then
                        clr(i) = temp2
                    ElseIf 3.0 * t3(i) < 2.0 Then
                        clr(i) = temp1 + (temp2 - temp1) * (2.0 / 3.0 - t3(i)) * 6.0
                    Else
                        clr(i) = temp1
                    End If
                Next i
                r = clr(0)
                g = clr(1)
                b = clr(2)
            End If
        End If

        Return Color.FromArgb(CInt(255 * r), CInt(255 * g), CInt(255 * b))
    End Function
End Class