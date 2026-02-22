Imports Npgsql

Public Class frmBooking

    ' Pricing loaded from database
    Private _seatPrices As New Dictionary(Of String, Decimal)()

    Private Sub frmBooking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSeatPrices()
        LoadPerformances()
        LoadBookings()
        UpdateTotalPrice()
    End Sub

    ' ===== LOAD SEAT PRICES FROM DATABASE =====
    Private Sub LoadSeatPrices()
        Try
            Using conn = DbHelper.GetConnection()
                conn.Open()
                Dim sql = "SELECT seat_type, price FROM seat_prices WHERE is_active = TRUE ORDER BY price"
                Using cmd As New NpgsqlCommand(sql, conn)
                    Using reader = cmd.ExecuteReader()
                        _seatPrices.Clear()
                        cboSeatType.Items.Clear()
                        While reader.Read()
                            Dim seatType = reader.GetString(0)
                            Dim price = reader.GetDecimal(1)
                            _seatPrices(seatType) = price
                            cboSeatType.Items.Add(seatType)
                        End While
                    End Using
                End Using
            End Using

            If cboSeatType.Items.Count > 0 Then
                cboSeatType.SelectedIndex = 0
            End If
        Catch ex As Exception
            MessageBox.Show("Lỗi khi tải bảng giá: " & ex.Message & vbCrLf & "Sử dụng giá mặc định.",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ' Fallback defaults
            _seatPrices("Normal") = 100000
            _seatPrices("VIP") = 200000
            _seatPrices("Double") = 350000
            cboSeatType.Items.AddRange({"Normal", "VIP", "Double"})
            cboSeatType.SelectedIndex = 0
        End Try
    End Sub

    ' ===== LOAD PERFORMANCES INTO COMBOBOX =====
    Private Sub LoadPerformances(Optional search As String = "")
        Try
            Using conn = DbHelper.GetConnection()
                conn.Open()

                Dim sql = "SELECT id, show_name || ' - ' || TO_CHAR(start_time, 'DD/MM/YYYY HH24:MI') AS display_text FROM performances"
                If Not String.IsNullOrWhiteSpace(search) Then
                    sql &= " WHERE LOWER(show_name) LIKE @search"
                End If
                sql &= " ORDER BY start_time DESC"

                Using cmd As New NpgsqlCommand(sql, conn)
                    If Not String.IsNullOrWhiteSpace(search) Then
                        cmd.Parameters.AddWithValue("@search", "%" & search.ToLower() & "%")
                    End If

                    Using reader = cmd.ExecuteReader()
                        cboPerformance.Items.Clear()
                        cboPerformance.DisplayMember = "Text"
                        cboPerformance.ValueMember = "Value"

                        Dim items As New List(Of KeyValuePair(Of Integer, String))()
                        While reader.Read()
                            items.Add(New KeyValuePair(Of Integer, String)(reader.GetInt32(0), reader.GetString(1)))
                        End While

                        cboPerformance.DataSource = items
                        cboPerformance.DisplayMember = "Value"
                        cboPerformance.ValueMember = "Key"
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi khi tải suất diễn: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ===== LOAD BOOKINGS INTO DATAGRIDVIEW =====
    Private Sub LoadBookings()
        Try
            Using conn = DbHelper.GetConnection()
                conn.Open()

                Dim sql = "SELECT b.id AS ""Booking ID"", p.show_name AS ""Vở diễn"", " &
                          "b.customer_name AS ""Khách hàng"", b.seat_type AS ""Loại ghế"", " &
                          "b.quantity AS ""Số lượng"", b.total_price AS ""Tổng tiền"", " &
                          "TO_CHAR(b.created_at, 'DD/MM/YYYY HH24:MI') AS ""Ngày đặt"", " &
                          "COALESCE(STRING_AGG(sa.seat_row || sa.seat_col::text, ', ' ORDER BY sa.seat_row, sa.seat_col), '(Chưa gán)') AS ""Ghế đã gán"" " &
                          "FROM bookings b " &
                          "JOIN performances p ON b.performance_id = p.id " &
                          "LEFT JOIN seat_assignments sa ON sa.booking_id = b.id " &
                          "GROUP BY b.id, p.show_name, b.customer_name, b.seat_type, b.quantity, b.total_price, b.created_at " &
                          "ORDER BY b.created_at DESC"

                Dim dt As New DataTable()
                Using adapter As New NpgsqlDataAdapter(sql, conn)
                    adapter.Fill(dt)
                End Using

                dgvBookings.DataSource = dt
            End Using
        Catch ex As Exception
            MessageBox.Show("Lỗi khi tải booking: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ===== SEARCH PERFORMANCE (BONUS) =====
    Private Sub txtSearchPerformance_TextChanged(sender As Object, e As EventArgs) Handles txtSearchPerformance.TextChanged
        LoadPerformances(txtSearchPerformance.Text.Trim())
    End Sub

    ' ===== CALCULATE TOTAL PRICE =====
    Private Sub UpdateTotalPrice()
        If nudQuantity Is Nothing OrElse lblTotalPrice Is Nothing Then Return
        Dim price As Decimal = GetSeatPrice()
        Dim total = price * nudQuantity.Value
        lblTotalPrice.Text = total.ToString("N0") & " VND"
    End Sub

    Private Function GetSeatPrice() As Decimal
        Dim seatType = cboSeatType.SelectedItem?.ToString()
        If seatType IsNot Nothing AndAlso _seatPrices.ContainsKey(seatType) Then
            Return _seatPrices(seatType)
        End If
        Return 0
    End Function

    Private Sub cboSeatType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSeatType.SelectedIndexChanged
        UpdateTotalPrice()
    End Sub

    Private Sub nudQuantity_ValueChanged(sender As Object, e As EventArgs) Handles nudQuantity.ValueChanged
        UpdateTotalPrice()
    End Sub

    ' ===== BOOK TICKET =====
    Private Sub btnBook_Click(sender As Object, e As EventArgs) Handles btnBook.Click
        ' Validate
        If cboPerformance.SelectedValue Is Nothing Then
            MessageBox.Show("Vui lòng chọn suất diễn.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(txtCustomerName.Text) Then
            MessageBox.Show("Vui lòng nhập tên khách hàng.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCustomerName.Focus()
            Return
        End If

        Dim performanceId = CInt(cboPerformance.SelectedValue)
        Dim totalPrice = GetSeatPrice() * nudQuantity.Value

        If MessageBox.Show($"Xác nhận đặt {nudQuantity.Value} vé ({cboSeatType.SelectedItem}) với tổng tiền {totalPrice:N0} VND?",
                           "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Try
            Using conn = DbHelper.GetConnection()
                conn.Open()
                Dim sql = "INSERT INTO bookings (performance_id, customer_name, seat_type, quantity, total_price) " &
                          "VALUES (@perf, @name, @type, @qty, @total)"
                Using cmd As New NpgsqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@perf", performanceId)
                    cmd.Parameters.AddWithValue("@name", txtCustomerName.Text.Trim())
                    cmd.Parameters.AddWithValue("@type", cboSeatType.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@qty", CInt(nudQuantity.Value))
                    cmd.Parameters.AddWithValue("@total", totalPrice)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Đặt vé thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtCustomerName.Clear()
            nudQuantity.Value = 1
            LoadBookings()
        Catch ex As Exception
            MessageBox.Show("Lỗi khi đặt vé: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ===== DELETE BOOKING =====
    Private Sub btnDeleteBooking_Click(sender As Object, e As EventArgs) Handles btnDeleteBooking.Click
        If dgvBookings.CurrentRow Is Nothing Then
            MessageBox.Show("Vui lòng chọn booking để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim bookingId = CInt(dgvBookings.CurrentRow.Cells("Booking ID").Value)

        If MessageBox.Show($"Bạn có chắc muốn xóa Booking #{bookingId}? Ghế đã gán cũng sẽ bị xóa.",
                           "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
            Return
        End If

        Try
            Using conn = DbHelper.GetConnection()
                conn.Open()
                Using cmd As New NpgsqlCommand("DELETE FROM bookings WHERE id=@id", conn)
                    cmd.Parameters.AddWithValue("@id", bookingId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Xóa booking thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadBookings()
        Catch ex As Exception
            MessageBox.Show("Lỗi khi xóa: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
