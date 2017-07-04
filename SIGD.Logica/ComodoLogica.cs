using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGD.Modelo;
using SIGD.DAO;


namespace SIGD.Logica
{
    public class ComodoLogica
    {

        ComodoDAO dao = null;

        public ComodoLogica(string ConnectionString)
        {
            dao = new ComodoDAO(ConnectionString);
        }


        
        public void InserirComodo(Comodo com)
        {
            dao.InserirComodo(com);
        }

        public List<Comodo> RecuperarTodos()
        {
            return dao.SelecionarTodosOsComodos();
        }

        public void ExcluirComodo(int idComodo)
        {
            dao.DeletarComodo(idComodo);
        }
    }
}
