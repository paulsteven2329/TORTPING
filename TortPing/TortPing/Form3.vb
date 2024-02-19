Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Status
Imports MySql.Data.MySqlClient


Public Class Form3
    Dim con As New MySql.Data.MySqlClient.MySqlConnection
    Dim cmd As New MySqlCommand
    Dim dr As MySqlDataReader
    Dim q As String
    Dim dt As New DataTable
    Dim da As New MySqlDataAdapter(cmd)

    Dim X As Integer = 0
    Dim Y As Integer = 0
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        insert()
        upd()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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

    'datagrid update
    Private Sub upd()
        If con.State = ConnectionState.Closed Then
            conn()
            con.Open()
        End If
        Try



            Dim table1 As New DataTable()
            cmd = New MySqlCommand("SELECT * FROM cpt", con)
            da.SelectCommand = cmd
            da.Fill(table1)
            DataGridView1.DataSource = table1

        Catch ex As MySqlException
            MessageBox.Show("Error connecting to the database. " & ex.Message)
        Finally
            con.Dispose()
        End Try
    End Sub

    'insert sub
    Private Sub insert()
        conn()
        Dim gender As String = String.Empty
        If RadioButton1.Checked Then
            gender = "male"
        ElseIf RadioButton2.Checked Then
            gender = "Female"
        End If
        Try

            con.Open()
            ' Check if textboxes are empty
            If TextBox1.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox8.Text = "" Or TextBox9.Text = "" Or TextBox10.Text = "" Or TextBox11.Text = "" Or TextBox12.Text = "" Or TextBox13.Text = "" Or TextBox14.Text = "" Or TextBox15.Text = "" Then
                MessageBox.Show("Please fill in all fields.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf Not IsNumeric(TextBox4.Text) Or TextBox4.Text.Length <> 10 Then
                MessageBox.Show("Please enter a 10-digit phone number.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                q = "INSERT INTO cpt(cp_ps, cp_name, cp_phone,cp_age,cp_email, cp_country, cp_state, cp_city, cp_street, cp_date, i_fdate, i_tdate, i_category, i_place,cp_gender) 
                VALUES('" & TextBox1.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "','" & TextBox8.Text & "',
                '" & TextBox9.Text & "','" & TextBox10.Text & "','" & TextBox11.Text & "','" & TextBox12.Text & "','" & TextBox13.Text & "','" & TextBox14.Text & "','" & TextBox15.Text & "','" & gender & "')"

                cmd = New MySqlCommand(q, con)
                dr = cmd.ExecuteReader
                con.Close()


                MessageBox.Show("added recored successfull")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally

            con.Dispose()
        End Try



    End Sub


    'update sub
    Private Sub updt()
        conn()

        Dim r As Integer
        Dim gender As String = String.Empty
        If RadioButton1.Checked Then
            gender = "male"
        ElseIf RadioButton2.Checked Then
            gender = "Female"
        End If
        Try
            con.Open()
            cmd = New MySqlCommand()
            With cmd
                .Connection = con
                .CommandText = "UPDATE cpt SET cp_name=@cp_name, cp_age=@cp_age, cp_gender=@cp_gender, cp_phone=@cp_phone, cp_email=@cp_email, cp_country=@cp_country, cp_state=@cp_state, cp_city=@cp_city, cp_street=@cp_street, cp_date=@cp_date, cp_ps=@cp_ps, i_category=@i_category, i_place=@i_place, i_fdate=@i_fdate, i_tdate=@i_tdate WHERE cp_id=@cp_id"
                .CommandType = CommandType.Text
                .Parameters.AddWithValue("@cp_name", TextBox3.Text)
                .Parameters.AddWithValue("@cp_age", TextBox5.Text)
                .Parameters.AddWithValue("@cp_gender", gender)
                .Parameters.AddWithValue("@cp_phone", TextBox4.Text)
                .Parameters.AddWithValue("@cp_email", TextBox6.Text)
                .Parameters.AddWithValue("@cp_country", TextBox7.Text)
                .Parameters.AddWithValue("@cp_state", TextBox8.Text)
                .Parameters.AddWithValue("@cp_city", TextBox9.Text)
                .Parameters.AddWithValue("@cp_street", TextBox10.Text)
                .Parameters.AddWithValue("@cp_date", Convert.ToDateTime(TextBox11.Text))
                .Parameters.AddWithValue("@cp_ps", TextBox1.Text)
                .Parameters.AddWithValue("@i_category", TextBox14.Text)
                .Parameters.AddWithValue("@i_place", TextBox15.Text)
                .Parameters.AddWithValue("@i_fdate", Convert.ToDateTime(TextBox12.Text))
                .Parameters.AddWithValue("@i_tdate", Convert.ToDateTime(TextBox13.Text))
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
                q = "DELETE FROM cpt WHERE  cp_id='" & selectedindex & "'"
                cmd = New MySqlCommand(q, con)

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


    Private Sub DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

        Dim rowIndex As Integer = e.RowIndex
        If rowIndex >= 0 Then
            TextBox1.Text = DataGridView1.Rows(rowIndex).Cells("cp_ps").Value.ToString()
            TextBox2.Text = DataGridView1.Rows(rowIndex).Cells("cp_id").Value.ToString()
            TextBox3.Text = DataGridView1.Rows(rowIndex).Cells("cp_name").Value.ToString()
            TextBox4.Text = DataGridView1.Rows(rowIndex).Cells("cp_phone").Value.ToString()
            TextBox5.Text = DataGridView1.Rows(rowIndex).Cells("cp_age").Value.ToString()
            TextBox6.Text = DataGridView1.Rows(rowIndex).Cells("cp_email").Value.ToString()
            TextBox7.Text = DataGridView1.Rows(rowIndex).Cells("cp_country").Value.ToString()
            TextBox8.Text = DataGridView1.Rows(rowIndex).Cells("cp_state").Value.ToString()
            TextBox9.Text = DataGridView1.Rows(rowIndex).Cells("cp_city").Value.ToString()
            TextBox10.Text = DataGridView1.Rows(rowIndex).Cells("cp_street").Value.ToString()
            TextBox11.Text = DataGridView1.Rows(rowIndex).Cells("cp_date").Value.ToString()
            TextBox12.Text = DataGridView1.Rows(rowIndex).Cells("i_fdate").Value.ToString()
            TextBox13.Text = DataGridView1.Rows(rowIndex).Cells("i_tdate").Value.ToString()
            TextBox14.Text = DataGridView1.Rows(rowIndex).Cells("i_category").Value.ToString()
            TextBox15.Text = DataGridView1.Rows(rowIndex).Cells("i_place").Value.ToString()

        End If
        DataGridView1.Refresh()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            conn()
            con.Open()
            q = "SELECT * FROM cpt WHERE cp_name LIKE @searchTerm OR cp_age
            LIKE @searchTerm OR cp_phone LIKE @searchTerm OR cp_email LIKE @searchTerm OR
            cp_country LIKE @searchTerm OR cp_state LIKE @searchTerm OR cp_city LIKE @searchTerm OR
            cp_street LIKE @searchTerm OR i_place LIKE @searchTerm OR cp_gender LIKE @searchTerm OR 
            cp_id LIKE @searchTerm OR cp_ps LIKE @searchTerm OR cp_date LIKE @searchTerm"
            cmd = New MySqlCommand(q, con)
            cmd.Parameters.AddWithValue("@searchTerm", "%" & TextBox2.Text & "%")
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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form2.Show()
        Me.Close()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click


        fir.Show()
        Me.Close()
    End Sub


End Class