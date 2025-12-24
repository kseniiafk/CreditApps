namespace CreditApps.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gridApps = new DataGridView();
            btnRefresh = new Button();
            btnCreate = new Button();
            cmbStatus = new ComboBox();
            txtStatusComment = new TextBox();
            btnChangeStatus = new Button();
            ((System.ComponentModel.ISupportInitialize)gridApps).BeginInit();
            SuspendLayout();
            // 
            // gridApps
            // 
            gridApps.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridApps.Location = new Point(0, 0);
            gridApps.Name = "gridApps";
            gridApps.ReadOnly = true;
            gridApps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridApps.Size = new Size(922, 301);
            gridApps.TabIndex = 0;
            gridApps.MouseLeave += gridApps_MouseLeave;
            gridApps.MouseMove += gridApps_MouseMove;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(79, 348);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(119, 54);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(237, 348);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(118, 54);
            btnCreate.TabIndex = 2;
            btnCreate.Text = "Создать заявку";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(417, 325);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(121, 23);
            cmbStatus.TabIndex = 3;
            // 
            // txtStatusComment
            // 
            txtStatusComment.Location = new Point(556, 325);
            txtStatusComment.Name = "txtStatusComment";
            txtStatusComment.PlaceholderText = "Комментарий";
            txtStatusComment.Size = new Size(193, 23);
            txtStatusComment.TabIndex = 4;
            // 
            // btnChangeStatus
            // 
            btnChangeStatus.Location = new Point(497, 366);
            btnChangeStatus.Name = "btnChangeStatus";
            btnChangeStatus.Size = new Size(122, 36);
            btnChangeStatus.TabIndex = 5;
            btnChangeStatus.Text = "Изменить статус";
            btnChangeStatus.UseVisualStyleBackColor = true;
            btnChangeStatus.Click += btnChangeStatus_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(922, 450);
            Controls.Add(btnChangeStatus);
            Controls.Add(txtStatusComment);
            Controls.Add(cmbStatus);
            Controls.Add(btnCreate);
            Controls.Add(btnRefresh);
            Controls.Add(gridApps);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)gridApps).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView gridApps;
        private Button btnRefresh;
        private Button btnCreate;
        private ComboBox cmbStatus;
        private TextBox txtStatusComment;
        private Button btnChangeStatus;
    }
}