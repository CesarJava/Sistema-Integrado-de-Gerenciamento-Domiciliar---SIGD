using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGD.DAO;
using SIGD.Modelo;

namespace SIGD.Logica
{
    public class ContatosLogica
    {
        ContatosDAO dao = null;
        public ContatosLogica(string ConnectionString)
        {
            dao = new ContatosDAO(ConnectionString);
        }

        public void InserirContato(Contatos Cont)
        {
            dao.InserirContato(Cont);
        }

        public List<Contatos> RecuperarTodos(int idUsuario)
        {
            return dao.SelecionarTodosOsContatos(idUsuario);
        }

        public Contatos RecuperarContato(int idContato, int idUsuario)
        {
            var consulta = from c in this.RecuperarTodos(idUsuario)
                           where c.Id == idContato
                           select c;

            return consulta.First<Contatos>();
        }


        public List<Contatos> RecuperarPorLetra(string Letra, int idUsuario)
        {
                return dao.SelecionarContatosPorLetra(Letra, idUsuario);
            
        }

        public void ExcluirContato(int idContato)
        {
            dao.DeletarContato(idContato);
        }

        public void AlterarContato(Contatos cont)
        {
            dao.AlterarContato(cont);
        }
    }
}
