Imports System.IO

Namespace My
    Partial Friend Class MyApplication

        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            MessageBox.Show("There's already one instance of Aero Adjuster running!", "Single-instance only", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub
    End Class


End Namespace

