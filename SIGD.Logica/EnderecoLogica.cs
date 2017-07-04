using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGD.Modelo;
using SIGD.DAO;

namespace SIGD.Logica
{
    public class EnderecoLogica
    {
        EnderecoDAO dao = null;
        public EnderecoLogica(string ConnectionString)
        {
            dao = new EnderecoDAO(ConnectionString);
        }

       
        public Endereco RecuperarEndereco(int CEP)
        {
            var consulta = (from p in dao.SelecionarPorCEP(CEP)
                            where p.CepLogradouro == CEP
                            select p).First<Endereco>();

            if (consulta != null)
            {
            return consulta;
            }

            else
            {
                return null;
            }
        }
    }
}
