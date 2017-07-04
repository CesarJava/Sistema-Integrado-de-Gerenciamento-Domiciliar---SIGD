using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIGD.Modelo;

namespace SIGD.DAO
{
    public class PropriedadeDAO
    {
        Conexao conexao = null;
        public PropriedadeDAO(string Stringdeconexao)
        {
            conexao = new Conexao(Stringdeconexao);
        }

        /// <summary>
        /// Método para Inserir uma nova Propriedade no Banco.
        /// </summary>
        /// <param name="Propriedade">Propriedade a ser Inserida no Banco </param>
        public void InserirPropriedade(Propriedade Propriedade)
        {
            string query = "";
            try
            {
                query += "Insert into tb_propriedade values(0,";
                query += "'" + Propriedade.Nome + "'," +
                    "" + Propriedade.IdComodo + "," +
                    "" + Propriedade.Status + "," +
                    "" + Propriedade.Potencia + "," +
                    "" + Propriedade.Consumo + "," +
                    "'" + Propriedade.DataImplementacao.ToString("yyyy-MM-dd") + "')";
                conexao.ExecutarSemRetorno(query);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }


        
        /// <summary>
        /// Método para Selecionar Todos as propriedades
        /// </summary>
        /// <returns>Lista de Propriedade a serem Tratadas.</returns>
        public List<Propriedade> SelecionarTodasPropriedades()
        {
            string query = "select * from tb_propriedade p inner join tb_comodo c on c.ID_cmd = p.ID_cmd";
            List<Propriedade> listadepropriedades = new List<Propriedade>();

            foreach (DataRow dr in conexao.ExcutarComRetorno(query).AsEnumerable())
            {
                Propriedade propriedade = new Propriedade();
                propriedade.Consumo = Convert.ToInt32(dr["consumo_prop"]);
                propriedade.DataImplementacao = DateTime.Parse(dr["DataImplementacao_prop"].ToString());
                propriedade.Id = Convert.ToInt32(dr["ID_prop"]);
                propriedade.IdComodo = Convert.ToInt32(dr["ID_cmd"]);
                propriedade.Nome = dr["nome_prop"].ToString();
                propriedade.Potencia = Convert.ToDouble(dr["potencia_prop"]);
                propriedade.Status = Convert.ToInt32(dr["status_prop"]);
                propriedade.DescComodo = dr["nome_cmd"].ToString();
             
                listadepropriedades.Add(propriedade);
            }
            return listadepropriedades;
        }

       

        public void UpdateEstado(int id_prop, int estado_prop)
        {
            string query = "";

            query += "UPDATE tb_propriedade SET status_prop = " + estado_prop +  " WHERE ID_prop = '" + id_prop + "'";
            conexao.ExecutarSemRetorno(query);
        }

        /// <summary>
        /// Método que altera uma propriedade do banco de dados.
        /// </summary>
        /// <param name="propriedade">Propriedade a ser alterada no banco de dados</param>
        public void AlterarPropriedade(Propriedade propriedade)
        {
            string query = "update tb_propriedade set " +
                    "Nome_prop='" + propriedade.Nome + "'," +
                    "id_cmd =" + propriedade.IdComodo + "," +
                    "status_prop =" + propriedade.Status + "," +
                    "Potencia_prop=" + propriedade.Potencia + "," +
                    "Consumo_prop=" + propriedade.Consumo + "," +
                    "DataImplementacao_prop='" + propriedade.DataImplementacao.ToString("yyyy-MM-dd") + 
                    "' where ID_prop= " + propriedade.Id;
            try
            {
                conexao.ExecutarSemRetorno(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Método que deleta uma propriedade do banco de dados.
        /// </summary>
        /// <param name="propriedade">Propriedade a ser deletada.</param>
        public void DeletarPropriedade(int idProp)
        {
            string query = "delete from tb_propriedade where id_prop= " +
                            idProp;

            try
            {
                conexao.ExecutarSemRetorno(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




       
    }
}
