using System;
using System.Collections.Generic;

namespace AgendaCCB.Data.Models
{
    public partial class PhoneNumber
    {
        public PhoneNumber()
        {
            PhoneNumberCollaborator = new HashSet<PhoneNumberCollaborator>();
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }

        public virtual ICollection<PhoneNumberCollaborator> PhoneNumberCollaborator { get; set; }
    }
}
