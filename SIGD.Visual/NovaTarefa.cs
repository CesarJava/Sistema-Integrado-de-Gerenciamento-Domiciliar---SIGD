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
    public partial class NovaTarefa : Form
    {
        Usuario usuario = null;
        RelatorioLogica rLog = new RelatorioLogica(Properties.Settings.Default.StringConexao);
        public NovaTarefa(Usuario user)
        {
            InitializeComponent();
            usuario = user;
        }


      


        private void NovaTarefa_Load(object sender, EventArgs e)
        {
            PropriedadeLogica prop = new PropriedadeLogica (Properties.Settings.Default.StringConexao);

            cbPropriedade.DataSource = prop.RecuperarTodos();
            cbPropriedade.DisplayMember = "Nome";
            cbPropriedade.ValueMember = "Id";

            cbAcao.SelectedIndex = 0;

            int anoatual = DateTime.Now.Year;
            
            List<string> ano = new List<string>();

            for (int i = 0; i < 5; i++)
            {
                int anoi = anoatual + i;
                ano.Add(anoi.ToString());
          
            }

            cbAno.DataSource = ano;

            dataGridView1.ForeColor = System.Drawing.Color.Gray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PropriedadeLogica proplog = new PropriedadeLogica(Properties.Settings.Default.StringConexao);
            Propriedade prop = new Propriedade();
            TarefaLogica tarlog = new TarefaLogica(Properties.Settings.Default.StringConexao);
            Tarefa tar = new Tarefa();

            
            int estado;
            int id_prop = Convert.ToInt16(cbPropriedade.SelectedValue.ToString());
            prop.Id = id_prop;
            
            string data_tmp = txtData.Text + " " + txtHora.Text;
            tar.IdProp = Convert.ToInt16(cbPropriedade.SelectedValue);
            tar.Acao = cbAcao.Text;

           
            if (cbAcao.SelectedItem.ToString() != "Ligar"  && cbAcao.SelectedItem.ToString() != "Desligar")
            {

                MessageBox.Show("Escolha uma ação.");
            }

            else{

                if(tar.Acao == "Ligar")
                {
                    estado = 1;
                }
                else
                {
                    estado = 0;
                }

                    tar.DataHora = DateTime.Parse(data_tmp);
                    try
                    {
                        proplog.UpdateEstado(id_prop, estado);
                        tarlog.InserirTarefa(tar);

                        // Inserir ação na tabela relatório
                        string descRelatorio = usuario.Login + " inseriu tarefa " + tar.Acao + " " + cbPropriedade.Text+ " para dia " + tar.DataHora;
                        rLog.InserirRelatorio(descRelatorio, usuario.Id);
                        
                        MessageBox.Show("Nova Tarefa Agendada!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex);
                    }
            }

           

            


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TarefaLogica tarlog = new TarefaLogica(Properties.Settings.Default.StringConexao);
           


            //limpa datagridview
            dataGridView1.Rows.Clear();

            //soma mais um, porque o índice do combox começa do zero
            int mes = cbMes.SelectedIndex + 1;
            int ano = Convert.ToInt32(cbAno.Text);
           
            try
            {
               
                    foreach (Tarefa t in tarlog.PesquisarTarefa(mes, ano))
                    {
                        dataGridView1.Rows.Add(t.Id, t.DescProp, t.Acao, t.DataHora, "Editar", "Excluir");
                    }
                

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex);
            }


        

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                TarefaLogica tLog = new TarefaLogica(Properties.Settings.Default.StringConexao);
                Tarefa tar = new Tarefa();

                // obtém a linha da célula selecionada
                DataGridViewRow linhaAtual = dataGridView1.CurrentRow;

                // Exibe o índice da linha atual
                int indice = linhaAtual.Index;
                //Adere à variável idProp o código do objeto selecionado
                int idTar = Convert.ToInt32(dataGridView1.Rows[indice].Cells["Codigo"].Value);


            if (e.ColumnIndex == (dataGridView1.Columns["Excluir"].Index))
            {
                // Inserir ação na tabela relatório
                tar = tLog.RecuperarTarefa(idTar);
                string descRelatorio = usuario.Login + " excluiu tarefa " + tar.Acao + " " + tar.DescProp + " do dia " + tar.DataHora;
                rLog.InserirRelatorio(descRelatorio, usuario.Id);

               
                //no fim, chama o método de delete para que exclua essa informação do banco.
                tLog.DeletarTarefa(idTar);

                
                //limpa o gridview
                dataGridView1.Rows.Clear();

                //preenche novamente
                try
                {

                    foreach (Tarefa t in tLog.RecuperarTodos())
                    {
                        dataGridView1.Rows.Add(t.Id, t.DescProp, t.Acao, t.DataHora, "Editar", "Excluir");
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex);
                }

                //Atualiza o grid
                int mes = cbMes.SelectedIndex + 1;
                int ano = Convert.ToInt16(cbAno.Text);

                if (mes != 0 || ano != 0)
                {
                    try
                    {
                        foreach (Tarefa t in tLog.PesquisarTarefa(mes, ano))
                        {
                            dataGridView1.Rows.Add(t.Id, t.DescProp, t.Acao, t.DataHora, "Editar", "Excluir");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex);
                    }
                }
                else
                {
                    MessageBox.Show("Preencha os campos de pesquisa");
                }
            }

            else if (e.ColumnIndex == dataGridView1.Columns["Editar"].Index)
            {
                if (dataGridView1.Rows[indice].Cells["Editar"].Value.ToString() == "Editar")
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells["Editar"].Value = "Salvar";
                    }
                    dataGridView1.ForeColor = System.Drawing.Color.Black;
                    dataGridView1.ReadOnly = false;

                    


                }


                else
                {
                    tar = tLog.RecuperarTarefa(idTar);
                    tar.Acao = dataGridView1.Rows[indice].Cells["Acao"].Value.ToString();
                    tar.DataHora = DateTime.Parse(dataGridView1.Rows[indice].Cells["Data"].Value.ToString());
                    tar.DescProp = dataGridView1.Rows[indice].Cells["Objeto"].Value.ToString();

                    tLog.AlterarTarefa(tar);

                    // Inserir ação na tabela relatório
                    string descRelatorio = usuario.Login + " alterou tarefa " + tar.Acao + " " + tar.DescProp + " para o dia " + tar.DataHora;
                    rLog.InserirRelatorio(descRelatorio, usuario.Id);


                        dataGridView1.ReadOnly = true;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            dataGridView1.Rows[i].Cells["Editar"].Value = "Editar";
                        }
                        dataGridView1.ForeColor = System.Drawing.Color.Gray;
                    
                }

               
               

               
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            TarefaLogica tLog = new TarefaLogica(Properties.Settings.Default.StringConexao);

            dataGridView1.Rows.Clear();
            try
            {

                foreach (Tarefa t in tLog.RecuperarTodos())
                {
                    dataGridView1.Rows.Add(t.Id, t.DescProp, t.Acao, t.DataHora, "Editar", "Excluir");
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex);
            }

        }
    }
}
