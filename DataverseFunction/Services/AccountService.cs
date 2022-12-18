using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;

namespace DataverseFunction
{
    public class AccountService
    {
        public async Task<Guid> CreateAccount(string postBody, ILogger _logger) {

            var SecretValue = Environment.GetEnvironmentVariable("SecretValue");
            var AppID = Environment.GetEnvironmentVariable("AppID");
            var InstanceUri = Environment.GetEnvironmentVariable("InstanceUri");
            var aid = new Guid();

            var _account = JsonConvert.DeserializeObject<Account>(postBody);

            string ConnectionStr = $@"AuthType=ClientSecret;
                        SkipDiscovery=true;url={InstanceUri};
                        Secret={SecretValue};
                        ClientId={AppID};
                        RequireNewInstance=true";
            
                using (ServiceClient svc = new ServiceClient(ConnectionStr))
                {
                    if (svc.IsReady)
                    {
                        var account = new Entity("account");
                        account.Attributes["name"] = _account.Name;
                        account.Attributes["accountnumber"] = _account.Number;
                        aid  = await svc.CreateAsync(account);
                    }
                }            

            return aid;
        
        }


        
               

    }
}
