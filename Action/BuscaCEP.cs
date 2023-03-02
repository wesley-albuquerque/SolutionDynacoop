using Microsoft.Xrm.Sdk.Workflow;
using RestSharp;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action
{
    public class BuscaCEP : ActionCore2
    {
        [Input("CEP")]
        public InArgument<string> CEP { get; set; }

        [Output("logradouro")]
        public OutArgument<string> Logradouro { get; set; }
        //[Output("complemento")]
        //public OutArgument<string> Complemento { get; set; }
        //[Output("bairro")]
        //public OutArgument<string> Bairro { get; set; }
        //[Output("uf")]
        //public OutArgument<string> Uf { get; set; }
        //[Output("ibge")]
        //public OutArgument<string> Ibge { get; set; }
        //[Output("ddd")]
        //public OutArgument<string> DDD { get; set; }

        public override void ExecuteAction(CodeActivityContext context)
        {
            //var options = new RestClientOptions($"https://viacep.com.br/ws/{CEP}")
            //{ 
            //    MaxTimeout = -1,
            //};

            var client = new RestClient($"https://viacep.com.br/ws/{CEP}");
            var request = new RestRequest("/json", Method.Get);
            //var request = new RestRequest("/json", Method.Get);
            // request.Method = Method.Get;
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("x-functions-key", "");
            RestResponse response = client.Execute(request);

            //List<EnderecoVO> enderecoVO = JsonConvert.DeserializeObject<List<EnderecoVO>>(response.Content);

            //var enderecoFound = from p in enderecoVO
            //                    where p.Cep == CEP.Get(context)
            //                    select p).Tolist().FirstOrDefault();

            //if (enderecofound == null)
            //{
            //    throw new Exception("Endereco não encontrado");
            //}


            Logradouro.Set(context, response.Content.ToString());

        }
    }
}
