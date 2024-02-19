Imports System.Drawing.Printing
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports Mysqlx
Imports Mysqlx.XDevAPI.Common
Imports Mysqlx.XDevAPI.Relational



Public Class Form9
    Dim con As New MySql.Data.MySqlClient.MySqlConnection
    Dim cmd As New MySqlCommand()
    Dim dr As MySqlDataReader
    Dim da As New MySqlDataAdapter()
    Dim dt As New DataTable
    Dim qu As String

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        upd()

    End Sub
    'connection update
    Private Sub conn()
        Dim myConnectionString As String
        myConnectionString = "datasource=localhost;" _
          & "port=2929;" _
          & "user id=root;" _
          & "password=2930;" _
          & "database=tortpingdb"
        con.ConnectionString = myConnectionString
    End Sub
    'datagrid update
    Private Sub upd()
        conn()
        Try

            con.Open()
            cmd.Connection = con
            cmd.CommandText = "SELECT * FROM inv"

            dr = cmd.ExecuteReader

            dt.Load(dr)

            dr.Close()
            con.Close()
            DataGridView1.DataSource = dt
            DataGridView1.Refresh()
            con.Close()

        Catch ex As MySqlException
            MessageBox.Show("Error connecting to the database. " & ex.Message)
        Finally
            con.Dispose()
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim rowIndex As Integer = e.RowIndex
        If rowIndex >= 0 Then
            TextBox1.Text = DataGridView1.Rows(rowIndex).Cells("iv_id").Value.ToString()
            TextBox2.Text = DataGridView1.Rows(rowIndex).Cells("iv_name").Value.ToString()
            TextBox3.Text = DataGridView1.Rows(rowIndex).Cells("iv_dpt").Value.ToString()
            TextBox4.Text = DataGridView1.Rows(rowIndex).Cells("iv_status").Value.ToString()
            TextBox5.Text = DataGridView1.Rows(rowIndex).Cells("iv_rdate").Value.ToString()

        End If
        DataGridView1.Refresh()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        insert()
        DataGridView1.Refresh()
        upd()

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        updt()
        upd()


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        delete()
        upd()

    End Sub

    'insert sub
    Private Sub insert()
        If con.State = ConnectionState.Closed Then
            conn()
            con.Open()
        End If
        Try

            qu = "INSERT INTO inv(iv_id, iv_name,iv_dpt, iv_status, iv_rdate) 
            VALUES('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "')"

            cmd = New MySqlCommand(qu, con)
            dr = cmd.ExecuteReader
            con.Close()

            MessageBox.Show("added recored successfull")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally

            con.Dispose()
        End Try
    End Sub

    'update sub
    Private Sub updt()
        Try
            conn()
            con.Open()
            Dim r As Integer
            cmd = New MySqlCommand()
            With cmd
                .Connection = con
                .CommandText = "UPDATE inv SET iv_name=@iv_name, iv_dpt=@iv_dpt, iv_status=@iv_status, iv_rdate=@iv_rdate WHERE iv_id=@iv_id"
                .CommandType = CommandType.Text
                .Parameters.AddWithValue("@iv_id", TextBox1.Text)
                .Parameters.AddWithValue("@iv_name", TextBox2.Text)
                .Parameters.AddWithValue("@iv_dpt", TextBox3.Text)
                .Parameters.AddWithValue("@iv_status", TextBox4.Text)
                .Parameters.AddWithValue("@iv_rdate", Convert.ToDateTime(TextBox5.Text))
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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form2.Show()
        Me.Close()

    End Sub

    'delete sub
    Private Sub delete()

        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            Dim selectedindex As String = DataGridView1.CurrentRow.Cells(0).Value.ToString()
            DataGridView1.Rows.Remove(row)
            Try
                If con.State = ConnectionState.Closed Then
                    conn()
                    con.Open()
                End If
                Dim sql = "DELETE FROM inv WHERE  iv_id='" & selectedindex & "'"
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


End Class