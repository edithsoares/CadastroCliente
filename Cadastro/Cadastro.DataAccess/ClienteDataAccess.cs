using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.DataAccess
{
    // Classe dos Métodos do crud (Model)
    public class ClienteDataAccess
    {
        // Insere um Cliente
        public static bool Insere(Cliente pCliente)
        {

            try
            {
                // Instancia do Banco
                CadastroDataClassesDataContext oDB = new CadastroDataClassesDataContext();

                // Inserir no banco
                oDB.Cliente.InsertOnSubmit(pCliente);
                oDB.SubmitChanges();
                oDB.Dispose();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        // Insere uma lista de clientes
        public static bool Insere(List<Cliente> pClientes)
        {
            try
            {
                CadastroDataClassesDataContext oDB = new CadastroDataClassesDataContext();
                oDB.Cliente.DeleteAllOnSubmit(pClientes);
                oDB.SubmitChanges();
                oDB.Dispose();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        // Deleta um Cliente
        public static bool Delete(Cliente pCliente)
        {
            try
            {
                CadastroDataClassesDataContext oDB = new CadastroDataClassesDataContext();
                oDB.Cliente.DeleteOnSubmit(pCliente);
                oDB.SubmitChanges();
                oDB.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Deleta uma lista de cliente
        //public static bool Delete(List<Cliente> pClientes)
        //{
        //    try
        //    {
        //        CadastroDataClassesDataContext oDB = new CadastroDataClassesDataContext();
        //        oDB.Cliente.DeleteAllOnSubmit(pClientes);
        //        oDB.SubmitChanges();
        //        oDB.Dispose();
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }
        //}

        // Deleta apenas um cliente
        public static bool Delete(int pCodigoCliente)
        {
            try
            {
                CadastroDataClassesDataContext oDB = new CadastroDataClassesDataContext();
                Cliente oCliente = (from Selecao in oDB.Cliente where Selecao.Codigo == pCodigoCliente select Selecao).SingleOrDefault();

                oDB.Cliente.DeleteOnSubmit(oCliente);
                oDB.SubmitChanges();
                oDB.Dispose();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        // Atualiza os dados
        public static bool Atualiza(Cliente pCliente)
        {
            try
            {
                CadastroDataClassesDataContext oDB = new CadastroDataClassesDataContext();
                Cliente oCliente = (from Selecao in oDB.Cliente where Selecao.Codigo == pCliente.Codigo select Selecao).SingleOrDefault();

                // Parametros
                oCliente.Ativo = pCliente.Ativo;
                oCliente.Bairro = pCliente.Bairro;
                oCliente.Cidade = pCliente.Cidade;
                oCliente.CPF = pCliente.CPF;
                oCliente.Logradouro = pCliente.Logradouro;
                oCliente.Nascimento = pCliente.Nascimento;
                oCliente.Nome = pCliente.Nome;
                oCliente.UF = pCliente.UF;

                oDB.SubmitChanges();
                oDB.Dispose();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        // Consultar Cliente
        public static Cliente Consultar(int pCodigoCliente)
        {
            CadastroDataClassesDataContext oDB = new CadastroDataClassesDataContext();
            Cliente oCliente = (from Selecao in oDB.Cliente where Selecao.Codigo == pCodigoCliente select Selecao).SingleOrDefault();
            return oCliente;
        }

        // Consultar uma lista de Clientes
        public static List<Cliente> Exibir()
        {
            CadastroDataClassesDataContext oDB = new CadastroDataClassesDataContext();
            // Em ordem alfabetica
            List<Cliente> osClientes = (from Selecao in oDB.Cliente orderby Selecao.Nome descending select Selecao).ToList<Cliente>();
            return osClientes;
        }
    }
}
