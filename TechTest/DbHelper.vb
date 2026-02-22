Imports Microsoft.Extensions.Configuration
Imports Npgsql

Module DbHelper
    ' Connection string is loaded from appsettings.json
    Private ReadOnly CONNECTION_STRING As String

    Sub New()
        Dim config = New ConfigurationBuilder() _
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) _
            .AddJsonFile("appsettings.json", optional:=False, reloadOnChange:=True) _
            .Build()

        CONNECTION_STRING = config.GetConnectionString("DefaultConnection")
    End Sub

    Public Function GetConnection() As NpgsqlConnection
        Return New NpgsqlConnection(CONNECTION_STRING)
    End Function

    Public Function TestConnection() As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function
End Module
