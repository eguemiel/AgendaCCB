using System;
using System.Collections.Generic;

namespace AgendaCCB.Api.ModelsData
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

        public Country IdCountryNavigation { get; set; }
        public ICollection<City> City { get; set; }
    }
}
