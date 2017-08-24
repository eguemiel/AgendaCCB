using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCCB.Web.Models
{
    public class PhoneNumberViewModel
    {
        public int Id { get; set; }

        public string Phone { get; set; }

        public TypePhone TypePhone { get; set; }

        public PhoneNumberViewModel()
        {

        }

        public PhoneNumberViewModel(PhoneNumberViewModel phoneNumber)
        {
            Id = phoneNumber.Id;
            Phone = phoneNumber.Phone;
            TypePhone = phoneNumber.TypePhone;
        }
    }

    public enum TypePhone
    {
        PrivateCellNumber,
        CompanyCellNumber,
        PrivateTelephoneNumber
    }
}
