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
        static List<string> Errors = new List<string>(3);
        public static bool CheckReg(string login,string password,string confirm)
        {
            Errors.Clear();
            if (login == "" || password == "" || confirm == "")
                Errors[0] = "Поля должны быть заполнены!";
            if (login.Length < 4 || password.Length < 6 || confirm.Length < 6)
                Errors[1] = "Минимальная длина логина - 4 символа, пароля - 6 символов";
            if (password != confirm)
                Errors[2] = "Пароли не совпадают!";
            if (Errors.Count > 0)
                return false;
            else
                return true;
        }
        public static bool CheckLog(string login, string password)
        {
            Errors.Clear();
            if (login == "" || password == "")
                Errors[0] = "Поля должны быть заполнены!";
            if (login.Length < 4 || password.Length < 6)
                Errors[1] = "Минимальная длина логина - 4 символа, пароля - 6 символов";
            if (Errors.Count > 0)
                return false;
            else
                return true;
        }
    }
    public class Parser
    {
        static public bool ParseLogin(string login)
        {
            Regex regex = new Regex("/^[a-zA-Z0-9-_]{3,20}$/");
            return (regex.IsMatch(login)) ? true : false;
        }

        static public bool ParsePassword(string password)
        {
            Regex regex = new Regex("(?=[#$-/:-?{-~!\" ^ _`\\[\\]a-zA-Z]*([0-9#$-/:-?{-~!\"^_`\\[\\]]))(?=[#$-/:-?{-~!\"^_`\\[\\]a-zA-Z0-9]*[a-zA-Z])[#$-/:-?{-~!\"^_`\\[\\]a-zA-Z0-9]{4,}");
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
            /*if (Checker.CheckLog(login,password))
            {
                if (Parser.ParseLogin(login) && Parser.ParsePassword(password))
                {
                    var user = _authModel.Auth(login, BCrypt.Net.BCrypt.HashPassword(password));
                    if (user != null)
                        return true;
                    else
                        return false;
                }
            }*/
            FileStream f = new FileStream("log.txt",FileMode.Create);
            byte[] info = new UTF8Encoding(true).GetBytes(login+"\n");
            f.Write(info, 0, info.Length);
            info = new UTF8Encoding(true).GetBytes(password + "\n");
            f.Write(info, 0, info.Length);
            f.Close();

            return false;
        }

        public bool Register(string login, string password, string confirm)
        {
            /*if (Checker.CheckReg(login, password, confirm))
            {
                if (Parser.ParseLogin(login) && Parser.ParsePassword(password))
                {
                    var user = _authModel.Read(login);
                    if (user != null)
                        return false;
                    else
                    {
                        _authModel.Create(login, BCrypt.Net.BCrypt.HashPassword(password));
                        return true;
                    }
                }
            }*/

            FileStream f = new FileStream("reg.txt", FileMode.Create);
            byte[] info = new UTF8Encoding(true).GetBytes(login + "\n");
            f.Write(info, 0, info.Length);
            info = new UTF8Encoding(true).GetBytes(password + "\n");
            f.Write(info, 0, info.Length);
            info = new UTF8Encoding(true).GetBytes(confirm + "\n");
            f.Write(info, 0, info.Length);
            f.Close();
            return false;
        }
    }
}
