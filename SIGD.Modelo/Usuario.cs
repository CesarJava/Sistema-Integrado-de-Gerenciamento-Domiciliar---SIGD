using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGD.Modelo
{
    public class Usuario
    {
        private int _id;
        private string _login;
        private string _nome;
        private DateTime _dataNasc;
        private string _sexo;
        private string _email;
        private string _senha;

        public string Senha
        {
            get { return _senha; }
            set { _senha = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public DateTime DataNasc
        {
            get { return _dataNasc; }
            set { _dataNasc = value; }
        }

        public string Sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }

        
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        

    }
}
