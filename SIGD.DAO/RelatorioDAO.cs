using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIGD.Modelo;

namespace SIGD.DAO
{
    public class RelatorioDAO
    {
        Conexao conexao = null;
        public RelatorioDAO(string StringDeConexao)
        {
            conexao = new Conexao(StringDeConexao);
        }
        /// <summary>
        /// Metodo para Inserir 
        /// </summary>
        /// <param name="relatorio">Relatorio</param>
        public void InserirRelatotio(Relatorio relatorio)
        {
            string query = "insert into tb_relatorio values(" +
                "0,"  + relatorio.IdUser + "," +
                "'" + relatorio.DescRelatorio + "'," +
                "'" + relatorio.DataHora.ToString("yyyy-MM-dd") + "')";

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
        /// Método que retorna todos os relatórios.
        /// </summary>
        /// <returns>Retorna os registros de relatórios do banco.</returns>
        public List<Relatorio> SelecionarTodosRelatorios()
        {
            string query = "select * from tb_relatorio";
            List<Relatorio> listaderelatorios = new List<Relatorio>();

            try
            {
                foreach (DataRow dr in conexao.ExcutarComRetorno(query).AsEnumerable())
                {
                    Relatorio relatorio = new Relatorio();
                    relatorio.Id = Convert.ToInt32(dr["ID_relat"]);
                    relatorio.IdUser = Convert.ToInt32(dr["ID_usuario"]);
                    relatorio.DataHora = DateTime.Parse(dr["DataHora_relat"].ToString());
                    relatorio.DescRelatorio = dr["desc_relat"].ToString();
                    listaderelatorios.Add(relatorio);
                }
                return listaderelatorios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return null;
            }


        }
    }
}
