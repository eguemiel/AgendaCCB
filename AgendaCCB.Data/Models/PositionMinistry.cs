using System;
using System.Collections.Generic;

namespace AgendaCCB.Data.Models
{
    public partial class PositionMinistry
    {
        public PositionMinistry()
        {
            PositionMinistryCollaborator = new HashSet<PositionMinistryCollaborator>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsMinistry { get; set; }

        public ICollection<PositionMinistryCollaborator> PositionMinistryCollaborator { get; set; }
    }
}
