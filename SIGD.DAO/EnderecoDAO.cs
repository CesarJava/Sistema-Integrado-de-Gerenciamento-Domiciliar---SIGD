using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIGD.Modelo;

namespace SIGD.DAO
{
    public class EnderecoDAO
    {
        Conexao conexao = null;
        public EnderecoDAO(string StringDeConexao)
        {
            conexao = new Conexao(StringDeConexao);
        }


        /// <summary>
        /// Método para Selecionar o endereço de acordo com o CEP
        /// </summary>
        /// <returns>Lista de Endereço a serem Tratadas.</returns>
        public List<Endereco> SelecionarPorCEP(int CEP)
        {
            string query = "select * from uf u inner join cidades c"
                            + " on c.cd_uf = u.cd_uf"
                            + " inner join bairros b"
                            + " on b.cd_cidade = c.cd_cidade"
                            + " inner join logradouros l"
                            + " on l.cd_bairro = b.cd_bairro"
                            + " where l.no_logradouro_cep = " + CEP;

            List<Endereco> listadeenderecos = new List<Endereco>();
            try
            {
                
                    foreach (DataRow dr in conexao.ExcutarComRetorno(query).AsEnumerable())
                    {
                        Endereco endereco = new Endereco();
                        endereco.CepLogradouro = Convert.ToInt32(dr["no_logradouro_cep"]);
                        endereco.IdBairro = Convert.ToInt32(dr["cd_bairro"]);
                        endereco.IdCidade = Convert.ToInt32(dr["cd_cidade"]);
                        endereco.IdLogradouro = Convert.ToInt32(dr["cd_logradouro"]);
                        endereco.IdUf = Convert.ToInt32(dr["cd_uf"]);
                        endereco.NomeBairro = dr["ds_bairro_nome"].ToString();
                        endereco.NomeCidade = dr["ds_cidade_nome"].ToString();
                        endereco.NomeLogradouro = dr["ds_logradouro_nome"].ToString();
                        endereco.NomeUf = dr["ds_uf_nome"].ToString();
                        endereco.SiglaUf = dr["ds_uf_sigla"].ToString();


                        listadeenderecos.Add(endereco);
                    }
                    return listadeenderecos;
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        
    }
}
