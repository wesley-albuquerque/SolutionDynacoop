using Dynacoop.Console.Arquitetura;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Dynacoop
{
    public class ContactManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity contact = (Entity)Context.InputParameters["Target"];
        }
    }
}
