using Microsoft.Data.SqlClient;
using CreditApps.WinForms.Data;

namespace CreditApps.WinForms.Repositories;

public class UserInfo
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Role { get; set; } = "";
}

public class UserRepository
{
    public UserInfo? Login(string login, string password)
    {
        using var conn = Db.Open();

        using var cmd = new SqlCommand(@"
SELECT TOP 1 Id, FullName, Role
FROM dbo.Users
WHERE Login = @login
  AND PasswordHash = @password
  AND IsActive = 1
", conn);

        cmd.Parameters.AddWithValue("@login", login);
        cmd.Parameters.AddWithValue("@password", password);

        using var r = cmd.ExecuteReader();
        if (!r.Read())
            return null;

        return new UserInfo
        {
            Id = r.GetInt32(0),
            FullName = r.GetString(1),
            Role = r.GetString(2)
        };
    }
}
