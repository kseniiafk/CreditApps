namespace CreditApps.WinForms
{
    partial class CreateApplicationForm
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
            txtFio = new TextBox();
            label1 = new Label();
            txtPassport = new TextBox();
            label2 = new Label();
            numIncome = new NumericUpDown();
            label3 = new Label();
            numAmount = new NumericUpDown();
            label4 = new Label();
            numTerm = new NumericUpDown();
            label5 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)numIncome).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numAmount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTerm).BeginInit();
            SuspendLayout();
            // 
            // txtFio
            // 
            txtFio.Location = new Point(30, 44);
            txtFio.Name = "txtFio";
            txtFio.Size = new Size(187, 23);
            txtFio.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 26);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 1;
            label1.Text = "ФИО Клиента";
            // 
            // txtPassport
            // 
            txtPassport.Location = new Point(30, 91);
            txtPassport.Name = "txtPassport";
            txtPassport.Size = new Size(187, 23);
            txtPassport.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 73);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 3;
            label2.Text = "Паспорт";
            // 
            // numIncome
            // 
            numIncome.DecimalPlaces = 2;
            numIncome.Location = new Point(281, 44);
            numIncome.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            numIncome.Name = "numIncome";
            numIncome.Size = new Size(120, 23);
            numIncome.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(281, 26);
            label3.Name = "label3";
            label3.Size = new Size(120, 15);
            label3.TabIndex = 5;
            label3.Text = "Ежемесячный доход";
            // 
            // numAmount
            // 
            numAmount.DecimalPlaces = 2;
            numAmount.Location = new Point(439, 44);
            numAmount.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            numAmount.Name = "numAmount";
            numAmount.Size = new Size(120, 23);
            numAmount.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(439, 26);
            label4.Name = "label4";
            label4.Size = new Size(91, 15);
            label4.TabIndex = 7;
            label4.Text = "Сумма кредита";
            // 
            // numTerm
            // 
            numTerm.Location = new Point(601, 45);
            numTerm.Name = "numTerm";
            numTerm.Size = new Size(120, 23);
            numTerm.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(601, 26);
            label5.Name = "label5";
            label5.Size = new Size(67, 15);
            label5.TabIndex = 9;
            label5.Text = "Срок (мес)";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(281, 114);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(106, 52);
            btnSave.TabIndex = 10;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(439, 114);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(114, 52);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // CreateApplicationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(748, 208);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(label5);
            Controls.Add(numTerm);
            Controls.Add(label4);
            Controls.Add(numAmount);
            Controls.Add(label3);
            Controls.Add(numIncome);
            Controls.Add(label2);
            Controls.Add(txtPassport);
            Controls.Add(label1);
            Controls.Add(txtFio);
            Name = "CreateApplicationForm";
            Text = "CreateApplicationForm";
            ((System.ComponentModel.ISupportInitialize)numIncome).EndInit();
            ((System.ComponentModel.ISupportInitialize)numAmount).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTerm).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFio;
        private Label label1;
        private TextBox txtPassport;
        private Label label2;
        private NumericUpDown numIncome;
        private Label label3;
        private NumericUpDown numAmount;
        private Label label4;
        private NumericUpDown numTerm;
        private Label label5;
        private Button btnSave;
        private Button btnCancel;
    }
}