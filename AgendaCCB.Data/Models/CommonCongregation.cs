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
        public string Address { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxPhoneNumber { get; set; }

        public City IdCityNavigation { get; set; }
        public ICollection<Collaborator> Collaborator { get; set; }
    }
}
