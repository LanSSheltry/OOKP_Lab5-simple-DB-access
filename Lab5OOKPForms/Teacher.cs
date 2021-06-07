using System.Collections.Generic;
using System.Linq;

namespace Lab5OOKPForms
{

    public class Teacher:IUserData
    {
        List<string> Subjects = new List<string>(); //Предметы преподавателя
        List<string> Costs = new List<string>(); //Стоимость каждого предмета
        private string id, login, password, name, surname, phone, article, methodology, role; //Личные данные

        public string Role
        {
            get { return role; }
            set
            {
                SetValue(value, ref role);
            }
        }

        public string ID //В БД это int primary key AUTO_INCREMENT, уникальный идентификатор пользователя
        {
            get { return id; }
            set
            {
                SetValue(value, ref id);
            }
        }
        public string Login //Логин пользователя
        {
            get { return login; }
            set
            {
                SetValue(value, ref login);
            }
        }

        public string Password //Пароль пользователя
        {
            get { return password; }
            set
            {
                SetValue(value, ref password);
            }
        }

        public string Name //Имя пользователя
        {
            get { return name; }
            set
            {
                SetValue(value, ref name);
            }
        }

        public string Surname //Фамилия пользователя
        {
            get { return surname; }
            set
            {
                SetValue(value, ref surname);
            }
        }

        public string Phone //Телефон пользователя
        {
            get { return phone; }
            set
            {
                SetValue(value, ref phone);
            }
        }

        public string Article //Работы преподавателя
        {
            get { return article; }
            set
            {
                SetValue(value, ref article);
            }
        }

        public string Methodology //Указанная методология
        {
            get { return methodology; }
            set
            {
                SetValue(value, ref methodology);
            }
        }

        public void SetValue(string value, ref string param) //Установить значение с элементарной проверкой правильности формата ввода
        {
            if (value.Length>0) param = value;
        }

        public void SetSubjectsAndCost(string Subject, string Cost) //Добавляет элементы в списки с соответствующими индексами
        {
            Subjects.Add(Subject);
            Costs.Add(Cost);
        }

        public ref List<string> GetSubjects() //Возвращает все предметы преподавателя
        {
            return ref Subjects;
        }

        public ref List<string> GetCosts() //Возвращает стоимости каждого предмета прподавателя
        {
            return ref Costs;
        }

        public void Log_in() //Некоторые действия при входе с соответствующими логином и паролем
        {
            SelectFrom getInf = new SelectFrom();
            List<List<string>> Data = getInf.GetInf("Role", "users", $@"where Login=""{login}"" and Password=""{password}""");
            if (Data.Count() > 0) role = Data[0][0];
            else role = "None";
        }
    }
}
