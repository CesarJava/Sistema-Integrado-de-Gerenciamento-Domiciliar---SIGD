using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGD.Modelo
{
    public class Endereco
    {

        private int _idUf;

        public int IdUf
        {
            get { return _idUf; }
            set { _idUf = value; }
        }

        private string _siglaUf;

        public string SiglaUf
        {
            get { return _siglaUf; }
            set { _siglaUf = value; }
        }


        private string _nomeUf;

        public string NomeUf
        {
            get { return _nomeUf; }
            set { _nomeUf = value; }
        }
        
        private int _idCidade;

        public int IdCidade
        {
            get { return _idCidade; }
            set { _idCidade = value; }
        }
        
        private string _nomeCidade;

        public string NomeCidade
        {
            get { return _nomeCidade; }
            set { _nomeCidade = value; }
        }
        
        
        private int _idBairro;

        public int IdBairro
        {
            get { return _idBairro; }
            set { _idBairro = value; }
        }
        
        private string _nomeBairro;

        public string NomeBairro
        {
            get { return _nomeBairro; }
            set { _nomeBairro = value; }
        }
        
        private int _idLogradouro;

        public int IdLogradouro
        {
            get { return _idLogradouro; }
            set { _idLogradouro = value; }
        }
        
        private string _nomeLogradouro;

        public string NomeLogradouro
        {
            get { return _nomeLogradouro; }
            set { _nomeLogradouro = value; }
        }
        
        private int _cepLogradouro;

        public int CepLogradouro
        {
            get { return _cepLogradouro; }
            set { _cepLogradouro = value; }
        }







    }
}
