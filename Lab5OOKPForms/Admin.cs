using System.Collections.Generic;
using System.Linq;

namespace Lab5OOKPForms
{
    interface IUserData //Интерфейс, описывающий методы установки значений и входа в аккаунт
    {
        void Log_in();
        void SetValue(string value, ref string param);
    }
    class Admin:IUserData //Класс администратора, идентичен классу преподавателя просто потому что проектировалось всё на коленке
    {
        private string id, login, password, role;

        public string Role
        {
            get { return role; }
            set
            {
                SetValue(value, ref role);
            }
        }
        public string ID
        {
            get { return id; }
            set
            {
                SetValue(value, ref id);
            }
        }
        public string Login
        {
            get { return login; }
            set
            {
                SetValue(value, ref login);
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                SetValue(value, ref password);
            }
        }

        public void Log_in()
        {
            SelectFrom getInf = new SelectFrom();
            List<List<string>> Data = getInf.GetInf("Role", "users", $@"where Login=""{login}"" and Password=""{password}""");
            if (Data.Count() > 0) role = Data[0][0];
            else role = "None";
        }

        public void SetValue(string value, ref string param)
        {
            if (value.Length > 0) param = value;
        }
    }
}
