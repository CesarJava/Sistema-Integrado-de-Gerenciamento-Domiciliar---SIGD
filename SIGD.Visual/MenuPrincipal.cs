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
using PortaParalelaControl;


namespace SIGD.Visual
{
    public partial class MenuPrincipal : Form
    {

        Usuario usuario = null;
        RelatorioLogica rLog = new RelatorioLogica(Properties.Settings.Default.StringConexao);
        bool relogar = false;
        //método para carregar informações no gridview
        public void AtualizarGrid()
        {
            ComodoLogica comodo = new ComodoLogica(Properties.Settings.Default.StringConexao);
            PropriedadeLogica prop = new PropriedadeLogica(Properties.Settings.Default.StringConexao);
              try 
            {
                foreach (Propriedade p in prop.RecuperarTodos().AsEnumerable())
                {
                    dataGridView1.Rows.Add(p.Id, p.Nome, p.DescComodo, p.DescStatus, "Alterar");
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex);
            }
        

           }
        public MenuPrincipal(Usuario user)
        {
            InitializeComponent();
            this.usuario = user;
            lblLogin.Text = "Seja Bem-Vindo, " + user.Nome + " !";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            relogar = true;
            this.Close();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            AtualizarGrid();
            txtSenha1.Enabled = false;
            txtSenha2.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            NovaTarefa nt = new NovaTarefa(this.usuario);
            
            nt.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (dataGridView1.Columns["Alterar"].Index))
            {
                
                PortasData Leds = new PortasData();
                PropriedadeLogica pLog = new PropriedadeLogica(Properties.Settings.Default.StringConexao);

                // obtém a linha da célula selecionada
                DataGridViewRow linhaAtual = dataGridView1.CurrentRow;
                

                // Exibe o índice da linha atual
                int indice = linhaAtual.Index;

                //Adere à variável idProp o código do objeto selecionado
                string idProp = dataGridView1.Rows[indice].Cells[0].Value.ToString();

                

                //Adere à variável descEstado o estado do objeto selecionado
                string descEstado = dataGridView1.Rows[indice].Cells[3].Value.ToString();
                int status;

                if (descEstado == "Ligado")
                {
                    status = 0;
                }
                else
                {
                    status = 1;
                }

                

                //no fim, chama o método de update para que mude o estado_prop com as novas informações.
                pLog.UpdateEstado(Convert.ToInt16(idProp), status);

                //inserir na tabela Relatório que o estado da propriedade foi alterado
                string descRelatorio = descEstado + " " + dataGridView1.Rows[indice].Cells["Descricao"].Value.ToString();
                rLog.InserirRelatorio(descRelatorio, usuario.Id);

                //limpa o gridview
                dataGridView1.Rows.Clear();

                //chama o método atualizargrid que atualiza as informações do grid
                AtualizarGrid();


            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            CadastrarObjetos co = new CadastrarObjetos(this.usuario);
            co.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            CadastrarUsuarios us = new CadastrarUsuarios(this.usuario);
            us.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ContatosTela con = new ContatosTela(this.usuario);
            con.Show();
        }

       

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Relatorio r = new Relatorio();
            r.Show();
        }

        private void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!relogar)
                Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtEmail.Text = usuario.Email;
            txtLogin.Text = usuario.Login;
            txtNome.Text = usuario.Nome;

            if (usuario.Sexo == "F")
            {
                cbSexo.Text = "Feminino";
            }

            else
            {
                cbSexo.Text = "Masculino";
            }
            dtpDataNasc.Value = usuario.DataNasc;
            panel2.Visible = true;

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                UsuarioLogica uLog = new UsuarioLogica(Properties.Settings.Default.StringConexao);
                Usuario user = new Usuario();
                user.Id = usuario.Id;
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

                if (cboxSenha.Checked == true && txtSenha2.Text != txtSenha1.Text)
                {
                    lblSenha.Text = "Novas senhas não correspondem.";
                    lblSenha.ForeColor = System.Drawing.Color.Red;
                }


                else
                {
                    
                    if (txtSenhaAntiga.Text == usuario.Senha)
                    {
                        if (uLog.VerificarLogin(txtLogin.Text) == false || txtLogin.Text == usuario.Login)
                        {
                            if (cboxSenha.Checked == true)
                            {
                                user.Senha = txtSenha1.Text;
                            }
                            else
                            {
                                user.Senha = txtSenhaAntiga.Text;
                            }
                            user.Login = txtLogin.Text;
                            uLog.AlterarUsuario(user);
                            MessageBox.Show("Sua conta foi alterada com sucesso!");
                            dataGridView1.Rows.Clear();
                            AtualizarGrid();


                            // Inserir ação na tabela relatório
                            string descRelatorio = usuario.Login + " alterou conta de " + usuario.Login;
                            rLog.InserirRelatorio(descRelatorio, usuario.Id);
                        }

                        else
                        {
                            lblLogin2.Text = "Login Indisponível.";
                            lblLogin2.ForeColor = System.Drawing.Color.Red;
                        }

                    }


                    else
                    {
                        MessageBox.Show("Senha antiga incorreta!");
                    }

                   

                }

            


                



            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex);
            }

        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {
            UsuarioLogica uLog = new UsuarioLogica(Properties.Settings.Default.StringConexao);

            if (uLog.VerificarLogin(txtLogin.Text) == false || txtLogin.Text == usuario.Login)
            {
                lblLogin2.Text = "Login Disponível.";
                lblLogin2.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblLogin2.Text = "Login Indisponível";
                lblLogin2.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void cboxSenha_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxSenha.Checked == false)
            {
                txtSenha1.Enabled = false;
                txtSenha2.Enabled = false;
            }

            else
            {
                txtSenha1.Enabled = true;
                txtSenha2.Enabled = true;
            }


        }

        
    }
}
