using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGD.Modelo
{
    public class Tarefa
    {
        private int _id;
        private int _idProp;
        private string _acao;
        private string _descProp;
        private DateTime _dataHora;



        public string DescProp
        {
            get { return _descProp; }
            set { _descProp = value; }
        }

        public string Acao
        {
        get { return _acao; }
        set { _acao = value; }
        }

        public int IdProp
        {
            get { return _idProp; }
            set { _idProp = value; }
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

    }
}
