using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGD.Modelo;
using SIGD.DAO;

namespace SIGD.Logica
{
    public class UsuarioLogica
    {
        UsuarioDAO dao = null;
        public UsuarioLogica(string StringConexao)
        {
            dao = new UsuarioDAO(StringConexao);
        }

        /// <summary>
        /// Método para sortear números aleatoriamente, sem repetir
        /// </summary>
        /// <param name="Min">Menor número permitido no sorteio</param>
        /// <param name="Max">Maior número permitido no sorteio</param>
        /// <param name="QtdSort">Quantidade de números a ser sorteados</param>
        /// <returns>Array de int contendo os números dispostos nos índices</returns>
        public int[] SortNum(int Min, int Max, int QtdSort)
        {
            Random rdm = new Random();
            int[] alt = new int[QtdSort];
            int[] JaFoi = new int[QtdSort];

            alt[0] = rdm.Next(Min, Max + 1);
            JaFoi[0] = alt[0];

            for (int alea = 1; alea < QtdSort; alea++)
            {
                alt[alea] = rdm.Next(Min, Max + 1);
                bool para = false, sortearNovamente = false;
                while (para == false)
                {
                    sortearNovamente = false;
                    for (int i = 0; i < QtdSort; i++)
                    {
                        if (alt[alea] == JaFoi[i])
                        {
                            sortearNovamente = true;
                            break;
                        }
                    }

                    if (sortearNovamente)
                        alt[alea] = rdm.Next(Min, Max + 1);
                    else
                        para = true;
                }
                JaFoi[alea] = alt[alea];
            }
            return alt;
        }

        /// <summary>
        /// Método que valida a senha, verificando se há letras maiúsculas, minúsculas, números e símbolos
        /// </summary>
        /// <param name="senha">Senha a ser verificada</param>
        /// <returns>True, se a senha for válida e False, se não.</returns>
        bool ValidaSenha(string senha)
        {
            if (senha.Length < 8)
                return false;

            bool[] testes = new bool[4];
            foreach (char c in senha)
            {
                //letras maiúsculas
                if ((int)c >= 65 && (int)c <= 90)
                    testes[0] = true;

                //letras minúsculas
                else if ((int)c >= 97 && (int)c <= 122)
                    testes[1] = true;

                //números
                else if (((int)c >= 48) && ((int)c <= 57))
                    testes[2] = true;

                //símbolos
                else if ((((int)c >= 32) && ((int)c <= 47)) ||
                        (((int)c >= 58) && ((int)c <= 64)) ||
                        (((int)c >= 91) && ((int)c <= 96)) ||
                        (((int)c >= 121) && ((int)c <= 126)))
                    testes[3] = true;
            }

            for (int i = 0; i < 4; i++)
            {
                if (testes[i] != true)
                    return false;
            }

            return true;
        }

        public void InserirUsuario(Usuario user)
        {
            if (!string.IsNullOrEmpty(user.Nome))
                if (!string.IsNullOrEmpty(user.Login))
                {
                    dao.InserirUsuario(user);

                }
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados no banco
        /// </summary>
        /// <returns></returns>
        public List<Usuario> RecuperarTodos()
        {
            return dao.SelecionarTodosUsuarios();
        }

        /// <summary>
        /// Retorna um usuário específico
        /// </summary>
        /// <param name="Id">Identificador do usuário</param>
        /// <returns>O usuário representado pelo ID informado</returns>
        public Usuario RecuperarUsuario(int Id)
        {
            var consulta = (from u in this.RecuperarTodos()
                            where u.Id == Id
                            select u);

            if (consulta.ToList<Usuario>() != null)
            {
                return consulta.First<Usuario>();
            }
            else
                throw new Exception("Usuário inexistente");
        }

        public Usuario RecuperarUsuario(string Login)
        {
            var consulta = (from u in this.RecuperarTodos()
                            where u.Login.ToLower() == Login.ToLower()
                            select u);

            if (consulta.ToList<Usuario>() != null)
            {
                return consulta.First<Usuario>();
            }
            else
                throw new Exception("Usuário inexistente");
        }

        /// <summary>
        /// Valida o login e senha informados, correlacionando ao banco
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="senha">Senha</param>
        /// <returns>True, se usuário e senha estiverem corretos</returns>
        public bool Login(string login, string senha)
        {
            if (this.RecuperarUsuario(login).Senha == senha)
            {
                return true;
            }
            throw new Exception("Senha incorreta");
        }


        /// <summary>
        /// Pesquisa logins existentes no banco
        /// </summary>
        /// <param name="login">Login</param>
        /// <returns>True, se login já existir</returns>
        public bool VerificarLogin(string login)
        {
            int contador = 0;
            foreach (Usuario user in dao.SelecionarTodosUsuarios())
            {
                if (user.Login.ToLower() == login.ToLower())
                {
                    contador += 1;
                }
            }

            if (contador != 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }


        /// <summary>
        /// Exclui o usuário do banco de dados
        /// </summary>
        /// <param name="IdUsuario">idUsuário</param>
        
        public void ExcluirUsuario(int idUser)
        {
            dao.DeletarUsuario(idUser);
        }



        /// <summary>
        /// Altera as informações do usuário no banco de dados
        /// </summary>
        /// <param name="IdUsuario">Objeto Usuario</param>

        public void AlterarUsuario(Usuario user)
        {
            dao.AlterarUsuario(user);
        }

    }
}
