using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjetoCadastro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string StringConexao = @"Data Source=localhost;Initial Catalog=Cadastro;Integrated Security=True";

        public void Limpar()
        {
            txtCodigo.Clear();
            txtNome.Clear();
            txtIdade.Clear();
            txtEndereco.Clear();
            txtNumero.Clear();
            txtBairro.Clear();

            txtNome.Focus();
        }

        public void CarregaDados()
        {
            
            try
            {
                //cria um DataTabale
                DataTable dt = new DataTable();
                    
                SqlConnection conn = new SqlConnection(StringConexao);
                    
                SqlDataAdapter da = new SqlDataAdapter("Select * from tb_Cadastro", conn);
                    
                //preenche o DataTable
                da.Fill(dt);
                    
                //exibe os dados no DataGridView
                dgvCadastro.DataSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                    MessageBox.Show("Erro : " + ex.Message);
            }
         }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult msg = MessageBox.Show("Deseja realmente sair ?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(msg == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            gbCadastros.Enabled = true;
            Limpar();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregaDados();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(StringConexao);

            try
            {
                cmd.CommandText = @"insert into tb_Cadastro (nome, idade, endereco, numero, bairro) values (@Nome, @Idade, @Endereco, @Numero, @Bairro)";
                cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = txtNome.Text;
                cmd.Parameters.Add("@Idade", SqlDbType.Int).Value = Convert.ToInt32(txtIdade.Text);
                cmd.Parameters.Add("@Endereco", SqlDbType.VarChar, 60).Value = txtEndereco.Text;
                cmd.Parameters.Add("@Numero", SqlDbType.Int).Value = Convert.ToInt32(txtNumero.Text);
                cmd.Parameters.Add("@Bairro", SqlDbType.VarChar, 30).Value = txtBairro.Text;

                cmd.Connection = con;

                con.Open();

                cmd.ExecuteNonQuery();

                MessageBox.Show("Dados salvos com sucesso!", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
                CarregaDados();
            }
        }
    }
}
 