using System;
using System.Collections.Generic;

namespace AgendaCCB.Api.ModelsData
{
    public partial class Country
    {
        public Country()
        {
            State = new HashSet<State>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<State> State { get; set; }
    }
}
