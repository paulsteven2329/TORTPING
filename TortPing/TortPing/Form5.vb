Imports System.Drawing.Printing
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Status
Imports MySql.Data.MySqlClient
Imports Mysqlx
Imports Mysqlx.XDevAPI.Common
Imports Mysqlx.XDevAPI.Relational

Public Class Form5

    Dim con As New MySql.Data.MySqlClient.MySqlConnection
    Dim cmd As New MySqlCommand()
    Dim dr As MySqlDataReader
    Dim da As New MySqlDataAdapter()
    Dim dt As New DataTable
    Dim bitmap As Bitmap

    Dim qu As String

    Dim X As Integer = 0
    Dim Y As Integer = 0

    'form
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Form2.Show()
        Me.Close()
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
            cmd.CommandText = "SELECT * FROM crm"

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


    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        upd()
    End Sub

    'insertion button
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        insert()
        upd()


    End Sub

    'update button
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        updt()
        upd()


    End Sub


    'delete button
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        delete()
        upd()
    End Sub

    'print button
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        PrintDialog1.Document = Me.PrintDocument1
        Dim buttonpressed As DialogResult = PrintDialog1.ShowDialog()

        If (buttonpressed = DialogResult.OK) Then
            Me.Height = Me.Height + Y
            DataGridView1.Height = DataGridView1.Height + Y
            PrintDocument1.Print()
        End If

    End Sub

    'searching button
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        search()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim rowIndex As Integer = e.RowIndex
        If rowIndex >= 0 Then
            TextBox1.Text = DataGridView1.Rows(rowIndex).Cells("cr_name").Value.ToString()
            TextBox2.Text = DataGridView1.Rows(rowIndex).Cells("cr_age").Value.ToString()
            TextBox3.Text = DataGridView1.Rows(rowIndex).Cells("cr_phone").Value.ToString()
            TextBox4.Text = DataGridView1.Rows(rowIndex).Cells("cr_email").Value.ToString()
            TextBox5.Text = DataGridView1.Rows(rowIndex).Cells("cr_country").Value.ToString()
            TextBox6.Text = DataGridView1.Rows(rowIndex).Cells("cr_state").Value.ToString()
            TextBox7.Text = DataGridView1.Rows(rowIndex).Cells("cr_city").Value.ToString()
            TextBox8.Text = DataGridView1.Rows(rowIndex).Cells("cr_street").Value.ToString()
            TextBox9.Text = DataGridView1.Rows(rowIndex).Cells("cr_crime").Value.ToString()
            TextBox10.Text = DataGridView1.Rows(rowIndex).Cells("cr_gender").Value.ToString()
            TextBox11.Text = DataGridView1.Rows(rowIndex).Cells("iv_id").Value.ToString()
            TextBox12.Text = DataGridView1.Rows(rowIndex).Cells("cr_ps").Value.ToString()
            TextBox13.Text = DataGridView1.Rows(rowIndex).Cells("cr_date").Value.ToString()
            TextBox14.Text = DataGridView1.Rows(rowIndex).Cells("cr_id").Value.ToString()
        End If
        DataGridView1.Refresh()
    End Sub


    'insert sub
    Private Sub insert()
        conn()
        Try

            con.Open()
            qu = "INSERT INTO crm(cr_name, cr_age,cr_phone, cr_email, cr_country, cr_state, cr_city, cr_street, cr_crime, cr_gender, iv_id, cr_ps, cr_date) 
            VALUES('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "','" & TextBox8.Text & "',
            '" & TextBox9.Text & "','" & TextBox10.Text & "','" & TextBox11.Text & "','" & TextBox12.Text & "','" & TextBox13.Text & "')"

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
        If con.State = ConnectionState.Closed Then
            conn()
            con.Open()
        End If

        Dim r As Integer

        Try

            cmd = New MySqlCommand()
            With cmd
                .Connection = con
                .CommandText = "UPDATE crm SET cr_name=@cr_name, cr_age=@cr_age, cr_phone=@cr_phone, cr_email=@cr_email, cr_country=@cr_country, cr_state=@cr_state, cr_city=@cr_city, cr_street=@cr_street, cr_crime=@cr_crime, cr_gender=@cr_gender, iv_id=@iv_id, cr_ps=@cr_ps, cr_date=@cr_date WHERE cr_id=@cr_id"
                .CommandType = CommandType.Text
                .Parameters.AddWithValue("@cr_name", TextBox1.Text)
                .Parameters.AddWithValue("@cr_age", TextBox2.Text)
                .Parameters.AddWithValue("@cr_phone", TextBox3.Text)
                .Parameters.AddWithValue("@cr_email", TextBox4.Text)
                .Parameters.AddWithValue("@cr_country", TextBox5.Text)
                .Parameters.AddWithValue("@cr_state", TextBox6.Text)
                .Parameters.AddWithValue("@cr_city", TextBox7.Text)
                .Parameters.AddWithValue("@cr_street", TextBox8.Text)
                .Parameters.AddWithValue("@cr_crime", TextBox9.Text)
                .Parameters.AddWithValue("@cr_gender", TextBox10.Text)
                .Parameters.AddWithValue("@iv_id", TextBox11.Text)
                .Parameters.AddWithValue("@cr_ps", TextBox12.Text)
                .Parameters.AddWithValue("@cr_date", Convert.ToDateTime(TextBox13.Text))
                .Parameters.AddWithValue("@cr_id", TextBox14.Text)
                .Parameters.AddWithValue("@cp_id", TextBox2.Text)
            End With
            r = cmd.ExecuteNonQuery()
            If r = 0 Then
                MessageBox.Show("DATA UPDATE FAILED.")
            Else
                MessageBox.Show("THE DATA HAS BEEN UPDATED IN THE DATABASE.")
            End If
            con.Close()
            DataGridView1.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            con.Dispose()
        End Try

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
                Dim sql = "DELETE FROM crm WHERE  cr_id='" & selectedindex & "'"
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



    Private Sub search()
        Try
            conn()
            con.Open()
            Dim sq As String = "SELECT * FROM crm WHERE cr_name LIKE @searchTerm OR cr_age
            LIKE @searchTerm OR cr_phone LIKE @searchTerm OR cr_email LIKE @searchTerm OR
            cr_country LIKE @searchTerm OR cr_state LIKE @searchTerm OR cr_city LIKE @searchTerm OR
            cr_street LIKE @searchTerm OR cr_crime LIKE @searchTerm OR cr_gender LIKE @searchTerm OR 
            iv_id LIKE @searchTerm OR cr_ps LIKE @searchTerm OR cr_date LIKE @searchTerm"
            cmd = New MySqlCommand(sq, con)
            cmd.Parameters.AddWithValue("@searchTerm", "%" & TextBox14.Text & "%")
            dr = cmd.ExecuteReader
            Dim dt As New DataTable
            dt.Load(dr)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error while searching data. " & ex.Message)
        Finally
            con.Close()
            dr.Close()
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim bm As New Bitmap(Me.DataGridView1.Width, Me.DataGridView1.Height)
        DataGridView1.DrawToBitmap(bm, New Rectangle(0, 0, Me.DataGridView1.Width, Me.DataGridView1.Height))
        e.Graphics.DrawImage(bm, 50, 40)
        Dim psd As New PageSetupDialog
        psd.Document = PrintDocument1
    End Sub
End Class