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
    public partial class ContatosTela : Form
    {
        Usuario usuario;
        RelatorioLogica rLog = new RelatorioLogica(Properties.Settings.Default.StringConexao);
        public ContatosTela(Usuario user)
        {
            InitializeComponent();
            usuario = user;
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ContatosLogica cLog = new ContatosLogica(Properties.Settings.Default.StringConexao);
            Contatos con = new Contatos();
            con.IdUsuario = usuario.Id;
            con.Cep = txtCEP.Text;
            con.DataNasc = dtpDataNasc.Value.Date;
            con.Email = txtEmail.Text;
            con.Nome = txtNome.Text;
            con.NumEnd = Convert.ToInt32(txtNum.Text);
            con.Tel = txtTelefone.Text;


            try
            {
                cLog.InserirContato(con);

                // Inserir ação na tabela relatório
                string descRelatorio = usuario.Login + " inseriu contato " + con.Nome;
                rLog.InserirRelatorio(descRelatorio, usuario.Id);


                MessageBox.Show("Contato cadastrado com sucesso!");

            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex);
            }
            

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string Letra = cbLetra.Text;

            ContatosLogica cLog = new ContatosLogica(Properties.Settings.Default.StringConexao);
            
            try
            {

                if (cbLetra.Text != "")
                {
                    if (cLog.RecuperarPorLetra(Letra, usuario.Id).Count == 0)
                    {
                        MessageBox.Show("Não existem contatos cuja letra inicial é " + Letra);
                    }

                    else
                    {
                        foreach (Contatos c in cLog.RecuperarPorLetra(Letra, usuario.Id))
                        {
                            dataGridView1.Rows.Add(c.Id, c.Nome, c.Cep, c.NumEnd, c.Tel, c.Email, "Editar", "Excluir");
                        }
                    }
                }


                else
                {
                    cLog.RecuperarTodos(usuario.Id);
                    foreach (Contatos c in cLog.RecuperarTodos(usuario.Id))
                    {
                        dataGridView1.Rows.Add(c.Id, c.Nome, c.Cep, c.NumEnd, c.Tel, c.Email, "Editar", "Excluir");
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            ContatosLogica cLog = new ContatosLogica(Properties.Settings.Default.StringConexao);
            Contatos contato = new Contatos();

            // obtém a linha da célula selecionada
            DataGridViewRow linhaAtual = dataGridView1.CurrentRow;

            // Exibe o índice da linha atual
            int indice = linhaAtual.Index;

            //Adere à variável idProp o código do objeto selecionado
            int idContt = Convert.ToInt16(dataGridView1.Rows[indice].Cells[0].Value);
            if (e.ColumnIndex == (dataGridView1.Columns["Excluir"].Index))
            {
                try
                {
                    // Inserir ação na tabela relatório
                    contato = cLog.RecuperarContato(idContt, usuario.Id);
                    string descRelatorio = usuario.Login + " excluiu contato " + contato.Nome;
                    rLog.InserirRelatorio(descRelatorio, usuario.Id);

                    //no fim, chama o método de update para que mude o estado_prop com as novas informações.
                    cLog.ExcluirContato(idContt);

                    //limpa o gridview
                    dataGridView1.Rows.Clear();

                    //chama o método atualizargrid que atualiza as informações do grid
                    this.button2_Click(sender, e);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex);
                }
            }


            else if (e.ColumnIndex == (dataGridView1.Columns["Editar"].Index))
            {

                if (dataGridView1.Rows[indice].Cells["Editar"].Value.ToString() == "Editar")
                {
                    dataGridView1.ForeColor = System.Drawing.Color.Black;
                    dataGridView1.ReadOnly = false;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells["Editar"].Value = "Salvar";
                    }
                }


                else
                {
                    contato = cLog.RecuperarContato(idContt, usuario.Id);
                    
                    contato.Cep = dataGridView1.Rows[indice].Cells["CEP"].Value.ToString();
                    contato.Email = dataGridView1.Rows[indice].Cells["Email"].Value.ToString();
                    contato.Nome = dataGridView1.Rows[indice].Cells["Nome"].Value.ToString();
                    contato.NumEnd = Convert.ToInt32(dataGridView1.Rows[indice].Cells["Numero"].Value);
                    contato.Tel = dataGridView1.Rows[indice].Cells["Tel"].Value.ToString();


                    cLog.AlterarContato(contato);

                    // Inserir ação na tabela relatório
                    string descRelatorio = usuario.Login + " alterou contato " + contato.Nome;
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

        private void ContatosTela_Load(object sender, EventArgs e)
        {
            dataGridView1.ForeColor = System.Drawing.Color.Gray;
        }

        private void txtCEP_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            EnderecoLogica eLog = new EnderecoLogica(Properties.Settings.Default.StringConexao);
            Endereco ende = eLog.RecuperarEndereco(Convert.ToInt32(txtCEP.Text));
            if (ende != null)
            {
                txtBairro.Text = ende.NomeBairro;
                txtCidade.Text = ende.NomeCidade;
                txtRua.Text = ende.NomeLogradouro;
                txtUf.Text = ende.SiglaUf;
            
            }

            else
            {
                MessageBox.Show("Não foi possível encontrar endereço, digite o CEP novamente.");
            }

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
