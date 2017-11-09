using System.Collections.Generic;
using System.Linq;
using Xamarin.Auth;

namespace AgendaCCB.App.Services.AppServices
{
    public class SecureStorageService
    {
        public string ObterChaveRealm()
        {
            Account account = AccountStore.Create().FindAccountsForService("Realm").FirstOrDefault();

            if (account == null)
                return null;

            return account.Properties["Chave"];
        }

        public void SalvarChaveRealm(string chave)
        {
            AccountStore store = AccountStore.Create();
            IEnumerable<Account> accounts = store.FindAccountsForService("Realm");
            Account account;

            if (accounts.Any())
            {
                account = accounts.FirstOrDefault();
                account.Properties["Chave"] = chave;
            }
            else
            {
                account = new Account();
                account.Properties.Add("Chave", chave);
            }
            
            store.Save(account, "Realm");
        }

    }
}
