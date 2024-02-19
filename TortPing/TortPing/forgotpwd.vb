Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class forgotpwd
    Dim con As New MySql.Data.MySqlClient.MySqlConnection
    Dim cmd As New MySqlCommand
    Dim dr As MySqlDataReader
    Dim q As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim r As Integer
        Dim myConnectionString As String
        myConnectionString = "datasource=localhost;" _
             & "port=2929;" _
             & "user id=root;" _
             & "password=2930;" _
             & "database=tortpingdb"
        con.ConnectionString = myConnectionString
        Try
            con.Open()
            cmd = New MySqlCommand()
            With cmd
                .Connection = con
                .CommandText = "UPDATE LOGIN SET A_PSD=@A_PSD WHERE A_ID=@A_ID"
                .CommandType = CommandType.Text
                .Parameters.AddWithValue("@A_ID", TextBox1.Text)
                .Parameters.AddWithValue("@A_PSD", TextBox2.Text)
            End With
            r = cmd.ExecuteNonQuery()
            If r = 0 Then
                MessageBox.Show("PASSWORD UPDATE FAILED.")
            Else
                MessageBox.Show("PASSWORD CHANGED..")
                Form1.Show()
                Me.Close()
            End If
            con.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            con.Dispose()
        End Try

        TextBox1.Text = ""
        TextBox2.Text = ""
        con.Close()
        con.Dispose()
    End Sub
End Class
