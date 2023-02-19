using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynacoop.Controller
{
    public class Controller
    {
        public static CrmServiceClient GetService()
        {
            string url = "wesleybanco";
            string clientId = "bbec4d60-4b24-46a0-94b5-fec525ff02cf";
            string clientSecret = "FUF8Q~i4c4mCZoAapodkSktugrbX_FOD2bJg7dgu";
            string stringConnection = $"AuthType=ClientSecret;url=https://{url}.crm2.dynamics.com;ClientId={clientId};ClientSecret={clientSecret};";
            CrmServiceClient serviceClient = new CrmServiceClient(stringConnection);

            if (!serviceClient.CurrentAccessToken.Equals(null))
                Console.WriteLine("Conexão Realizado com sucesso");
            else
                Console.WriteLine("Erro na conexão");

            return serviceClient;

        }
    }
}
