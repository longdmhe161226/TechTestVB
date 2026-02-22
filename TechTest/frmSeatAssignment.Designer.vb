<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSeatAssignment
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
        Me.ClientSize = New Size(750, 680)
        Me.Text = "Gán Ghế - Seat Assignment"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Font = New Font("Segoe UI", 9)

        ' ===== TOP PANEL: Booking selector =====
        Me.pnlTop = New Panel()
        Me.pnlTop.Location = New Point(12, 12)
        Me.pnlTop.Size = New Size(726, 55)
        Me.pnlTop.BorderStyle = BorderStyle.FixedSingle
        Me.Controls.Add(Me.pnlTop)

        Dim lblBooking As New Label()
        lblBooking.Text = "Booking ID:"
        lblBooking.Location = New Point(15, 17)
        lblBooking.AutoSize = True
        lblBooking.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        Me.pnlTop.Controls.Add(lblBooking)

        Me.cboBooking = New ComboBox()
        Me.cboBooking.Location = New Point(115, 13)
        Me.cboBooking.Size = New Size(450, 25)
        Me.cboBooking.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboBooking.Font = New Font("Segoe UI", 10)
        Me.pnlTop.Controls.Add(Me.cboBooking)

        Me.btnLoadSeats = New Button()
        Me.btnLoadSeats.Text = "Tải sơ đồ"
        Me.btnLoadSeats.Location = New Point(580, 10)
        Me.btnLoadSeats.Size = New Size(120, 33)
        Me.btnLoadSeats.BackColor = Color.FromArgb(63, 81, 181)
        Me.btnLoadSeats.ForeColor = Color.White
        Me.btnLoadSeats.FlatStyle = FlatStyle.Flat
        Me.btnLoadSeats.FlatAppearance.BorderSize = 0
        Me.btnLoadSeats.Cursor = Cursors.Hand
        Me.pnlTop.Controls.Add(Me.btnLoadSeats)

        ' ===== INFO PANEL =====
        Me.pnlInfo = New Panel()
        Me.pnlInfo.Location = New Point(12, 75)
        Me.pnlInfo.Size = New Size(726, 40)
        Me.Controls.Add(Me.pnlInfo)

        Me.lblInfo = New Label()
        Me.lblInfo.Text = "Chọn booking để bắt đầu gán ghế"
        Me.lblInfo.Location = New Point(5, 10)
        Me.lblInfo.AutoSize = True
        Me.lblInfo.Font = New Font("Segoe UI", 10)
        Me.lblInfo.ForeColor = Color.DarkSlateBlue
        Me.pnlInfo.Controls.Add(Me.lblInfo)

        Me.lblSeatCount = New Label()
        Me.lblSeatCount.Text = ""
        Me.lblSeatCount.Location = New Point(450, 10)
        Me.lblSeatCount.AutoSize = True
        Me.lblSeatCount.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        Me.lblSeatCount.ForeColor = Color.FromArgb(233, 30, 99)
        Me.pnlInfo.Controls.Add(Me.lblSeatCount)

        ' ===== STAGE LABEL =====
        Me.lblStage = New Label()
        Me.lblStage.Text = "🎭  SÂN KHẤU  🎭"
        Me.lblStage.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        Me.lblStage.ForeColor = Color.White
        Me.lblStage.BackColor = Color.FromArgb(63, 81, 181)
        Me.lblStage.TextAlign = ContentAlignment.MiddleCenter
        Me.lblStage.Size = New Size(540, 40)
        Me.lblStage.Location = New Point(105, 120)
        Me.Controls.Add(Me.lblStage)

        ' ===== SEAT MAP PANEL =====
        Me.pnlSeatMap = New Panel()
        Me.pnlSeatMap.Location = New Point(12, 170)
        Me.pnlSeatMap.Size = New Size(726, 430)
        Me.pnlSeatMap.AutoScroll = True
        Me.pnlSeatMap.BorderStyle = BorderStyle.FixedSingle
        Me.Controls.Add(Me.pnlSeatMap)

        ' ===== LEGEND =====
        Me.pnlLegend = New Panel()
        Me.pnlLegend.Location = New Point(12, 610)
        Me.pnlLegend.Size = New Size(500, 30)
        Me.Controls.Add(Me.pnlLegend)

        Dim lblAvailable As New Label()
        lblAvailable.Text = "⬜ Trống"
        lblAvailable.Location = New Point(0, 5)
        lblAvailable.AutoSize = True
        lblAvailable.Font = New Font("Segoe UI", 9)
        Me.pnlLegend.Controls.Add(lblAvailable)

        Dim lblSelected As New Label()
        lblSelected.Text = "🟦 Đang chọn"
        lblSelected.Location = New Point(80, 5)
        lblSelected.AutoSize = True
        lblSelected.Font = New Font("Segoe UI", 9)
        Me.pnlLegend.Controls.Add(lblSelected)

        Dim lblTaken As New Label()
        lblTaken.Text = "🟥 Đã gán (booking khác)"
        lblTaken.Location = New Point(185, 5)
        lblTaken.AutoSize = True
        lblTaken.Font = New Font("Segoe UI", 9)
        Me.pnlLegend.Controls.Add(lblTaken)

        ' ===== SAVE BUTTON =====
        Me.btnSave = New Button()
        Me.btnSave.Text = "💾 Lưu ghế đã chọn"
        Me.btnSave.Location = New Point(540, 605)
        Me.btnSave.Size = New Size(198, 40)
        Me.btnSave.BackColor = Color.FromArgb(76, 175, 80)
        Me.btnSave.ForeColor = Color.White
        Me.btnSave.FlatStyle = FlatStyle.Flat
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        Me.btnSave.Cursor = Cursors.Hand
        Me.btnSave.Enabled = False
        Me.Controls.Add(Me.btnSave)
    End Sub

    Friend WithEvents pnlTop As Panel
    Friend WithEvents pnlInfo As Panel
    Friend WithEvents pnlSeatMap As Panel
    Friend WithEvents pnlLegend As Panel
    Friend WithEvents cboBooking As ComboBox
    Friend WithEvents btnLoadSeats As Button
    Friend WithEvents lblInfo As Label
    Friend WithEvents lblSeatCount As Label
    Friend WithEvents lblStage As Label
    Friend WithEvents btnSave As Button
End Class
