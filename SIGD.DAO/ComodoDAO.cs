using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGD.Modelo;
using System.Data;

namespace SIGD.DAO
{
    public class ComodoDAO
    {

         Conexao conexao;

        public ComodoDAO(string StringConexao)
        {
            conexao = new Conexao(StringConexao);
        }

        /// <summary>
        /// Metodo para Inserir um novo Comodo.
        /// </summary>
        /// <param name="comodo">Entre com um Comodo.</param>
        public void InserirComodo(Comodo comodo)
        {
            string query = "";
            try
            {
                query += "insert into tb_comodo values(0,'" + comodo.NomeComodo + "')";
                conexao.ExecutarSemRetorno(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

       

        /// <summary>
        /// Metodo para recuperar todos os comodos da casa
        /// </summary>
        /// <returns> Uma lista de Comodos</returns>
        public List<Comodo> SelecionarTodosOsComodos()
        {
            string query = "select * from tb_comodo";
            List<Comodo> listadecomodos = new List<Comodo>();
            try
            {
                foreach (DataRow dr in conexao.ExcutarComRetorno(query).AsEnumerable())
                {
                    Comodo comodo = new Comodo();
                    comodo.IdComodo = Convert.ToInt32(dr["id_cmd"]);
                    comodo.NomeComodo = dr["nome_cmd"].ToString();
                    
                    listadecomodos.Add(comodo);
                }
                return listadecomodos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// Método para Deletar um Comodo do Banco de Dados.
        /// </summary>
        /// <param name="contato">Int idComodo a ser deletado.</param>
        public void DeletarComodo(int idComodo)
        {
            string query = "delete from tb_comodo where ID_cmd = " +
                            idComodo;

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
