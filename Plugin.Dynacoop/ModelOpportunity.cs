using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

//namespace Plugin.Dynacoop
//{
//    public class ModelOpportunity
//    {
//        public Entity Account = new Entity("account");
//        public string LogicalName = "account";
//        crmSer

//        public void BuscaDaConta() 
//        {
//            QueryExpression busca = new QueryExpression(LogicalName);
//            busca.ColumnSet.AddColumns("dcp_nmr_total_opp");
//            busca.Criteria.AddCondition("accountid", ConditionOperator.Equal, ((EntityReference)oppotunity["parentaccountid"]).Id);
//            EntityCollection contas = service.RetrieveMultiple(busca);
//            Entity contOpp = contas.Entities.FirstOrDefault();
//        }

//    }
//}
