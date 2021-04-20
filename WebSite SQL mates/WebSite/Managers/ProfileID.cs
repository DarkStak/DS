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
        static int Counter;
        static string[] Errors = new string[3];
        public static string? CheckReg(string login,string password,string confirm)
        {
            /*Counter = 0;
            for (var z = 0; z < 3; z++)
                Errors[z] = "";*/
            if (login == null || password == null || confirm == null)
                return "Поля должны быть заполнены!";
            else if (login.Length < 4 || password.Length < 6 || confirm.Length < 6)
                return "Минимальная длина логина - 4 символа, пароля - 6 символов";
            else if (password != confirm)
                return "Пароли не совпадают!";
            /*for (var iter = 0; iter < 3; iter++)
                if (Errors[iter] != "")
                    Counter++;*/
            /*if (Counter > 0)
                return false;
            else
                return true;*/
            return null;
        }
        public static bool CheckLog(string login, string password)
        {
            Counter = 0;
            for (var z = 0; z < 3; z++)
                Errors[z] = "";
            if (login == null || password == null)
                Errors[0] = "Поля должны быть заполнены!";
            else if (login.Length < 4 || password.Length < 6)
                Errors[1] = "Минимальная длина логина - 4 символа, пароля - 6 символов";
            for (var iter = 0; iter < 3; iter++)
                if (Errors[iter] != "")
                    Counter++;
            if (Counter > 0)
                return false;
            else
                return true;
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

        public bool Login(string login, string password)
        {
            if (Checker.CheckLog(login,password))
            {
                if (Parser.ParseLogin(login) && Parser.ParsePassword(password))
                {
                    var user = _authModel.Auth(login, BCrypt.Net.BCrypt.HashPassword(password));
                    if (user != null)
                        return true;
                    else
                        return false;
                }
            }
            /*FileStream f = new FileStream("log.txt",FileMode.Create);
            byte[] info = new UTF8Encoding(true).GetBytes(login+"\n");
            f.Write(info, 0, info.Length);
            info = new UTF8Encoding(true).GetBytes(password + "\n");
            f.Write(info, 0, info.Length);
            f.Close();*/

            return false;
        }

        public string Register(string login, string password, string confirm)
        {
            string answer = Checker.CheckReg(login, password, confirm);
            if (answer == null)
            {
                if (Parser.ParseLogin(login) && Parser.ParsePassword(password))
                {
                    var user = _authModel.Read(login);
                    if (user != null)
                        return "Такой аккаунт уже зарегестрирован!";
                    else
                    {
                        _authModel.Create(login, BCrypt.Net.BCrypt.HashPassword(password));
                        return "Регистрация прошла успешно!";
                    }
                }
            }

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
