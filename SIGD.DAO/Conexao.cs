using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace SIGD.DAO
{
    public class Conexao
    {
         MySqlConnection mysqlConnection = null;

        public Conexao(string stringConexao)
        {
            mysqlConnection = new MySqlConnection(stringConexao);
        }

        public void ExecutarSemRetorno(string query)
        {
            if (mysqlConnection.State != System.Data.ConnectionState.Open)
            {
                mysqlConnection.Open();
            }

            MySqlCommand comando = new MySqlCommand(query, mysqlConnection);
            comando.ExecuteNonQuery();

            mysqlConnection.Close();
        }

        public DataTable ExcutarComRetorno(string query)
        {
            if (mysqlConnection.State != System.Data.ConnectionState.Open)
            {
                mysqlConnection.Open();
            }

            MySqlCommand comando = new MySqlCommand(query, mysqlConnection);
            MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();
            adaptador.Fill(ds);
            return ds.Tables[0];
        }
     
    

    }
}
