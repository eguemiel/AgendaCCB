using System;
using System.Collections.Generic;

namespace AgendaCCB.Api.ModelsData
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

        public ICollection<Collaborator> Collaborator { get; set; }
    }
}
