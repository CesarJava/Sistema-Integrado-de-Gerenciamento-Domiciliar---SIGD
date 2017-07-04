using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGD.Modelo
{
    public class Comodo
    {
        int _idComodo;
        string _nomeComodo;

        public int IdComodo
        {
            get { return _idComodo; }
            set { _idComodo = value; }
        }
      

        public string NomeComodo
        {
            get { return _nomeComodo; }
            set { _nomeComodo = value; }
        }

    }
}
