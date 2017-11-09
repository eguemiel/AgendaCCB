using System;
using System.Collections.Generic;

namespace AgendaCCB.Api.ModelsData
{
    public partial class AppauthorizationToUse
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public DateTime Created { get; set; }
        public string UserCreator { get; set; }
        public bool? Used { get; set; }

        public AspNetUsers UserCreatorNavigation { get; set; }
    }
}
