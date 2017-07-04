using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGD.DAO;
using SIGD.Modelo;

namespace SIGD.Logica
{
    public class TarefaLogica
    {
        TarefaDAO dao = null;
        public TarefaLogica(string ConnectionString)
        {
            dao = new TarefaDAO(ConnectionString);
        }

        public void InserirTarefa(Tarefa tarefa)
        {
            
            if (tarefa.DataHora < DateTime.Now)
            {
                throw new Exception("Não é possível agendar tarefas para datas anteriores ao dia atual");
            }

            else
            {
                dao.InserirTarefa(tarefa);
            }
        
        }

        public List<Tarefa> RecuperarTodos()
        {
            return dao.SelecionarTodasTarefas();
        }

        public Tarefa RecuperarTarefa(int Id)
        {
            var consulta = (from t in dao.SelecionarTodasTarefas()
                            where t.Id == Id
                            select t).First<Tarefa>();

            return consulta;
        }


        public List<Tarefa> PesquisarTarefa(int mes, int ano)
        {
            try
            {
                var consulta = from t in dao.SelecionarTodasTarefas()
                               where t.DataHora.Month == mes && t.DataHora.Year == ano
                               select t;
                return consulta.ToList<Tarefa>();
                
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }


        public void DeletarTarefa(int Id)
        {
            try
            {
                dao.DeletarTarefa(Id);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void AlterarTarefa(Tarefa tar)
        {
            dao.AlterarTarefa(tar);
        }
        
    }
}
