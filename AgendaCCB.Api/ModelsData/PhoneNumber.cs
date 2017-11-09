using System;
using System.Collections.Generic;

namespace AgendaCCB.Api.ModelsData
{
    public partial class PhoneNumber
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public int IdCollaborador { get; set; }

        public Collaborator IdCollaboradorNavigation { get; set; }
    }
}
