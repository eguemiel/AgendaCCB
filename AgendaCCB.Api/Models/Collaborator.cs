﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCCB.Api.Models
{
    public class Collaborator
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CommumCongregation { get; set; }

        public string PositionMinistry { get; set; }

        public List<PhoneNumber> PhoneNumber { get; set; }
    }

    public class PhoneNumber
    {
        public string Number { get; set; }

        public string PhoneType { get; set; }
    }
}
