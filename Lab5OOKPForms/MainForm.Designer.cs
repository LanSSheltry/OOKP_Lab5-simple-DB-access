
namespace Lab5OOKPForms
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ListBoxOfTeachers = new System.Windows.Forms.ListBox();
            this.SearchField = new System.Windows.Forms.TextBox();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SignIn = new System.Windows.Forms.Button();
            this.Login = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.SignUp = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ListBoxOfTeachers
            // 
            this.ListBoxOfTeachers.FormattingEnabled = true;
            this.ListBoxOfTeachers.Location = new System.Drawing.Point(12, 85);
            this.ListBoxOfTeachers.Name = "ListBoxOfTeachers";
            this.ListBoxOfTeachers.Size = new System.Drawing.Size(354, 186);
            this.ListBoxOfTeachers.TabIndex = 0;
            this.ListBoxOfTeachers.SelectedValueChanged += new System.EventHandler(this.Selected_Changed);
            // 
            // SearchField
            // 
            this.SearchField.Location = new System.Drawing.Point(12, 57);
            this.SearchField.Name = "SearchField";
            this.SearchField.Size = new System.Drawing.Size(120, 20);
            this.SearchField.TabIndex = 1;
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(9, 39);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(106, 15);
            this.SearchLabel.TabIndex = 3;
            this.SearchLabel.Text = "Search by subject:";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(138, 55);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 4;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchBySubject_Click);
            // 
            // SignIn
            // 
            this.SignIn.Location = new System.Drawing.Point(305, 55);
            this.SignIn.Name = "SignIn";
            this.SignIn.Size = new System.Drawing.Size(61, 23);
            this.SignIn.TabIndex = 5;
            this.SignIn.Text = "Sign In";
            this.SignIn.UseVisualStyleBackColor = true;
            this.SignIn.Click += new System.EventHandler(this.SignIn_Click);
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(240, 3);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(126, 20);
            this.Login.TabIndex = 6;
            this.Login.Text = "Login";
            this.Login.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LogClick);
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(240, 29);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(126, 20);
            this.Password.TabIndex = 8;
            this.Password.Text = "Password";
            this.Password.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PassClick);
            // 
            // SignUp
            // 
            this.SignUp.Location = new System.Drawing.Point(240, 54);
            this.SignUp.Name = "SignUp";
            this.SignUp.Size = new System.Drawing.Size(59, 23);
            this.SignUp.TabIndex = 9;
            this.SignUp.Text = "Sign Up";
            this.SignUp.UseVisualStyleBackColor = true;
            this.SignUp.Click += new System.EventHandler(this.SignUp_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Max to Min price",
            "Min to Max price"});
            this.comboBox1.Location = new System.Drawing.Point(109, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(104, 21);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.TabStop = false;
            this.comboBox1.Text = "Min to Max price";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Sort by:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 290);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.SignUp);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.SignIn);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.SearchLabel);
            this.Controls.Add(this.SearchField);
            this.Controls.Add(this.ListBoxOfTeachers);
            this.Name = "MainForm";
            this.Text = "AVRM";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListBoxOfTeachers;
        private System.Windows.Forms.TextBox SearchField;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button SignIn;
        private System.Windows.Forms.TextBox Login;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button SignUp;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}

