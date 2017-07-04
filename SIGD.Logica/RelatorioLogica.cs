using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGD.Modelo;
using SIGD.DAO;

namespace SIGD.Logica
{
    public class RelatorioLogica
    {
        RelatorioDAO dao = null;

        public RelatorioLogica(string ConnectionString)
        {
            dao = new RelatorioDAO(ConnectionString);
        }

        public void InserirRelatorio(string descRelatorio, int idUser)
        {
            Relatorio relatorio = new Relatorio();
            relatorio.DataHora = DateTime.Now;
            relatorio.DescRelatorio = descRelatorio;
            relatorio.IdUser = idUser;


            dao.InserirRelatotio(relatorio);

        }

        public List<Relatorio> RecuperarTodos()
        {
            return dao.SelecionarTodosRelatorios();
        }

        public List<Relatorio> RecuperarRelatorio(DateTime data)
        {
            var consulta = from r in this.RecuperarTodos()
                           where (DateTime.Equals(r.DataHora, data))
                           select r;

            return consulta.ToList<Relatorio>();
        }
    }
}
