using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgendaCCB.Data.Models
{
    public class UsefulPhoneMetadata
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Nome do Local")]
        public string LocalName { get; set; }

        [DisplayName("Telefone")]
        public string PhoneNumber { get; set; }
    }

    [ModelMetadataType(typeof(UsefulPhoneMetadata))]
    public partial class UsefulPhone { }
}
