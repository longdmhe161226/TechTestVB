Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Test database connection on startup
        If DbHelper.TestConnection() Then
            lblStatus.Text = "Kết nối server thành công"
            lblStatus.ForeColor = Color.Green
        Else
            lblStatus.Text = "Không thể kết nối server"
            lblStatus.ForeColor = Color.Red
        End If
    End Sub

    Private Sub btnPerformance_Click(sender As Object, e As EventArgs) Handles btnPerformance.Click
        Dim frm As New frmPerformanceMaster()
        frm.ShowDialog()
    End Sub

    Private Sub btnBooking_Click(sender As Object, e As EventArgs) Handles btnBooking.Click
        Dim frm As New frmBooking()
        frm.ShowDialog()
    End Sub

    Private Sub btnSeatAssignment_Click(sender As Object, e As EventArgs) Handles btnSeatAssignment.Click
        Dim frm As New frmSeatAssignment()
        frm.ShowDialog()
    End Sub

End Class
