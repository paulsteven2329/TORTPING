Imports MySql.Data.MySqlClient

Public Class fir
    Dim con As New MySql.Data.MySqlClient.MySqlConnection
    Dim cmd As New MySqlCommand
    Dim dr As MySqlDataReader
    Dim q As String
    Dim dt As New DataTable
    Dim da As New MySqlDataAdapter(cmd)


    Dim X As Integer = 0
    Dim Y As Integer = 0

    Private Sub fir_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Visible = True
        Label2.Visible = False
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = False
        Label6.Visible = False
        Label7.Visible = False
        Label8.Visible = False
        Label9.Visible = False
        Label10.Visible = False
        Label11.Visible = False
        Label12.Visible = False
        Label13.Visible = False
        Label14.Visible = False
        Label15.Visible = False
        Label16.Visible = False
        Label17.Visible = False
        Label18.Visible = False
        Label19.Visible = False
        Label20.Visible = False
        Label21.Visible = False
        Label22.Visible = False
        Label23.Visible = False
        Label24.Visible = False
        Label25.Visible = False
        Label26.Visible = False
        Label27.Visible = False
        Label28.Visible = False
        Label29.Visible = False
        Label30.Visible = False
        Label31.Visible = False

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim myConnectionString As String
        myConnectionString = "datasource=localhost;" _
          & "port=2929;" _
          & "user id=root;" _
          & "password=2930;" _
          & "database=tortpingdb"
        con.ConnectionString = myConnectionString
        Label2.Text = "CPMPLAINT DETAIL"
        Label3.Text = "incident Detail"
        Label4.Visible = True
        Label5.Visible = True
        Label6.Visible = True
        Label7.Visible = True
        Label8.Visible = True
        Label9.Visible = True
        Label10.Visible = True
        Label11.Visible = True
        Label12.Visible = True
        Label13.Visible = True
        Label14.Visible = True
        Label15.Visible = True
        Label16.Visible = True
        Label17.Visible = True
        Label18.Visible = True
        Label19.Visible = True
        Label20.Visible = True

        con.Open()

        q = "SELECT * FROM cpt WHERE cp_id ='" & TextBox1.Text & "' "

        Using con
            Using cmd As New MySqlCommand(q, con)

                dr = cmd.ExecuteReader()
                While dr.Read()
                    Label20.Text = dr("cp_id").ToString()
                    Label4.Text = dr("cp_name").ToString()
                    Label5.Text = dr("cp_phone").ToString()
                    Label6.Text = dr("cp_email").ToString()
                    Label7.Text = dr("cp_country").ToString()
                    Label8.Text = dr("cp_state").ToString()
                    Label9.Text = dr("cp_city").ToString()
                    Label10.Text = dr("cp_street").ToString()
                    Label11.Text = dr("cp_date").ToString()
                    Label12.Text = dr("cp_ps").ToString()
                    Label13.Text = dr("i_category").ToString()
                    Label14.Text = dr("i_place").ToString()
                    Label15.Text = dr("i_fdate").ToString()
                    Label16.Text = dr("i_tdate").ToString()
                    Label1.Visible = False
                    Button3.Visible = False
                    TextBox1.Visible = False
                    Label17.Visible = True
                    Label18.Visible = True
                    Label19.Visible = True
                    Label21.Visible = True
                    Label22.Visible = True
                    Label23.Visible = True
                    Label24.Visible = True
                    Label25.Visible = True
                    Label26.Visible = True
                    Label27.Visible = True
                    Label28.Visible = True
                    Label29.Visible = True
                    Label30.Visible = True
                    Label31.Visible = True
                End While
                dr.Close()
            End Using
        End Using
        con.Close()
        con.Dispose()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TextBox1.Text = "" Then
            MessageBox.Show("Please enter the complaint id and click on the submit")
            Exit Sub
        End If

        PrintDialog1.Document = Me.PrintDocument1
        Dim buttonpressed As DialogResult = PrintDialog1.ShowDialog()

        If (buttonpressed = DialogResult.OK) Then
            Me.Height = Me.Height + Y
            Panel1.Height = Panel1.Height + Y
            PrintDocument1.Print()
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim bm As New Bitmap(Me.Panel1.Width, Me.Panel1.Height)
        Panel1.DrawToBitmap(bm, New Rectangle(0, 0, Me.Panel1.Width, Me.Panel1.Height))
        e.Graphics.DrawImage(bm, 50, 40)
        Dim psd As New PageSetupDialog
        psd.Document = PrintDocument1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form2.Show()
        Me.Close()

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class