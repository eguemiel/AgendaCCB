using Realms;
using System;
using Realms.Exceptions;

namespace AgendaCCB.App.Services.AppServices
{
    public class BaseService
    {
        private readonly RealmConfiguration realmConfig;

        public BaseService()
        {       
            //Configura versionamento do realm para que seja possivel realizar o migration, caso alguma estrutura tenha sido alterada.            
            realmConfig = new RealmConfiguration() { SchemaVersion = 1 };
        }

        public Realm CreateNewRealmInstance()
        {
            var service = new SecureStorageService();
            string chave = service.GetRealmKey();

            if (String.IsNullOrEmpty(chave))
            {               
                GerarAndSalvarNovaChaveRealm(out chave);
            }

            //realmConfig.EncryptionKey = Convert.FromBase64String(chave);
            
            try
            {
                return Realm.GetInstance(realmConfig);
            }
            catch (RealmFileAccessErrorException e)        
            {
                // Recria o realm para caso de encryption key ter sido removida ou esteja inválida.
                Realm.DeleteRealm(realmConfig);
                return Realm.GetInstance(realmConfig);
            }
        }

        private void GerarAndSalvarNovaChaveRealm(out string chaveGerada)
        {
            byte[] bytesChave = new Byte[64];
            new Random().NextBytes(bytesChave);
            chaveGerada = Convert.ToBase64String(bytesChave);
            new SecureStorageService().SaveRealmKey(chaveGerada);
        }
    }
}
