using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

//Для связи с базой данных используется файл MySqlClient.dll
//Программа представляет собой простейшую БД для репетиторов
//Схема соответствующей БД представлена в файле DBScheme.jpg

namespace Lab5OOKPForms
{
    public partial class MainForm : Form //Текущая форма - главная страница приложения
    {
        Teacher teacher = new Teacher(); //Экземпляр класса, который является набором данных о некотором преподавателе
        List<Teacher> TDisplay = new List<Teacher>(); //Список, включающий элементы типа Teacher
        List<List<string>> QueryResult; //Результат запроса БД
        SelectFrom select = new SelectFrom(); //Экземпляр класса, отвечающего за загрузку данных из БД
        int UserID;
        string RealPassword;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            teacher.Password = "";
            DisplayList();
        }

        private void LogClick(object sender, MouseEventArgs e) //При нажатии на поле логин значение обнуляется
        {
            if (Login.Text=="Login") Login.Text = ""; 
        }

        private void PassClick(object sender, MouseEventArgs e) //При нажатии на поле пароль значение обнуляется
        {
            if (Password.Text=="Password") Password.Text = "";
        }

        private void SignUp_Click(object sender, EventArgs e) //Нажатие кнопки Sign Up
        {
            teacher = new Teacher(); //Создание экземпляра класса для нового преподавателя
            SIgnUpForm sp = new SIgnUpForm(); //Создание формы для заполнения преподавателем личных данных
            sp.Show(); //Показать форму
        }

        private void DisplayList() //Заполнение элемента ListBox данными из БД
        {
            SelectFrom s = new SelectFrom();
            GetTeachersData();
            for (int i=0; i<TDisplay.Count; i++)
            {
                string item = $"Name: {TDisplay[i].Name}, Surname: {TDisplay[i].Surname}, Phone: {TDisplay[i].Phone}";
                ListBoxOfTeachers.Items.Add(item);
            }
        }

        public void GetTeachersData() //Получить данные о конкретном преподавателе из БД
        {
            SelectFrom s = new SelectFrom();
            List<List<string>> users = s.GetInf("*", "users") ; //Списки данных о каждом пользователе в БД
            List<List<string>> teachers = s.GetInf("*", "teachers") ;
            List<List<string>> methodology = s.GetInf("*", "methodology");
            List<List<string>> article = s.GetInf("*", "article");
            List<List<string>> lessons = s.GetInf("*", "lessons");
            for (int i = 0; i < teachers.Count-1; i++)
            {
                teacher = new Teacher();
                teacher.ID = users[i][0];
                teacher.Login = users[i][1];
                teacher.Name = teachers[i][1];
                teacher.Surname = teachers[i][2];
                teacher.Phone = teachers[i][3];
                teacher.Methodology = methodology[i][1];
                teacher.Article = article[i][1];
                for (int j=0; j<lessons.Count-1; j++)
                {
                    if (lessons[j][0] == users[i][0]) teacher.SetSubjectsAndCost(lessons[j][1], lessons[j][2]);
                }
                TDisplay.Add(teacher);
            }
        }

        private void SearchBySubject_Click(object sender, EventArgs e) //Поиск преподавателей по предмету
        {
            List<string> items = new List<string>();
            List<double> price = new List<double>();
            ListBoxOfTeachers.Items.Clear();
            int index = 0;
            for (int i=0; i<TDisplay.Count; i++)
            {
                if (SearchField.Text != "")
                {
                    index = 0;
                    for (int j = 0; j < TDisplay[i].GetSubjects().Count; j++)
                    {
                        if (TDisplay[i].GetSubjects()[j] == SearchField.Text)
                        {
                            items.Add($"Name: {TDisplay[i].Name}, Surname: {TDisplay[i].Surname}, Phone: {TDisplay[i].Phone}");
                            price.Add(Convert.ToDouble(TDisplay[i].GetCosts()[j]));
                        }

                    }
                }
                else
                {
                    TDisplay.Clear();
                    DisplayList();
                    break;
                }
            }

            if (index == 0) //Сортировка результатов по цене за указанный предмет
            {
                while (items.Count>0)
                {
                    if (comboBox1.Text == "Max to Min price")
                    {
                        ListBoxOfTeachers.Items.Add(items[price.IndexOf(price.Max())]+$" cost: {price.Max()}");
                        items.RemoveAt(price.IndexOf(price.Max()));
                        price.RemoveAt(price.IndexOf(price.Max()));
                    }
                    else if (comboBox1.Text == "Min to Max price")
                    {
                        ListBoxOfTeachers.Items.Add(items[price.IndexOf(price.Min())]+$" cost: {price.Min()}");
                        items.RemoveAt(price.IndexOf(price.Min()));
                        price.RemoveAt(price.IndexOf(price.Min()));
                    }
                }
            }
        }

