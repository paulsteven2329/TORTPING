Public Class Form7
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form2.Show()
        Me.Hide()

    End Sub

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cr()
    End Sub


    Private Sub cr()
        Dim rect As New Rectangle(0, 0, Me.Width, Me.Height)
        Dim path As New System.Drawing.Drawing2D.GraphicsPath()
        path.AddArc(rect.X, rect.Y, 40, 40, 180, 90)
        path.AddArc(rect.X + rect.Width - 40, rect.Y, 40, 40, 270, 90)
        path.AddArc(rect.X + rect.Width - 40, rect.Y + rect.Height - 40, 40, 40, 0, 90)
        path.AddArc(rect.X, rect.Y + rect.Height - 40, 40, 40, 90, 90)
        path.CloseAllFigures()

        Me.Region = New System.Drawing.Region(path)
    End Sub
End Class