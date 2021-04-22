using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Storage;
using WebSite.Storage.Entity;
using WebSite.Models;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace WebSite.Managers
{
    public class Checker
    {
        public static string? CheckReg(string login,string password,string confirm)
        {
            if (login == "" || password == "" || confirm == "")
                return "Поля должны быть заполнены!";
            else if (login.Length < 4 || password.Length < 6 || confirm.Length < 6)
                return "Минимальная длина логина - 4 символа, пароля - 6 символов";
            else if (password != confirm)
                return "Пароли не совпадают!";
            return null;
        }
        public static string? CheckLog(string login, string password)
        {
            if (login == "" || password == "")
                return "Поля должны быть заполнены!";
            else if (login.Length < 4 || password.Length < 6)
                return "Минимальная длина логина - 4 символа, пароля - 6 символов";
            return null;
        }
    }
    public class Parser
    {
        static public bool ParseLogin(string login)
        {
            Regex regex = new Regex("/^[a-zA-Z0-9-_]{4,20}$/");
            return (regex.IsMatch(login)) ? true : false;
        }

        static public bool ParsePassword(string password)
        {
            Regex regex = new Regex("(?=[#$-/:-?{-~!\" ^ _`\\[\\]a-zA-Z]*([0-9#$-/:-?{-~!\"^_`\\[\\]]))(?=[#$-/:-?{-~!\"^_`\\[\\]a-zA-Z0-9]*[a-zA-Z])[#$-/:-?{-~!\"^_`\\[\\]a-zA-Z0-9]{6,100}");
            return (regex.IsMatch(password)) ? true : false;
        }
    }
    public class ProfileID : IProfileID
    {
        private IAuthModel _authModel;

        public ProfileID(IAuthModel authModel)
        {
            _authModel = authModel;
        }

        public string Login(string login, string password)
        {
            string answer = Checker.CheckLog(login, password);
            if (answer == null)
            {
                if (Parser.ParseLogin(login) && Parser.ParsePassword(password))
                {
                    var user = _authModel.Auth(login, BCrypt.Net.BCrypt.HashPassword(password));
                    if (user != null)
                        return "Авторизация прошла успешно!";
                    else
                        return "Логин или пароль неверны!";
                }
            }
            return "Ошибка при авторизации!";
        }

        public string Register(string login, string password, string confirm)
        {
            string answer = Checker.CheckReg(login, password, confirm);
            if (answer == null)
            {
                //if (Parser.ParseLogin(login) && Parser.ParsePassword(password))
                {
                    var user = _authModel.Read(login);
                    if (user.Result != null)
                        return "Такой аккаунт уже зарегестрирован!";
                    else
                    {
                        _authModel.Create(login, BCrypt.Net.BCrypt.HashPassword(password));
                        return "Регистрация прошла успешно!";
                    }
                }
            }

            return "Ошибка при регистрации!";

            /*FileStream f = new FileStream("reg.txt", FileMode.Create);
            byte[] info = new UTF8Encoding(true).GetBytes(login + "\n");
            f.Write(info, 0, info.Length);
            info = new UTF8Encoding(true).GetBytes(password + "\n");
            f.Write(info, 0, info.Length);
            info = new UTF8Encoding(true).GetBytes(confirm + "\n");
            f.Write(info, 0, info.Length);
            f.Close();*/
            return answer;
        }
    }
}
