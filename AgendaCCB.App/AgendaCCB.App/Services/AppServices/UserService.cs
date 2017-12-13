using AgendaCCB.App.Models;
using AgendaCCB.App.Services.Api;
using Newtonsoft.Json;
using Realms;
using System;
using System.Threading.Tasks;

namespace AgendaCCB.App.Services.AppServices
{
    public class UserService : BaseService
    {
        public void SaveSessionUser(UserAppSession UserAppSession)
        {
            using (Realm realm = CreateNewRealmInstance())
            {
                var userSession = GetUser(realm);

                using (var trans = realm.BeginWrite())
                {
                    userSession.Id = Convert.ToInt64(UserAppSession.Id);
                    userSession.PhoneNumber = UserAppSession.PhoneNumber;
                    userSession.Token = UserAppSession.Token;
                    userSession.TutorialVisualized = UserAppSession.TutorialVisualized;
                    userSession.Logged = UserAppSession.Logged;
                  
                    realm.Add(userSession, true);

                    trans.Commit();
                }
            }
        }

        public UserAppSession GetUsuario(bool desconectaObjetoRealm = false)
        {
            return GetUser(CreateNewRealmInstance(), desconectaObjetoRealm);            
        }

        public void UpdateFlagTutorial(bool visualizado = true)
        {
            Realm realm = CreateNewRealmInstance();
            var userSession = GetUser(realm);

            using (var trans = realm.BeginWrite())
            {
                userSession.TutorialVisualized = true;
                realm.Add(userSession, true);

                trans.Commit();
            }
        }

        public void Logout()
        {
            using (Realm realm = CreateNewRealmInstance())
            {
                var userSession = GetUser(realm);

                using (var trans = realm.BeginWrite())
                {
                    userSession.Logged = false;
                    userSession.Token = null;                    
                    userSession.Token = null;

                    realm.Add(userSession, true);
                    trans.Commit();
                }
            }
        }

        private UserAppSession GetUser(Realm realm, bool logoffObjectRealm = false)
        {
            var userSession = realm.Find<UserAppSession>(0);
            
            if (logoffObjectRealm)
                userSession = JsonConvert.DeserializeObject<UserAppSession>(JsonConvert.SerializeObject(userSession));

            if (userSession == null)
                return new UserAppSession();

            return userSession;
        }

        public async Task<bool> TryRefreshToken()
        {
            UserAppSession usuarioSessao = GetUsuario();
            ApiReturn retorno;
            retorno = await Login(usuarioSessao.PhoneNumber, usuarioSessao.Token);
            return retorno.Success;
        }

        public async Task<ApiReturn> Login(string phoneNumber, string token)
        {
            var api = new ApiLoginService();
            api.TryLoginUser = false;

            ApiReturn<UserAppSessionSerializeModel> objectReturned = await api.Login(phoneNumber, token);            

            if (objectReturned.Success)
            {
                UserAppSession sessao = objectReturned.Object.ConvertToSession();
                sessao.Logged = true;
                sessao.PhoneNumber = phoneNumber;
                sessao.Token = token;
                SaveSessionUser(sessao);
            }         

            return objectReturned.NoType();        
        }
    }
}
