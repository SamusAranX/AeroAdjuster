Public Class Settings
    Public Enum PicturePortion
        EntirePicture
        TopLeft
        TopCenter
        TopRight
        MiddleLeft
        MiddleCenter
        MiddleRight
        BottomLeft
        BottomCenter
        BottomRight
    End Enum

    Public Enum TimingFunctions
        None
        Ease
        Linear
        EaseIn
        EaseOut
        EaseInOut
    End Enum

    Public Property invertColor As Boolean

    Public Property adjustBrightness As Boolean

    Public Property picPort As PicturePortion

    Public Property ignoreNonColors As Boolean

    Public Property transitions As Boolean

    Public Property timingFunction As TimingFunctions

    Public Property changeDesktopColors As Boolean
End Class
