<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(500, 400)
        Me.Text = "Hệ thống Bán Vé Nhà Hát"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        ' Title Label
        Me.lblTitle = New Label()
        Me.lblTitle.Text = "HỆ THỐNG BÁN VÉ NHÀ HÁT"
        Me.lblTitle.Font = New Font("Segoe UI", 18, FontStyle.Bold)
        Me.lblTitle.ForeColor = Color.DarkSlateBlue
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New Point(70, 30)
        Me.Controls.Add(Me.lblTitle)

        ' Subtitle
        Me.lblSubtitle = New Label()
        Me.lblSubtitle.Text = "Theater Ticket System"
        Me.lblSubtitle.Font = New Font("Segoe UI", 11, FontStyle.Italic)
        Me.lblSubtitle.ForeColor = Color.Gray
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Location = New Point(155, 70)
        Me.Controls.Add(Me.lblSubtitle)

        ' Button 1: Performance Management
        Me.btnPerformance = New Button()
        Me.btnPerformance.Text = "📋  Quản lý Suất diễn"
        Me.btnPerformance.Size = New Size(340, 55)
        Me.btnPerformance.Location = New Point(80, 120)
        Me.btnPerformance.Font = New Font("Segoe UI", 12, FontStyle.Regular)
        Me.btnPerformance.BackColor = Color.FromArgb(63, 81, 181)
        Me.btnPerformance.ForeColor = Color.White
        Me.btnPerformance.FlatStyle = FlatStyle.Flat
        Me.btnPerformance.FlatAppearance.BorderSize = 0
        Me.btnPerformance.Cursor = Cursors.Hand
        Me.Controls.Add(Me.btnPerformance)

        ' Button 2: Booking
        Me.btnBooking = New Button()
        Me.btnBooking.Text = "🎫  Đặt Vé"
        Me.btnBooking.Size = New Size(340, 55)
        Me.btnBooking.Location = New Point(80, 190)
        Me.btnBooking.Font = New Font("Segoe UI", 12, FontStyle.Regular)
        Me.btnBooking.BackColor = Color.FromArgb(0, 150, 136)
        Me.btnBooking.ForeColor = Color.White
        Me.btnBooking.FlatStyle = FlatStyle.Flat
        Me.btnBooking.FlatAppearance.BorderSize = 0
        Me.btnBooking.Cursor = Cursors.Hand
        Me.Controls.Add(Me.btnBooking)

        ' Button 3: Seat Assignment
        Me.btnSeatAssignment = New Button()
        Me.btnSeatAssignment.Text = "💺  Gán Ghế"
        Me.btnSeatAssignment.Size = New Size(340, 55)
        Me.btnSeatAssignment.Location = New Point(80, 260)
        Me.btnSeatAssignment.Font = New Font("Segoe UI", 12, FontStyle.Regular)
        Me.btnSeatAssignment.BackColor = Color.FromArgb(233, 30, 99)
        Me.btnSeatAssignment.ForeColor = Color.White
        Me.btnSeatAssignment.FlatStyle = FlatStyle.Flat
        Me.btnSeatAssignment.FlatAppearance.BorderSize = 0
        Me.btnSeatAssignment.Cursor = Cursors.Hand
        Me.Controls.Add(Me.btnSeatAssignment)

        ' Connection status label
        Me.lblStatus = New Label()
        Me.lblStatus.Text = "Đang kiểm tra kết nối..."
        Me.lblStatus.Font = New Font("Segoe UI", 9)
        Me.lblStatus.ForeColor = Color.Gray
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New Point(150, 350)
        Me.Controls.Add(Me.lblStatus)
    End Sub

    Friend WithEvents btnPerformance As Button
    Friend WithEvents btnBooking As Button
    Friend WithEvents btnSeatAssignment As Button
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents lblStatus As Label
End Class
