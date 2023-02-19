using Dynacoop.Model;
using Microsoft.Rest;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;



namespace Dynacoop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient serviceClient = Controller.Controller.GetService();

            Console.WriteLine("O que você deseja fazer?");
            Console.WriteLine("1 - Atualizar/Criar Conta");
            Console.WriteLine("2 - Deletar conta pelo ID");
            Console.WriteLine("3 - Procurar por uma conta");
            var input = Console.ReadLine();

            Conta conta = new Conta(serviceClient);
            if (input.ToString() == "1")
            {

                Guid accountId = conta.Create();
                Console.WriteLine("Digite o novo telefone");
                var resposta3 = Console.ReadLine();

                conta.Update(resposta3, accountId);

            }
            else if (input.ToString() == "2")
            {
                Console.WriteLine("Digite o id da conta a ser excluída");
                Guid accountId = new Guid(Console.ReadLine());
                conta.Delete(accountId);

            }
            else if (input.ToString() == "3")
            {
                Console.WriteLine("1 pesquisar uma conta por id");
                Console.WriteLine("2 pesquisar um telefone pelo nome da conta");
                Console.WriteLine("3 pesquisar uma conta por nome do contato");
                Console.WriteLine("4 pesquisar uma conta por telefone");
                Console.WriteLine("5 Contas que começam com?");
                var input2 = Console.ReadLine();


                if (input2.ToString() == "1")
                {
                    Console.WriteLine("Qual o id da conta?");
                    Guid accountId = new Guid(Console.ReadLine());
                    Entity account = conta.GetAccountById(accountId);
                    Console.WriteLine($"A conta encontrada é {account["name"].ToString()}");

                }
                else if (input2.ToString() == "2")
                {
                    Console.WriteLine("Qual o nome da conta?");
                    string accountName = Console.ReadLine();
                    Entity account = conta.GetAccountByName(accountName);
                    Console.WriteLine($"O telefone da conta encontrada é {account["telephone1"].ToString()}");

                }
                else if (input2.ToString() == "3")
                {
                    Console.WriteLine("Qual o nome do contato?");
                    string accountName = Console.ReadLine();
                    Entity account = conta.GetAccountByContactName(accountName, new string[] { "name" });
                    Console.WriteLine($"A conta encontrada é {account["name"].ToString()}");

                }else if (input2.ToString() == "4")
                {
                    Console.WriteLine("Qual o telefone da conta?");
                    string accountName = Console.ReadLine();
                    Entity account = conta.GetAccounBytelephone(accountName);
                    Console.WriteLine($"A conta encontrada é {account["name"].ToString()}");
                }
                

                else
                    Console.WriteLine("Opção inválida");
            }
            Console.ReadKey();

        }


    }
}
