using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGD.Modelo
{
    public class Contatos
    {
        private int _id;
        private int _idUsuario;
        private string _nome;
        private string _cep;
        private int _numEnd;
        private string _tel;
        private DateTime _dataNasc;
        private string _email;
        
        public int IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public DateTime DataNasc
        {
            get { return _dataNasc; }
            set { _dataNasc = value; }
        }

        public string Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }

        public int NumEnd
        {
            get { return _numEnd; }
            set { _numEnd = value; }
        }

        public string Cep
        {
            get { return _cep; }
            set { _cep = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

    }
}
