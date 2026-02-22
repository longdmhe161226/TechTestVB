Imports Npgsql

Public Class frmSeatAssignment

    Private _currentBookingId As Integer = -1
    Private _currentPerformanceId As Integer = -1
    Private _maxSeats As Integer = 0
    Private _selectedSeats As New List(Of String)()          ' e.g. "A1", "B5"
    Private _alreadyAssignedSeats As New List(Of String)()   ' seats assigned for THIS booking (from DB)
    Private _seatButtons As New Dictionary(Of String, Button)()

    Private Const ROWS As Integer = 10  ' A-J
    Private Const COLS As Integer = 10  ' 1-10
    Private Const BTN_SIZE As Integer = 55
    Private Const BTN_MARGIN As Integer = 5

    Private Sub frmSeatAssignment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadBookings()
    End Sub

    ' ===== LOAD BOOKINGS INTO COMBOBOX =====
    Private Sub LoadBookings()
        Try
            Using conn = DbHelper.GetConnection()
                conn.Open()
                Dim sql = "SELECT b.id, '#' || b.id || ' - ' || b.customer_name || ' (' || p.show_name || ') - ' || b.quantity || ' vé' AS display " &
                          "FROM bookings b JOIN performances p ON b.performance_id = p.id ORDER BY b.id DESC"

                Using cmd As New NpgsqlCommand(sql, conn)
                    Using reader = cmd.ExecuteReader()
                        Dim items As New List(Of KeyValuePair(Of Integer, String))()
                        While reader.Read()
                            items.Add(New KeyValuePair(Of Integer, String)(reader.GetInt32(0), reader.GetString(1)))
                        End While
                        cboBooking.DataSource = items
                        cboBooking.DisplayMember = "Value"
                        cboBooking.ValueMember = "Key"
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi khi tải danh sách booking: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ===== LOAD SEAT MAP =====
    Private Sub btnLoadSeats_Click(sender As Object, e As EventArgs) Handles btnLoadSeats.Click
        If cboBooking.SelectedValue Is Nothing Then
            MessageBox.Show("Vui lòng chọn booking.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        _currentBookingId = CInt(cboBooking.SelectedValue)
        _selectedSeats.Clear()
        _alreadyAssignedSeats.Clear()

        Try
            Using conn = DbHelper.GetConnection()
                conn.Open()

                ' Get booking info
                Dim sqlBooking = "SELECT performance_id, quantity FROM bookings WHERE id = @id"
                Using cmdBooking As New NpgsqlCommand(sqlBooking, conn)
                    cmdBooking.Parameters.AddWithValue("@id", _currentBookingId)
                    Using reader = cmdBooking.ExecuteReader()
                        If reader.Read() Then
                            _currentPerformanceId = reader.GetInt32(0)
                            _maxSeats = reader.GetInt32(1)
                        Else
                            MessageBox.Show("Booking không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return
                        End If
                    End Using
                End Using

                ' Get seats already assigned for this booking
                Dim sqlMySeats = "SELECT seat_row || seat_col::text FROM seat_assignments WHERE booking_id = @bid"
                Using cmdMySeats As New NpgsqlCommand(sqlMySeats, conn)
                    cmdMySeats.Parameters.AddWithValue("@bid", _currentBookingId)
                    Using reader = cmdMySeats.ExecuteReader()
                        While reader.Read()
                            Dim seatKey = reader.GetString(0)
                            _alreadyAssignedSeats.Add(seatKey)
                            _selectedSeats.Add(seatKey)
                        End While
                    End Using
                End Using

                ' Get ALL assigned seats for this performance (other bookings)
                Dim takenSeats As New HashSet(Of String)()
                Dim sqlTaken = "SELECT seat_row || seat_col::text FROM seat_assignments WHERE performance_id = @pid AND booking_id <> @bid"
                Using cmdTaken As New NpgsqlCommand(sqlTaken, conn)
                    cmdTaken.Parameters.AddWithValue("@pid", _currentPerformanceId)
                    cmdTaken.Parameters.AddWithValue("@bid", _currentBookingId)
                    Using reader = cmdTaken.ExecuteReader()
                        While reader.Read()
                            takenSeats.Add(reader.GetString(0))
                        End While
                    End Using
                End Using

                ' Build the seat map
                BuildSeatMap(takenSeats)
            End Using

            UpdateSeatCountLabel()
            btnSave.Enabled = True
            lblInfo.Text = $"Booking #{_currentBookingId} | Tối đa {_maxSeats} ghế"

        Catch ex As Exception
            MessageBox.Show("Lỗi khi tải sơ đồ ghế: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ===== BUILD SEAT MAP (10x10 GRID) =====
    Private Sub BuildSeatMap(takenSeats As HashSet(Of String))
        pnlSeatMap.Controls.Clear()
        _seatButtons.Clear()

        Dim rowLabels = "ABCDEFGHIJ"
        Dim offsetX = 40  ' offset for row labels
        Dim offsetY = 10

        ' Add column headers (1-10)
        For c = 1 To COLS
            Dim colLabel As New Label()
            colLabel.Text = c.ToString()
            colLabel.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            colLabel.ForeColor = Color.DarkSlateBlue
            colLabel.TextAlign = ContentAlignment.MiddleCenter
            colLabel.Size = New Size(BTN_SIZE, 20)
            colLabel.Location = New Point(offsetX + (c - 1) * (BTN_SIZE + BTN_MARGIN), offsetY)
            pnlSeatMap.Controls.Add(colLabel)
        Next

        ' Add rows
        For r = 0 To ROWS - 1
            Dim rowChar = rowLabels(r).ToString()

            ' Row label
            Dim rowLabel As New Label()
            rowLabel.Text = rowChar
            rowLabel.Font = New Font("Segoe UI", 11, FontStyle.Bold)
            rowLabel.ForeColor = Color.DarkSlateBlue
            rowLabel.TextAlign = ContentAlignment.MiddleCenter
            rowLabel.Size = New Size(30, BTN_SIZE)
            rowLabel.Location = New Point(5, offsetY + 25 + r * (BTN_SIZE + BTN_MARGIN))
            pnlSeatMap.Controls.Add(rowLabel)

            For c = 1 To COLS
                Dim seatKey = rowChar & c.ToString()
                Dim btn As New Button()
                btn.Name = "btn_" & seatKey
                btn.Text = seatKey
                btn.Size = New Size(BTN_SIZE, BTN_SIZE)
                btn.Location = New Point(offsetX + (c - 1) * (BTN_SIZE + BTN_MARGIN), offsetY + 25 + r * (BTN_SIZE + BTN_MARGIN))
                btn.Font = New Font("Segoe UI", 8.5, FontStyle.Bold)
                btn.FlatStyle = FlatStyle.Flat
                btn.FlatAppearance.BorderColor = Color.LightGray
                btn.FlatAppearance.BorderSize = 1
                btn.Cursor = Cursors.Hand
                btn.Tag = seatKey

                If takenSeats.Contains(seatKey) Then
                    ' Taken by another booking - RED
                    btn.BackColor = Color.FromArgb(244, 67, 54)
                    btn.ForeColor = Color.White
                    btn.Enabled = False
                    btn.Cursor = Cursors.No
                ElseIf _alreadyAssignedSeats.Contains(seatKey) Then
                    ' Already assigned to this booking - BLUE
                    btn.BackColor = Color.FromArgb(33, 150, 243)
                    btn.ForeColor = Color.White
                Else
                    ' Available - WHITE
                    btn.BackColor = Color.White
                    btn.ForeColor = Color.Black
                End If

                AddHandler btn.Click, AddressOf SeatButton_Click
                pnlSeatMap.Controls.Add(btn)
                _seatButtons(seatKey) = btn
            Next
        Next
    End Sub

    ' ===== SEAT BUTTON CLICK =====
    Private Sub SeatButton_Click(sender As Object, e As EventArgs)
        Dim btn = CType(sender, Button)
        Dim seatKey = btn.Tag.ToString()

        If _selectedSeats.Contains(seatKey) Then
            ' Deselect
            _selectedSeats.Remove(seatKey)
            btn.BackColor = Color.White
            btn.ForeColor = Color.Black
        Else
            ' Check limit
            If _selectedSeats.Count >= _maxSeats Then
                MessageBox.Show($"Bạn chỉ được chọn tối đa {_maxSeats} ghế!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Select
            _selectedSeats.Add(seatKey)
            btn.BackColor = Color.FromArgb(33, 150, 243)
            btn.ForeColor = Color.White
        End If

        UpdateSeatCountLabel()
    End Sub

    Private Sub UpdateSeatCountLabel()
        lblSeatCount.Text = $"Đã chọn: {_selectedSeats.Count} / {_maxSeats}"
        If _selectedSeats.Count = _maxSeats Then
            lblSeatCount.ForeColor = Color.FromArgb(76, 175, 80)
        ElseIf _selectedSeats.Count > _maxSeats Then
            lblSeatCount.ForeColor = Color.Red
        Else
            lblSeatCount.ForeColor = Color.FromArgb(233, 30, 99)
        End If
    End Sub

    ' ===== SAVE SEAT ASSIGNMENTS =====
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If _currentBookingId = -1 Then
            MessageBox.Show("Vui lòng tải sơ đồ ghế trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If _selectedSeats.Count <> _maxSeats Then
            MessageBox.Show($"Bạn phải chọn đúng {_maxSeats} ghế. Hiện tại đã chọn {_selectedSeats.Count}.",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim seatList = String.Join(", ", _selectedSeats.OrderBy(Function(s) s))
        If MessageBox.Show($"Lưu {_selectedSeats.Count} ghế: {seatList}?",
                           "Xác nhận lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Try
            Using conn = DbHelper.GetConnection()
                conn.Open()

                Using transaction = conn.BeginTransaction()
                    Try
                        ' Remove old assignments for this booking
                        Using cmdDel As New NpgsqlCommand("DELETE FROM seat_assignments WHERE booking_id = @bid", conn, transaction)
                            cmdDel.Parameters.AddWithValue("@bid", _currentBookingId)
                            cmdDel.ExecuteNonQuery()
                        End Using

                        ' Insert new assignments
                        For Each seatKey In _selectedSeats
                            Dim seatRow = seatKey(0).ToString()
                            Dim seatCol = Integer.Parse(seatKey.Substring(1))

                            Using cmdIns As New NpgsqlCommand(
                                "INSERT INTO seat_assignments (booking_id, performance_id, seat_row, seat_col) VALUES (@bid, @pid, @row, @col)", conn, transaction)
                                cmdIns.Parameters.AddWithValue("@bid", _currentBookingId)
                                cmdIns.Parameters.AddWithValue("@pid", _currentPerformanceId)
                                cmdIns.Parameters.AddWithValue("@row", seatRow)
                                cmdIns.Parameters.AddWithValue("@col", seatCol)
                                cmdIns.ExecuteNonQuery()
                            End Using
                        Next

                        transaction.Commit()
                        MessageBox.Show("Lưu ghế thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ' Refresh
                        _alreadyAssignedSeats.Clear()
                        _alreadyAssignedSeats.AddRange(_selectedSeats)

                    Catch ex As Exception
                        transaction.Rollback()
                        If ex.Message.Contains("unique") OrElse ex.Message.Contains("duplicate") Then
                            MessageBox.Show("Ghế bị trùng với booking khác! Vui lòng tải lại sơ đồ.",
                                            "Lỗi trùng ghế", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            btnLoadSeats_Click(Nothing, Nothing)
                        Else
                            Throw
                        End If
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi khi lưu: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
