using System.Collections.Generic;
using System.Linq;
using Xamarin.Auth;

namespace AgendaCCB.App.Services.AppServices
{
    public class SecureStorageService
    {
        public string GetRealmKey()
        {   
            return "YWdlbmRhY2NiMjAxN2FnZW5kYWNjYjIwMTdhZ2VuZGFjY2IyMDE3";
        }

        public void SaveRealmKey(string key)
        {
            return;
        }

    }
}
