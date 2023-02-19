using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Dynacoop
{
    public class AccountManager : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            tracingService.Trace("Iniciou processo do Plugin");
            IPluginExecutionContext context = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
            IOrganizationServiceFactory serviceFactory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            

            Entity oppotunity = (Entity)context.InputParameters["Target"];

            EntityReference accountId = oppotunity.Contains("parentaccountid") ? (EntityReference)oppotunity["parentaccountid"] : null;
            tracingService.Trace("Iniciou processo do Plugin2");

            if (accountId != null)
            {
                QueryExpression busca = new QueryExpression("account");
                busca.ColumnSet.AddColumns("dcp_nmr_total_opp");
                busca.Criteria.AddCondition("accountid", ConditionOperator.Equal, oppotunity["parentaccountid"]);
                EntityCollection contas = service.RetrieveMultiple(busca);
                Entity contOpp = contas.Entities.FirstOrDefault();

                Entity conta = new Entity("account");
                conta.Id = ((EntityReference)oppotunity.Attributes["parentaccountid"]).Id;
                int numOpp = contOpp.Contains("dcp_nmr_total_opp") ? (int)conta.Attributes["dcp_nmr_total_opp"] : 0;
                numOpp += 1;
                contOpp.Attributes["dcp_nmr_total_opp"] = numOpp;
                service.Update(contOpp);
                


            }
        }
    }
}
