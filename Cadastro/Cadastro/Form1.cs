using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cadastro.DataAccess;

// Classe da View

namespace Cadastro
{
    public partial class frmCadastroCliente : Form
    {
        List<Cliente> ListaClientes = new List<Cliente>();
        public frmCadastroCliente()
        {
            InitializeComponent();
        }

        private void frmCadastroCliente_Load(object sender, EventArgs e)
        {
            // Inicializa dgv 
            dgvDados.DataSource = ClienteDataAccess.Insere(ListaClientes);
        }
        

        // Habilita ou desabilita os campos dependendo do estado
        private void HabilitaOuDesabilitaCompos(bool pNovoStatus)
        {
            txtBairro.Enabled = pNovoStatus;
            txtCidade.Enabled = pNovoStatus;
            txtCodigo.Enabled = pNovoStatus;
            mktCpf.Enabled = pNovoStatus;
            txtLogradouro.Enabled = pNovoStatus;
            txtNome.Enabled = pNovoStatus;
            chbAtivo.Enabled = pNovoStatus;
            dtpNascimento.Enabled = pNovoStatus;
            txtUf.Enabled = pNovoStatus;

        }


        // Limpar tela
        private void Limpartela()
        {
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtCodigo.Text = "";
            mktCpf.Text = "";
            txtLogradouro.Text = "";
            txtNome.Text = "";
            chbAtivo.Checked = true;
            dtpNascimento.Value = DateTime.Now;
            txtUf.Text = "";
        }


        // Fechar
        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // inserir cliente no Banco
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && mktCpf.Text != "")
            {
                // Cria um novo Objeto
                Cliente novoCliente = new Cliente();

                // Preenche os atributos do objeto com os dados do formulário
                novoCliente.Ativo = chbAtivo.Checked;
                novoCliente.Bairro = txtBairro.Text;
                novoCliente.Cidade = txtCidade.Text;
               // novoCliente.Codigo = Convert.ToInt32(txtCodigo.Text);
                novoCliente.CPF = mktCpf.Text;
                novoCliente.Nascimento = dtpNascimento.Value;
                novoCliente.Logradouro = txtLogradouro.Text;
                novoCliente.Nome = txtNome.Text;
                novoCliente.UF = txtUf.Text;

                // Inserir o objeto no tabela cliente
                if (!ClienteDataAccess.Insere(novoCliente))
                {
                    MessageBox.Show("Falha ao inserir os dados!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Cliente inserido com sucesso!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Atualiza o datagrid em relação a nova lista de clientes
                dgvDados.DataSource = null;
                dgvDados.DataSource = ListaClientes.AsReadOnly();
                dgvDados.Refresh();

                // Volta os campos ára o estado original
                Limpartela();
                HabilitaOuDesabilitaCompos(false);
            } else {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Excluir
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var cod = Convert.ToInt32(txtCodigo.Text);
            dgvDados.DataSource = ClienteDataAccess.Delete(cod);
            HabilitaOuDesabilitaCompos(false);
        }

       
        // Consulta
        private void btnConsultar_Click_1(object sender, EventArgs e)
        {
            //Cliente cli = new Cliente();
            var cod = Convert.ToInt32(txtCodigo.Text);
            var retorno = ClienteDataAccess.Consultar(cod);

            chbAtivo.Checked = retorno.Ativo;
            txtBairro.Text = retorno.Bairro;
            txtCidade.Text = retorno.Cidade;
            txtCodigo.Text = Convert.ToString(retorno.Codigo);
            mktCpf.Text = retorno.CPF;
            dtpNascimento.Value = retorno.Nascimento;
            txtLogradouro.Text = retorno.Logradouro;
            txtNome.Text = retorno.Nome;
            txtUf.Text = retorno.UF;
        }


        // Exibir lista de Clientes
        private void btnExibir_Click(object sender, EventArgs e)
        {
            dgvDados.DataSource = ClienteDataAccess.Exibir();
            HabilitaOuDesabilitaCompos(false);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Cliente cli = new Cliente();
            cli.Ativo = chbAtivo.Checked;
            cli.Bairro = txtBairro.Text;
            cli.Cidade = txtCidade.Text;
            cli.Codigo = Convert.ToInt32(txtCodigo.Text);
            cli.CPF = mktCpf.Text;
            cli.Nascimento = dtpNascimento.Value;
            cli.Logradouro = txtLogradouro.Text;
            cli.Nome = txtNome.Text;
            cli.UF = txtUf.Text;

            ClienteDataAccess.Atualiza(cli);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            HabilitaOuDesabilitaCompos(true);
        }
    }
}
