using System;
using System.Windows.Forms;
using CreditApps.WinForms.Repositories;

namespace CreditApps.WinForms
{
    public partial class MainForm : Form
    {
        private readonly UserInfo _user;
        private readonly LoanApplicationRepository _apps = new();

        // Tooltip для комментариев
        private readonly ToolTip _tip = new ToolTip();
        private int _lastRowIndex = -1;

        public MainForm(UserInfo user)
        {
            InitializeComponent();
            _user = user;
        }

        // ===== СОБЫТИЕ ЗАГРУЗКИ ФОРМЫ =====
        // ОБЯЗАТЕЛЬНО привязать: MainForm.Load → MainForm_Load
        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = $"Кредитные заявки — {_user.FullName} ({_user.Role})";

            // Настройка таблицы
            gridApps.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridApps.ReadOnly = true;
            gridApps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridApps.MultiSelect = false;

            // Заполнение статусов
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[]
            {
                "Новая",
                "На проверке",
                "Одобрена",
                "Отказ",
                "Выдана",
                "Закрыта"
            });
            cmbStatus.SelectedIndex = 0;

            LoadData();
        }

        // ===== ЗАГРУЗКА ДАННЫХ =====
        private void LoadData()
        {
            try
            {
                gridApps.DataSource = _apps.GetAll();

                // Прячем числовой статус
                if (gridApps.Columns["Status"] != null)
                    gridApps.Columns["Status"].Visible = false;

                // Переименовываем колонки
                if (gridApps.Columns["StatusText"] != null)
                    gridApps.Columns["StatusText"].HeaderText = "Статус";

                if (gridApps.Columns["Id"] != null)
                    gridApps.Columns["Id"].HeaderText = "№";

                if (gridApps.Columns["ApplicantFullName"] != null)
                    gridApps.Columns["ApplicantFullName"].HeaderText = "ФИО клиента";

                if (gridApps.Columns["RequestedAmount"] != null)
                    gridApps.Columns["RequestedAmount"].HeaderText = "Сумма";

                if (gridApps.Columns["TermMonths"] != null)
                    gridApps.Columns["TermMonths"].HeaderText = "Срок (мес)";

                if (gridApps.Columns["CreatedAt"] != null)
                    gridApps.Columns["CreatedAt"].HeaderText = "Дата создания";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки заявок: " + ex.Message);
            }
        }

        // ===== КНОПКА ОБНОВИТЬ =====
        // ОБЯЗАТЕЛЬНО привязать: btnRefresh.Click → btnRefresh_Click
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        // ===== КНОПКА СОЗДАТЬ ЗАЯВКУ =====
        // ОБЯЗАТЕЛЬНО привязать: btnCreate.Click → btnCreate_Click
        private void btnCreate_Click(object sender, EventArgs e)
        {
            using var f = new CreateApplicationForm(_user);
            if (f.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        // ===== КНОПКА ИЗМЕНИТЬ СТАТУС =====
        // ОБЯЗАТЕЛЬНО привязать: btnChangeStatus.Click → btnChangeStatus_Click
        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            if (gridApps.CurrentRow == null)
            {
                MessageBox.Show("Выберите заявку");
                return;
            }

            var row = (LoanApplicationRow)gridApps.CurrentRow.DataBoundItem;

            int fromStatus = row.Status;
            int toStatus = cmbStatus.SelectedIndex;

            string? comment = string.IsNullOrWhiteSpace(txtStatusComment.Text)
                ? null
                : txtStatusComment.Text.Trim();

            try
            {
                _apps.ChangeStatus(row.Id, fromStatus, toStatus, _user.Id, comment);
                txtStatusComment.Text = "";
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка смены статуса: " + ex.Message);
            }
        }

        // ===== КНОПКА ИСТОРИЯ =====
        // ОБЯЗАТЕЛЬНО привязать: btnHistory.Click → btnHistory_Click
        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (gridApps.CurrentRow == null)
            {
                MessageBox.Show("Выберите заявку");
                return;
            }

            var row = (LoanApplicationRow)gridApps.CurrentRow.DataBoundItem;

            try
            {
                var history = _apps.GetHistory(row.Id);

                if (history.Count == 0)
                {
                    MessageBox.Show("История пуста");
                    return;
                }

                string text = "";
                foreach (var h in history)
                {
                    text += $"{h.ChangedAt:dd.MM.yyyy HH:mm} | " +
                            $"{h.FromStatusText} → {h.ToStatusText} | " +
                            $"{h.Comment}\n";
                }

                MessageBox.Show(text, "История изменений");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка получения истории: " + ex.Message);
            }
        }

        // ===== TOOLTIP ПРИ НАВЕДЕНИИ НА СТРОКУ =====
        // ОБЯЗАТЕЛЬНО привязать: gridApps.MouseMove → gridApps_MouseMove
        private void gridApps_MouseMove(object sender, MouseEventArgs e)
        {
            var hit = gridApps.HitTest(e.X, e.Y);

            // Мышь не над строкой
            if (hit.RowIndex < 0)
            {
                _tip.Hide(gridApps);
                _lastRowIndex = -1;
                return;
            }

            // Всё ещё на той же строке
            if (hit.RowIndex == _lastRowIndex)
                return;

            _lastRowIndex = hit.RowIndex;

            var item = gridApps.Rows[hit.RowIndex].DataBoundItem as LoanApplicationRow;
            if (item == null) return;

            try
            {
                var comment = _apps.GetLastComment(item.Id);
                if (string.IsNullOrWhiteSpace(comment))
                    comment = "Комментариев нет";

                _tip.Show(comment, gridApps, e.Location.X + 15, e.Location.Y + 15);
            }
            catch
            {
                // игнорируем ошибки tooltip
            }
        }

        // ===== СКРЫВАЕМ TOOLTIP ПРИ УХОДЕ МЫШИ =====
        // ОБЯЗАТЕЛЬНО привязать: gridApps.MouseLeave → gridApps_MouseLeave
        private void gridApps_MouseLeave(object sender, EventArgs e)
        {
            _tip.Hide(gridApps);
            _lastRowIndex = -1;
        }
    }
}
