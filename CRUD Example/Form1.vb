Imports MySql.Data.MySqlClient
Imports System.Data
Public Class Form1
    Dim con As MySqlConnection
    Dim cmd As New MySqlCommand
    Dim dataadapter As New MySqlDataAdapter
    Dim datatable As New DataTable

    Sub data_load(ByVal data As String)
        con = New MySqlConnection()
        con.ConnectionString = "server=127.0.0.1; userid=root; password=12345; database=crud"

        Try

            datatable.Clear()

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            cmd.CommandText = data
            cmd.Connection = con
            dataadapter.SelectCommand = cmd
            dataadapter.Fill(datatable)

            With DataGridView1
                .DataSource = datatable
                .Columns(0).HeaderText = "ID"
                .Columns(1).HeaderText = "First Name"
                .Columns(2).HeaderText = "Middle Name"
                .Columns(3).HeaderText = "Last Name"
                .Columns(0).Width = 50
                .Columns(1).Width = 150
                .Columns(2).Width = 150
                .Columns(3).Width = 150
            End With

            con.Close()

        Catch ex As Exception
            MessageBox.Show("Error" & ex.Message.ToString)
        Finally
            con.Dispose()
        End Try





    End Sub

    Private Sub Query(ByVal data As String)
        Try
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If


            cmd.Connection = con
            cmd.CommandText = data
            cmd.ExecuteNonQuery()

            tbFirst.Clear()
            tbLastName.Clear()
            tbMiddle.Clear()

            data_load("SELECT id, fname, mname, lname FROM users")
            con.Close()


        Catch ex As Exception
            MessageBox.Show("Error" & ex.Message.ToString)
        Finally
            con.Dispose()

        End Try
    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        data_load("SELECT id, fname, mname, lname FROM users")
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Query("INSERT INTO users(fname, mname, lname) VALUES('" & tbFirst.Text _
              & "','" & tbMiddle.Text & "','" & tbLastName.Text & "')")
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        tbFirst.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString
        tbMiddle.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
        tbLastName.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString


    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Query("UPDATE users SET fname='" & tbFirst.Text & "', mname='" & tbMiddle.Text & "', lname='" _
              & tbLastName.Text & "' WHERE id=" & DataGridView1.CurrentRow.Cells(0).Value.ToString)
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Query("DELETE FROM users WHERE id=" & DataGridView1.CurrentRow.Cells(0).Value.ToString)
    End Sub

    Private Sub tbSearch_TextChanged(sender As Object, e As EventArgs) Handles tbSearch.TextChanged
        data_load("SELECT id, fname, mname, lname FROM users" _
                  & "WHERE fname = '" & tbSearch.Text & "' OR lname='" & tbSearch.Text & "'")

    End Sub
End Class
