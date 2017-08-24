using System;
using System.Collections.Generic;

namespace AgendaCCB.Data.Models
{
    public partial class PositionMinistry
    {
        public PositionMinistry()
        {
            Collaborator = new HashSet<Collaborator>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsMinistry { get; set; }

        public virtual ICollection<Collaborator> Collaborator { get; set; }
    }
}
