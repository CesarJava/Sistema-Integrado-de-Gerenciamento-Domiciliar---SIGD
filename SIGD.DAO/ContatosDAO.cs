using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIGD.Modelo;

namespace SIGD.DAO
{
    public class ContatosDAO
    {
        Conexao conexao;

        public ContatosDAO(string StringConexao)
        {
            conexao = new Conexao(StringConexao);
        }
        /// <summary>
        /// Metodo para Inserir um novo Contato.
        /// </summary>
        /// <param name="contato">Entre com um Contato.</param>
        public void InserirContato(Contatos contato)
        {
            string query = "";
            try
            {
                query += "insert into tb_contato values(0," + contato.IdUsuario + ",'" +
                        contato.Nome + "'," +
                        "'" + contato.Cep + "'," +
                        "" + contato.NumEnd + "," +
                        "'" + contato.Tel + "'," +
                        "'" + contato.Email + "'," +
                        "'" + contato.DataNasc.ToString("yyyy-MM-dd") + "')";
                conexao.ExecutarSemRetorno(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// Metodo para recuperar todos os contatos do usuário logado.
        /// </summary>
        /// <returns> Uma lista de Contatos</returns>
        public List<Contatos> SelecionarTodosOsContatos(int idUsuario)
        {
            string query = "select * from tb_contato where id_usuario =" + idUsuario;
            List<Contatos> listadecontatos = new List<Contatos>();
            try
            {
                foreach (DataRow dr in conexao.ExcutarComRetorno(query).AsEnumerable())
                {
                    Contatos contato = new Contatos();
                    contato.Id = Convert.ToInt32(dr["id_contato"]);
                    contato.IdUsuario = Convert.ToInt32(dr["id_usuario"]);
                    contato.Nome = dr["Nome_contato"].ToString();
                    contato.Cep = dr["CEP_contato"].ToString();
                    contato.NumEnd = Convert.ToInt32(dr["Num_contato"]);
                    contato.Tel = dr["tel_contato"].ToString();
                    contato.DataNasc = DateTime.Parse(dr["DataNasc_contato"].ToString());
                    contato.Email = dr["Email_contato"].ToString();

                    listadecontatos.Add(contato);
                }
                return listadecontatos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// Metodo para recuperar todos os contatos do Usuário.
        /// </summary>
        /// <returns> Uma lista de Contatos</returns>
        public List<Contatos> SelecionarContatosPorLetra(string Letra, int idUsuario)
        {
            string query = "select * from tb_contato where nome_contato like '" + Letra + "%" + "' and id_usuario =" + idUsuario;
            List<Contatos> listadecontatos = new List<Contatos>();
            try
            {
                foreach (DataRow dr in conexao.ExcutarComRetorno(query).AsEnumerable())
                {
                    Contatos contato = new Contatos();
                    contato.Id = Convert.ToInt32(dr["id_contato"]);
                    contato.IdUsuario = Convert.ToInt32(dr["id_usuario"]);
                    contato.Nome = dr["Nome_contato"].ToString();
                    contato.Cep = dr["CEP_contato"].ToString();
                    contato.NumEnd = Convert.ToInt32(dr["Num_contato"]);
                    contato.Tel = dr["tel_contato"].ToString();
                    contato.DataNasc = DateTime.Parse(dr["DataNasc_contato"].ToString());
                    contato.Email = dr["Email_contato"].ToString();
                    listadecontatos.Add(contato);
                }
                return listadecontatos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Método para Deletar um Contato do Banco de Dados.
        /// </summary>
        /// <param name="contato">Objeto contato a ser deletado.</param>
        public void DeletarContato(int idContato)
        {
            string query = "delete from tb_contato where ID_contato = " +
                            idContato;

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
        /// Método que altera os dados de um contato.
        /// </summary>
        /// <param name="contato">Int idcontato a ser alterado.</param>
        public void AlterarContato(Contatos contato)
        {
            string query = "update tb_contato set " +
                        "Nome_contato='" + contato.Nome + "'," +
                        "CEP_contato= '" + contato.Cep + "'," +
                        "Num_contato=" + contato.NumEnd + "," +
                        "Tel_contato='" + contato.Tel + "'," +
                        "DataNasc_contato='" + contato.DataNasc.ToString("yyyy-MM-dd") + "' ," + 
                        "Email_contato='" + contato.Email +
                        "' where id_contato=" + contato.Id;
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
