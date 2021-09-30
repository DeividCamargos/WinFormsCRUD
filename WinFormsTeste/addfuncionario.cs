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
        }
        string connectionString = "Server=.\\SQLEXPRESS;Database=Funcionarios;User=sa;Password=abcd.1234;ConnectRetryCount=0;MultipleActiveResultSets=true";

        private void btn_add_Click(object sender, EventArgs e)
        {

        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string queryString = "insert into Funcionario (Nome,Telefone,Celular,Email,Endereco,Numero,Bairro,RG,CPF)" +
                //    " values (@Nome,@Telefone,@Celular,@Email,@Endereco,@Numero,@Bairro,@RG,@CPF)";

                //SqlCommand command = new SqlCommand(queryString, connection);

                //command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txt_nome.Text;
                //command.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = maskedTextBox_tel.Text;
                //command.Parameters.Add("@Celular", SqlDbType.VarChar).Value = maskedTextBox_cel.Text;
                //command.Parameters.Add("@Email", SqlDbType.VarChar).Value = txt_email.Text;
                //command.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = txt_endereco.Text;
                //command.Parameters.Add("@Numero", SqlDbType.VarChar).Value = txt_numero.Text;
                //command.Parameters.Add("@Bairro", SqlDbType.VarChar).Value = txt_bairro.Text;
                //command.Parameters.Add("@RG", SqlDbType.VarChar).Value = txt_rg.Text;
                //command.Parameters.Add("@CPF", SqlDbType.VarChar).Value = txt_cpf.Text;

                string queryString = $"insert into Funcionario (Nome,Telefone,Celular,Email,Endereco,Numero,Bairro,RG,CPF)" +
                    $" values ('{txt_nome.Text}','{maskedTextBox_tel.Text}','{maskedTextBox_cel.Text}'," +
                    $"'{txt_email.Text}','{txt_endereco.Text}','{txt_numero.Text}','{txt_bairro.Text}','{txt_rg.Text}','{txt_cpf.Text}')";

                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    command.ExecuteNonQuery();

                    MessageBox.Show("Funcionario cadastrado com sucesso!");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
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
                    label_codigo.Text = dados["Id"].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    txt_pesquisa.Clear();
                }
            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "update Funcionario set Nome = @Nome, Telefone = @Telefone, Celular  = @Celular," +
                    "Email = @Email, Endereco = @Endereco, Numero = @Numero, Bairro = @Bairro, RG = @RG, CPF = @CPF " +
                    $"where Id = @Id";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Id", label_codigo.Text);
                command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txt_nome.Text;
                command.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = maskedTextBox_tel.Text;
                command.Parameters.Add("@Celular", SqlDbType.VarChar).Value = maskedTextBox_cel.Text;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = txt_email.Text;
                command.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = txt_endereco.Text;
                command.Parameters.Add("@Numero", SqlDbType.VarChar).Value = txt_numero.Text;
                command.Parameters.Add("@Bairro", SqlDbType.VarChar).Value = txt_bairro.Text;
                command.Parameters.Add("@RG", SqlDbType.VarChar).Value = txt_rg.Text;
                command.Parameters.Add("@CPF", SqlDbType.VarChar).Value = txt_cpf.Text;

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
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "delete from Funcionario where Nome=@Nome";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Nome", txt_nome.Text);

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
            }
        }
    }
}
 
