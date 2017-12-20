using System;
using System.Collections.Generic;

namespace AgendaCCB.Data.Models
{
    public partial class Collaborator
    {
        public Collaborator()
        {
            PhoneNumber = new HashSet<PhoneNumber>();
            PositionMinistryCollaborator = new HashSet<PositionMinistryCollaborator>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCommonCongregation { get; set; }

        public CommonCongregation IdCommonCongregationNavigation { get; set; }
        public ICollection<PhoneNumber> PhoneNumber { get; set; }
        public ICollection<PositionMinistryCollaborator> PositionMinistryCollaborator { get; set; }
    }
}
