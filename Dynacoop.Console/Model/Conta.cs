using Microsoft.Rest;
using Microsoft.SqlServer.Server;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace Dynacoop.Model
{
    public class Conta
    {
        public Entity Account = new Entity("account");
        public string LogicalName = "account";


        public CrmServiceClient ServiceClient { get; set; }

        public Conta(CrmServiceClient serviceClient)
        {
            this.ServiceClient = serviceClient;
            this.LogicalName = "account";
        }

        public Guid Create()
        {

            Account["name"] = "Wesley";
            Account["telephone1"] = "11984382372";
            Account["fax"] = "11984382372";

            Account["dcp_tipo_relacao"] = new OptionSetValue(100000001);
            Account["dcp_nmr_total_opp"] = 0;
            Account["dcp_valor_total_opp"] = new Money(0);
            Account["parentaccountid"] = new EntityReference("account", new Guid("11a64150-b9ac-ed11-83fe-000d3a888a06"));

            Guid accountId = ServiceClient.Create(Account);

            Console.WriteLine("Conta Criada com Sucesso");

            return accountId;



        }
        public void Update(string telefone, Guid accountId)
        {
            Account.Id = accountId;
            Account["telephone1"] = telefone;
            ServiceClient.Update(Account);
            Console.WriteLine("Conta atualizada com sucesso");
            Console.ReadKey();
        }
        public void Delete(Guid accountId)
        {
            ServiceClient.Delete(LogicalName, accountId);
            Console.WriteLine("Conta deletada com sucesso");
            Console.ReadKey();
        }

        public Entity GetAccountById(Guid id)
        {
            var context = new OrganizationServiceContext(ServiceClient);

            return (from a in context.CreateQuery("account")
                    join b in context.CreateQuery("contact")
                    on ((EntityReference)a["primarycontactid"]).Id equals b["contactid"]
                    where (Guid)a["accountid"] == id
                    select a).ToList().FirstOrDefault();

        }


        public Entity GetAccountByName(string name)
        {
            QueryExpression queryAccount = new QueryExpression(LogicalName);
            queryAccount.ColumnSet.AddColumns("telephone1", "primarycontactid");
            queryAccount.Criteria.AddCondition("name", ConditionOperator.Equal, name);
            EntityCollection accounts = ServiceClient.RetrieveMultiple(queryAccount);

            if (accounts.Entities.Count() > 0)
                return accounts.Entities.FirstOrDefault();
            else
                Console.WriteLine("Nenhuma conta encontrada com esse nome");
            return null;

        }

        public Entity GetAccountByContactName(string name, string[] columns)
        {
            QueryExpression queryAccount = new QueryExpression(LogicalName);
            queryAccount.ColumnSet.AddColumns(columns);
            queryAccount.AddLink("contact", "primarycontactid", "contactid");
            queryAccount.LinkEntities.FirstOrDefault().LinkCriteria.AddCondition("firstname", ConditionOperator.Equal, name);
            EntityCollection accounsts = ServiceClient.RetrieveMultiple(queryAccount);
            return accounsts.Entities.FirstOrDefault();
        }


        public Entity GetAccounBytelephone(string telephone1)
        {
            string fechtXML = $@"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
<entity name='account'>
<attribute name='name'/>
<attribute name='primarycontactid'/>
<attribute name='telephone1'/>
<attribute name='accountid'/>
<order attribute='name' descending='false'/>
<filter type='and'>
<condition attribute='telephone1' operator='eq' value='{telephone1}'/>
</filter>
</entity>
</fetch>";
            EntityCollection accounts = ServiceClient.RetrieveMultiple(new FetchExpression(fechtXML));

            return accounts.Entities.FirstOrDefault();
            }

        //    public void UpsertMultipleRequest(EntityCollection entityCollection)
        //    {
        //        OrganizationRequestCollection requestCollection = new OrganizationRequestCollection();

        //        foreach (Entity entity in entityCollection.Entities)
        //        {
        //            AddUpsertRequest(requestCollection, entity);
        //            //UpsertRequest upsertRequest = new UpsertRequest();
        //            //{
        //            //    Target = entity
        //            //};
        //            //requestCollection.Add(upsertRequest);

        //        }

        //        ExecuteMultipleRequest executeMultipleRequest = new ExecuteMultipleRequest()
        //        {
        //            Requests = requestCollection,
        //            Settings = new ExecuteMultipleSettings()
        //            {
        //                ContinueOnError = true,
        //                ReturnResponses = true
        //            }
        //        };

        //        ExecuteMultipleResponse executeMultipleResponse = (ExecuteMultipleResponse).thisServiceCliente.Execute(executeMultipleResponse);

        //        foreach (var executeResponse in executeMultipleResponse.Responses)
        //        {
        //            if (executeResponse.Fault != null)
        //                Console.WriteLine(executeResponse.Fault.ToString());
        //        }

    }
}
