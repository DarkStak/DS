using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Managers
{
    public class Generator : IGenerator
    {
        public string emailGenerator()
        {
            int counter = 0;
            int[] array = new int[62];
            for (int iter = 48; iter < 58; iter++)
            {
                array[counter] = iter;
                counter++;
            }
            for (int iter = 65; iter < 91; iter++)
            {
                array[counter] = iter;
                counter++;
            }
            for (int iter = 97; iter < 123; iter++)
            {
                array[counter] = iter;
                counter++;
            }

            var Randomer = new Random();

            char[] main = new char[10];
            for (int iter = 0; iter < 10; iter++)
                main[iter] = (char)array[Randomer.Next(62)];
            return new string(main) + "@mail.ru";
        }
        public string passwordGenerator()
        {
            int counter = 0;
            int[] array = new int[62];
            for (int iter = 48; iter < 58; iter++)
            {
                array[counter] = iter;
                counter++;
            }
            for (int iter = 65; iter < 91; iter++)
            {
                array[counter] = iter;
                counter++;
            }
            for (int iter = 97; iter < 123; iter++)
            {
                array[counter] = iter;
                counter++;
            }

            var Randomer = new Random();

            char[] main = new char[10];
            for (int iter = 0; iter < 10; iter++)
                main[iter] = (char)array[Randomer.Next(62)];
            return new string(main);
        }
        public string keyGenerator()
        {
            int counter = 0;
            int[] array = new int[62];
            for (int iter = 48; iter < 58; iter++)
            {
                array[counter] = iter;
                counter++;
            }
            for (int iter = 65; iter < 91; iter++)
            {
                array[counter] = iter;
                counter++;
            }
            for (int iter = 97; iter < 123; iter++)
            {
                array[counter] = iter;
                counter++;
            }
            char[] part1 = new char[4];
            char[] part2 = new char[4];
            char[] part3 = new char[4];
            char[] part4 = new char[4];
            var Randomer = new Random();
            for (int iter = 0; iter < 4; iter++)
            {
                part1[iter] = (char)array[Randomer.Next(62)];
                part2[iter] = (char)array[Randomer.Next(62)];
                part3[iter] = (char)array[Randomer.Next(62)];
                part4[iter] = (char)array[Randomer.Next(62)];
            }
            return new string(part1) + "-" + new string(part2) + "-" + new string(part3) + "-" + new string(part4);
        }
        public string urlGenerator()
        {
            int counter = 0;
            int[] array = new int[62];
            for (int iter = 48; iter < 58; iter++)
            {
                array[counter] = iter;
                counter++;
            }
            for (int iter = 65; iter < 91; iter++)
            {
                array[counter] = iter;
                counter++;
            }
            for (int iter = 97; iter < 123; iter++)
            {
                array[counter] = iter;
                counter++;
            }
            char[] server = new char[10];
            var Randomer = new Random();
            for (int iter = 0; iter < 10; iter++)
                server[iter] = (char)array[Randomer.Next(62)];
            return "https://" + new string(server) + ".com/";
        }
        public string CookieGenerator()
        {
            int counter = 0;
            int[] array = new int[62];
            for (int iter = 48; iter < 58; iter++)
            {
                array[counter] = iter;
                counter++;
            }
            for (int iter = 65; iter < 91; iter++)
            {
                array[counter] = iter;
                counter++;
            }
            for (int iter = 97; iter < 123; iter++)
            {
                array[counter] = iter;
                counter++;
            }
            char[] session = new char[256];
            var Randomer = new Random();
            for (int iter = 0; iter < 256; iter++)
                session[iter] = (char)array[Randomer.Next(62)];
            return new string(session);
        }
    }
}
