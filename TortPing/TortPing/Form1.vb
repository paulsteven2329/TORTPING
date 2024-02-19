Imports System.Reflection.Emit
Imports MySql.Data.MySqlClient
Imports System.Drawing.Drawing2D

Public Class Form1
    Dim con As New MySql.Data.MySqlClient.MySqlConnection
    Dim cmd As New MySqlCommand
    Dim dr As MySqlDataReader




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim radius As Integer = 10 ' set the border radius

        For Each btn As Button In Me.Controls.OfType(Of Button)()
            Dim path As New GraphicsPath()
            Dim rect As Rectangle = New Rectangle(0, 0, btn.Width, btn.Height)
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90)
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90)
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90)
            path.CloseAllFigures()
            btn.Region = New Region(path)
        Next


        Dim myConnectionString As String
        myConnectionString = "datasource=localhost;" _
             & "port=2929;" _
             & "user id=root;" _
             & "password=2930;" _
             & "database=tortpingdb"

        Try
            con.ConnectionString = myConnectionString
            con.Open()

        Catch ex As MySqlException
            MessageBox.Show("Error connecting to the database. " & ex.Message)
            Application.Exit()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim q As String = "SELECT * FROM login WHERE a_id='" & TextBox1.Text & "' AND a_psd='" & TextBox2.Text & "'"

        Try
            cmd = New MySqlCommand(q, con)
            dr = cmd.ExecuteReader

            Dim count As Integer
            count = 0
            While dr.Read()
                count += 1
            End While

            If count = 1 Then
                MessageBox.Show("SIGNING IN..")
                Form2.Show()
                Me.Hide()

            ElseIf count > 1 Then
                MessageBox.Show("Invalid Admin Id or Password")
            Else
                MessageBox.Show("Invalid Admin Id or Password")
            End If
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
            dr.Close()
        End Try

        TextBox1.Text = ""
        TextBox2.Text = ""
        con.Close()
        con.Dispose()
        Form1_Load(sender, e)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim qu As DialogResult

        qu = MessageBox.Show("Are you sure you want to exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If qu = DialogResult.Yes Then
            Application.Exit()
        End If

    End Sub



    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        forgotpwd.Show()
        Me.Hide()
    End Sub
End Class