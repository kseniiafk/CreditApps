using Microsoft.Data.SqlClient;

namespace CreditApps.WinForms.Data;

public static class Db
{
    public static readonly string ConnectionString =
        @"Server=.\SQLEXPRESS;Database=CreditAppsDb;Trusted_Connection=True;TrustServerCertificate=True;";

    public static SqlConnection Open()
    {
        var conn = new SqlConnection(ConnectionString);
        conn.Open();
        return conn;
    }
}
