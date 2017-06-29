using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCCB.Models
{
    public class Contact
    {
        public string Name { get; set; }

        public string SurName { get; set; }

        public string Company { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string BornDate { get; set; }

        public bool IsParente { get; set; }
    }
}
