<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPerformanceMaster
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
        Me.Text = "Quản lý Suất diễn"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Font = New Font("Segoe UI", 9)

        ' ===== INPUT PANEL =====
        Me.pnlInput = New Panel()
        Me.pnlInput.Location = New Point(12, 12)
        Me.pnlInput.Size = New Size(876, 140)
        Me.pnlInput.BorderStyle = BorderStyle.FixedSingle
        Me.Controls.Add(Me.pnlInput)

        ' Show Name
        Dim lblShowName As New Label()
        lblShowName.Text = "Tên vở diễn:"
        lblShowName.Location = New Point(15, 15)
        lblShowName.AutoSize = True
        lblShowName.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(lblShowName)

        Me.txtShowName = New TextBox()
        Me.txtShowName.Location = New Point(130, 12)
        Me.txtShowName.Size = New Size(300, 25)
        Me.txtShowName.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(Me.txtShowName)

        ' Start Time
        Dim lblStartTime As New Label()
        lblStartTime.Text = "Thời gian:"
        lblStartTime.Location = New Point(450, 15)
        lblStartTime.AutoSize = True
        lblStartTime.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(lblStartTime)

        Me.dtpStartTime = New DateTimePicker()
        Me.dtpStartTime.Location = New Point(540, 12)
        Me.dtpStartTime.Size = New Size(220, 25)
        Me.dtpStartTime.Format = DateTimePickerFormat.Custom
        Me.dtpStartTime.CustomFormat = "dd/MM/yyyy HH:mm"
        Me.dtpStartTime.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(Me.dtpStartTime)

        ' Duration
        Dim lblDuration As New Label()
        lblDuration.Text = "Thời lượng (phút):"
        lblDuration.Location = New Point(15, 55)
        lblDuration.AutoSize = True
        lblDuration.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(lblDuration)

        Me.nudDuration = New NumericUpDown()
        Me.nudDuration.Location = New Point(170, 52)
        Me.nudDuration.Size = New Size(100, 25)
        Me.nudDuration.Minimum = 1
        Me.nudDuration.Maximum = 600
        Me.nudDuration.Value = 90
        Me.nudDuration.Font = New Font("Segoe UI", 10)
        Me.pnlInput.Controls.Add(Me.nudDuration)

        ' Buttons
        Me.btnAdd = New Button()
        Me.btnAdd.Text = "➕ Thêm"
        Me.btnAdd.Location = New Point(15, 95)
        Me.btnAdd.Size = New Size(110, 35)
        Me.btnAdd.BackColor = Color.FromArgb(76, 175, 80)
        Me.btnAdd.ForeColor = Color.White
        Me.btnAdd.FlatStyle = FlatStyle.Flat
        Me.btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.Cursor = Cursors.Hand
        Me.pnlInput.Controls.Add(Me.btnAdd)

        Me.btnUpdate = New Button()
        Me.btnUpdate.Text = "✏️ Sửa"
        Me.btnUpdate.Location = New Point(135, 95)
        Me.btnUpdate.Size = New Size(110, 35)
        Me.btnUpdate.BackColor = Color.FromArgb(33, 150, 243)
        Me.btnUpdate.ForeColor = Color.White
        Me.btnUpdate.FlatStyle = FlatStyle.Flat
        Me.btnUpdate.FlatAppearance.BorderSize = 0
        Me.btnUpdate.Cursor = Cursors.Hand
        Me.pnlInput.Controls.Add(Me.btnUpdate)

        Me.btnDelete = New Button()
        Me.btnDelete.Text = "🗑️ Xóa"
        Me.btnDelete.Location = New Point(255, 95)
        Me.btnDelete.Size = New Size(110, 35)
        Me.btnDelete.BackColor = Color.FromArgb(244, 67, 54)
        Me.btnDelete.ForeColor = Color.White
        Me.btnDelete.FlatStyle = FlatStyle.Flat
        Me.btnDelete.FlatAppearance.BorderSize = 0
        Me.btnDelete.Cursor = Cursors.Hand
        Me.pnlInput.Controls.Add(Me.btnDelete)

        Me.btnClear = New Button()
        Me.btnClear.Text = "🔄 Làm mới"
        Me.btnClear.Location = New Point(375, 95)
        Me.btnClear.Size = New Size(120, 35)
        Me.btnClear.BackColor = Color.FromArgb(158, 158, 158)
        Me.btnClear.ForeColor = Color.White
        Me.btnClear.FlatStyle = FlatStyle.Flat
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.Cursor = Cursors.Hand
        Me.pnlInput.Controls.Add(Me.btnClear)

        ' ===== SEARCH PANEL =====
        Me.pnlSearch = New Panel()
        Me.pnlSearch.Location = New Point(12, 160)
        Me.pnlSearch.Size = New Size(876, 50)
        Me.pnlSearch.BorderStyle = BorderStyle.FixedSingle
        Me.Controls.Add(Me.pnlSearch)

        Dim lblSearch As New Label()
        lblSearch.Text = "🔍 Tìm kiếm:"
        lblSearch.Location = New Point(15, 14)
        lblSearch.AutoSize = True
        lblSearch.Font = New Font("Segoe UI", 10)
        Me.pnlSearch.Controls.Add(lblSearch)

        Me.txtSearch = New TextBox()
        Me.txtSearch.Location = New Point(120, 11)
        Me.txtSearch.Size = New Size(200, 25)
        Me.txtSearch.Font = New Font("Segoe UI", 10)
        Me.txtSearch.PlaceholderText = "Tên vở diễn..."
        Me.pnlSearch.Controls.Add(Me.txtSearch)

        Dim lblFrom As New Label()
        lblFrom.Text = "Từ ngày:"
        lblFrom.Location = New Point(340, 14)
        lblFrom.AutoSize = True
        lblFrom.Font = New Font("Segoe UI", 10)
        Me.pnlSearch.Controls.Add(lblFrom)

        Me.dtpFrom = New DateTimePicker()
        Me.dtpFrom.Location = New Point(410, 11)
        Me.dtpFrom.Size = New Size(140, 25)
        Me.dtpFrom.Format = DateTimePickerFormat.Short
        Me.dtpFrom.Font = New Font("Segoe UI", 9)
        Me.pnlSearch.Controls.Add(Me.dtpFrom)

        Dim lblTo As New Label()
        lblTo.Text = "Đến:"
        lblTo.Location = New Point(560, 14)
        lblTo.AutoSize = True
        lblTo.Font = New Font("Segoe UI", 10)
        Me.pnlSearch.Controls.Add(lblTo)

        Me.dtpTo = New DateTimePicker()
        Me.dtpTo.Location = New Point(600, 11)
        Me.dtpTo.Size = New Size(140, 25)
        Me.dtpTo.Format = DateTimePickerFormat.Short
        Me.dtpTo.Font = New Font("Segoe UI", 9)
        Me.pnlSearch.Controls.Add(Me.dtpTo)

        Me.btnSearch = New Button()
        Me.btnSearch.Text = "Tìm"
        Me.btnSearch.Location = New Point(755, 8)
        Me.btnSearch.Size = New Size(80, 32)
        Me.btnSearch.BackColor = Color.FromArgb(63, 81, 181)
        Me.btnSearch.ForeColor = Color.White
        Me.btnSearch.FlatStyle = FlatStyle.Flat
        Me.btnSearch.FlatAppearance.BorderSize = 0
        Me.btnSearch.Cursor = Cursors.Hand
        Me.pnlSearch.Controls.Add(Me.btnSearch)

        ' ===== DATAGRIDVIEW =====
        Me.dgvPerformances = New DataGridView()
        Me.dgvPerformances.Location = New Point(12, 220)
        Me.dgvPerformances.Size = New Size(876, 390)
        Me.dgvPerformances.AllowUserToAddRows = False
        Me.dgvPerformances.AllowUserToDeleteRows = False
        Me.dgvPerformances.ReadOnly = True
        Me.dgvPerformances.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvPerformances.MultiSelect = False
        Me.dgvPerformances.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPerformances.BackgroundColor = Color.White
        Me.dgvPerformances.RowHeadersVisible = False
        Me.dgvPerformances.Font = New Font("Segoe UI", 9.5)
        Me.dgvPerformances.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        Me.dgvPerformances.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(63, 81, 181)
        Me.dgvPerformances.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        Me.dgvPerformances.EnableHeadersVisualStyles = False
        Me.dgvPerformances.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 255)
        Me.Controls.Add(Me.dgvPerformances)
    End Sub

    Friend WithEvents pnlInput As Panel
    Friend WithEvents pnlSearch As Panel
    Friend WithEvents txtShowName As TextBox
    Friend WithEvents dtpStartTime As DateTimePicker
    Friend WithEvents nudDuration As NumericUpDown
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents btnSearch As Button
    Friend WithEvents dgvPerformances As DataGridView
End Class
