using System;
using System.Collections.Generic;

namespace AgendaCCB.Api.ModelsData
{
    public partial class CommonCongregation
    {
        public CommonCongregation()
        {
            Collaborator = new HashSet<Collaborator>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCity { get; set; }

        public City IdNavigation { get; set; }
        public ICollection<Collaborator> Collaborator { get; set; }
    }
}
