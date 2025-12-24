using CreditApps.WinForms.Repositories;

namespace CreditApps.WinForms;

public partial class LoginForm : Form
{
    private readonly UserRepository _users = new();

    public LoginForm()
    {
        InitializeComponent();

        txtLogin.Text = "admin";
        txtPassword.Text = "admin123";
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            var login = txtLogin.Text.Trim();
            var pass = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            var user = _users.Login(login, pass);

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            Hide();
            using var f = new MainForm(user);
            f.ShowDialog();
            Close();


            // дальше будет MainForm
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка: " + ex.Message);
        }
    }
}
