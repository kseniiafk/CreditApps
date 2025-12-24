using CreditApps.WinForms.Repositories;

namespace CreditApps.WinForms;

public partial class CreateApplicationForm : Form
{
    private readonly UserInfo _user;
    private readonly LoanApplicationRepository _repo = new();

    public CreateApplicationForm(UserInfo user)
    {
        InitializeComponent();
        _user = user;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtFio.Text) ||
                string.IsNullOrWhiteSpace(txtPassport.Text))
            {
                MessageBox.Show("Заполните ФИО и паспорт");
                return;
            }

            _repo.Create(
                txtFio.Text.Trim(),
                txtPassport.Text.Trim(),
                numIncome.Value,
                numAmount.Value,
                (int)numTerm.Value,
                _user.Id
            );

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка: " + ex.Message);
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}
