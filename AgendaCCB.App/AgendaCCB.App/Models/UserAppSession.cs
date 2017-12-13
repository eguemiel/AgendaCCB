using Realms;

namespace AgendaCCB.App.Models
{
    public class UserAppSession : RealmObject
    {
        [PrimaryKey]
        public long RealmId { get; set; }
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
        public bool TutorialVisualized { get; set; }
        public bool Logged { get; set; }
    }
}
