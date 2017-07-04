using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGD.Modelo
{
   public class Relatorio
    {
       
        private int _idUser;     
        private int _id;
        private DateTime _dataHora;
        private string _descRelatorio;

        public string DescRelatorio
        {
            get { return _descRelatorio; }
            set { _descRelatorio = value; }
        }

      

        public DateTime DataHora
        {
            get { return _dataHora; }
            set { _dataHora = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

      

        public int IdUser
        {
            get { return _idUser; }
            set { _idUser = value; }
        }

    

    }
}
