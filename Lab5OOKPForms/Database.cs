using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Lab5OOKPForms
{
    class SelectFrom //Класс, отвечающий за загрузку данных из БД
    {
        private static MySqlConnection DBSelect = new MySqlConnection("server=localhost;port=3306;username=root;password=Kmwz05020403;database=usersdata"); //Подключение к БД

        public List<List<string>> GetInf(string fields, string tables, string where = "") //Получение информации из определенных полей заданой таблицы
        {
            DBSelect.Open();
            string sql = $@"Select {fields} from {tables} {where};";
            List<List<string>> output = new List<List<string>>();
            output.Add(new List<string>());
            MySqlCommand com = new MySqlCommand(sql, DBSelect);
            MySqlDataReader reader = com.ExecuteReader();
            int counter = 0;

            while (reader.Read())
            {
                output.Add(new List<string>());
                for (int i = 0; i < reader.FieldCount; i++)
                { output[counter].Add(reader[i].ToString()); }
                counter++;
            }
            reader.Close();
            DBSelect.Close();
            if (counter>0) return output; //Проверка на наличие данных в возвращаемом двумерном списке
            else { output[0].Add(""); return output; }
        }

    }

    class WriteTo
    {
        public static MySqlConnection DBInsert = new MySqlConnection("server=localhost;port=3306;username=root;password=Kmwz05020403;database=usersdata"); //Подключение к БД
        Teacher teacher;
        public WriteTo(ref Teacher teacher)
        {
            this.teacher = teacher;
        }

        public void CreateTeacherAccount() //Создание аккаунта преподавателя
        {
            string Role = "1";
            string sql =
                $@"Insert into Users (UserId, Login, Password, Role) values (null, ""{teacher.Login}"", ""{teacher.Password}"", ""{Role}"");" +
                $@"Insert into Teachers (UserId, Name, Surname, `Phone number`) values (null, ""{teacher.Name}"", ""{teacher.Surname}"", ""{teacher.Phone}"");" +
                $@"Insert into Methodology (UserId, Methodology) values ((select UserId from Teachers where UserId=(select UserId from Users where Login=""{teacher.Login}"")), ""{teacher.Methodology}"");" +
                $@"Insert into Article (UserId, Article) values ((select UserId from Teachers where UserId=(select UserId from Users where Login=""{teacher.Login}"")), ""{teacher.Article}"");";
            SQLCommand(sql);
            for (int i = 0; i < teacher.GetSubjects().Count; i++)
            {
                AddSubject(teacher.Login, teacher.GetSubjects()[i], teacher.GetCosts()[i]);
            }
            DBInsert.Close();
        }

        public void DeleteTeacherAccount() //Удаление аккаунта пользователя
        {
            string sql =
                $@"Delete from Users where UserId=""{teacher.ID}"";" +
                $@"Delete from Teachers where UserId=""{teacher.ID}"";" +
                $@"Delete from Methodology where UserId=""{teacher.ID}"";" +
                $@"Delete from Article where UserId=""{teacher.ID}"";" +
                $@"Delete from Lessons where UserId=""{teacher.ID}"";" +
                $@"Delete from Subjects where UserId=""{teacher.ID}"";";
            SQLCommand(sql);
            DBInsert.Close();
        }

        public void UpdateTeacherAccount() //Обновить данные в аккаунте преподавателя
        {
            string sql =
                $@"Update Users set Login=""{teacher.Login}"", Password=""{teacher.Password}"" where UserId=""{teacher.ID}"";" +
                $@"Update Teachers set Name=""{teacher.Name}"", Surname=""{teacher.Surname}"", `Phone number`=""{teacher.Phone}"" where UserId=""{teacher.ID}"";" +
                $@"Update Methodology set Methodology=""{teacher.Methodology}"" where UserId=""{teacher.ID}"";" +
                $@"Update Article set Article=""{teacher.Article}"" where UserId=""{teacher.ID}"";" +
                $@"Delete from Lessons where UserId=""{teacher.ID}"";" +
                $@"Delete from Subjects where UserId=""{teacher.ID}"";";
            SQLCommand(sql);
            for (int i =0; i<teacher.GetSubjects().Count; i++)
            {
                AddSubject(teacher.Login, teacher.GetSubjects()[i], teacher.GetCosts()[i]);
            }
            DBInsert.Close();
        }


        public void AddSubject(string Login, string Subject, string Cost) //Добавление предметов и их стоимости
        {
            string sql = $@"Insert Into Subjects (UserId, Subject) values ((select UserId from Teachers where UserId=(select UserId from Users where Login=""{Login}"")), ""{Subject}"");" +
                    $@"Insert Into Lessons (UserId, Subject, Cost) values ((select UserId from Teachers where UserId=(select UserId from Users where Login=""{Login}"")), ""{Subject}"", ""{Cost}"");";
            SQLCommand(sql);
        }

        public void SQLCommand(string sql) //Обращение к БД, в данном случае к MySQL Workbench
        {
            MySqlCommand command = new MySqlCommand(sql, DBInsert);
            DBInsert.Close();
            command.ExecuteNonQuery();
        }

    }
}
