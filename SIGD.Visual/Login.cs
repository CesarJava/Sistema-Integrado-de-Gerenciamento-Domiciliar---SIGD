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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UsuarioLogica user = new UsuarioLogica(Properties.Settings.Default.StringConexao);

            try
            {

                if (user.Login(txtLogin.Text, txtSenha.Text))
                {
                    MenuPrincipal menu = new MenuPrincipal(user.RecuperarUsuario(txtLogin.Text));
                    this.Hide();
                    menu.ShowDialog();
                    this.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
            }
        }
    }
}
