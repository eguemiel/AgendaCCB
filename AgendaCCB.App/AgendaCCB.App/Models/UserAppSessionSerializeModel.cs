using System;

namespace AgendaCCB.App.Models
{
    public class UserAppSessionSerializeModel
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public string Imagem { get; set; }
        public bool TutorialVisualized { get; set; }

        public bool Logged { get; set; }

        public void MapTo(UserAppSession usuarioAppSessao)
        {
            usuarioAppSessao.Id = Id;
            usuarioAppSessao.PhoneNumber = PhoneNumber;
            usuarioAppSessao.Token = Token;
            usuarioAppSessao.TutorialVisualized = TutorialVisualized;
        }

        public UserAppSession ConvertToSession()
        {
            var session = new UserAppSession();
            MapTo(session);
            return session;
        }        
    }
}
