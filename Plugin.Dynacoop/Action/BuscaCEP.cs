using Microsoft.Xrm.Sdk.Workflow;
using Newtonsoft.Json;
using Plugin.Dynacoop.Arquitetura;
using Plugin.Dynacoop.VO;
using RestSharp;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Dynacoop.Extensions;

namespace Plugin.Dynacoop.Action
{
    public class BuscaCEP : ActionCore
    {
        [Input("CEP")]
        public InArgument<string> CEP { get; set; }
        [Output("logradouro")]
        public OutArgument<string> Logradouro { get; set; }
        [Output("complemento")]
        public OutArgument<string> Complemento { get; set; }
        [Output("bairro")]
        public OutArgument<string> Bairro { get; set; }
        [Output("localidade")]
        public OutArgument<string> Localidade { get; set; }
        [Output("uf")]
        public OutArgument<string> Uf { get; set; }
        [Output("ibge")]
        public OutArgument<string> Ibge { get; set; }
        [Output("ddd")]
        public OutArgument<string> DDD { get; set; }

        [Output("erro")]
        public OutArgument<bool> Erro { get; set; }
        public string Log { get; set; }
        public override void ExecuteAction(CodeActivityContext context)
        {
            try
            {
                //var options = new RestClientOptions($"https://viacep.com.br/ws/{CEP}")
                //{ 
                //    MaxTimeout = -1,
                //};

                Log += "Entrou no processo ";

                var client = new RestClient($"https://viacep.com.br/ws/{CEP.Get(context)}");


                var request = new RestRequest("/json", Method.Get);
                //// request.Method = Method.Get;
                ////request.AddHeader("Content-Type", "application/json");
                ////request.AddHeader("x-functions-key", "");
                RestResponse response = client.Execute(request);
                Log += " Fez a request";

                ////List<EnderecoVO> enderecoVO = JsonConvert.DeserializeObject<List<EnderecoVO>>(response.Content);

                var endereco = Endereco.FromJson(response.Content);

                //var enderecoFound = from p in enderecoVO
                //                    where p.Cep == CEP.Get(context)
                //                    select p).Tolist().FirstOrDefault();

                //if (enderecofound == null)
                //{
                //    throw new Exception("Endereco não encontrado");
                //}
                if (!endereco.Erro)
                {
                    Logradouro.Set(context, endereco.Logradouro);
                    Complemento.Set(context, endereco.Complemento);
                    Bairro.Set(context, endereco.Bairro);
                    Localidade.Set(context, endereco.Localidade);
                    Uf.Set(context, endereco.Uf);
                    Ibge.Set(context, endereco.Ibge);
                    DDD.Set(context, endereco.Ddd);

                }
                else
                {
                    Erro.Set(context, endereco.Erro);
                    Logradouro.Set(context, string.Empty);
                    Complemento.Set(context, string.Empty);
                    Bairro.Set(context, string.Empty);
                    Localidade.Set(context, string.Empty);
                    Uf.Set(context, string.Empty);
                    Ibge.Set(context, string.Empty);
                    DDD.Set(context, string.Empty);

                }

                
            }
            catch (Exception ex)
            {
                throw new Exception(Log + " - " + ex.ToString());
            }

        }
    }
}
