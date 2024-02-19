Imports MySql.Data.MySqlClient

Public Class report

    Dim con As New MySql.Data.MySqlClient.MySqlConnection
    Dim cmd As New MySqlCommand
    Dim dr As MySqlDataReader
    Dim q As String
    Dim dt As New DataTable
    Dim da As New MySqlDataAdapter(cmd)
    Private Sub report_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim myConnectionString As String
        myConnectionString = "datasource=localhost;" _
          & "port=2929;" _
          & "user id=root;" _
          & "password=2930;" _
          & "database=tortpingdb"
        con.ConnectionString = myConnectionString


        con.Open()

        Try



            Dim table1 As New DataTable()
            cmd = New MySqlCommand("SELECT * FROM cpt", con)
            da.SelectCommand = cmd
            da.Fill(table1)
            DataGridView1.DataSource = table1

        Catch ex As MySqlException
            MessageBox.Show("Error connecting to the database. " & ex.Message)
        Finally
            da.Dispose()
            con.Close()
            con.Dispose()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim myConnectionString As String
        myConnectionString = "datasource=localhost;" _
          & "port=2929;" _
          & "user id=root;" _
          & "password=2930;" _
          & "database=tortpingdb"
        con.ConnectionString = myConnectionString

        If con.State = ConnectionState.Closed Then

            con.Open()
        End If
        Try

            Dim table1 As New DataTable()
            cmd = New MySqlCommand("SELECT * FROM cpt where cp_date between @date1 and @date2", con)
            cmd.Parameters.AddWithValue("@date1", DateTimePicker1.Value.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@date2", DateTimePicker2.Value.ToString("yyyy-MM-dd"))
            da.SelectCommand = cmd
            da.Fill(table1)
            DataGridView1.DataSource = table1

        Catch ex As MySqlException
            MessageBox.Show("Error connecting to the database. " & ex.Message)
        Finally
            con.Close()
            con.Dispose()
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.Show()
        Me.Close()
    End Sub
End Class