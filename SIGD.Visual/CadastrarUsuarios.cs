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
    public partial class CadastrarUsuarios : Form
    {
        Usuario usuario;
        RelatorioLogica rLog = new RelatorioLogica(Properties.Settings.Default.StringConexao);
        public CadastrarUsuarios(Usuario user)
        {
            InitializeComponent();
            usuario = user;
        }

        public void AtualizarGrid()
        {
            UsuarioLogica uLog = new UsuarioLogica(Properties.Settings.Default.StringConexao);
            dataGridView1.ForeColor = System.Drawing.Color.Gray;
            try
            {
                foreach (Usuario u in uLog.RecuperarTodos())
                {
                    dataGridView1.Rows.Add(u.Id, u.Nome,  u.Login, u.DataNasc, u.Email,  "Editar", "Excluir");
                }
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

        private void CadastrarUsuarios_Load(object sender, EventArgs e)
        {
            AtualizarGrid();
        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {
            UsuarioLogica uLog = new UsuarioLogica(Properties.Settings.Default.StringConexao);

            if (uLog.VerificarLogin(txtLogin.Text) == false)
            {
                lblLogin.Text = "Login Disponível.";
                lblLogin.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblLogin.Text = "Login Indisponível";
                lblLogin.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioLogica uLog = new UsuarioLogica(Properties.Settings.Default.StringConexao);
                Usuario user = new Usuario();

                user.DataNasc = dtpDataNasc.Value.Date;
                user.Email = txtEmail.Text;
                if (cbSexo.Text == "Feminino")
                {
                    user.Sexo = "F";
                }
                else
                {
                    user.Sexo = "M";
                }

                user.Nome = txtNome.Text;


                if (txtSenha1.Text == txtSenha2.Text)
                {
                    user.Senha = txtSenha1.Text;
                    if (uLog.VerificarLogin(txtLogin.Text) == false)
                    {
                        user.Login = txtLogin.Text;
                        uLog.InserirUsuario(user);

                        // Inserir ação na tabela relatório
                        string descRelatorio = usuario.Login + " inseriu usuário " + user.Login;
                        rLog.InserirRelatorio(descRelatorio, usuario.Id);


                        MessageBox.Show("Usuário Cadastrado com Sucesso!");
                        dataGridView1.Rows.Clear();
                        AtualizarGrid();
                    }

                    else
                    {
                        lblLogin.Text = "Login Indisponível.";
                        lblLogin.ForeColor = System.Drawing.Color.Red;
                    }

                }

                else
                {
                    lblSenha.Text = "Senhas não correspondem.";
                    lblSenha.ForeColor = System.Drawing.Color.Red;
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // obtém a linha da célula selecionada
            DataGridViewRow linhaAtual = dataGridView1.CurrentRow;

            // Exibe o índice da linha atual
            int indice = linhaAtual.Index;

            if (e.ColumnIndex == (dataGridView1.Columns["Excluir"].Index))
            {
                try
                {
                    UsuarioLogica uLog = new UsuarioLogica(Properties.Settings.Default.StringConexao);
                    Usuario user = new Usuario();
                   

                    //Adere à variável idProp o código do objeto selecionado
                    int idUser = Convert.ToInt16(dataGridView1.Rows[indice].Cells[0].Value);

                    //no fim, chama o método de update para que mude o estado_prop com as novas informações.
                    uLog.ExcluirUsuario(idUser);


                    // Inserir ação na tabela relatório
                    user = uLog.RecuperarUsuario(idUser);
                    string descRelatorio = usuario.Login + " excluiu usuário " + user.Login;
                    rLog.InserirRelatorio(descRelatorio, usuario.Id);

                    //limpa o gridview
                    dataGridView1.Rows.Clear();

                    //chama o método atualizargrid que atualiza as informações do grid
                    AtualizarGrid();

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
                    

                    Usuario usu = new Usuario();
                    UsuarioLogica uLog = new UsuarioLogica(Properties.Settings.Default.StringConexao);

                    usu = uLog.RecuperarUsuario(Convert.ToInt32(dataGridView1.Rows[indice].Cells[0].Value));
                    string login_tmp = usu.Login;
                    usu.Login = dataGridView1.Rows[indice].Cells["Login"].Value.ToString();
                    usu.Nome = dataGridView1.Rows[indice].Cells["Nome"].Value.ToString();
                    usu.DataNasc = DateTime.Parse(dataGridView1.Rows[indice].Cells["DataNasc"].Value.ToString());
                    usu.Email = dataGridView1.Rows[indice].Cells["Email"].Value.ToString();

                    if (uLog.VerificarLogin(usu.Login) == false || login_tmp == usu.Login)
                    {
                        uLog.AlterarUsuario(usu);

                        // Inserir ação na tabela relatório
                        string descRelatorio = usuario.Login + " alterou conta do usuário " + usu.Login;
                        rLog.InserirRelatorio(descRelatorio, usuario.Id);


                        dataGridView1.ReadOnly = true; 
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            dataGridView1.Rows[i].Cells["Editar"].Value = "Editar";
                        }
                        dataGridView1.ForeColor = System.Drawing.Color.Gray;
                    }

                    else
                    {
                        MessageBox.Show("Login Indisponível, digite outro");
                    }


                    
                    
                }

                
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

    }
}
