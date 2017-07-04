using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIGD.Modelo;
using SIGD.Logica;

namespace SIGD.Visual
{
    public partial class EditarObjeto : Form
    {
        Usuario usuario = null;
        Propriedade prop = null;
        RelatorioLogica rLog = new RelatorioLogica(Properties.Settings.Default.StringConexao);
        public EditarObjeto(Usuario user, Propriedade pt)
        {
            InitializeComponent();
            usuario = user;
            prop = pt;
        }

        private void EditarObjeto_Load(object sender, EventArgs e)
        {
            ComodoLogica comodo = new ComodoLogica(Properties.Settings.Default.StringConexao);

            cbComodo.DataSource = comodo.RecuperarTodos();
            cbComodo.DisplayMember = "NomeComodo";
            cbComodo.ValueMember = "IdComodo";



            txtConsumo.Text = prop.Consumo.ToString();
            txtNome.Text = prop.Nome;
            txtPotencia.Text = prop.Potencia.ToString();

            
            

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PropriedadeLogica pLog = new PropriedadeLogica(Properties.Settings.Default.StringConexao);

            prop.IdComodo = Convert.ToInt32(cbComodo.SelectedValue.ToString());
            prop.Nome = txtNome.Text;
            prop.Potencia = Convert.ToInt32(txtPotencia.Text);

            if (cbEstado.Text == "Ligado")
                prop.Status = 1;
            
            else
                prop.Status = 0;
            pLog.AlterarPropriedade(prop);

            // Inserir ação na tabela relatório
            string descRelatorio = usuario.Login + " alterou propriedade " + prop.Nome;
            rLog.InserirRelatorio(descRelatorio, usuario.Id);

            

            MessageBox.Show("Objeto editado com sucesso!");

            this.Close();

        }
    }
}
