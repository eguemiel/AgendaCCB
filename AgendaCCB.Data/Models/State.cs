using System;
using System.Collections.Generic;

namespace AgendaCCB.Data.Models
{
    public partial class State
    {
        public State()
        {
            City = new HashSet<City>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCountry { get; set; }
        public string Code { get; set; }

        public virtual ICollection<City> City { get; set; }
        public virtual Country IdCountryNavigation { get; set; }
    }
}
