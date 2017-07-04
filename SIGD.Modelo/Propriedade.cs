using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGD.Modelo
{
     public class Propriedade
    {
        
        private int _id;
        private string _nome;
        private int _idComodo;
        private int _status;
        private double _potencia;
        private double _consumo;
        private DateTime _dataImplementacao;
        private string _descStatus;
        private string _descComodo;

        public string DescComodo
        {
            get { return _descComodo; }
            set { _descComodo = value; }
        }

        public string DescStatus
        {
            get { return _descStatus; }
            set { _descStatus = value; }
        }


        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int IdComodo
        {
            get { return _idComodo; }
            set { _idComodo = value; }
        }

              

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

       


        public double Potencia
        {
            get { return _potencia; }
            set { _potencia = value; }
        }

        

        public double Consumo
        {
            get { return _consumo; }
            set { _consumo = value; }
        }

      

        public DateTime DataImplementacao
        {
            get { return _dataImplementacao; }
            set { _dataImplementacao = value; }
        }

    }
}
