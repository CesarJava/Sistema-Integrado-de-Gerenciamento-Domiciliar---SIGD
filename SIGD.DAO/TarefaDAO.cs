using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIGD.Modelo;

namespace SIGD.DAO
{
    public class TarefaDAO
    {
        Conexao conexao = null;
        public TarefaDAO(string StringDeConexao)
        {
            conexao = new Conexao(StringDeConexao);
        }

        /// <summary>
        /// Insere uma nova Tarefa a ser executada.
        /// </summary>
        /// <param name="tarefa">Tarefa a ser Inserida. </param>
        public void InserirTarefa(Tarefa tarefa)
        {
            string query = "";
            query += "Insert into tb_tarefa values (" +
                "0," + tarefa.IdProp + ",'" + tarefa.Acao + "'," +
                "'" + tarefa.DataHora.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            conexao.ExecutarSemRetorno(query);
        }

        /// <summary>
        /// Método para selecionar todos os registros do Banco de Dados.
        /// </summary>
        /// <returns>Retorna uma lista de Tarefas. </returns>
        public List<Tarefa> SelecionarTodasTarefas()
        {
            List<Tarefa> listadetarefas = new List<Tarefa>();
            string query = "select * from tb_tarefa t inner join tb_propriedade p on p.ID_prop = t.ID_prop";
            try
            {
                foreach (DataRow dr in conexao.ExcutarComRetorno(query).AsEnumerable())
                {
                    Tarefa tarefa = new Tarefa();

                    tarefa.Id = Convert.ToInt32(dr["id_tarefa"]);
                    tarefa.IdProp = Convert.ToInt16(dr["Id_prop"]);
                    tarefa.Acao = dr["Desc_tarefa"].ToString();
                    tarefa.DataHora = DateTime.Parse(dr["datahora_tarefa"].ToString());
                    tarefa.DescProp = dr["Nome_prop"].ToString();
                    listadetarefas.Add(tarefa);
                }
                return listadetarefas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        

        /// <summary>
        /// Método que deleta uma tarefa no banco de dados.
        /// </summary>
        /// <param name="tarefa">Tarefa a ser alterada no banco de dados.</param>
        public void DeletarTarefa(int idTarefa)
        {
            string query = "delete from tb_tarefa where ID_tarefa = " + idTarefa;

            try
            {
                conexao.ExecutarSemRetorno(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlterarTarefa(Tarefa tarefa)
        {
            string query = "update tb_tarefa set " +
                "Id_prop= " + tarefa.IdProp + "," +
                "Desc_tarefa='" + tarefa.Acao + "'," +
                "DataHora_tarefa='" + tarefa.DataHora.ToString("yyyy-MM-dd") + "'";
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
