using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIGD.Modelo;

namespace SIGD.DAO
{
    public class UsuarioDAO
    {
        Conexao conexao = null;
        public UsuarioDAO(string StringDeConexao)
        {
            conexao = new Conexao(StringDeConexao);

        }

        /// <summary>
        /// Método Para Inserir um Usuario
        /// </summary>
        /// <param name="Usuario"> Entre com um Usuario</param>
        public void InserirUsuario(Usuario Usuario)
        {
            string query = "";
            try
            {
                query += "Insert into tb_usuario values(";
                query += "0,'" + Usuario.Login + "'," +
                    "'" + Usuario.Senha + "'," +
                    "'" + Usuario.Nome + "'," +
                    "'" + Usuario.Sexo + "'," +
                    "'" + Usuario.DataNasc.ToString(" yyyy-MM-dd") + "'," +
                    "'" + Usuario.Email +
                    "')";
                conexao.ExecutarSemRetorno(query);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
        /// <summary>
        /// Método para recuperar todos os Usuários.
        /// </summary>
        /// <returns>Retorna uma Lista de Usuarios. </returns>
        public List<Usuario> SelecionarTodosUsuarios()
        {
            List<Usuario> listausuarios = new List<Usuario>();
            string query = "select * from tb_usuario";
            try
            {
                foreach (DataRow dr in conexao.ExcutarComRetorno(query).AsEnumerable())
                {
                    Usuario user = new Usuario();
                    user.Id = Convert.ToInt32(dr["id_usuario"]);
                    user.Login = dr["login_usuario"].ToString();
                    user.Nome = dr["nome_usuario"].ToString();
                    user.Sexo = dr["sexo_usuario"].ToString();
                    user.Email = dr["email_usuario"].ToString();
                    user.DataNasc = DateTime.Parse(dr["dataNasc_usuario"].ToString());
                    user.Senha = dr["senha_usuario"].ToString();
                    listausuarios.Add(user);
                }
                return listausuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Método que deleta um usuario do banco de dados.
        /// </summary>
        /// <param name="usuario">Usuario a ser deletado.</param>
        public void DeletarUsuario(int idUser)
        {
            string query = "delete from tb_usuario where ID_usuario = " +
                            idUser;

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
        /// Método que altera um usuario no banco de dados.
        /// </summary>
        /// <param name="Usuario">Usuario a ser alterado.</param>
        public void AlterarUsuario(Usuario Usuario)
        {
            string query = "update tb_usuario set " +
                    "login_usuario='" + Usuario.Login + "'," +
                    "senha_usuario='" + Usuario.Senha + "'," +
                    "nome_usuario='" + Usuario.Nome + "'," +
                    "datanasc_usuario='" + Usuario.DataNasc.ToString(" yyyy-MM-dd") + "'," +
                    "sexo_usuario='" + Usuario.Sexo + "'," +
                    "email_usuario='" + Usuario.Email + "'";
                    
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
