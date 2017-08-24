using System;
using System.Collections.Generic;

namespace AgendaCCB.Data.Models
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

        public virtual ICollection<Collaborator> Collaborator { get; set; }
        public virtual City IdCityNavigation { get; set; }
    }
}