        private void SignIn_Click(object sender, EventArgs e) //Событие нажатия кнопки "Sign In"
        {
            teacher = new Teacher();
            teacher.Login = Login.Text;
            teacher.Password = Password.Text;
            SearchID();
        }


        public void SearchID() //Поиск идентификатора пользователя
        {
            Admin user = new Admin();
            user.Login = Login.Text;
            user.Password = Password.Text;
            user.Log_in();
            if (user.Role == "2") MessageBox.Show("Вы вошли как администратор!");
            else if (user.Role == "1") GetTeacherInfByLogin();
            else { MessageBox.Show("Аккаунта не существует!"); }
        }

        public void GetTeacherInfByLogin() //Информация о пользователе по логину
        {
            if (GetUserIDByLogin())
            {
                DBTableTeachers();
                DBTableUsers();
                DBTableLessons();
                DBTableMethodology();
                DBTableArticle();
                SIgnUpForm si = new SIgnUpForm(ref teacher, 1);
                si.Show();
            }
        }

        private bool GetUserIDByLogin() //Получение идентификатора пользователя по его логину
        {
            try
            {
                UserID = Convert.ToInt32(select.GetInf("*", "users", $@"where Login=""{teacher.Login}""")[0][0]);
                teacher.ID = UserID.ToString();
                RealPassword = select.GetInf("Password", "users", $@"where Login=""{teacher.Login}""")[0][0];
                if (teacher.Password != RealPassword )
                {
                    MessageBox.Show("Incorrect Password!");
                    return false;
                }
                else return true;
            }
            catch 
            {
                MessageBox.Show("Incorrect Login!");
                return false; 
            }
        }

        public void DBTableTeachers() //Обращение к таблице "Teachers"
        {
            QueryResult = select.GetInf("*", "teachers", $@"where UserId=""{UserID}""");
            teacher.Name = QueryResult[0][1];
            teacher.Surname = QueryResult[0][2];
            teacher.Phone = QueryResult[0][3];
        }

        public void DBTableUsers() //Обращение к таблице "Users"
        {
            QueryResult = select.GetInf("*", "users", $@"where UserId=""{UserID}""");
            teacher.Password = QueryResult[0][2];
        }

        public void DBTableMethodology() //Обращение к таблице "Methodology"
        {
            QueryResult = select.GetInf("*", "methodology", $@"where UserId=""{UserID}""");
            teacher.Methodology = QueryResult[0][1];
        }

        public void DBTableArticle() //Обращение к таблице "Article"
        {
            QueryResult = select.GetInf("*", "article", $@"where UserId=""{UserID}""");
            teacher.Article = QueryResult[0][1];
        }

        public void DBTableLessons() //Обращение к таблице "Lessons"
        {
            QueryResult = select.GetInf("*", "lessons", $@"where UserId=""{UserID}""");
            for (int i = 0; i < QueryResult.Count; i++)
            {
                if (QueryResult[i].Count > 0)
                { teacher.SetSubjectsAndCost(QueryResult[i][1], QueryResult[i][2]); }
            }
        }

        private void Selected_Changed(object sender, EventArgs e) //Показать в отдельной форме открытые данные о выбранном преподавателе
        {
                Teacher t = new Teacher();
                for (int i = 0; i < TDisplay.Count; i++)
                {
                    if (ListBoxOfTeachers.SelectedItem.ToString() == $"Name: {TDisplay[i].Name}, Surname: {TDisplay[i].Surname}, Phone: {TDisplay[i].Phone}")
                    {
                        t = TDisplay[i];
                        SIgnUpForm inf = new SIgnUpForm(ref t, 2);
                        inf.Show();
                        break;
                    }
                }
        }
    }
}
