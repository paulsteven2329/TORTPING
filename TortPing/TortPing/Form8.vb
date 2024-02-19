Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Form8
    Dim con As New MySql.Data.MySqlClient.MySqlConnection
    Dim cmd As New MySqlCommand
    Dim dr As MySqlDataReader
    Dim da As New MySqlDataAdapter(cmd)
    Dim q As String



    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        upd()

    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Form2.Show()
        Me.Close()

    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim pwd As String = "1234"
        Dim apwd As String = InputBox("ENTER THE PASSWORD ", "Enter HERE")
        If pwd = apwd Then
            Form9.Show()
            Me.Hide()
        Else
            MessageBox.Show("ERROR!!!!")
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If con.State = ConnectionState.Closed Then
            conn()
            con.Open()
        End If
        Try

            Dim q As String = "INSERT INTO login(a_id, a_name, a_psd,a_phone, a_email, a_date) VALUES(@a_id, @a_name, @a_psd, @a_phone, @a_email, @a_date)"
            Using cmd As New MySqlCommand(q, con)
                cmd.Parameters.AddWithValue("@a_id", TextBox1.Text)
                cmd.Parameters.AddWithValue("@a_name", TextBox2.Text)
                cmd.Parameters.AddWithValue("@a_psd", TextBox3.Text)
                cmd.Parameters.AddWithValue("@a_phone", TextBox4.Text)
                cmd.Parameters.AddWithValue("@a_email", TextBox5.Text)
                cmd.Parameters.AddWithValue("@a_date", Convert.ToDateTime(TextBox6.Text))
                cmd.ExecuteNonQuery()
            End Using


            MessageBox.Show("Data Inserted Successfully")
            upd()
            con.Close()

        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
            con.Dispose()

        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim rowIndex As Integer = e.RowIndex
        If rowIndex >= 0 Then
            TextBox1.Text = DataGridView1.Rows(rowIndex).Cells("a_id").Value.ToString()
            TextBox2.Text = DataGridView1.Rows(rowIndex).Cells("a_name").Value.ToString()
            TextBox3.Text = DataGridView1.Rows(rowIndex).Cells("a_psd").Value.ToString()
            TextBox4.Text = DataGridView1.Rows(rowIndex).Cells("a_phone").Value.ToString()
            TextBox5.Text = DataGridView1.Rows(rowIndex).Cells("a_email").Value.ToString()
            TextBox6.Text = DataGridView1.Rows(rowIndex).Cells("a_date").Value.ToString()

        End If
        DataGridView1.Refresh()
    End Sub
    Private Sub conn()
        Dim myConnectionString As String
        myConnectionString = "datasource=localhost;" _
          & "port=2929;" _
          & "user id=root;" _
          & "password=2930;" _
          & "database=tortpingdb"
        con.ConnectionString = myConnectionString

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If con.State = ConnectionState.Closed Then
            conn()
            con.Open()
        End If
        Dim r As Integer
        Try

            cmd = New MySqlCommand()
            With cmd
                .Connection = con
                .CommandText = "UPDATE login SET a_name=@a_name, a_psd=@a_psd, a_phone=@a_phone, a_email=@a_email, a_date=@a_date  WHERE a_id=@a_id"
                .CommandType = CommandType.Text
                .Parameters.AddWithValue("@a_id", TextBox1.Text)
                .Parameters.AddWithValue("@a_name", TextBox2.Text)
                .Parameters.AddWithValue("@a_psd", TextBox3.Text)
                .Parameters.AddWithValue("@a_phone", TextBox4.Text)
                .Parameters.AddWithValue("@a_email", TextBox5.Text)
                .Parameters.AddWithValue("@a_date", Convert.ToDateTime(TextBox6.Text))

            End With
            r = cmd.ExecuteNonQuery()
            If r = 0 Then
                MessageBox.Show("DATA UPDATE FAILED.")
            Else
                MessageBox.Show("THE DATA HAS BEEN UPDATED IN THE DATABASE.")
            End If
            con.Close()
            DataGridView1.Refresh()
            upd()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            con.Dispose()
        End Try
    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            Dim selectedindex As String = DataGridView1.CurrentRow.Cells(0).Value.ToString()
            DataGridView1.Rows.Remove(row)
            Try
                If con.State = ConnectionState.Closed Then
                    conn()
                    con.Open()
                End If
                Dim sql = "DELETE FROM login WHERE  a_id='" & selectedindex & "'"
                cmd = New MySqlCommand(sql, con)

                Dim r = cmd.ExecuteNonQuery

                If r > 0 Then
                    MsgBox("THE DATA HAS BEEN DELETED FROM THE DATABASE.")
                Else
                    MsgBox("FAILED TO DELETE THE DATA.", MsgBoxStyle.Information)
                End If

                con.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Finally
                con.Dispose()
            End Try
        Next '
        upd()
    End Sub

    Private Sub upd()
        If con.State = ConnectionState.Closed Then
            conn()
            con.Open()
        End If
        Try



            Dim table1 As New DataTable()
            cmd = New MySqlCommand("SELECT * FROM login", con)
            da.SelectCommand = cmd
            da.Fill(table1)
            DataGridView1.DataSource = table1

        Catch ex As MySqlException
            MessageBox.Show("Error connecting to the database. " & ex.Message)
        Finally
            con.Dispose()
        End Try
    End Sub

End Class
