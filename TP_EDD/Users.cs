using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_EDD
{
    public class Users
    {
        //Atributos usuario y password
        public string Usuario { get; set; }
        public string Password { get; set; }


        public Users(string usuario, string password) //Constructor
        {
            Usuario = usuario;
            Password = password;
        }

    }


}
