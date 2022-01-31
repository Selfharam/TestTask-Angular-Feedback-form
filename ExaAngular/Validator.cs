using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExaAngular
{
    internal class Validator
    {
        public Validator()
        {

        }

        public bool checkPhone(string phone)
        {
            Regex regex = new Regex(@"^[7-8]{1}\d{3}\d{3}\d{2}\d{2}$");
            bool result = regex.IsMatch(phone);
            return result;
        }

        public bool checkName(string name)
        {
            Regex regex = new Regex(@"^[а-яА-Я]{0,20}$");
            bool result = regex.IsMatch(name);
            return result;
        }

        public bool checkEmail(string mail)
        {
            Regex regex = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$");
            bool result = regex.IsMatch(mail);
            return result;
        }
    }
}
