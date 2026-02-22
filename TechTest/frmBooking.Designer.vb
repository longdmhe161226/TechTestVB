<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBooking
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(900, 620)
        Me.Text = "Đặt Vé"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Font = New Font("Segoe UI", 9)

        ' ===== BOOKING INPUT PANEL =====
        Me.pnlInput = New Panel()
        Me.pnlInput.Location = New Point(12, 12)
        Me.pnlInput.Size = New Size(876, 180)
        Me.pnlInput.BorderStyle = BorderStyle.FixedSingle
        Me.Controls.Add(Me.pnlInput)

        ' Search performance (bonus)
        Dim lblSearchPerf As New Label()
        lblSearchPerf.Text = "🔍 Tìm suất diễn:"
        lblSearchPerf.Location = New Point(15, 15)
        lblSearchPerf.AutoSize = True
        lblSearchPerf.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(lblSearchPerf)

        Me.txtSearchPerformance = New TextBox()
        Me.txtSearchPerformance.Location = New Point(155, 12)
        Me.txtSearchPerformance.Size = New Size(250, 25)
        Me.txtSearchPerformance.Font = New Font("Segoe UI", 10)
        Me.txtSearchPerformance.PlaceholderText = "Gõ để tìm kiếm..."
        Me.pnlInput.Controls.Add(Me.txtSearchPerformance)

        ' Performance ComboBox
        Dim lblPerformance As New Label()
        lblPerformance.Text = "Suất diễn:"
        lblPerformance.Location = New Point(430, 15)
        lblPerformance.AutoSize = True
        lblPerformance.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(lblPerformance)

        Me.cboPerformance = New ComboBox()
        Me.cboPerformance.Location = New Point(520, 12)
        Me.cboPerformance.Size = New Size(340, 25)
        Me.cboPerformance.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboPerformance.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(Me.cboPerformance)

        ' Customer Name
        Dim lblCustomer As New Label()
        lblCustomer.Text = "Tên khách hàng:"
        lblCustomer.Location = New Point(15, 55)
        lblCustomer.AutoSize = True
        lblCustomer.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(lblCustomer)

        Me.txtCustomerName = New TextBox()
        Me.txtCustomerName.Location = New Point(155, 52)
        Me.txtCustomerName.Size = New Size(250, 25)
        Me.txtCustomerName.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(Me.txtCustomerName)

        ' Seat Type
        Dim lblSeatType As New Label()
        lblSeatType.Text = "Loại ghế:"
        lblSeatType.Location = New Point(430, 55)
        lblSeatType.AutoSize = True
        lblSeatType.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(lblSeatType)

        Me.cboSeatType = New ComboBox()
        Me.cboSeatType.Location = New Point(520, 52)
        Me.cboSeatType.Size = New Size(200, 25)
        Me.cboSeatType.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboSeatType.Font = New Font("Segoe UI", 10)
        ' Seat types loaded from database at runtime
        Me.pnlInput.Controls.Add(Me.cboSeatType)

        ' Quantity
        Dim lblQuantity As New Label()
        lblQuantity.Text = "Số lượng vé:"
        lblQuantity.Location = New Point(15, 95)
        lblQuantity.AutoSize = True
        lblQuantity.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(lblQuantity)

        Me.nudQuantity = New NumericUpDown()
        Me.nudQuantity.Location = New Point(155, 92)
        Me.nudQuantity.Size = New Size(100, 25)
        Me.nudQuantity.Minimum = 1
        Me.nudQuantity.Maximum = 100
        Me.nudQuantity.Value = 1
        Me.nudQuantity.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(Me.nudQuantity)

        ' Total Price
        Dim lblPriceLabel As New Label()
        lblPriceLabel.Text = "Tổng tiền:"
        lblPriceLabel.Location = New Point(300, 95)
        lblPriceLabel.AutoSize = True
        lblPriceLabel.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(lblPriceLabel)

        Me.lblTotalPrice = New Label()
        Me.lblTotalPrice.Text = "100,000 VND"
        Me.lblTotalPrice.Location = New Point(390, 95)
        Me.lblTotalPrice.AutoSize = True
        Me.lblTotalPrice.Font = New Font("Segoe UI", 13, FontStyle.Bold)
        Me.lblTotalPrice.ForeColor = Color.FromArgb(0, 150, 136)
        Me.pnlInput.Controls.Add(Me.lblTotalPrice)

        ' Buttons
        Me.btnBook = New Button()
        Me.btnBook.Text = "🎫 Đặt Vé"
        Me.btnBook.Location = New Point(15, 135)
        Me.btnBook.Size = New Size(140, 35)
        Me.btnBook.BackColor = Color.FromArgb(76, 175, 80)
        Me.btnBook.ForeColor = Color.White
        Me.btnBook.FlatStyle = FlatStyle.Flat
        Me.btnBook.FlatAppearance.BorderSize = 0
        Me.btnBook.Cursor = Cursors.Hand
        Me.btnBook.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        Me.pnlInput.Controls.Add(Me.btnBook)

        Me.btnDeleteBooking = New Button()
        Me.btnDeleteBooking.Text = "🗑️ Xóa Booking"
        Me.btnDeleteBooking.Location = New Point(165, 135)
        Me.btnDeleteBooking.Size = New Size(150, 35)
        Me.btnDeleteBooking.BackColor = Color.FromArgb(244, 67, 54)
        Me.btnDeleteBooking.ForeColor = Color.White
        Me.btnDeleteBooking.FlatStyle = FlatStyle.Flat
        Me.btnDeleteBooking.FlatAppearance.BorderSize = 0
        Me.btnDeleteBooking.Cursor = Cursors.Hand
        Me.pnlInput.Controls.Add(Me.btnDeleteBooking)

        ' ===== BOOKINGS DATAGRIDVIEW =====
        Me.dgvBookings = New DataGridView()
        Me.dgvBookings.Location = New Point(12, 200)
        Me.dgvBookings.Size = New Size(876, 410)
        Me.dgvBookings.AllowUserToAddRows = False
        Me.dgvBookings.AllowUserToDeleteRows = False
        Me.dgvBookings.ReadOnly = True
        Me.dgvBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvBookings.MultiSelect = False
        Me.dgvBookings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvBookings.BackgroundColor = Color.White
        Me.dgvBookings.RowHeadersVisible = False
        Me.dgvBookings.Font = New Font("Segoe UI", 9.5)
        Me.dgvBookings.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        Me.dgvBookings.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 150, 136)
        Me.dgvBookings.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        Me.dgvBookings.EnableHeadersVisualStyles = False
        Me.dgvBookings.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(232, 245, 233)
        Me.Controls.Add(Me.dgvBookings)
    End Sub

    Friend WithEvents pnlInput As Panel
    Friend WithEvents txtSearchPerformance As TextBox
    Friend WithEvents cboPerformance As ComboBox
    Friend WithEvents txtCustomerName As TextBox
    Friend WithEvents cboSeatType As ComboBox
    Friend WithEvents nudQuantity As NumericUpDown
    Friend WithEvents lblTotalPrice As Label
    Friend WithEvents btnBook As Button
    Friend WithEvents btnDeleteBooking As Button
    Friend WithEvents dgvBookings As DataGridView
End Class
