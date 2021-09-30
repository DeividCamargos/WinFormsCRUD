using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTeste
{
    public partial class addfuncionario : Form
    {
        public addfuncionario()
        {
            InitializeComponent();
            SetBtnFalse();
            Clean();

        }
        string connectionString = "Server=.\\SQLEXPRESS;Database=Funcionarios;User=sa;Password=abcd.1234;ConnectRetryCount=0;MultipleActiveResultSets=true";

        private void btn_add_Click(object sender, EventArgs e)
        {
            txt_nome.Enabled = true;
            maskedTextBox_tel.Enabled = true;
            maskedTextBox_cel.Enabled = true;
            txt_email.Enabled = true;
            txt_endereco.Enabled = true;
            txt_numero.Enabled = true;
            txt_bairro.Enabled = true;
            txt_rg.Enabled = true;
            txt_cpf.Enabled = true;
            txt_codigo.Enabled = true;

            btn_apagar.Enabled = false; 
            btn_salvar.Enabled = true;
            btn_editar.Enabled = false;

            Clean();

        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "insert into Funcionario (Nome,Telefone,Celular,Email,Endereco,Numero,Bairro,RG,CPF,Codigo)" +
                    " values (@Nome,@Telefone,@Celular,@Email,@Endereco,@Numero,@Bairro,@RG,@CPF,@Codigo)";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txt_nome.Text;
                command.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = maskedTextBox_tel.Text;
                command.Parameters.Add("@Celular", SqlDbType.VarChar).Value = maskedTextBox_cel.Text;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = txt_email.Text;
                command.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = txt_endereco.Text;
                command.Parameters.Add("@Numero", SqlDbType.VarChar).Value = txt_numero.Text;
                command.Parameters.Add("@Bairro", SqlDbType.VarChar).Value = txt_bairro.Text;
                command.Parameters.Add("@RG", SqlDbType.VarChar).Value = txt_rg.Text;
                command.Parameters.Add("@CPF", SqlDbType.VarChar).Value = txt_cpf.Text;
                command.Parameters.AddWithValue("@Codigo", txt_codigo.Text);

                //string queryString = $"insert into Funcionario (Nome,Telefone,Celular,Email,Endereco,Numero,Bairro,RG,CPF,Codigo)" +
                //    $" values ('{txt_nome.Text}','{maskedTextBox_tel.Text}','{maskedTextBox_cel.Text}'," +
                //    $"'{txt_email.Text}','{txt_endereco.Text}','{txt_numero.Text}','{txt_bairro.Text}','{txt_rg.Text}','{txt_cpf.Text}','{txt_codigo.Text}')";

                //SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    command.ExecuteNonQuery();

                    MessageBox.Show("Funcionario cadastrado com sucesso!");

                    Clean();
                    SetBtnFalse();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    
                }                       
            } 
        }
         
        private void btn_buscar_Click(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "select * from Funcionario where Nome = @Nome";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txt_pesquisa.Text;

                try
                {
                    if (txt_pesquisa.Text == string.Empty)
                    {
                        throw new Exception("Voce não digitou um nome!");
                    }

                    connection.Open();
                    
                    var dados = command.ExecuteReader();

                    if (dados.HasRows == false)
                    {
                        throw new Exception("Nome não cadastrado!"); 
                    }

                    dados.Read();

                    txt_nome.Text = dados["Nome"].ToString();
                    maskedTextBox_tel.Text = dados["Telefone"].ToString();
                    maskedTextBox_cel.Text = dados["Celular"].ToString();
                    txt_email.Text = dados["Email"].ToString();
                    txt_endereco.Text = dados["Endereco"].ToString();
                    txt_numero.Text = dados["Numero"].ToString();
                    txt_bairro.Text = dados["Bairro"].ToString();
                    txt_rg.Text = dados["RG"].ToString();
                    txt_cpf.Text = dados["CPF"].ToString();

                    txt_codigo.Text = dados["Codigo"].ToString();

                    txt_codigo.Enabled = false;
                    txt_nome.Enabled = true;
                    maskedTextBox_tel.Enabled = true;
                    maskedTextBox_cel.Enabled = true;
                    txt_email.Enabled = true;
                    txt_endereco.Enabled = true;
                    txt_numero.Enabled = true;
                    txt_bairro.Enabled = true;
                    txt_rg.Enabled = true;
                    txt_cpf.Enabled = true; 

                    btn_salvar.Enabled = false;
                    btn_editar.Enabled = true;
                    btn_apagar.Enabled = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); 
                } 
            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "update Funcionario set Nome = @Nome, Telefone = @Telefone, Celular  = @Celular," +
                    "Email = @Email, Endereco = @Endereco, Numero = @Numero, Bairro = @Bairro, RG = @RG, CPF = @CPF " +
                    $"where Codigo = @Codigo";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txt_nome.Text;
                command.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = maskedTextBox_tel.Text;
                command.Parameters.Add("@Celular", SqlDbType.VarChar).Value = maskedTextBox_cel.Text;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = txt_email.Text;
                command.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = txt_endereco.Text;
                command.Parameters.Add("@Numero", SqlDbType.VarChar).Value = txt_numero.Text;
                command.Parameters.Add("@Bairro", SqlDbType.VarChar).Value = txt_bairro.Text;
                command.Parameters.Add("@RG", SqlDbType.VarChar).Value = txt_rg.Text;
                command.Parameters.Add("@CPF", SqlDbType.VarChar).Value = txt_cpf.Text;
                command.Parameters.AddWithValue("@Codigo", txt_codigo.Text);
                try
                {
                    connection.Open();

                    command.ExecuteNonQuery();

                    MessageBox.Show("Alterações realizadas com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_apagar_Click(object sender, EventArgs e)
        { 
            if(MessageBox.Show("Deseja apagar?", "Alerta", MessageBoxButtons.YesNo) == 
                DialogResult.No)
            {
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "delete from Funcionario where Codigo=@Codigo";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Codigo", txt_codigo.Text);

                try
                {
                    connection.Open();

                    command.ExecuteNonQuery();

                    MessageBox.Show("Usuario excluído com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    SetBtnFalse();
                    Clean();
                }
            }
            
        }

        private void Clean()
        {
            txt_nome.Clear();
            maskedTextBox_tel.Clear();
            maskedTextBox_cel.Clear();
            txt_email.Clear();
            txt_endereco.Clear();
            txt_numero.Clear();
            txt_bairro.Clear();
            txt_rg.Clear();
            txt_cpf.Clear();
            txt_codigo.Clear();
            txt_pesquisa.Clear();
        }
       
        private void SetBtnFalse()
        {
            txt_nome.Enabled = false;
            maskedTextBox_tel.Enabled = false;
            maskedTextBox_cel.Enabled = false;
            txt_email.Enabled = false;
            txt_endereco.Enabled = false;
            txt_numero.Enabled = false;
            txt_bairro.Enabled = false;
            txt_rg.Enabled = false;
            txt_cpf.Enabled = false;
            txt_codigo.Enabled = false; 

            btn_apagar.Enabled = false;
            btn_editar.Enabled = false;
            btn_salvar.Enabled = false;
        }
    }
    
}
 
