using System;
using System.Collections.Generic;

namespace AgendaCCB.Data.Models
{
    public partial class Collaborator
    {
        public Collaborator()
        {
            PhoneNumber = new HashSet<PhoneNumber>();
            PhoneNumberCollaborator = new HashSet<PhoneNumberCollaborator>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdPositionMinisty { get; set; }
        public int IdCommonCongregation { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumber { get; set; }
        public virtual ICollection<PhoneNumberCollaborator> PhoneNumberCollaborator { get; set; }
        public virtual CommonCongregation IdCommonCongregationNavigation { get; set; }
        public virtual PositionMinistry IdPositionMinistyNavigation { get; set; }
    }
}
