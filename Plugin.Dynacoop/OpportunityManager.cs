using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Dynacoop
{
    public class OpportunityManager : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
            IOrganizationServiceFactory serviceFactory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            Entity OppDelete = (Entity)context.PreEntityImages["PreImage"];


            if (context.MessageName.ToLower() == "create")
            {
                Entity oppotunity = (Entity)context.InputParameters["Target"];
                EntityReference accountId = oppotunity.Contains("parentaccountid") ? (EntityReference)oppotunity["parentaccountid"] : null;



                QueryExpression busca = new QueryExpression("account");
                busca.ColumnSet.AddColumns("dcp_nmr_total_opp");
                busca.Criteria.AddCondition("accountid", ConditionOperator.Equal, ((EntityReference)oppotunity["parentaccountid"]).Id);
                EntityCollection contas = service.RetrieveMultiple(busca);
                Entity contOpp = contas.Entities.FirstOrDefault();


                Entity conta = new Entity("account");
                conta.Id = ((EntityReference)oppotunity.Attributes["parentaccountid"]).Id;
                int numOpp = contOpp.Contains("dcp_nmr_total_opp") ? (int)contOpp.Attributes["dcp_nmr_total_opp"] : 0;
                numOpp += 1;
                contOpp.Attributes["dcp_nmr_total_opp"] = numOpp;
                service.Update(contOpp);
            }
            if (context.MessageName.ToLower() == "delete")
            {
                //var OppDelete = context.PreEntityImages["PreImage"];
                QueryExpression busca = new QueryExpression("account");
                busca.ColumnSet.AddColumns("dcp_nmr_total_opp");
                busca.Criteria.AddCondition("accountid", ConditionOperator.Equal, ((EntityReference)OppDelete["parentaccountid"]).Id);
                EntityCollection contas = service.RetrieveMultiple(busca);
                Entity contOpp = contas.Entities.FirstOrDefault();


                Entity conta = new Entity("account");
                conta.Id = ((EntityReference)OppDelete.Attributes["parentaccountid"]).Id;
                int numOpp = contOpp.Contains("dcp_nmr_total_opp") ? (int)contOpp.Attributes["dcp_nmr_total_opp"] : 0;
                numOpp -= 1;
                contOpp.Attributes["dcp_nmr_total_opp"] = numOpp;
                service.Update(contOpp);

            }
        }
    }
}

