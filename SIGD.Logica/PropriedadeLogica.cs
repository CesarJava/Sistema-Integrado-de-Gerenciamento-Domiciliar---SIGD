using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGD.DAO;
using SIGD.Modelo;
using System.Data;

namespace SIGD.Logica
{
    public class PropriedadeLogica
    {

        PropriedadeDAO dao = null;
        Propriedade prop = null;
        public PropriedadeLogica(string ConnectionString)
        {
            dao = new PropriedadeDAO(ConnectionString);
        }

        public void InserirPropriedade(Propriedade prop)
        {
            dao.InserirPropriedade(prop);
        }

        public void ExcluirPropriedade(int IdProp)
        {
            dao.DeletarPropriedade(IdProp);
        }


        public Propriedade RecuperarPropriedade(int Id)
        {
            var consulta = (from p in this.RecuperarTodos()
                            where p.Id == Id
                            select p).First<Propriedade>();
            return consulta;
        }

        public Propriedade RecuperarPropriedade(string Nome)
        {
            var consulta = (from p in this.RecuperarTodos()
                            where p.Nome == Nome
                            select p).First<Propriedade>();
            return consulta;
        }

        public List<Propriedade> RecuperarTodos()
        {
            List<Propriedade> lista = new List<Propriedade>();
            foreach (Propriedade p in dao.SelecionarTodasPropriedades())
            {
                if (p.Status == 1)
                    p.DescStatus = "Ligado";
                else
                    p.DescStatus = "Desligado";
                lista.Add(p);
            }


            return lista;



        }

        public void UpdateEstado(int id_prop, int estado_prop)
        {
            dao.UpdateEstado(id_prop, estado_prop);            

        }


        public void AlterarPropriedade(Propriedade prop)
        {
            dao.AlterarPropriedade(prop);
        }

      

       

        
        
    }
}
