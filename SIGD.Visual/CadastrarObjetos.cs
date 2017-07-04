using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIGD.Logica;
using SIGD.Modelo;

namespace SIGD.Visual
{
    public partial class CadastrarObjetos : Form
    {
        RelatorioLogica rLog = new RelatorioLogica(Properties.Settings.Default.StringConexao);
        Usuario usuario = null;
        public CadastrarObjetos(Usuario user)
        {
            InitializeComponent();
            usuario = user;
        }

        public void AtualizarGrid()
        {
            PropriedadeLogica pLog = new PropriedadeLogica(Properties.Settings.Default.StringConexao);
            try
            {
                foreach (Propriedade p in pLog.RecuperarTodos())
                {
                    dataGridView1.Rows.Add(p.Id, p.Nome, p.DescComodo,  "Editar", "Excluir");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex);
            }
        }
        private void CadastrarObjetos_Load(object sender, EventArgs e)
        {
            AtualizarGrid();

            ComodoLogica comodo = new ComodoLogica(Properties.Settings.Default.StringConexao);

            cbComodo.DataSource = comodo.RecuperarTodos();
            cbComodo.DisplayMember = "NomeComodo";
            cbComodo.ValueMember = "IdComodo";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (dataGridView1.Columns["Excluir"].Index))
            {
                PropriedadeLogica pLog = new PropriedadeLogica(Properties.Settings.Default.StringConexao);
                Propriedade prop = new Propriedade();

                // obtém a linha da célula selecionada
                DataGridViewRow linhaAtual = dataGridView1.CurrentRow;

                // Exibe o índice da linha atual
                int indice = linhaAtual.Index;

                //Adere à variável idProp o código do objeto selecionado
                int idProp = Convert.ToInt16(dataGridView1.Rows[indice].Cells[0].Value);

                //no fim, chama o método de update para que mude o estado_prop com as novas informações.
                pLog.ExcluirPropriedade(idProp);

                // Inserir ação na tabela relatório
                prop = pLog.RecuperarPropriedade(idProp);
                string descRelatorio = usuario.Login + " excluiu propriedade " + prop.Nome;
                rLog.InserirRelatorio(descRelatorio, usuario.Id);

                //limpa o gridview
                dataGridView1.Rows.Clear();

                //chama o método atualizargrid que atualiza as informações do grid
                AtualizarGrid();
            }

            else if (e.ColumnIndex == (dataGridView1.Columns["Editar"].Index))
            {

                PropriedadeLogica pLog = new PropriedadeLogica(Properties.Settings.Default.StringConexao);

                // obtém a linha da célula selecionada
                DataGridViewRow linhaAtual = dataGridView1.CurrentRow;

                // Exibe o índice da linha atual
                int indice = linhaAtual.Index;

                //Adere à variável idProp o código do objeto selecionado
                int idProp = Convert.ToInt16(dataGridView1.Rows[indice].Cells[0].Value);

                EditarObjeto editobj = new EditarObjeto(usuario,pLog.RecuperarPropriedade(idProp));
                editobj.Show();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Propriedade p = new Propriedade();
            PropriedadeLogica pLog = new PropriedadeLogica(Properties.Settings.Default.StringConexao);

            p.Consumo = Convert.ToInt32(txtConsumo.Text);
            p.DataImplementacao = DateTime.Today;
            p.Nome = txtNome.Text;
            p.Potencia = Convert.ToInt32(txtPotencia.Text);
            if (cbEstado.Text == "Ligado")
            {
                p.Status = 1;
            }
            else
            {
                p.Status = 0;
            }


            try
            {
                pLog.InserirPropriedade(p);

                // Inserir ação na tabela relatório
                string descRelatorio = usuario.Login + " inseriu propriedade " + p.Nome;
                rLog.InserirRelatorio(descRelatorio, usuario.Id);

                MessageBox.Show("Propriedade cadastrada com sucesso!");
                dataGridView1.Rows.Clear();
                AtualizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex);
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
