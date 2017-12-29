using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCCB.Api.Models
{
    public class PhoneNumber
    {
        public string Number { get; set; }

        public string PhoneType { get; set; }
    }

    public enum TypePhone
    {
        [DisplayName("Celular Particular")]
        PrivateCellNumber = 1,
        [DisplayName("Celular Empresa")]
        CompanyCellNumber = 2,
        [DisplayName("Fixo Particular")]
        PrivateTelephoneNumber = 3,
        [DisplayName("Fixo Empresa")]
        CompanyTelephoneNumber = 4
    }
}
