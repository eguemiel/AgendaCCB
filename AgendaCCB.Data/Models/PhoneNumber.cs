using System;
using System.Collections.Generic;

namespace AgendaCCB.Data.Models
{
    public partial class PhoneNumber
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public int IdCollaborador { get; set; }

        public virtual Collaborator IdCollaboradorNavigation { get; set; }
    }
}
