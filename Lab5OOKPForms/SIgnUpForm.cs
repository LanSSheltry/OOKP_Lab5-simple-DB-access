using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab5OOKPForms
{
    public partial class SIgnUpForm : Form //Форма, изначально отвечающая только за регистрацию. Позднее была модифицирована для отображения информации и ее изменения при входе
    {
        Teacher teacher;
        string SubjCosts="";
        string Status = "SignUp";

        public SIgnUpForm() //Вид формы для регистрации
        {
            teacher = new Teacher();
            teacher.Name = "0";
            teacher.Surname = "0";
            teacher.Phone = "0";
            teacher.Login = "0";
            teacher.Password = "0";
            teacher.Methodology = "0";
            teacher.Article = "0";
            InitializeComponent();
            DeleteProfile.Visible = false;
            SaveProfile.Width = 376;
            SaveProfile.Height = 23;
            SaveProfile.Location = new Point(12, 202);
        }

        public SIgnUpForm(ref Teacher t, int mode)
        {
            InitializeComponent();
            teacher = t;
            if (mode == 2) InformationScenary(); //В зависимости от значения переменной mode форма примет соответствующий вид
            else if (mode == 1) SignInScenary();
        }

        private void SIgnUpForm_Load(object sender, EventArgs e)
        {

        }

        public void InformationScenary() //Форма открывает доступные данные о пользователе для просмотра
        {
            button1.Visible = false;
            button2.Visible = false;
            SaveProfile.Visible = false;
            DeleteProfile.Visible = false;
            TPassword.Visible = false;
            label5.Visible = false;
            label8.Text = "Subjects and costs:";
            SubjCost.Visible = false;
            TSubject.Visible = false;
            label9.Visible = false;
            TextBox.Height = 158;
            TextBox.Location= new Point(222, 38);
            TName.ReadOnly = true;
            TSurname.ReadOnly = true;
            TPhone.ReadOnly = true;
            TLogin.ReadOnly = true;
            TPassword.ReadOnly = true;
            TMethodology.ReadOnly = true;
            TArticle.ReadOnly = true;
            WriteProfileData();
        }

        private void SignInScenary() //Форма принимает вид для изменения информации аккаунта, если логин и пароль верны
        {
            WriteProfileData();
            Status = "SignIn";
        }


        private void DisplaySubjects() //Отображние предметов
        {
            for (int i = 0; i < teacher.GetSubjects().Count; i++)
            {
                SubjCosts += $"\nSubject: {teacher.GetSubjects()[i]}; Cost: {teacher.GetCosts()[i]};";
            }
        }

        private bool CheckLogin() //Проверка логина на правильность
        {
            SelectFrom s = new SelectFrom();
            List<List<string>> output = s.GetInf("Login", "Users");
            for (int i=0; i<output[0].Count; i++)
            {
                if (teacher.Login==output[0][i])
                {
                    return true;
                }
            }
            return false;
        }

        private void SaveProfile_Click(object sender, EventArgs e) //Сохранение введенных/измененных данных
        {
            ReadProfileData();
            if (teacher.Name!= "0" & teacher.Surname!="0" & teacher.Phone != "0" & teacher.Login !="0" & teacher.Password != "0")
            {
                if (Status == "SignUp")
                {
                    if (!CheckLogin())
                    {
                        WriteTo w = new WriteTo(ref teacher);
                        w.CreateTeacherAccount();
                        SIgnUpForm.ActiveForm.Close();
                    }
                    else
                    {
                        WarningForm w = new WarningForm("An account with this login already exists!");
                        w.Show();
                        WriteProfileData();
                        TLogin.Text = "";
                        TextBox.Text = "";
                    }
                }
                else if (Status == "SignIn")
                {
                    WriteTo w = new WriteTo(ref teacher);
                    w.UpdateTeacherAccount();
                    SIgnUpForm.ActiveForm.Close();
                }
            }
            else 
            {
                string field="";
                if (teacher.Name=="0") { field = "Name"; }
                else if (teacher.Surname == "0") field = "Surname";
                else if (teacher.Phone == "0") field = "Phone";
                else if (teacher.Login == "0") field = "Login";
                else if (teacher.Password == "0") field = "Password";
                WarningForm w = new WarningForm($"Incorrect Data in field {field}!");
                w.Show();
            }
        }

        public void ReadProfileData() //Для вноса в БД
        {
            teacher.Name = TName.Text;
            teacher.Surname = TSurname.Text;
            teacher.Phone = TPhone.Text;
            teacher.Login = TLogin.Text;
            teacher.Password = TPassword.Text;
            teacher.Methodology = TMethodology.Text;
            teacher.Article = TArticle.Text;
        }

        private void WriteProfileData() //Для загрузки из БД
        {
            TName.Text = teacher.Name;
            TSurname.Text = teacher.Surname;
            TPhone.Text = teacher.Phone;
            TLogin.Text = teacher.Login;
            TPassword.Text = teacher.Password;
            TMethodology.Text = teacher.Methodology;
            TArticle.Text = teacher.Article;
            DisplaySubjects();
            TextBox.Text = SubjCosts;
        }

        private void AddSubject_Click(object sender, EventArgs e) //Добавить предмет в перечень
        {
            if (TSubject.Text.Length > 0 & SubjCost.Text.Length > 0 & teacher.GetSubjects().Contains(TSubject.Text) == false)
            {
                teacher.SetSubjectsAndCost(TSubject.Text, SubjCost.Text);
                SubjCosts += $"\nSubject: {TSubject.Text}; Cost: {SubjCost.Text};";
            }
            else { }
            TextBox.Text = SubjCosts;
        }

        private void Remove_Click(object sender, EventArgs e) //Удалить предмет из перечня
        {
            if (TSubject.Text.Length > 0 & SubjCost.Text.Length > 0 & teacher.GetSubjects().Contains(TSubject.Text) == true)
            {
                teacher.GetSubjects().Remove(TSubject.Text);
                teacher.GetCosts().Remove(SubjCost.Text);
                SubjCosts = "";
                DisplaySubjects();
                TextBox.Text = SubjCosts;
            }
        }

        private void DeleteProfile_Click(object sender, EventArgs e) //Удалить профиль
        {
            WriteTo w = new WriteTo(ref teacher);
            w.DeleteTeacherAccount();
            WarningForm warning = new WarningForm("Account deleted!");
            warning.Show();
            SIgnUpForm.ActiveForm.Close();
        }
    }
}
