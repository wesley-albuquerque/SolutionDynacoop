using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTeste
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite o CEP");
            var cep = Console.ReadLine();
            var client = new RestClient($"https://viacep.com.br/ws/{cep}");
            var request = new RestRequest("/json", Method.Get);
            //var request = new RestRequest("/json", Method.Get);
            // request.Method = Method.Get;
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("x-functions-key", "");
            RestResponse response = client.Execute(request);

            var endereco = Endereco.FromJson(response.Content);
            //List<Endereco> enderecoVO = JsonConvert.DeserializeObject<List<Endereco>>(response.Content);


            // var options2 = new RestClientOptions("https://dynaccop2023-productapi.azurewebsites.net")
            // {
            //     MaxTimeout = -1,
            // };

            // var client2 = new RestClient(options2);


            // var request2 = new RestRequest("/product", Method.Post);


            //var response2 = client2.Execute(request2);


            Console.WriteLine(endereco.Uf);

            Console.WriteLine(response.Content.ToString());
            Console.ReadLine();
        }
    }
}
