using System;
using System.Collections.Generic;

namespace AgendaCCB.Data.Models
{
    public partial class PositionMinistryCollaborator
    {
        public int Id { get; set; }
        public int IdCollaborator { get; set; }
        public int IdPositionMinistry { get; set; }

        public Collaborator IdCollaboratorNavigation { get; set; }
        public PositionMinistry IdPositionMinistryNavigation { get; set; }
    }
}
