Imports Npgsql

Public Class frmPerformanceMaster

    Private _selectedId As Integer = -1

    Private Sub frmPerformanceMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpFrom.Value = DateTime.Today.AddMonths(-1)
        dtpTo.Value = DateTime.Today.AddMonths(6)
        LoadPerformances()
    End Sub

    ''' Load all performances (or filtered) into DataGridView
    Private Sub LoadPerformances(Optional searchName As String = "", Optional fromDate As DateTime = Nothing, Optional toDate As DateTime = Nothing)
        Try
            Using conn = DbHelper.GetConnection()
                conn.Open()

                Dim sql = "SELECT id, show_name, start_time, duration_minutes, created_at FROM performances WHERE 1=1"
                Dim cmd As New NpgsqlCommand()
                cmd.Connection = conn

                If Not String.IsNullOrWhiteSpace(searchName) Then
                    sql &= " AND LOWER(show_name) LIKE @search"
                    cmd.Parameters.AddWithValue("@search", "%" & searchName.ToLower() & "%")
                End If

                If fromDate <> Nothing AndAlso fromDate <> DateTime.MinValue Then
                    sql &= " AND start_time >= @from"
                    cmd.Parameters.AddWithValue("@from", fromDate)
                End If

                If toDate <> Nothing AndAlso toDate <> DateTime.MinValue Then
                    sql &= " AND start_time <= @to"
                    cmd.Parameters.AddWithValue("@to", toDate.Date.AddDays(1))
                End If

                sql &= " ORDER BY start_time DESC"
                cmd.CommandText = sql

                Dim dt As New DataTable()
                Dim adapter As New NpgsqlDataAdapter(cmd)
                adapter.Fill(dt)

                ' Rename columns for display
                dt.Columns("id").ColumnName = "ID"
                dt.Columns("show_name").ColumnName = "Tên vở diễn"
                dt.Columns("start_time").ColumnName = "Thời gian bắt đầu"
                dt.Columns("duration_minutes").ColumnName = "Thời lượng (phút)"
                dt.Columns("created_at").ColumnName = "Ngày tạo"

                dgvPerformances.DataSource = dt
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi khi tải dữ liệu: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' Add a new performance
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If Not ValidateInput() Then Return

        Try
            Using conn = DbHelper.GetConnection()
                conn.Open()
                Dim sql = "INSERT INTO performances (show_name, start_time, duration_minutes) VALUES (@name, @start, @duration)"
                Using cmd As New NpgsqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@name", txtShowName.Text.Trim())
                    cmd.Parameters.AddWithValue("@start", dtpStartTime.Value)
                    cmd.Parameters.AddWithValue("@duration", CInt(nudDuration.Value))
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Thêm suất diễn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadPerformances()
        Catch ex As Exception
            MessageBox.Show("Lỗi khi thêm suất diễn: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' Update selected performance
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If _selectedId = -1 Then
            MessageBox.Show("Vui lòng chọn một suất diễn để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not ValidateInput() Then Return

        If MessageBox.Show("Bạn có chắc muốn cập nhật suất diễn này?", "Xác nhận",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Try
            Using conn = DbHelper.GetConnection()
                conn.Open()
                Dim sql = "UPDATE performances SET show_name=@name, start_time=@start, duration_minutes=@duration WHERE id=@id"
                Using cmd As New NpgsqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@name", txtShowName.Text.Trim())
                    cmd.Parameters.AddWithValue("@start", dtpStartTime.Value)
                    cmd.Parameters.AddWithValue("@duration", CInt(nudDuration.Value))
                    cmd.Parameters.AddWithValue("@id", _selectedId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadPerformances()
        Catch ex As Exception
            MessageBox.Show("Lỗi khi cập nhật: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' Delete selected performance
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If _selectedId = -1 Then
            MessageBox.Show("Vui lòng chọn một suất diễn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Bạn có chắc muốn xóa suất diễn này? Tất cả booking và ghế liên quan cũng sẽ bị xóa.",
                           "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        Try
            Using conn = DbHelper.GetConnection()
                conn.Open()
                Using cmd As New NpgsqlCommand("DELETE FROM performances WHERE id=@id", conn)
                    cmd.Parameters.AddWithValue("@id", _selectedId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            LoadPerformances()
        Catch ex As Exception
            MessageBox.Show("Lỗi khi xóa: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' Search by name and date range
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadPerformances(txtSearch.Text.Trim(), dtpFrom.Value, dtpTo.Value)
    End Sub

    ''' Click on DataGridView row to select for editing
    Private Sub dgvPerformances_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPerformances.CellClick
        If e.RowIndex < 0 Then Return

        Dim row = dgvPerformances.Rows(e.RowIndex)
        _selectedId = CInt(row.Cells("ID").Value)
        txtShowName.Text = row.Cells("Tên vở diễn").Value.ToString()
        dtpStartTime.Value = CDate(row.Cells("Thời gian bắt đầu").Value)
        nudDuration.Value = CInt(row.Cells("Thời lượng (phút)").Value)
    End Sub

    ''' Clear input fields
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
        LoadPerformances()
    End Sub

    Private Sub ClearFields()
        _selectedId = -1
        txtShowName.Clear()
        dtpStartTime.Value = DateTime.Now
        nudDuration.Value = 90
        txtSearch.Clear()
    End Sub

    Private Function ValidateInput() As Boolean
        If String.IsNullOrWhiteSpace(txtShowName.Text) Then
            MessageBox.Show("Vui lòng nhập tên vở diễn.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtShowName.Focus()
            Return False
        End If
        Return True
    End Function

End Class
