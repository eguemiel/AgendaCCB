using System;
using System.Collections.Generic;

namespace AgendaCCB.Data.Models
{
    public partial class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdState { get; set; }

        public virtual CommonCongregation CommonCongregation { get; set; }
        public virtual State IdStateNavigation { get; set; }
    }
}
