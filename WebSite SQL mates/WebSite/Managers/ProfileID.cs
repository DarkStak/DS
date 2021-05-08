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
using Microsoft.AspNetCore.Http;

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

        public LoginAnswer Login(string login, string password)
        {
            LoginAnswer Result = new LoginAnswer();
            string answer = Checker.CheckLog(login, password);
            Result.account = null;
            Result.result = answer;
            if (answer == null)
            {
                {
                    var user = _authModel.Auth(login, password);
                    if (user.Result != null)
                    {
                        Result.account=_authModel.ReadAccount(login).Result;
                        Result.result = "Авторизация прошла успешно!";
                    }
                    else
                    {
                        Result.account = null;
                        Result.result = "Логин или пароль неверны!";
                    }
                        
                }
            }
            return Result;
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
                        _authModel.CreateAccount(login, BCrypt.Net.BCrypt.HashPassword(password));
                        return "Регистрация прошла успешно!";
                    }
                }
                //else
                    //return "Логин или пароль слабые!";
            }
            else
                return answer;
        }

        public LoginAnswer ChangePassword(Account User, string password, string newpassword, string newconfirm)
        {
            LoginAnswer Res = new LoginAnswer();
            bool Result = BCrypt.Net.BCrypt.Verify(password, User.password);
            if (Result)
            {
                if ((newpassword == newconfirm) && (newpassword.Length >= 6))
                {
                    Profile UpdateProfile = new Profile();
                    UpdateProfile = _authModel.Read(User.login).Result;
                    UpdateProfile.password = BCrypt.Net.BCrypt.HashPassword(newpassword);
                    Account UpdateAccount = new Account();
                    UpdateAccount = _authModel.ReadAccount(User.login).Result;
                    UpdateAccount.password = BCrypt.Net.BCrypt.HashPassword(newpassword);
                    _authModel.Update(UpdateProfile);
                    _authModel.UpdateAccount(UpdateAccount);
                    Res.result = "Пароль успешно обновлён!";
                }
                else
                {
                    if (newpassword.Length < 6)
                        Res.result = "Пароль должен быть минимум 6 символов!";
                    else
                        Res.result = "Пароли не совпадают!";
                }
            }
            else
            {
                Res.result = "Неверный пароль!";
            }
            Res.account = User;
            return Res;
        }

        public bool UploadFile(IFormFile ufile)
        {
            if (ufile != null && ufile.Length > 0)
            {
                var fileName = Path.GetFileName(ufile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\avatars", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ufile.CopyTo(fileStream);
                }
                return true;
            }
            return false;
        }
        public LoginAnswer UpdateAvatar(Account User,IFormFile Avatar)
        {
            LoginAnswer Res = new LoginAnswer();
            Res.account = User;
            byte[] jpegMagic = new byte[] { 255, 216, 255, 224 };
            if (Avatar == null)
            {
                Res.result = "Файл не выбран!";
            }
            else
            {
                var fileStream = Avatar.OpenReadStream();
                byte[] bytes = new byte[4];
                fileStream.Read(bytes, 0, 4);
                if (Avatar.Length > 52428800)
                    Res.result = "Файл слишком большой!";
                else if (Avatar.ContentType != "image/jpeg")
                    Res.result = "Файл не является jpg!";
                else if (jpegMagic.SequenceEqual(bytes) == false)
                    Res.result = "Файл поврежден!";
                else
                {
                    Res.result = Avatar.FileName;
                    UploadFile(Avatar);
                    Account UpdateAccount = new Account();
                    UpdateAccount = _authModel.ReadAccount(User.login).Result;
                    Res.account.Avatar = "/avatars/" + Avatar.FileName;
                    UpdateAccount.Avatar = "/avatars/" + Avatar.FileName;
                    _authModel.UpdateAccount(UpdateAccount);
                }
            }
            return Res;
        }
    }
}
