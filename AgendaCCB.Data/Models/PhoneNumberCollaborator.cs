using System;
using System.Collections.Generic;

namespace AgendaCCB.Data.Models
{
    public partial class PhoneNumberCollaborator
    {
        public int Id { get; set; }
        public int IdPhoneNumber { get; set; }
        public int IdCollaborator { get; set; }

        public virtual Collaborator IdCollaboratorNavigation { get; set; }
        public virtual PhoneNumber IdPhoneNumberNavigation { get; set; }
    }
}
